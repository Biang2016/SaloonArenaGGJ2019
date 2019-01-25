using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class InsideBlock : LevelMapBlock
{
    public static void InitializeBlock(AllLevelMapColors.MapBlockType blockType, Transform parent, int pos_X, int pos_Y)
    {
        switch (blockType)
        {
            case AllLevelMapColors.MapBlockType.Wall:
            {
                GameObjectPoolManager.Instance.Pool_WallBlockPool.AllocateGameObject<WallBlock>(parent);
                break;
            }
            case AllLevelMapColors.MapBlockType.Floor:
            {
                GameObjectPoolManager.Instance.Pool_FloorBlockPool.AllocateGameObject<FloorBlock>(parent);
                break;
            }
            case AllLevelMapColors.MapBlockType.InsideBlock:
            {
                GameObjectPoolManager.Instance.Pool_InsideBlockPool.AllocateGameObject<InsideBlock>(parent);
                break;
            }
        }
    }
}