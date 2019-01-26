using UnityEngine;

public abstract class LevelMapBlock : PoolObject
{
    public static LevelMapBlock InitializeBlock(AllLevelMapColors.MapBlockType blockType, RectTransform parent, int pos_X, int pos_Y)
    {
        LevelMapBlock block = null;
        switch (blockType)
        {
            case AllLevelMapColors.MapBlockType.Wall:
            {
                block = GameObjectPoolManager.Instance.Pool_WallBlockPool.AllocateGameObject<WallBlock>(parent);
                break;
            }
            case AllLevelMapColors.MapBlockType.Floor:
            {
                block = GameObjectPoolManager.Instance.Pool_FloorBlockPool.AllocateGameObject<FloorBlock>(parent);
                break;
            }
            case AllLevelMapColors.MapBlockType.InsideBlock:
            {
                block = GameObjectPoolManager.Instance.Pool_InsideBlockPool.AllocateGameObject<InsideBlock>(parent);
                break;
            }
        }

        float blockWidth = parent.rect.width / GameBoardManager.Instance.GameBoardWidth;
        float blockHeight = parent.rect.height / GameBoardManager.Instance.GameBoardHeight;
        Rect rect = ((RectTransform) block.transform).rect;
        ((RectTransform) block.transform).sizeDelta = new Vector2(blockWidth, blockHeight);
        float x = -(float) (GameBoardManager.Instance.GameBoardWidth - 1) / 2 * blockWidth + blockWidth * pos_X;
        float y = -(float) (GameBoardManager.Instance.GameBoardHeight - 1) / 2 * blockHeight + blockHeight * pos_Y;
        block.transform.localPosition = new Vector2(x, y);
        return block;
    }
}