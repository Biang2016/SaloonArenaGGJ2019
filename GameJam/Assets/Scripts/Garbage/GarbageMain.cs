using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GarbageMain : PoolObject
{
    private bool can_Pick;
    public int num_;
    public CircleCollider2D circle;
    public Rigidbody2D Rigidbody2D;
    public Image Image;
    public SpriteAtlas GarbageSpriteAtlas;
    public static Sprite[] Sprites;
    float time = 0;

    public override void PoolRecycle()
    {
        base.PoolRecycle();
        Rigidbody2D.drag = GameManager.Instance.AmmoDrag;
    }

    private void Awake()
    {
        Sprites = new Sprite[GarbageSpriteAtlas.spriteCount];
        GarbageSpriteAtlas.GetSprites(Sprites);
        circle.isTrigger = true;
    }

    void Start()
    {
    }

    void Update()
    {
        if (Rigidbody2D.velocity.magnitude < GameManager.Instance.GarbageBulletBeLitterSpeedThreshold)
        {
            Rigidbody2D.drag = 100000;
            SoundPlay("sfx/ShootMiss", 0.3f);
        }
    }

    public void Initialize()
    {
        int index = Random.Range(0, Sprites.Length);
        Image.sprite = Sprites[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBody>().Pick_Garbage(num_);
            PoolRecycle();
        }
    }
}