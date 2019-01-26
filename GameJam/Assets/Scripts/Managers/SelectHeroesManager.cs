using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHeroesManager : MonoSingleton<SelectHeroesManager>
{
    private SelectHeroesManager()
    {
    }

    void Awake()
    {
        M_StateMachine = new StateMachine();
    }

    [SerializeField] private Canvas SelectHeroesCanvas;
    [SerializeField] private Image BG;

    [SerializeField] private HeroButton[] HeroButtons;
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
            Instance.SelectHeroesCanvas.enabled = true;
        }

        private void HideMenu()
        {
            Instance.SelectHeroesCanvas.enabled = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            string Index_name = "P" + (i + 1) + "_";
            float hor = Input.GetAxisRaw(Index_name + "hor");
            if (hor == -1)
            {
                SelectHeroForPlayer((Players) i, false);
            }

            if (hor == 1)
            {
                SelectHeroForPlayer((Players) i, true);
            }
        }
    }

    void SelectHeroForPlayer(Players player, bool upScroll)
    {
        HeroButtons[(int) player].CurrentRobotIndex1++;
    }
}