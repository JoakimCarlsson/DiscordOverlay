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
        IntPtr dwDesiredAccess,
        [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
        string lpName
    );
    
    [LibraryImport("kernel32.dll")]
    public static partial IntPtr MapViewOfFile(
        IntPtr hFileMappingObject,
        IntPtr dwDesiredAccess,
        IntPtr dwFileOffsetHigh,
        IntPtr dwFileOffsetLow,
        IntPtr dwNumberOfBytesToMap
    );
    
    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool UnmapViewOfFile(IntPtr lpBaseAddress);
    
    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool CloseHandle(IntPtr hObject);
    
    public const IntPtr FILE_MAP_ALL_ACCESS = 0xF001F;
}