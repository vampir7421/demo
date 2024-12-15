using System.Runtime.InteropServices;

namespace BraveCOM
{
    static class Brave
    {
        public static string BraveCLSID = "{576B31AF-6369-4B6B-8560-E4B203A97A8B}";
    }

    [ComVisible(true)]
    [Guid("5A9A9462-2FA1-4FEB-B7F2-DF3D19134463")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevator
    {
        [PreserveSig]
        int RunRecoveryCRXElevated(
            [In, MarshalAs(UnmanagedType.LPWStr)] string crx_path,
            [In, MarshalAs(UnmanagedType.LPWStr)] string browser_appid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string browser_version,
            [In, MarshalAs(UnmanagedType.LPWStr)] string session_id,
            [In] uint caller_proc_id,
            [Out] out UIntPtr proc_handle);

        [PreserveSig]
        int EncryptData(
            [In] ProtectionLevel protection_level,
            [In, MarshalAs(UnmanagedType.BStr)] string plaintext,
            [Out, MarshalAs(UnmanagedType.BStr)] out string ciphertext,
            [Out] out uint last_error);

        [PreserveSig]
        int DecryptData(
            [In, MarshalAs(UnmanagedType.BStr)] string ciphertext,
            [Out, MarshalAs(UnmanagedType.BStr)] out string plaintext,
            [Out] out uint last_error);

        [PreserveSig]
        int InstallVPNServices();
    }

    [ComVisible(true)]
    [Guid("3218DA17-49C2-479A-8290-311DBFB86490")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromium : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("F396861E-0C8E-4C71-8256-2FAE6D759CE9")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChrome : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("9EBAD7AC-6E1E-4A1C-AA85-1A70CADA8D82")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromeBeta : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("1E43C77B-48E6-4A4C-9DB2-C2971706C255")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromeDev : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("1DB2116F-71B7-49F0-8970-33B1DACFB072")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromeCanary : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("17239BF1-A1DC-4642-846C-1BAC85F96A10")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorDevelopment : IElevator
    {
    }

    public enum ProtectionLevel
    {
        PROTECTION_NONE = 0,
        PROTECTION_PATH_VALIDATION_OLD = 1,
        PROTECTION_PATH_VALIDATION = 2,
        PROTECTION_MAX = 3
    }

    [ComVisible(true)]
    [Guid("C3B01C4D-FBD4-4E65-88AD-0972D75808C2")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ElevatorLib
    {
    }
}

