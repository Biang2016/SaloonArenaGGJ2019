using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenuManager : MonoSingleton<ScoreMenuManager>
{
    private ScoreMenuManager()
    {
    }

    void Awake()
    {
        M_StateMachine = new StateMachine();
    }

    [SerializeField] private Canvas ScoreMenuCanvas;
    [SerializeField] private Transform ScoreMenuPanel;

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
                        AudioManager.Instance.BGMLoop("bgm/Result");
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
            Instance.ScoreMenuCanvas.enabled = true;
        }

        private void HideMenu()
        {
            Instance.ScoreMenuCanvas.enabled = false;
        }
    }

    public List<ScoreBar> ScoreBars = new List<ScoreBar>();

    public void Initialize(IOrderedEnumerable<KeyValuePair<Players, int>> playerRankScores)
    {
        int maxScore = 0;
        foreach (KeyValuePair<Players, int> kv in playerRankScores)
        {
            maxScore = kv.Value;
            break;
        }

        int i = 0;
        SortedDictionary<Players, ScoreBar> sbs = new SortedDictionary<Players, ScoreBar>();
        foreach (KeyValuePair<Players, int> kv in playerRankScores)
        {
            ScoreBar sb = GameObjectPoolManager.Instance.Pool_PlayerScoreBar.AllocateGameObject<ScoreBar>(ScoreMenuPanel);
            sb.Initialize(kv.Key, kv.Value, maxScore, i);
            sbs.Add(kv.Key, sb);
            ScoreBars.Add(sb);
            i++;
        }

        foreach (KeyValuePair<Players, ScoreBar> kv in sbs)
        {
            kv.Value.transform.SetAsLastSibling();
        }
    }

    void Update()
    {
        if (M_StateMachine.GetState() == StateMachine.States.Hide) return;
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameManager.Instance.RestartGame();
        }
    }

    public void Reset()
    {
        M_StateMachine.SetState(StateMachine.States.Hide);
        foreach (ScoreBar scoreBar in ScoreBars)
        {
            scoreBar.PoolRecycle();
        }

        ScoreBars.Clear();
    }
}