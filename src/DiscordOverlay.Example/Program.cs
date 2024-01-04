var frame = new Frame(2560, 1440);

var process = Process.GetProcessesByName("EscapeFromTarkov").FirstOrDefault();
if (process is null)
{
    Console.WriteLine("EscapeFromTarkov is not running.");
    return;
}

var processInfo = new GraphicsPipe.ConnectedProcessInfo
{
    ProcessId = process.Id
};

var isConnected = GraphicsPipe.ConnectToProcess(processInfo);

if (isConnected)
{
    var centerX = frame.Width / 2;
    var centerY = frame.Height / 2;
    string fullText = "Hello World";
    string currentText = "";

    for (int i = 0; i <= fullText.Length; i++)
    {
        currentText = fullText.Substring(0, i);
        Drawing.ClearCanvas(frame);
        Drawing.DrawString(frame, currentText, centerX, centerY, Colours.Red, 24);
        Drawing.DrawCircle(frame, centerX, centerY, 100, Colours.Green);

        GraphicsPipe.SendFrame(processInfo, frame.Width, frame.Height, frame.Buffer, frame.Size);

        Thread.Sleep(500);
    }
}

Console.ReadKey();
Console.WriteLine("Overlay operation completed.");