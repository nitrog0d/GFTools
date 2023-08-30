using System.Security.Cryptography;

namespace GFProxy;

public static class Program {
    public const int ProxyLoginServerPort = 6969;
    public const string LoginServerIP = "154.56.43.223";
    public const int LoginServerPort = 7543;

    public const int ProxyWorldServerPort = 7070;
    public const string WorldServerIP = "154.56.43.223";
    public const int WorldServerPort = 5567;

    public const int ProxyZoneServerPort = 7071;
    public const string ZoneServerIP = "154.56.43.223";
    public const int ZoneServerPort = 10020;

    private const string RSAXmlKey = "<RSAKeyValue><Modulus>0N5TWVqI5L+5B0PuaTCea+VIwM9M9ZTFkbDITSS2rHmwr8rxSdaUGn3CqLIPPtOABV5DP/R45DSec+Tz34eLWx7A7/vBsPod4ZOtvaDCDJcKY9XiS14ATH803VLJq6jWHeoGQUPHP1lP64picBHu4/6br30pnhjUl4/CfCUm6rwpgK4f02c7lfuFyZVv/OImgBaJvzQhNHIFiRFtWdw1VGbqAPzXydXlSKifIoWqlrdzLWqDbC6tNsY7sm1ut2ZRETp6pddxvZPf23JyuxqV3aO6bZvcgGdo3ai4X2xHi4QWum4bsUFhWb2+uMpb6hiSqFyBzF6FXhs//eoS7mfDPQ==</Modulus><Exponent>//8=</Exponent><P>+y+KMBBFNlEUu6Q0e6tnXBsiQb/z1v5bgBGLjndZp1GcUiS9ZHk/jSvwsgrUsdmhkDxlXhP2bnJ/Qm55VxSklMrcCe1iJUkLODg63Qhh9R5Zb5yKEDLpjDMtbt21BRAWB5geOsRpCGQDBqSXKQfzgfB/iKOU13JDTDdqe9YJx9s=</P><Q>1N8nPRU2RoIzBbsq5Ol4QSV3lxUsu35E5hu5h+9P2AMiIStFviNIJRaPT6j6aHbt01/5/fUfhWt2eDGEh/Xk1d1O3CQTQcFQ/P4Rx1q8C22SMr/WiXc25Kns+xMzSVdDKjj/3iQxKCtgSMQL1zTslTFBj2Xa8j1b0+jtYYv4uMc=</Q><DP>W6O6mLV4RYIoB4C2WlhphKa2zR15bcenX0dF5hZ2vp5nmutcN5TngUMqCc43WL2TS7ckXk+8thXhfTjU41a+RbqrsxbaZh6lA36fECRQCQHEeJlfg7pmqHmQK8Nz/rXWD0lRSJfSPU7h3X1HVtJqa7EN6AI9njsgwl316fWG9P0=</DP><DQ>NMOUiWRvXGFZ7uGXP1xtvvN8W9sHdXB9nFazjzkMT2L2NJbiXEczBQRggeswHFy2x38tgfPnyAWu3vGE/wgdxHAjIAvBIamAMHVZ9Xee26QARLx5dYQWCzt5jKUK91SsZn+PSyrbqgSKXhOV0uldHUfWu9XG3pm8AH5YdOoajEc=</DQ><InverseQ>emFcp8+aTEW4FnV0qwg0o5ljqz8FhFE3Qpn1H05keXr15ybqCV7qyvA8XVuLpnFUeu/o2VUieBR/HSVaKeE5aoihjMEnhcc2PiC+mjkaP0yVMHMKd3HVE+JGhQQ3fXbCe6Axu1X3TRRSmfe8xXrqiRDni7gy1UWco+6NbQdLISY=</InverseQ><D>mXUA53W4au/6i34HJdV7KbZk8vUAeqng9RO7rND2+px+WRXN7dKRMCu3cJIKX23Yy0p1aPEwxrIjYD8FHy3/o1iyjsMgp/vacfZmqulHmnGOdrm5dKKrkN9CISEVBbAXx6AYaomLa2k0bbxgYMC9qMVW/PxM0NEE+cL7YRrYu2rWY+JQ1LI5H9sc2DlB+Odd5z+uZhf2gamG4wN6oTXSBn+2e2pg84MYZc26v/Lm4EIM+dgQrxH4NmWeSb66aHXPyd3ZQgFt5+t8zcQdZqgcCF7uJY8UgOwy3vNsOQQ0zjRF1GO5/0882tfeAdx8ajcakBa8gZDQ+llx7sZ7F/n5Mw==</D></RSAKeyValue>";
    public static RSA RSA { get; private set; }

    public static void Main(string[] args) {
        Logger.InfoLine("Initializing Proxy...");

        RSA = RSA.Create();
        RSA.FromXmlString(RSAXmlKey);

        _ = new LoginServer(ProxyLoginServerPort, LoginServerIP, LoginServerPort);
        _ = new WorldServer(ProxyWorldServerPort, WorldServerIP, WorldServerPort);
        _ = new ZoneServer(ProxyZoneServerPort, ZoneServerIP, ZoneServerPort);

        Console.Title = "GFProxy";
    }
}
