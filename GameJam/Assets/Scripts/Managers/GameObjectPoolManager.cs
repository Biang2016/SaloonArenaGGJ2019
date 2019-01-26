public class GameObjectPoolManager : MonoSingleton<GameObjectPoolManager>
{
    private GameObjectPoolManager()
    {
    }

    public GameObjectPool Pool_WallBlockPool;
    public PoolObject WallBlocksPrefab;
    public GameObjectPool Pool_FloorBlockPool;
    public PoolObject FloorBlocksPrefab;
    public GameObjectPool Pool_InsideBlockPool;
    public PoolObject InsideBlockPrefab;

    public GameObjectPool[] Pool_PlayerSlotPools;
    public PoolObject[] PlayerSlotPrefabs;

    public GameObjectPool Pool_Garbage;
    public PoolObject GarbagePrefab;
    public GameObjectPool Pool_HeroButton;
    public PoolObject HeroButtonPrefab;

    public GameObjectPool Pool_GarbageLitter;
    public PoolObject GarbageLitterPrefabs;

    void Awake()
    {
        Pool_WallBlockPool.Initiate(WallBlocksPrefab, 100);
        Pool_FloorBlockPool.Initiate(FloorBlocksPrefab, 100);
        Pool_InsideBlockPool.Initiate(InsideBlockPrefab, 100);
        for (int i = 0; i < Pool_PlayerSlotPools.Length; i++)
        {
            Pool_PlayerSlotPools[i].Initiate(PlayerSlotPrefabs[i], 1);
        }

        Pool_HeroButton.Initiate(HeroButtonPrefab, 4);
        Pool_Garbage.Initiate(GarbagePrefab, 300);
        Pool_GarbageLitter.Initiate(GarbageLitterPrefabs, 300);
    }
}