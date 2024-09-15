using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    // Assign your 3 panoramic images from the inspector
    public Texture[] skyboxTextures;
    public float switchInterval = 5f; // Time between switches (in seconds)

    private int currentIndex = 0;
    private Material skyboxMaterial;

    void Start()
    {
        // Make sure you have a skybox material assigned in the RenderSettings
        skyboxMaterial = RenderSettings.skybox;
        if (skyboxMaterial != null && skyboxTextures.Length > 0)
        {
            // Initialize the first texture
            skyboxMaterial.SetTexture("_MainTex", skyboxTextures[currentIndex]);
            InvokeRepeating("SwitchSkyboxTexture", switchInterval, switchInterval);
        }
        else
        {
            Debug.LogError("No skybox material or textures found!");
        }
    }

    void SwitchSkyboxTexture()
    {
        // Switch to the next texture in the array
        currentIndex = (currentIndex + 1) % skyboxTextures.Length;
        skyboxMaterial.SetTexture("_MainTex", skyboxTextures[currentIndex]);
    }
}
