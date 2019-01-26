using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class StartMenuManager : MonoSingleton<StartMenuManager>
{
    private StartMenuManager()
    {
    }

    void Awake()
    {
        M_StateMachine = new StateMachine();
    }

    [SerializeField] private Canvas StartMenuCanvas;

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
                        AudioManager.Instance.BGMFadeIn("bgm/StartMenu_0");
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
            Instance.StartMenuCanvas.enabled = true;
        }

        private void HideMenu()
        {
            Instance.StartMenuCanvas.enabled = false;
        }
    }

    public void OnStartGameButtonClick()
    {
        GameBoardManager.Instance.GenerateMap("LevelTest");
        M_StateMachine.SetState(StateMachine.States.Hide);
        GameBoardManager.Instance.M_StateMachine.SetState(GameBoardManager.StateMachine.States.Show);
    }

    public void OnQuitGameButtonClick()
    {
        Application.Quit();
    }
}