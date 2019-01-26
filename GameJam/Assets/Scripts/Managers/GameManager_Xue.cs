using UnityEngine;

public partial class GameManager
{
    void Awake_Xue()
    {
        InitializeClientGameSettings();
      
    }

    void Start_Xue()
    {
        StartMenuManager.Instance.M_StateMachine.SetState(StartMenuManager.StateMachine.States.Show);
        GameBoardManager.Instance.M_StateMachine.SetState(GameBoardManager.StateMachine.States.Hide);
        SelectHeroesManager.Instance.M_StateMachine.SetState(SelectHeroesManager.StateMachine.States.Hide);
    }

    public static int GameBoardWidth;
    public static int GameBoardHeight;

    private void InitializeClientGameSettings()
    {
        AllLevelMapColors.AddAllColors(Application.streamingAssetsPath + "/Config/LevelMapColor.xml");
    }
}