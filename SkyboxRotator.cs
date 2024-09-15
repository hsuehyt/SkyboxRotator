using System.Collections;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    // Assign your 3 panoramic images from the inspector
    public Texture[] skyboxTextures;
    public float switchInterval = 5f; // Time between switches (in seconds)
    public float fadeDuration = 1f; // Duration of the fade effect (in seconds)

    private int currentIndex = 0;
    private Material skyboxMaterial;
    private bool isFading = false;

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
        if (!isFading)
        {
            // Start the fading coroutine
            StartCoroutine(FadeToNextTexture());
        }
    }

    private IEnumerator FadeToNextTexture()
    {
        isFading = true;

        // Get the current texture and the next texture
        Texture currentTexture = skyboxTextures[currentIndex];
        currentIndex = (currentIndex + 1) % skyboxTextures.Length;
        Texture nextTexture = skyboxTextures[currentIndex];

        // Set the next texture as the blend texture
        skyboxMaterial.SetTexture("_BlendTex", nextTexture);

        // Gradually increase the blend factor to create the fade effect
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float blendFactor = Mathf.Clamp01(elapsedTime / fadeDuration);
            skyboxMaterial.SetFloat("_Blend", blendFactor);
            yield return null;
        }

        // Once the fade is complete, set the next texture as the main texture
        skyboxMaterial.SetTexture("_MainTex", nextTexture);
        skyboxMaterial.SetFloat("_Blend", 0f); // Reset blend factor

        isFading = false;
    }
}
