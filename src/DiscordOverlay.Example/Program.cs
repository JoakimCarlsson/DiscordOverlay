using SkiaSharp;

var frame = new Frame(2560, 1440);

var process = Process.GetProcessesByName("ProjectZomboid64").FirstOrDefault();
if (process is null)
{
    Console.WriteLine("Project Zomboid is not running.");
    return;
}

var processInfo = new GraphicsPipe.ConnectedProcessInfo
{
    ProcessId = process.Id
};

var isConnected = GraphicsPipe.ConnectToProcess(processInfo);

if (isConnected)
{
    var random = new Random();
    for (int i = 0; i < 100; i++)
    {
        var x = random.Next(0, frame.Width);
        var y = random.Next(0, frame.Height);
        var shouldFill = random.Next(0, 2) == 1;
        var radius = random.Next(0, 100);
        var color = new SKColor(
            (byte)random.Next(0, 255), 
            (byte)random.Next(0, 255), 
            (byte)random.Next(0, 255)
            );
        Drawing.DrawCircle(
            frame,
            x,
            y, 
            radius, 
            color, 
            shouldFill
            );
    }

    GraphicsPipe.SendFrame(processInfo, frame.Width, frame.Height, frame.Buffer, frame.Size);
}

Console.ReadKey();
Console.WriteLine("Overlay operation completed.");