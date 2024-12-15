using System.Runtime.InteropServices;

namespace ChromeCOM
{
    static class Chrome
    {
        public static string ChromeCLSID = "{708860E0-F641-4611-8895-7D867DD3675B}";
    }

    static class Chromium
    {
        public static string ChromiumCLSID = "{D133B120-6DB4-4D6B-8BFE-83BF8CA1B1B0}";
    }

    [ComVisible(true)]
    [Guid("A949CB4E-C4F9-44C4-B213-6BF8AA9AC69C")]
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
    }

    [ComVisible(true)]
    [Guid("B88C45B9-8825-4629-B83E-77CC67D9CEED")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromium : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("463ABECF-410D-407F-8AF5-0DF35A005CC8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChrome : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("A2721D66-376E-4D2F-9F0F-9070E9A42B5F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromeBeta : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("BB2AA26B-343A-4072-8B6F-80557B8CE571")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromeDev : IElevator
    {
    }

    [ComVisible(true)]
    [Guid("4F7CE041-28E9-484F-9DD0-61A8CACEFEE4")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElevatorChromeCanary : IElevator
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
    [Guid("0014D784-7012-4A79-8AB6-ADDB8193A06E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ElevatorLib
    {
    }
}

