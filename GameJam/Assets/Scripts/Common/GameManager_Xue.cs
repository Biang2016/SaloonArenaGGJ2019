using UnityEngine;

public partial class GameManager
{
    void Awake_Xue()
    {
        AllLevelMapColors.AddAllColors(Application.streamingAssetsPath + "/Config/LevelMapColor.xml");
    }

    void Start_Xue()
    {
    }
}