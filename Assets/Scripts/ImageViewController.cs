using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Manages the loading and display of JPEG images on a quad object in Unity.
/// </summary>
public class ImageViewer : MonoBehaviour
{
    /// <summary>
    /// The quad GameObject used to display the images. Must be assigned in the inspector.
    /// </summary>
    public GameObject quadObject;

    /// <summary>
    /// List of loaded image textures.
    /// </summary>
    private List<Texture2D> imageTextures = new List<Texture2D>();

    /// <summary>
    /// Index of the currently displayed image.
    /// </summary>
    private int currentImageIndex = -1;

    /// <summary>
    /// Renderer component of the quad object.
    /// </summary>
    private Renderer quadRenderer;

    /// <summary>
    /// Initializes the ImageViewer, checks for required components, and loads images.
    /// </summary>
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

    /// <summary>
    /// Loads all JPEG images from the application's persistent data path.
    /// </summary>
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

    /// <summary>
    /// Displays the next image in the list on the quad object.
    /// If the end of the list is reached, it wraps around to the beginning.
    /// </summary>
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