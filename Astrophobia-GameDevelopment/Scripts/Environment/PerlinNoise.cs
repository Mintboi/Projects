
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 2048;
    public int height = 2048;

    public float Scale = 20;
    public float offsetX = 100f;
    public float offsetY = 100f;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
        offsetX = Random.Range(0f, 999f);
        offsetY = Random.Range(0f, 999f);
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        //Generate a perlin noisemap

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        //Applies the perlin noisemap to the texture

        return texture;
    }
    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * Scale + offsetX;
        float yCoord = (float)y / height * Scale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
