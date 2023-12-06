namespace DiscordOverlay.Helpers;

public static class Drawing
{
    private static SKSurface? _surface;
    private static SKCanvas? _canvas;
    private static GCHandle _bufferHandle;
    private static Frame? _currentFrame;

    private static void EnsureSkiaInitialized(Frame frame)
    {
        if (_currentFrame == frame) return;
        
        _canvas?.Dispose();
        _surface?.Dispose();
        if (_bufferHandle.IsAllocated)
            _bufferHandle.Free();

        var info = new SKImageInfo(frame.Width, frame.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
        _bufferHandle = GCHandle.Alloc(frame.Buffer, GCHandleType.Pinned);
        IntPtr ptr = _bufferHandle.AddrOfPinnedObject();
        _surface = SKSurface.Create(info, ptr, frame.Width * 4);
        _canvas = _surface.Canvas;
        _currentFrame = frame;
    }

    public static void DrawLine(
        Frame frame, 
        int x1,
        int y1,
        int x2, 
        int y2,
        SKColor color
        )
    {
        EnsureSkiaInitialized(frame);
        using var paint = new SKPaint();
        paint.Color = color;
        paint.StrokeWidth = 1;
        _canvas?.DrawLine(x1, y1, x2, y2, paint);
    }

    public static void DrawRectangle(
        Frame frame,
        SKRect rect,
        SKColor color,
        bool fill = false
        )
    {
        EnsureSkiaInitialized(frame);
        using var paint = new SKPaint();
        paint.Color = color;
        paint.Style = fill ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
        _canvas?.DrawRect(rect, paint);
    }

    public static void DrawString(
        Frame frame,
        string text, 
        int x, 
        int y, 
        SKColor color,
        float fontSize
        )
    {
        EnsureSkiaInitialized(frame);
        using var paint = new SKPaint();
        paint.Color = color;
        paint.TextSize = fontSize;
        paint.IsAntialias = true;
        _canvas?.DrawText(text, x, y, paint);
    }
    
    public static void DrawCircle(
        Frame frame,
        int x,
        int y,
        int radius,
        SKColor color,
        bool fill = false
        )
    {
        EnsureSkiaInitialized(frame);
        using var paint = new SKPaint();
        paint.Color = color;
        paint.Style = fill ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
        _canvas?.DrawCircle(x, y, radius, paint);
    }

    public static void CleanupSkia()
    {
        _canvas?.Dispose();
        _surface?.Dispose();
        if (_bufferHandle.IsAllocated)
            _bufferHandle.Free();

        _canvas = null;
        _surface = null;
        _currentFrame = null;
    }
}