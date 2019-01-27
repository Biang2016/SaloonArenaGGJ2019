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
                OnChangePlayerScore();
            }
        }
    }

    public IOrderedEnumerable<KeyValuePair<Players, int>> PlayerScoreRank;

    public void OnChangePlayerScore()
    {
        Dictionary<Players, int> PlayerScores = new Dictionary<Players, int>();
        foreach (KeyValuePair<Players, Robots> kv in TutorialMenuManager.Instance.PlayerSelection)
        {
            switch (kv.Key)
            {
                case Players.Player1:
                {
                    PlayerScores.Add(kv.Key, ScorePlayer1);
                    break;
                }
                case Players.Player2:
                {
                    PlayerScores.Add(kv.Key, ScorePlayer2);
                    break;
                }
                case Players.Player3:
                {
                    PlayerScores.Add(kv.Key, ScorePlayer3);
                    break;
                }
                case Players.Player4:
                {
                    PlayerScores.Add(kv.Key, ScorePlayer4);
                    break;
                }
            }
        }

        PlayerScoreRank = from objDic in PlayerScores orderby objDic.Value descending select objDic;
        List<int> playerIndices = new List<int>();
        foreach (KeyValuePair<Players, int> kv in PlayerScoreRank)
        {
            playerIndices.Add((int) kv.Key);
        }

        int index = 0;
        Players firstPlayer = Players.Player1;
        foreach (KeyValuePair<Players, int> kv in PlayerScoreRank)
        {
            if (index == 0) firstPlayer = kv.Key;
            PlayerIcons[index].sprite = PlayerIconSprites[playerIndices[index]];
            PlayerScoreTexts[index].text = kv.Value.ToString();
            index++;
        }

        for (int i = 0; i < 4; i++)
        {
            GameBoardManager.Instance.Players[i].CrownImage.enabled = i == (int) firstPlayer;
        }
    }

    public void Reset()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;
        ScorePlayer3 = 0;
        ScorePlayer4 = 0;
        OnChangePlayerScore();
        M_StateMachine.SetState(StateMachine.States.Hide);
    }
}