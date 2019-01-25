using UnityEngine;

public partial class GameManager : MonoSingleton<GameManager>
{
    private GameManager()
    {
    }

    void Awake()
    {
        Awake_Xue();
        Start_Xue();
    }

    public Camera BattleGroundCamera;

    #region 游戏全局参数

    

    #endregion
}