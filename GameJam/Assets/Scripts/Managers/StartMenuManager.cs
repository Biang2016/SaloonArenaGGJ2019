using System.Collections.Generic;
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
                        AudioManager.Instance.BGMLoop("bgm/StartMenu");
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

    void Update()
    {
        if (M_StateMachine.GetState() == StateMachine.States.Hide) return;
        if (Input.anyKeyDown)
        {
            M_StateMachine.SetState(StateMachine.States.Hide);
            SelectHeroesManager.Instance.M_StateMachine.SetState(SelectHeroesManager.StateMachine.States.Show);
        }
    }

    public void Reset()
    {
        M_StateMachine.SetState(StateMachine.States.Hide);
    }

    public void Reset_1()
    {
        M_StateMachine.SetState(StateMachine.States.Show);
    }
}