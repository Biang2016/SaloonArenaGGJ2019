using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class InsideBlock : LevelMapBlock
{
    [SerializeField] private BoxCollider2D BoxCollider2D;

    protected override void Init()
    {
        BoxCollider2D.size = new Vector2(BlockWidth, BlockHeight);
    }
}