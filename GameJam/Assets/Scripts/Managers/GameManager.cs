using UnityEngine;

public partial class GameManager : MonoSingleton<GameManager>
{
    private GameManager()
    {
    }

    void Awake()
    {
        Awake_Xue();
        Awake_Li();
    }

    void Start()
    {
        Start_Xue();
        Start_Li();
    }

    public Camera BattleGroundCamera;

    #region 游戏全局参数

    public float GarbageBulletBeLitterSpeedThreshold = 80f;

    #endregion
}