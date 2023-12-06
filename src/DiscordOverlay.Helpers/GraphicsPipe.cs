namespace DiscordOverlay.Helpers;

public abstract class GraphicsPipe
{
    private static readonly int HeaderSize = Marshal.SizeOf<Header>();
    private static Header _cachedHeader = null!;
    
    [StructLayout(LayoutKind.Sequential)]
    public class Header
    {
        public int Magic;
        public int FrameCount;
        public int NoClue;
        public int Width;
        public int Height;
        public byte[] Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ConnectedProcessInfo
    {
        public int ProcessId;
        public IntPtr File;
        public IntPtr MappedAddress;
    }

    public static bool ConnectToProcess(ConnectedProcessInfo processInfo)
    {
        var mappedFilename = "DiscordOverlay_Framebuffer_Memory_" + processInfo.ProcessId;
        processInfo.File = Memory.OpenFileMapping(
            Memory.FILE_MAP_ALL_ACCESS, 
            false,
            mappedFilename
            );
        
        if (processInfo.File == IntPtr.Zero)
            return false;

        processInfo.MappedAddress = Memory.MapViewOfFile(
            processInfo.File, 
            Memory.FILE_MAP_ALL_ACCESS,
            0, 
            0, 
            0
            );
        
        _cachedHeader ??= Marshal.PtrToStructure<Header>(processInfo.MappedAddress) ?? throw new ArgumentNullException(nameof(_cachedHeader), "Header is null.");
        
        return processInfo.MappedAddress != IntPtr.Zero;
    }

    public static void DisconnectFromProcess(ConnectedProcessInfo processInfo)
    {
        Memory.UnmapViewOfFile(processInfo.MappedAddress);
        processInfo.MappedAddress = IntPtr.Zero;

        Memory.CloseHandle(processInfo.File);
        processInfo.File = IntPtr.Zero;
    }


    public static void SendFrame(
        ConnectedProcessInfo processInfo,
        int width,
        int height,
        byte[] frame, 
        int size
    )
    {
        if (processInfo.MappedAddress == IntPtr.Zero)
            throw new InvalidOperationException("Process is not connected.");
        
        var bufferPtr = IntPtr.Add(processInfo.MappedAddress, HeaderSize);
        Marshal.Copy(frame, 0, bufferPtr, size);

        _cachedHeader.Width = width;
        _cachedHeader.Height = height;
        _cachedHeader.FrameCount++;
        
        Marshal.StructureToPtr(_cachedHeader, processInfo.MappedAddress, false);
    }
}