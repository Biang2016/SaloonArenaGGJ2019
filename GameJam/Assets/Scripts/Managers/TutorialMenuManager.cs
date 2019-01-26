using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenuManager : MonoSingleton<TutorialMenuManager>
{
    private TutorialMenuManager()
    {
    }

    void Awake()
    {
        M_StateMachine = new StateMachine();
    }

    [SerializeField] private Image BG;

    [SerializeField] private Canvas TutorialMenuCanvas;

    public StateMachine M_StateMachine;

    public class StateMachine
    {
        public StateMachine()
        {
            state = States.Default;
            previousState = States.Default;
        }

        public enum States
        {
            Default,
            Hide,
            Show,
        }

        public bool IsShow()
        {
            return state != States.Default && state != States.Hide;
        }

        private States state;
        private States previousState;

        public void SetState(States newState)
        {
            if (state != newState)
            {
                switch (newState)
                {
                    case States.Hide:
                    {
                        HideMenu();
                        break;
                    }
                    case States.Show:
                    {
                        ShowMenu();
                        AudioManager.Instance.BGMFadeIn("bgm/Battle_0");
                        break;
                    }
                }

                previousState = state;
                state = newState;
            }
        }

        public void ReturnToPreviousState()
        {
            SetState(previousState);
        }

        public States GetState()
        {
            return state;
        }

        public void Update()
        {
        }

        private void ShowMenu()
        {
            Instance.TutorialMenuCanvas.enabled = true;
        }

        private void HideMenu()
        {
            Instance.TutorialMenuCanvas.enabled = false;
        }
    }

    void Start()
    {
        foreach (PlayerTutorialConfirm ptc in PlayerTutorialConfirms)
        {
            ptc.Initialize(Players.NoPlayer);
        }
    }

    public bool IsGameStart = false;
    public PlayerTutorialConfirm[] PlayerTutorialConfirms = new PlayerTutorialConfirm[4];

    void Update()
    {
        if (M_StateMachine.GetState() == StateMachine.States.Hide) return;
        if (IsGameStart) return;
        if (Input.GetKeyDown(KeyCode.F6))
        {
            StartGame();
            return;
        }
        for (int i = 0; i < 4; i++)
        {
            string Index_name = "P" + (i + 1) + "_";
            if (Input.GetButtonDown(Index_name + "fire") && Input.GetAxisRaw(Index_name + "fire")==-1)
            {
                if (PlayerTutorialConfirms[i].M_PlayerState == PlayerTutorialConfirm.PlayerState.None)
                {
                    PlayerTutorialConfirms[i].M_PlayerState = PlayerTutorialConfirm.PlayerState.Waiting;
                    PlayerTutorialConfirms[i].Initialize((Players) i);
                }
                else if (PlayerTutorialConfirms[i].M_PlayerState == PlayerTutorialConfirm.PlayerState.Waiting)
                {
                    PlayerTutorialConfirms[i].M_PlayerState = PlayerTutorialConfirm.PlayerState.Ready;
                }
                else if (PlayerTutorialConfirms[i].M_PlayerState == PlayerTutorialConfirm.PlayerState.Ready)
                {
                    PlayerTutorialConfirms[i].M_PlayerState = PlayerTutorialConfirm.PlayerState.Waiting;
                }
            }
        }

        bool canStartGame = true;

        for (int i = 0; i < 4; i++)
        {
            canStartGame &= PlayerTutorialConfirms[i].M_PlayerState == PlayerTutorialConfirm.PlayerState.Ready;
        }

        if (canStartGame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        IsGameStart = true;
        M_StateMachine.SetState(StateMachine.States.Hide);
        GameBoardManager.Instance.GenerateMap("LevelTest");
        GameBoardManager.Instance.M_StateMachine.SetState(GameBoardManager.StateMachine.States.Show);
        BattleScorePanelManager.Instance.M_StateMachine.SetState(BattleScorePanelManager.StateMachine.States.Show);
        GameBoardManager.Instance.StartGame();
    }
}