# DiscordOverlay

## Overview
`DiscordOverlay.Helpers` is a proof of concept C# library designed to demonstrate how to use Discord's overlay for drawing custom graphics. It utilizes SkiaSharp for graphics rendering and system-level functions for memory manipulation.

## Features
- **Direct Memory Management**: Leverages kernel32.dll for memory operations within Discord's process.
- **Graphics Rendering**: Utilizes SkiaSharp for high-quality 2D graphics.
- **Overlay Management**: Controls the overlay in Discord, with capabilities for various graphical operations.

## How It Works

### Hooking into Discord
- The library interacts with Discord's process memory using `Memory` class methods like `OpenFileMapping` and `MapViewOfFile`.
- These methods access and map Discord's memory, enabling the overlay to be drawn within its graphical interface.

### Graphics Rendering with SkiaSharp
- SkiaSharp is used for drawing graphics onto the overlay.
- The `Drawing` class offers methods such as `DrawLine`, `DrawRectangle`, `DrawString`, and `DrawCircle`.

### Managing Graphics Frames and Writing to the Buffer
- **Frame Management**: `GraphicsPipe` manages the connection to Discord, handling the shared memory for frame data.
- **Frame Representation**: The `Frame` class represents each graphics frame, storing dimensions and pixel data.
- **Buffer Writing Process**:
  - The graphics drawn using SkiaSharp are rendered onto a frame buffer (`Frame.Buffer`).
  - This buffer is a byte array representing pixel data for the entire frame.
  - The `GraphicsPipe.SendFrame` method is used to send this frame to the shared memory.
  - It involves copying the frame buffer to the shared memory location mapped to Discord's process.
  - The header (`GraphicsPipe.Header`) is updated with the new frame's dimensions and frame count.
  - This process ensures that each new frame is accurately rendered in the overlay.

### Images
![image](https://github.com/JoakimCarlsson/DiscordOverlay/assets/6985685/69176977-8d39-4959-8087-bc9360e7d808)

![image](https://github.com/JoakimCarlsson/DiscordOverlay/assets/6985685/61f98526-1255-4f3c-8abf-335892617b83)
