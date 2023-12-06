namespace DiscordOverlay.Helpers;

public static partial class Memory
{

    [LibraryImport(
        "kernel32.dll", 
        EntryPoint = "OpenFileMappingW",
        StringMarshalling = StringMarshalling.Utf16,
        SetLastError = true
        )]
    public static partial IntPtr OpenFileMapping(
        uint dwDesiredAccess,
        [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
        string lpName
    );
    
    [LibraryImport("kernel32.dll")]
    public static partial IntPtr MapViewOfFile(
        IntPtr hFileMappingObject,
        uint dwDesiredAccess,
        uint dwFileOffsetHigh,
        uint dwFileOffsetLow,
        uint dwNumberOfBytesToMap
    );
    
    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UnmapViewOfFile(IntPtr lpBaseAddress);
    
    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool CloseHandle(IntPtr hObject);
    
    public const uint FILE_MAP_ALL_ACCESS = 0xF001F;
}