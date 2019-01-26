using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotBlock : LevelMapBlock
{
    [SerializeField] private Image PlayerSlotImage;
    [SerializeField] private Sprite[] PlayerSlotSprites;
    public Players Player;

    public void Initialize(Players player)
    {
        PlayerSlotImage.sprite = PlayerSlotSprites[(int) player];
    }

    protected override void Init()
    {
    }
}