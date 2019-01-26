using UnityEngine;

public partial class GameManager
{
    void Awake_Xue()
    {
        InitializeClientGameSettings();
    }

    void Start_Xue()
    {
        GameBoardManager.Instance.GenerateMap("LevelTest");
        StartMenuManager.Instance.M_StateMachine.SetState(StartMenuManager.StateMachine.States.Show);
        GameBoardManager.Instance.M_StateMachine.SetState(GameBoardManager.StateMachine.States.Hide);
        SelectHeroesManager.Instance.M_StateMachine.SetState(SelectHeroesManager.StateMachine.States.Hide);
        TutorialMenuManager.Instance.M_StateMachine.SetState(TutorialMenuManager.StateMachine.States.Hide);
        BattleScorePanelManager.Instance.M_StateMachine.SetState(BattleScorePanelManager.StateMachine.States.Hide);
        ScoreMenuManager.Instance.M_StateMachine.SetState(ScoreMenuManager.StateMachine.States.Hide);
    }

    public void RestartGame()
    {
        StartMenuManager.Instance.Reset();
        SelectHeroesManager.Instance.Reset();
        GameBoardManager.Instance.Reset();
        TutorialMenuManager.Instance.Reset();
        BattleScorePanelManager.Instance.Reset();
        ScoreMenuManager.Instance.Reset();
    }

    private void InitializeClientGameSettings()
    {
        AllLevelMapColors.AddAllColors(Application.streamingAssetsPath + "/Config/LevelMapColor.xml");
    }
}