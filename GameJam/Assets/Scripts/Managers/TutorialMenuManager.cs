using System.Collections;
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

    public void Initialize()
    {
        foreach (PlayerTutorialConfirm ptc in PlayerTutorialConfirms)
        {
            ptc.Initialize(Players.NoPlayer);
        }

        foreach (Players player in PlayerSelection.Keys)
        {
            PlayerTutorialConfirms[(int) player].Initialize(player);
        }
    }

    internal bool IsGameStart = false;
    public PlayerTutorialConfirm[] PlayerTutorialConfirms = new PlayerTutorialConfirm[4];

    public Dictionary<Players, Robots> PlayerSelection = new Dictionary<Players, Robots>();

    void Update()
    {
        if (M_StateMachine.GetState() == StateMachine.States.Hide) return;
        if (IsGameStart) return;

        foreach (Players player in PlayerSelection.Keys)
        {
            string Index_name = "P" + ((int) player + 1) + "_";
            if (Input.GetButtonDown(Index_name + "fire"))
            {
                if (PlayerTutorialConfirms[(int) player].M_PlayerState == PlayerTutorialConfirm.PlayerState.Waiting)
                {
                    AudioManager.Instance.SoundPlay("sfx/Select", 0.2f);
                    PlayerTutorialConfirms[(int) player].M_PlayerState = PlayerTutorialConfirm.PlayerState.Ready;
                }
            }
        }

        bool canStartGame = true;

        foreach (Players player in PlayerSelection.Keys)
        {
            canStartGame &= PlayerTutorialConfirms[(int) player].M_PlayerState == PlayerTutorialConfirm.PlayerState.Ready;
        }

        if (canStartGame)
        {
            Co_StartGame();
        }
    }

    void Co_StartGame()
    {
        IsGameStart = true;
        M_StateMachine.SetState(StateMachine.States.Hide);
        GameBoardManager.Instance.M_StateMachine.SetState(GameBoardManager.StateMachine.States.Show);
        BattleScorePanelManager.Instance.M_StateMachine.SetState(BattleScorePanelManager.StateMachine.States.Show);
        GameBoardManager.Instance.InitializePlayers();
        GameBoardManager.Instance.StartGame();
    }

    public void Reset()
    {
        IsGameStart = false;
        PlayerSelection.Clear();
        Initialize();
        M_StateMachine.SetState(StateMachine.States.Hide);
    }
}