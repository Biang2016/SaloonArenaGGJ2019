using UnityEngine;

public abstract class LevelMapBlock : PoolObject
{
    protected float BlockWidth;
    protected float BlockHeight;

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
            case AllLevelMapColors.MapBlockType.Player1Slot:
            {
                block = GameObjectPoolManager.Instance.Pool_PlayerSlotPools[0].AllocateGameObject<PlayerSlotBlock>(parent);
                ((PlayerSlotBlock) block).Initialize(Players.Player1);
                break;
            }
            case AllLevelMapColors.MapBlockType.Player2Slot:
            {
                block = GameObjectPoolManager.Instance.Pool_PlayerSlotPools[1].AllocateGameObject<PlayerSlotBlock>(parent);
                ((PlayerSlotBlock) block).Initialize(Players.Player2);
                break;
            }
            case AllLevelMapColors.MapBlockType.Player3Slot:
            {
                block = GameObjectPoolManager.Instance.Pool_PlayerSlotPools[2].AllocateGameObject<PlayerSlotBlock>(parent);
                ((PlayerSlotBlock) block).Initialize(Players.Player3);
                break;
            }
            case AllLevelMapColors.MapBlockType.Player4Slot:
            {
                block = GameObjectPoolManager.Instance.Pool_PlayerSlotPools[3].AllocateGameObject<PlayerSlotBlock>(parent);
                ((PlayerSlotBlock) block).Initialize(Players.Player4);
                break;
            }
        }

        block.BlockWidth = parent.rect.width / GameBoardManager.Instance.GameBoardWidth;
        block.BlockHeight = parent.rect.height / GameBoardManager.Instance.GameBoardHeight;
        Rect rect = ((RectTransform) block.transform).rect;
        ((RectTransform) block.transform).sizeDelta = new Vector2(block.BlockWidth, block.BlockHeight);
        float x = -(float) (GameBoardManager.Instance.GameBoardWidth - 1) / 2 * block.BlockWidth + block.BlockWidth * pos_X;
        float y = -(float) (GameBoardManager.Instance.GameBoardHeight - 1) / 2 * block.BlockHeight + block.BlockHeight * pos_Y;
        block.transform.localPosition = new Vector2(x, y);
        block.Init();
        return block;
    }

    protected abstract void Init();
}