using UnityEngine;
using System.IO;

# if UNITY_EDITOR
using UnityEditor;

public class UIMockupGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate UI Mockups")]
    public static void GenerateMockups()
    {
        string folderPath = "Assets/Textures/Mockups";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        CreateTexture(folderPath, "Background_Mockup", 1920, 1080, new Color(0.1f, 0.1f, 0.1f));
        CreateTexture(folderPath, "Button_Mockup", 256, 64, Color.gray);
        CreateTexture(folderPath, "Panel_Mockup", 512, 512, new Color(0.2f, 0.2f, 0.2f, 0.8f));
        CreateTexture(folderPath, "LifeBar_Background", 256, 32, Color.black);
        CreateTexture(folderPath, "LifeBar_Fill", 256, 32, Color.green);
        CreateTexture(folderPath, "Coin_Mockup", 64, 64, Color.yellow);
        CreateTexture(folderPath, "Bomb_Mockup", 64, 64, Color.red);
        CreateTexture(folderPath, "Basket_Mockup", 128, 64, Color.blue);

        AssetDatabase.Refresh();
        Debug.Log("UI Mockups generated in: " + folderPath);
    }

    private static void CreateTexture(string path, string name, int width, int height, Color color)
    {
        Texture2D tex = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }
        tex.SetPixels(pixels);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(path, name + ".png"), bytes);
        DestroyImmediate(tex);
    }
}
#endif
