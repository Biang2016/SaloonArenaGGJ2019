public class GameObjectPoolManager : MonoSingleton<GameObjectPoolManager>
{
    private GameObjectPoolManager()
    {
    }

    public GameObjectPool Pool_BlocksPool;
    public PoolObject BlocksPrefab;

    


    void Awake()
    {
        Pool_BlocksPool.Initiate(BlocksPrefab, 200);
    }
}