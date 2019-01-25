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

    void Awake()
    {
        Pool_WallBlockPool.Initiate(WallBlocksPrefab, 100);
        Pool_FloorBlockPool.Initiate(FloorBlocksPrefab, 100);
        Pool_InsideBlockPool.Initiate(InsideBlockPrefab, 100);
    }
}