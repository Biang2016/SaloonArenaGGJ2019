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

    void Awake()
    {
        Pool_WallBlockPool.Initiate(WallBlocksPrefab, 100);
        Pool_FloorBlockPool.Initiate(FloorBlocksPrefab, 100);
        Pool_InsideBlockPool.Initiate(InsideBlockPrefab, 100);
        for (int i = 0; i < Pool_PlayerSlotPools.Length; i++)
        {
            Pool_PlayerSlotPools[i].Initiate(PlayerSlotPrefabs[i], 1);
        }
    }
}