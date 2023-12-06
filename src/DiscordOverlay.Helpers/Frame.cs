namespace DiscordOverlay.Helpers;

public class Frame
{
    public int Width { get; }
    public int Height { get; }
    public int Size { get; }
    public byte[] Buffer { get; }

    public Frame(int width, int height)
    {
        Width = width;
        Height = height;
        Size = width * height * 4;
        Buffer = new byte[Size];
    }

    public void Clean()
    {
        Array.Clear(Buffer, 0, Size);
    }
}