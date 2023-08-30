using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace GFProxy;

public class ProxyClientBase {
    public ProxyServerBaseImpl Context { get; set; }
    public Logger Logger { get; set; }

    public TcpClient ClientSocket { get; set; }
    public TcpClient ServerSocket { get; set; }

    public RSA ServerRSA { get; private set; }
    private RC4Engine ClientRC4 { get; set; }
    private RC4Engine ServerRC4 { get; set; }
    private RC4Engine ProxyClientRC4 { get; set; }
    private RC4Engine ProxyServerRC4 { get; set; }

    private bool FirstClientPacket { get; set; } = true;
    private bool FirstServerPacket { get; set; } = true;

    public void Init() {
        var clientThread = new Thread(HandleClientSocket);
        clientThread.Name = $"{Logger.Source} - {ClientSocket.Client.RemoteEndPoint} Client Thread";
        clientThread.Start();

        var serverThread = new Thread(HandleServerSocket);
        serverThread.Name = $"{Logger.Source} - {ClientSocket.Client.RemoteEndPoint} Server Thread";
        serverThread.Start();
    }

    public virtual bool HandleClientPacket(ushort commandId, ref byte[] command) => true;

    public virtual bool HandleServerPacket(ushort commandId, ref byte[] command) => true;

    private void HandleServerRSA(byte[] data) {
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

        // Get our own RSA key's params
        var ourRsaParams = Program.RSA.ExportParameters(false);

        var ourRsaPublicKeyModulus = ourRsaParams.Modulus;
        var ourRsaExponent = ourRsaParams.Exponent;

        // Create a custom packet to send our RSA key
        var customPacket = new byte[(sizeof(int) * 2) + ourRsaPublicKeyModulus.Length + ourRsaExponent.Length];

        // Copy the sizes to the new packet buffer
        Buffer.BlockCopy(BitConverter.GetBytes(ourRsaPublicKeyModulus.Length), 0, customPacket, 0, sizeof(uint));
        Buffer.BlockCopy(BitConverter.GetBytes(ourRsaExponent.Length), 0, customPacket, sizeof(uint), sizeof(uint));

        // Copy the RSA key stuff to the buffer
        Buffer.BlockCopy(ourRsaPublicKeyModulus, 0, customPacket, sizeof(uint) * 2, ourRsaPublicKeyModulus.Length);
        Buffer.BlockCopy(ourRsaExponent, 0, customPacket, (sizeof(uint) * 2) + ourRsaPublicKeyModulus.Length, ourRsaExponent.Length);

        // Send packet
        ClientSocket.GetStream().Write(customPacket);
    }

    private void HandleClientRC4(byte[] data) {
        // Decrypt the RC4 Key with our RSA Key (That was sent from the server, first packet ever)
        var decryptedRc4Key = Program.RSA.Decrypt(data, RSAEncryptionPadding.Pkcs1);

        // Initialize RC4 instances
        ClientRC4 = new RC4Engine();
        ClientRC4.Init(false, new KeyParameter(decryptedRc4Key));

        ServerRC4 = new RC4Engine();
        ServerRC4.Init(false, new KeyParameter(decryptedRc4Key));

        // This is to modify packets without fucking the RC4 state
        ProxyClientRC4 = new RC4Engine();
        ProxyClientRC4.Init(false, new KeyParameter(decryptedRc4Key));

        ProxyServerRC4 = new RC4Engine();
        ProxyServerRC4.Init(false, new KeyParameter(decryptedRc4Key));

        // Logger.InfoLine($"RC4 Key: {Convert.ToHexString(decryptedRc4Key)}");

        // Reencrypt the RC4 key to send to the server with the key sent to us
        var reencrypted = ServerRSA.Encrypt(decryptedRc4Key, RSAEncryptionPadding.Pkcs1);

        ServerSocket.GetStream().Write(reencrypted);
    }

    private void HandleClientRawPacket(byte[] packet) {
        // Logger.InfoLine($"Client: {Convert.ToBase64String(packet)}");

        // Get command id
        var commandId = BitConverter.ToUInt16(packet, 0);
        // Create a byte array of the size of the actual packet
        var command = new byte[packet.Length - 2];
        // Copy the actual command to the command variable
        Buffer.BlockCopy(packet, sizeof(ushort), command, 0, command.Length);
        
        // Handle this packet, if the function returns false, it will not be sent to the recipient.
        if (!HandleClientPacket(commandId, ref command)) {
            return;
        }

        // Recreate our command 
        packet = new byte[sizeof(ushort) + command.Length];
        // Copy the command id
        Buffer.BlockCopy(BitConverter.GetBytes(commandId), 0, packet, 0, sizeof(ushort));
        // Copy the command content
        Buffer.BlockCopy(command, 0, packet, sizeof(ushort), command.Length);

        // Create a new packet
        var newPacketSize = packet.Length;
        var newPacket = new byte[sizeof(ushort) + newPacketSize];

        // Copy the packet size to our new packet
        Buffer.BlockCopy(BitConverter.GetBytes(newPacketSize), 0, newPacket, 0, sizeof(ushort));

        // Logger.InfoLine($"Client Recreated: {newPacketSize} - {Convert.ToBase64String(packet)}");

        // Encrypt our new packet
        ProxyClientRC4.ProcessBytes(packet, packet);
        // Copy our packet to the new packet
        Buffer.BlockCopy(packet, 0, newPacket, sizeof(ushort), packet.Length);

        // Logger.InfoLine($"Client Reencrypted: {Convert.ToBase64String(newPacket)}");

        // Send packet
        ServerSocket.GetStream().Write(newPacket);
    }

    private void HandleServerRawPacket(byte[] packet) {
        // Logger.InfoLine($"Server: {Convert.ToBase64String(packet)}");

        // Get command id
        var commandId = BitConverter.ToUInt16(packet, 0);
        // Create a byte array of the size of the actual packet
        var command = new byte[packet.Length - 2];
        // Copy the actual command to the command variable
        Buffer.BlockCopy(packet, sizeof(ushort), command, 0, command.Length);

        // Handle this packet, if the function returns false, it will not be sent to the recipient.
        if (!HandleServerPacket(commandId, ref command)) {
            return;
        }

        // Recreate our command 
        packet = new byte[sizeof(ushort) + command.Length];
        // Copy the command id
        Buffer.BlockCopy(BitConverter.GetBytes(commandId), 0, packet, 0, sizeof(ushort));
        // Copy the command content
        Buffer.BlockCopy(command, 0, packet, sizeof(ushort), command.Length);

        // Create a new packet
        var newPacketSize = packet.Length;
        var newPacket = new byte[sizeof(ushort) + newPacketSize];

        // Copy the packet size to our new packet
        Buffer.BlockCopy(BitConverter.GetBytes(newPacketSize), 0, newPacket, 0, sizeof(ushort));

        // Logger.InfoLine($"Server Recreated: {newPacketSize} - {Convert.ToBase64String(packet)}");

        // Encrypt our new packet
        ProxyServerRC4.ProcessBytes(packet, packet);
        // Copy our packet to the new packet
        Buffer.BlockCopy(packet, 0, newPacket, sizeof(ushort), packet.Length);

        // Logger.InfoLine($"Server Reencrypted: {Convert.ToBase64String(newPacket)}");

        // Send packet
        ClientSocket.GetStream().Write(newPacket);
    }

    private void HandleClientSocket() {
        var stream = ClientSocket.GetStream();

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
                if (FirstClientPacket) {
                    FirstClientPacket = false;
                    var data = new byte[bytesRead];
                    Buffer.BlockCopy(buffer, 0, data, 0, bytesRead);
                    HandleClientRC4(data);
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

                            HandleClientRawPacket(packetBuffer);
                        }
                        
                    }


                }
                
            } else break;
        }

        Logger.VerboseLine("Client disconnected.");
        ClientSocket.Dispose();

        if (ServerSocket.Connected) {
            Logger.VerboseLine("Server connection was open, closing...");
            ServerSocket.Dispose();
        }
    }

    private void HandleServerSocket() {
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
                            ClientRC4.ProcessBytes(packetBuffer, packetBuffer);

                            HandleServerRawPacket(packetBuffer);
                        }
                    }
                }

            } else break;
        }

        Logger.VerboseLine("Server disconnected.");
        ServerSocket.Dispose();

        if (ClientSocket.Connected) {
            Logger.VerboseLine("Client connection was openc closing...");
            ClientSocket.Dispose();
        }
    }
}
