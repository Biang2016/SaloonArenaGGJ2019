using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameBoardManager : MonoSingleton<GameBoardManager>
{
    public int GameBoardWidth = 57;
    public int GameBoardHeight = 30;
    [SerializeField] private RectTransform MapContainer;

    public PlayerBody[] Players;

    private float leftTime;

    public float LeftTime
    {
        get { return leftTime; }
        set
        {
            leftTime = value;
            Min = Mathf.CeilToInt(Mathf.CeilToInt(leftTime) / 60);
            Second = Mathf.CeilToInt(Mathf.CeilToInt(leftTime) - Min * 60);

            if (Min == 0 && Second == 0)
            {
                EndGame();
            }
        }
    }

    private int min;

    public int Min
    {
        get { return min; }
        set
        {
            if (value != min)
            {
                BattleScorePanelManager.Instance.TimeText.text = Min.ToString() + ":" + Second.ToString();
                min = value;
            }
        }
    }

    private int second;

    public int Second
    {
        get { return second; }
        set
        {
            if (value != second)
            {
                BattleScorePanelManager.Instance.TimeText.text = Min.ToString() + ":" + Second.ToString();
                second = value;
            }
        }
    }

    private GameBoardManager()
    {
    }

    void Awake()
    {
        M_StateMachine = new StateMachine();
    }

    void Start()
    {
    }

    [SerializeField] private Canvas GameBoardCanvas;
    public Canvas GameBoardMovingThingsCanvas;
    public Canvas GameBoardGarbagesCanvas;

    public List<GarbageMain> Garbages = new List<GarbageMain>();

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
            Instance.GameBoardCanvas.enabled = true;
            Instance.GameBoardMovingThingsCanvas.enabled = true;
            Instance.GameBoardGarbagesCanvas.enabled = true;
        }

        private void HideMenu()
        {
            Instance.GameBoardCanvas.enabled = false;
            Instance.GameBoardMovingThingsCanvas.enabled = false;
            Instance.GameBoardGarbagesCanvas.enabled = false;
        }
    }

    private bool isInGame = false;

    void Update()
    {
        if (!isInGame)
        {
            return;
        }

        LeftTime -= Time.deltaTime;
    }
    /*
    public LevelMapBlock[,] LevelMapBlocks;

    public void GenerateMap(string levelName)
    {
        LevelMapBlocks = new LevelMapBlock[GameBoardWidth, GameBoardHeight];
        foreach (LevelMapBlock block in LevelMapBlocks)
        {
            if (block)
            {
                block.PoolRecycle();
            }
        }

        LevelMap levelMap = LevelMapManager.Instance.LevelMaps[levelName];
        for (int i = 0; i < levelMap.LevelMapIndices.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.LevelMapIndices.GetLength(1); j++)
            {
                LevelMapBlock block = LevelMapBlock.InitializeBlock((AllLevelMapColors.MapBlockType) levelMap.LevelMapIndices[i, j], MapContainer, i, j);
                LevelMapBlocks[i, j] = block;
            }
        }

        GenerateStarterGarbages();
    }*/

    public void GenerateStarterGarbages()
    {
        for (int i = 0; i < GameManager.Instance.StarterFloorGarbage; i++)
        {
            GarbageMain gm = GameObjectPoolManager.Instance.Pool_Garbage.AllocateGameObject<GarbageMain>(GameBoardGarbagesCanvas.transform);
            gm.Initialize();
            gm.transform.position = new Vector2(Random.Range(-MapContainer.rect.width / 2, MapContainer.rect.width / 2), Random.Range(-MapContainer.rect.height / 2, MapContainer.rect.height / 2));
            Garbages.Add(gm);
        }
    }

    public void InitializePlayers()
    {
        foreach (PlayerBody player in Players)
        {
            player.gameObject.SetActive(false);
        }

        foreach (KeyValuePair<Players, Robots> kv in TutorialMenuManager.Instance.PlayerSelection)
        {
            Players[(int) kv.Key].gameObject.SetActive(true);
            Players[(int) kv.Key].WhichRobot = kv.Value;
            Players[(int) kv.Key].Initialize();
        }
    }

    public void StartGame()
    {
        isInGame = true;
        LeftTime = GameManager.Instance.LevelTime;
        BattleScorePanelManager.Instance.TimeText.text = "1:00";
    }

    public void EndGame()
    {
        isInGame = false;
        ScoreMenuManager.Instance.M_StateMachine.SetState(ScoreMenuManager.StateMachine.States.Show);
        ScoreMenuManager.Instance.Initialize(BattleScorePanelManager.Instance.PlayerScoreRank);
        M_StateMachine.SetState(StateMachine.States.Hide);
        BattleScorePanelManager.Instance.M_StateMachine.SetState(BattleScorePanelManager.StateMachine.States.Hide);
    }

    public void Reset()
    {
        foreach (GarbageMain gb in Garbages)
        {
            gb.PoolRecycle();
        }

        Garbages.Clear();
        GenerateStarterGarbages();
        M_StateMachine.SetState(StateMachine.States.Hide);
    }
}