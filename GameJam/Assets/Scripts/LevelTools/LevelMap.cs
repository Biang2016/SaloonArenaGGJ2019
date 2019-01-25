public class LevelMap
{
    public string LevelName;
    public int[,] LevelMapIndices;

    public LevelMap(string levelName, int[,] levelMapIndices)
    {
        LevelName = levelName;
        LevelMapIndices = levelMapIndices;
    }
}