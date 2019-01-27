using System.Collections;
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
                        AudioManager.Instance.BGMFadeIn("bgm/SelectHero_2");
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

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            HeroButton hb = GameObjectPoolManager.Instance.Pool_HeroButton.AllocateGameObject<HeroButton>(BG.transform);
            hb.Initialize(Players.NoPlayer, (Robots) i);
            HeroButtons[i] = hb;
            HeroButtons[i].JoinKeyText.text = keys.ToCharArray()[i].ToString();
        }
    }

    private string keys = "c,n/";

    internal float SelectButtonPressCD = 0.3f;
    internal float[] SelectButtonPressTicks = new float[4] {1f, 1f, 1f, 1f};
    internal bool IsGameStart = false;

    void Update()
    {
        if (M_StateMachine.GetState() == StateMachine.States.Hide) return;
        if (IsGameStart) return;
        if (Input.GetKeyUp(KeyCode.F6))
        {
            int countPlayer = 0;
            bool isPlayerNoReady = false;
            for (int i = 0; i < 4; i++)
            {
                if (HeroButtons[i].M_PlayerState == HeroButton.PlayerState.Ready)
                {
                    countPlayer++;
                }

                if (HeroButtons[i].M_PlayerState == HeroButton.PlayerState.Waiting)
                {
                    isPlayerNoReady = true;
                }
            }

            if (isPlayerNoReady)
            {
                NoticeManager.Instance.ShowInfoPanelCenter("还有玩家没有准备呀", 0f, 1f);
                return;
            }

            if (countPlayer >= 2)
            {
                StartCoroutine(Co_StartTutorial());
                return;
            }
            else
            {
                NoticeManager.Instance.ShowInfoPanelCenter("需要两名以上玩家才能开始游戏", 0f, 1f);
                return;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (HeroButtons[i].M_PlayerState == HeroButton.PlayerState.Waiting)
            {
                SelectButtonPressTicks[i] += Time.deltaTime;
                string Index_name = "P" + (i + 1) + "_";
                float hor = Input.GetAxisRaw(Index_name + "hor");

                if (hor == -1)
                {
                    if (SelectButtonPressTicks[i] > SelectButtonPressCD)
                    {
                        SelectButtonPressTicks[i] = 0;
                        SelectHeroForPlayer((Players) i, false);
                    }
                }
                else if (hor == 1)
                {
                    if (SelectButtonPressTicks[i] > SelectButtonPressCD)
                    {
                        SelectButtonPressTicks[i] = 0;
                        SelectHeroForPlayer((Players) i, true);
                    }
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            string Index_name = "P" + (i + 1) + "_";
            if (Input.GetButtonDown(Index_name + "fire"))
            {
                if (HeroButtons[i].M_PlayerState == HeroButton.PlayerState.None)
                {
                    HeroButtons[i].M_PlayerState = HeroButton.PlayerState.Waiting;
                    HeroButtons[i].Initialize((Players) i, (Robots) i);
                }
                else if (HeroButtons[i].M_PlayerState == HeroButton.PlayerState.Waiting)
                {
                    AudioManager.Instance.SoundPlay("sfx/Select", 0.2f);
                    HeroButtons[i].M_PlayerState = HeroButton.PlayerState.Ready;
                }
                else if (HeroButtons[i].M_PlayerState == HeroButton.PlayerState.Ready)
                {
                    AudioManager.Instance.SoundPlay("sfx/ShootHit", 0.6f);
                    HeroButtons[i].M_PlayerState = HeroButton.PlayerState.Waiting;
                }
            }
        }

        bool canStartGame = true;

        for (int i = 0; i < 4; i++)
        {
            canStartGame &= HeroButtons[i].M_PlayerState == HeroButton.PlayerState.Ready;
        }

        if (canStartGame)
        {
            StartCoroutine(Co_StartTutorial());
        }
    }

    void SelectHeroForPlayer(Players player, bool upScroll)
    {
        if (upScroll)
        {
            HeroButtons[(int) player].CurrentRobotIndex++;
        }
        else
        {
            HeroButtons[(int) player].CurrentRobotIndex--;
        }

        AudioManager.Instance.SoundPlay("sfx/SelectSwitch_0");
    }

    IEnumerator Co_StartTutorial()
    {
        yield return new WaitForSeconds(1f);
        IsGameStart = true;
        M_StateMachine.SetState(StateMachine.States.Hide);
        TutorialMenuManager.Instance.M_StateMachine.SetState(TutorialMenuManager.StateMachine.States.Show);
        TutorialMenuManager.Instance.PlayerSelection.Clear();
        foreach (HeroButton bh in HeroButtons)
        {
            if (bh.M_PlayerState == HeroButton.PlayerState.Ready)
            {
                if (!TutorialMenuManager.Instance.PlayerSelection.ContainsKey(bh.Player))
                {
                    TutorialMenuManager.Instance.PlayerSelection.Add(bh.Player, (Robots) bh.CurrentRobotIndex);
                }
            }
        }

        TutorialMenuManager.Instance.Initialize();

        yield return null;
    }

    public void Reset()
    {
        M_StateMachine.SetState(StateMachine.States.Show);
        int index = 0;
        foreach (HeroButton heroButton in HeroButtons)
        {
            heroButton.Initialize(Players.NoPlayer, (Robots) index);
            index++;
        }

        for (int i = 0; i < 4; i++)
        {
            SelectButtonPressTicks[i] = 0;
        }

        IsGameStart = false;
    }
}