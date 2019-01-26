using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GarbageMain : PoolObject
{
    private bool can_Pick;

    public bool CanPick
    {
        get { return can_Pick; }
        set
        {
            can_Pick = value;
            circle.isTrigger = can_Pick;
        }
    }

    public int num_;
    public int damage;
    public CircleCollider2D circle;
    public Rigidbody2D Rigidbody2D;
    public Image Image;
    public SpriteAtlas GarbageSpriteAtlas;
    public static Sprite[] Sprites;

    public override void PoolRecycle()
    {
        base.PoolRecycle();
        CanPick = false;
        Rigidbody2D.drag = 0.2f;
    }

    private void Awake()
    {
        Sprites = new Sprite[GarbageSpriteAtlas.spriteCount];
        GarbageSpriteAtlas.GetSprites(Sprites);
        CanPick = false;
    }

    void Start()
    {
    }

    void Update()
    {
        if (!CanPick && Rigidbody2D.velocity.magnitude < GameManager.Instance.GarbageBulletBeLitterSpeedThreshold)
        {
            Rigidbody2D.drag = 100000;
            CanPick = true;
            SoundPlay("sfx/ShootMiss", 0.3f);
        }
    }

    public void Initialize()
    {
        int index = Random.Range(0, Sprites.Length);
        Image.sprite = Sprites[index];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerBody>().Hitted(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerBody>().Pick_Garbage(num_);
    }

    public float time;
    public int Damage;
}