using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleScorePanelManager : MonoSingleton<BattleScorePanelManager>
{
    private BattleScorePanelManager()
    {
    }

    void Awake()
    {
        M_StateMachine = new StateMachine();
    }

    [SerializeField] private Canvas BatlleScoreCanvas;
    [SerializeField] private Image[] PlayerIcons;
    [SerializeField] private Sprite[] PlayerIconSprites;
    [SerializeField] private Text[] PlayerScoreTexts;
    public Text TimeText;

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
            Instance.BatlleScoreCanvas.enabled = true;
        }

        private void HideMenu()
        {
            Instance.BatlleScoreCanvas.enabled = false;
        }
    }

    void Start()
    {
    }

    public int _score_Player1 = 0;

    public int ScorePlayer1
    {
        get { return _score_Player1; }
        set
        {
            if (value != _score_Player1)
            {
                _score_Player1 = value;
                PlayerScoreTexts[0].text = value.ToString();
                OnChangePlayerScore();
            }
        }
    }

    public int _score_Player2 = 0;

    public int ScorePlayer2
    {
        get { return _score_Player2; }
        set
        {
            if (value != _score_Player2)
            {
                _score_Player2 = value;
                PlayerScoreTexts[1].text = value.ToString();
                OnChangePlayerScore();
            }
        }
    }

    public int _score_Player3 = 0;

    public int ScorePlayer3
    {
        get { return _score_Player3; }
        set
        {
            if (value != _score_Player3)
            {
                _score_Player3 = value;
                PlayerScoreTexts[2].text = value.ToString();
                OnChangePlayerScore();
            }
        }
    }

    public int _score_Player4 = 0;

    public int ScorePlayer4
    {
        get { return _score_Player4; }
        set
        {
            if (value != _score_Player4)
            {
                _score_Player4 = value;
                PlayerScoreTexts[3].text = value.ToString();
                OnChangePlayerScore();
            }
        }
    }

    public void OnChangePlayerScore()
    {
        Dictionary<Players, int> PlayerScores = new Dictionary<Players, int>();
        PlayerScores.Add(Players.Player1, ScorePlayer1);
        PlayerScores.Add(Players.Player2, ScorePlayer2);
        PlayerScores.Add(Players.Player3, ScorePlayer3);
        PlayerScores.Add(Players.Player4, ScorePlayer4);
        IOrderedEnumerable<KeyValuePair<Players, int>> dicSort = from objDic in PlayerScores orderby objDic.Value descending select objDic;
        int index = 0;
        foreach (KeyValuePair<Players, int> kv in dicSort)
        {
            PlayerIcons[(int) kv.Key].sprite = PlayerIconSprites[index];
            index++;
        }
    }
}