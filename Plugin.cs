using BepInEx;
using UnityEngine;
using System.IO;
using System;

[BepInPlugin("instel.baldiisnear", "Baldi is Near", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private Texture2D texture;
    private Baldi baldi;
    private PlayerMovement player;

    private void Awake()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Modded/instel.baldinear/meme.png");
        texture = LoadTexture(filePath);
    }

    private Texture2D LoadTexture(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        byte[] fileData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        if (tex.LoadImage(fileData))
        {
            return tex;
        }
        return null;
    }

    private void OnGUI()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();

        }

        if (baldi == null)
        {
            baldi = FindObjectOfType<Baldi>();
        }

        if (player == null || baldi == null || texture == null)
        {
            return;
        }

        float width = texture.width;
        float height = texture.height;

        float maxWidth = Screen.width * 0.5f;
        float maxHeight = Screen.height * 0.5f;
        if (width > maxWidth)
        {
            float ratio = maxWidth / width;
            width *= ratio;
            height *= ratio;
        }
        if (height > maxHeight)
        {
            float ratio = maxHeight / height;
            width *= ratio;
            height *= ratio;
        }

        float x = Screen.width - width - 10;
        float y = (Screen.height - height) / 2f;

        GUI.DrawTexture(new Rect(x, y, width, height), texture);

        float distance = Vector3.Distance(player.transform.position, baldi.transform.position);
        string distanceText = Math.Round(distance).ToString();
        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            fontSize = 30,
            normal = { textColor = Color.black },
            alignment = TextAnchor.UpperCenter
        };

        GUI.Label(new Rect(x - 50, y + height / 2f - 118, width, 50), distanceText, style);

    }
}
