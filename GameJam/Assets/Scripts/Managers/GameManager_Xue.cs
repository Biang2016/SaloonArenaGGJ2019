using UnityEngine;

public partial class GameManager
{
    void Awake_Xue()
    {
        InitializeClientGameSettings();
    }

    void Start_Xue()
    {
    }

    public static int GameBoardWidth;
    public static int GameBoardHeight;
    
    private void InitializeClientGameSettings()
    {
        AllLevelMapColors.AddAllColors(Application.streamingAssetsPath + "/Config/LevelMapColor.xml");
    }
}