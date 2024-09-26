using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class ImageViewer : MonoBehaviour
{
    public GameObject quadObject; // Assign this in the inspector
    private List<Texture2D> imageTextures = new List<Texture2D>();
    private int currentImageIndex = -1;
    private Renderer quadRenderer;

    void Start()
    {
        if (quadObject == null)
        {
            Debug.LogError("Quad object is not assigned. Please assign it in the inspector.");
            return;
        }

        quadRenderer = quadObject.GetComponent<Renderer>();
        if (quadRenderer == null)
        {
            Debug.LogError("Quad object does not have a Renderer component.");
            return;
        }

        LoadImagesFromPersistentDataPath();
    }

    void LoadImagesFromPersistentDataPath()
    {
        string persistentPath = Application.persistentDataPath;
        string[] jpegFiles = Directory.GetFiles(persistentPath, "*.jpg").Concat(Directory.GetFiles(persistentPath, "*.jpeg")).ToArray();

        foreach (string filePath in jpegFiles)
        {
            Texture2D texture = new Texture2D(2, 2);
            byte[] imageData = File.ReadAllBytes(filePath);
            
            if (texture.LoadImage(imageData))
            {
                imageTextures.Add(texture);
            }
        }

        Debug.Log($"Loaded {imageTextures.Count} images from persistent data path.");
        ShowNextImage();
    }

    public void ShowNextImage()
    {
        if (imageTextures.Count == 0)
        {
            Debug.LogWarning("No images available to display.");
            return;
        }

        currentImageIndex = (currentImageIndex + 1) % imageTextures.Count;
        quadRenderer.material.mainTexture = imageTextures[currentImageIndex];
    }
}