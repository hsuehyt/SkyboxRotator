# Skybox Rotator with Fading Effect

This Unity project rotates between different skybox textures with a smooth fading effect. It uses a custom shader for blending textures and a C# script to handle the rotation and transition logic.

## Features

- Automatically transitions between skybox textures at a set interval.
- Smooth fading effect between textures using a custom shader.
- Configurable switch interval and fade duration.

## Setup

### Requirements

- Unity 2021.3 or later
- Basic understanding of Unity's skybox system and shaders

### Installation

1. Clone or download this repository into your Unity project's `Assets` folder.
   - Repository: [Skybox Rotator GitHub](https://github.com/hsuehyt/SkyboxRotator)
2. Assign your panoramic skybox textures in the inspector of the `SkyboxRotator` script.
3. Create a material using the custom shader for skybox blending:
   - Right-click in the Project window and select **Create > Material**.
   - Assign the shader `Custom/SkyboxFadeShader` to the material.
   - The shader code is available [here](https://github.com/hsuehyt/SkyboxRotator/blob/main/SkyboxFadeShader.shader).
4. Apply this material as the skybox material in **Edit > Render Settings**.
5. Assign your skybox textures to the `SkyboxRotator` script in the Unity Inspector.

### Usage

1. Drag the `SkyboxRotator` script onto any GameObject in your scene.
2. In the Inspector, set the following parameters:
   - **Skybox Textures**: Assign textures to this array.
   - **Switch Interval**: Set the time (in seconds) between switching textures.
   - **Fade Duration**: Set the time (in seconds) for the fade transition between textures.
3. Press play to see the skybox textures transition with fading effects.

## Custom Shader

The shader `SkyboxFadeShader` is used to blend between two textures over time using a blend factor. The blend factor is controlled by the `SkyboxRotator` script to smoothly transition between the textures.

### Shader Code

The shader code can be found in the repository: [SkyboxFadeShader.shader](https://github.com/hsuehyt/SkyboxRotator/blob/main/SkyboxFadeShader.shader).