using UnityEngine;
using UnityEngine.UI;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private Texture2D LevelMapImage;

    void Awake()
    {
        Color[] colors = LevelMapImage.GetPixels();
        Debug.Log(LevelMapImage.width);
        Debug.Log(LevelMapImage.height);
        Debug.Log(colors.Length);

        string debugStr = "";
        for (int i = 0; i < LevelMapImage.height; i++)
        {
            for (int j = 0; j < LevelMapImage.width; j++)
            {
                Color color = colors[i * LevelMapImage.width + j];
                string color_str = Utils.ColorToHTMLColor(color);

                if (AllLevelMapColors.ColorDict_Rev.ContainsKey(color_str))
                {
                    int index = (int) AllLevelMapColors.ColorDict_Rev[color_str];
                    debugStr += index;
                }
            }

            debugStr += "\n";
        }

        Debug.Log(debugStr);
    }
}