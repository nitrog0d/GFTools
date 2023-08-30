using GFGMTool.Commands;
using GFTools.Common;
using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.Json;

namespace GFGMTool;

public static class Program {
    public const string ZoneServerIP = "172.18.216.109";
    public const int GMToolPort = 10321;

    public static Logger Logger { get; private set; }
    public static TcpClient ServerSocket { get; set; }
    private static bool FirstServerPacket = true;
    private static RSA ServerRSA { get; set; }
    private static RC4Engine ClientRC4 { get; set; }
    private static RC4Engine ServerRC4 { get; set; }

    public static void Main(string[] args) {
        Logger = new("GMTool");
        Console.Title = "GFGMTool";

        Console.WriteLine("Connecting to ZoneServer GMTool...");

        ServerSocket = new TcpClient(ZoneServerIP, GMToolPort);

        var serverThread = new Thread(HandleServerSocket);
        serverThread.Name = $"Server Socket Thread";
        serverThread.Start();

        Console.WriteLine("Connected!");

        ReceiveConsoleInput();
    }

    public static void ReceiveConsoleInput() {
        while (true) {
            var input = Console.ReadLine();

            var serverTextCommand = new CG_ServerTextCommand() {
                Command = input
            };

            ServerSocket.GetStream().Write(CreatePacket(CreateMessage(serverTextCommand)));
        }
    }

    private static void HandleServerRawPacket(byte[] packet) {
        // Logger.InfoLine($"Server: {Convert.ToBase64String(packet)}");

        // Get command id
        var commandId = (ZoneServerCommands.CG_Commands)BitConverter.ToUInt16(packet, 0);
        // Create a byte array of the size of the actual packet
        var command = new byte[packet.Length - 2];
        // Copy the actual command to the command variable
        Buffer.BlockCopy(packet, sizeof(ushort), command, 0, command.Length);

        switch (commandId) {
            case ZoneServerCommands.CG_Commands.NC_CG_ClientTextOutput:
                var textOutput = new CG_ClientTextOutput();
                textOutput.Deserialize(command);
                Logger.InfoLine($"CG_ClientTextOutput - {JsonSerializer.Serialize(textOutput)}");
                break;
            case ZoneServerCommands.CG_Commands.NC_CG_ClientCharacterList:
                var characterList = new CG_ClientCharacterList();
                characterList.Deserialize(command);
                Logger.InfoLine($"CG_ClientCharacterList - {JsonSerializer.Serialize(characterList)}");

                /*var clientCharacter = new CG_ClientCharacter() {
                    ID = characterList.Characters[0].ID
                };

                ServerSocket.GetStream().Write(CreatePacket(CreateMessage(clientCharacter)));*/
                break;
            case ZoneServerCommands.CG_Commands.NC_CG_ClientCharacter:
                var clientCharacter = new CG_ClientCharacter();
                clientCharacter.Deserialize(command);
                Logger.InfoLine($"CG_ClientCharacter - {JsonSerializer.Serialize(clientCharacter)}");
                break;
            case ZoneServerCommands.CG_Commands.NC_CG_ServerTextCommand:
                var textOutput2 = new CG_ServerTextCommand();
                textOutput2.Deserialize(command);
                Logger.InfoLine($"CG_ServerTextCommand - {JsonSerializer.Serialize(textOutput2)}");
                break;
            default:
                Logger.InfoLine($"Server: {Enum.GetName(commandId)}");
                break;
        }
    }

    private static void HandleServerSocket() {
        var stream = ServerSocket.GetStream();

        var buffer = new byte[65535];

        var readingPacket = false;
        var packetRead = 0;
        var packetSize = 0;
        byte[] packetBuffer = null;

        while (true) {
            int bytesRead;

            try {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
            } catch (Exception) {
                break;
            }

            if (bytesRead > 0) {
                if (FirstServerPacket) {
                    FirstServerPacket = false;
                    var data = new byte[bytesRead];
                    Buffer.BlockCopy(buffer, 0, data, 0, bytesRead);
                    HandleServerRSA(data);
                } else {
                    var memoryStream = new MemoryStream(buffer, 0, bytesRead);
                    var binaryReader = new BinaryReader(memoryStream);

                    while (memoryStream.Position != memoryStream.Length) {
                        if (!readingPacket) {
                            readingPacket = true;
                            packetRead = 0;
                            packetSize = binaryReader.ReadUInt16();
                            packetBuffer = new byte[packetSize];
                        }

                        packetBuffer[packetRead++] = binaryReader.ReadByte();

                        if (packetRead == packetSize) {
                            readingPacket = false;

                            // Decrypt
                            ServerRC4.ProcessBytes(packetBuffer, packetBuffer);

                            HandleServerRawPacket(packetBuffer);
                        }
                    }
                }

            } else break;
        }

        Logger.VerboseLine("Server disconnected.");
        ServerSocket.Dispose();
    }

    private static void HandleServerRSA(byte[] data) {
        // Copy the RSA public key modulus to buffers
        var rsaPublicKeyModulusSize = BitConverter.ToUInt32(data, 0);
        var rsaPublicKeyModulus = new byte[rsaPublicKeyModulusSize];
        Buffer.BlockCopy(data, sizeof(uint) * 2, rsaPublicKeyModulus, 0, rsaPublicKeyModulus.Length);

        // Copy the RSA public exponent to buffers
        var exponentSize = BitConverter.ToUInt32(data, sizeof(uint));
        var exponent = new byte[exponentSize];
        Buffer.BlockCopy(data, (sizeof(uint) * 2) + (int)rsaPublicKeyModulusSize, exponent, 0, exponent.Length);

        // Create a RSA instance with them
        ServerRSA = RSA.Create(new RSAParameters() { Modulus = rsaPublicKeyModulus, Exponent = exponent });

        // Logger.InfoLine($"Server RSA key was sent!");

        // Generate random RC4 key
        var rc4Key = RandomNumberGenerator.GetBytes(5);
        ClientRC4 = new RC4Engine();
        ClientRC4.Init(false, new KeyParameter(rc4Key));
        ServerRC4 = new RC4Engine();
        ServerRC4.Init(false, new KeyParameter(rc4Key));

        // Encrypt our RC4 key
        var encryptedRc4Key = ServerRSA.Encrypt(rc4Key, RSAEncryptionPadding.Pkcs1);
        
        // Give it to the server
        ServerSocket.GetStream().Write(encryptedRc4Key);

        // Logger.InfoLine($"Our RC4 was sent!");

        // Login
        var login = new CG_ServerLogin() {
            Account = "a",
            Password = "a"
        };

        ServerSocket.GetStream().Write(CreatePacket(CreateMessage(login)));
    }

    public static byte[] CreateMessage<T>(T packet) where T : CommandBase {
        var serialized = packet.Serialize();
        var messageId = (ushort)packet.GetType().GetField("CommandID").GetRawConstantValue();

        var messageSize = serialized.Length;
        var message = new byte[sizeof(ushort) + messageSize];

        Buffer.BlockCopy(BitConverter.GetBytes(messageId), 0, message, 0, sizeof(ushort));
        Buffer.BlockCopy(serialized, 0, message, sizeof(ushort), messageSize);

        return message;
    }

    public static byte[] CreatePacket(byte[] data) {
        // Encrypt
        ClientRC4.ProcessBytes(data, data);

        var packetSize = (ushort)data.Length;
        var packet = new byte[sizeof(ushort) + packetSize];

        Buffer.BlockCopy(BitConverter.GetBytes(packetSize), 0, packet, 0, sizeof(ushort));
        Buffer.BlockCopy(data, 0, packet, sizeof(ushort), packetSize);

        return packet;
    }
}
