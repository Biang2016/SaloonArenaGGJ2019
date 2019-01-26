using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameBoardManager : MonoSingleton<GameBoardManager>
{
    public int GameBoardWidth = 57;
    public int GameBoardHeight = 30;
    [SerializeField] private RectTransform MapContainer;

    private GameBoardManager()
    {
    }

    void Awake()
    {
    }

    void Start()
    {
    }

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
    }
}