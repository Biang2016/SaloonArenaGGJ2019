using System.Linq;
using UnityEngine;

public partial class GameBoardManager : MonoSingleton<GameBoardManager>
{
    public static int GameBoardWidth;
    public static int GameBoardHeight;
    [SerializeField] private Transform MapContainer;

    private GameBoardManager()
    {
    }

    void Awake()
    {
    }

    void Start()
    {
    }

    public void GenerateMap(string levelName)
    {
        LevelMap levelMap = LevelMapManager.Instance.LevelMaps[levelName];
        for (int i = 0; i < levelMap.LevelMapIndices.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.LevelMapIndices.GetLength(1); j++)
            {
//                LevelMapBlock.Instantiate()
            }
        }
    }
}