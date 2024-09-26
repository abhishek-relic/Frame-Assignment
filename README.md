# VR Photo Frame Application

## Table of Contents
1. [Project Overview](#project-overview)
2. [Features](#features)
3. [Requirements](#requirements)
4. [Setup and Installation](#setup-and-installation)
5. [Usage](#usage)
6. [Code Structure](#code-structure)
7. [Performance Considerations](#performance-considerations)
8. [Known Issues and Limitations](#known-issues-and-limitations)
9. [Future Enhancements](#future-enhancements)

## Project Overview

This VR application allows users to interact with a virtual photo frame in a VR environment. Users can manipulate the frame, toggle its floating behavior, and cycle through different photos loaded from the device's storage.

## Features

- Interactive photo frame that can be grabbed, moved, scaled, and rotated
- "Activate Float" toggle button to make the frame float in the air
- "Next photo" action button to switch displayed photos
- Dynamic loading of photos from device storage
- Compatible with Meta Quest 2 and Meta Quest 3 devices

## Requirements

- Developerd on Unity 2022.3.17f1
- Meta Quest 2 or Meta Quest 3 device for testing and deployment
- Git for version control

## Setup and Installation

1. Clone this repository:
   ```
   git clone https://github.com/yourusername/vr-photo-frame.git
   ```

2. Open the project in Unity (version 2022.3.17f1).

3. Ensure you have the Meta SDK All-In-One package installed in your Unity project.

4. Build Settings:
   - Switch platform to Android

5. Player Settings:
   - Set "Color Space" to Linear
   - Set "Graphics APIs" to OpenGLES3
   - Set minimum API Level to Android 10.0
   - Install XR Plugin management
   - Enable 'Oculus' for Windows and Android

6. Place your JPG images in the application's persistent data path on the Quest device:
   `/sdcard/Android/data/DefaultCompany/Frame-Assignment/`

## Usage

1. Build and run the application on your Meta Quest device.
2. Use your VR controllers or hands to interact with the photo frame:
   - Grab the frame to move it
   - Use both controllers to scale
   - Grab UI indicators on the edges of the frame to rotate the frame
   - Grab UI indicators on the corners of the frame to rotate the frame
3. Press the "Activate Float" button to toggle the frame's floating behavior
4. Press the "Next photo" button to cycle through available photos

## Code Structure

The application consists of two main scripts:

### FramePositionController.cs

This script manages the position and rotation of the photo frame, including the floating behavior.

Key components:
- `ResetPosition`: Resets the frame's position based on its current state
- `ActivateFloat`: Toggles the floating behavior
- `SmoothMoveToCoroutine`: Handles smooth transitions between positions

### ImageViewer.cs

This script handles the loading and display of images on the photo frame.

Key components:
- `LoadImagesFromPersistentDataPath`: Loads JPEG images from the device storage
- `ShowNextImage`: Cycles to the next available image

## Performance Considerations

- Images are loaded at startup to minimize runtime performance impact
- Smooth transitions use coroutines to distribute processing over multiple frames

## Known Issues and Limitations

- Currently only supports JPG image format
- Limited to images stored in the application's persistent data path

## Future Enhancements

- Support for additional image formats (PNG, WebP, etc.)
