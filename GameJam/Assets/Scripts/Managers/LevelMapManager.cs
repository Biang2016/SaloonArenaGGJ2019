using System.Collections.Generic;
using UnityEngine;

public class LevelMapManager : MonoSingleton<LevelMapManager>
{
    [SerializeField] private Texture2D[] LevelMapImages;

    public Dictionary<string, LevelMap> LevelMaps = new Dictionary<string, LevelMap>();

    void Awake()
    {
    }

    void Start()
    {
        foreach (Texture2D t2d in LevelMapImages)
        {
            int[,] indices = new int[GameBoardManager.Instance.GameBoardWidth, GameBoardManager.Instance.GameBoardHeight];

            Color[] colors = t2d.GetPixels();
            string debugStr = "";
            for (int i = 0; i < t2d.height; i++)
            {
                for (int j = 0; j < t2d.width; j++)
                {
                    Color color = colors[i * t2d.width + j];
                    string color_str = Utils.ColorToHTMLColor(color);

                    if (AllLevelMapColors.ColorDict_Rev.ContainsKey(color_str))
                    {
                        int index = (int) AllLevelMapColors.ColorDict_Rev[color_str];
                        indices[j, i] = index;
                        debugStr += index;
                    }
                }

                debugStr += "\n";
            }

            Debug.Log(debugStr);
            LevelMap levelMap = new LevelMap(t2d.name, indices);
            LevelMaps.Add(t2d.name, levelMap);
        }
    }
}