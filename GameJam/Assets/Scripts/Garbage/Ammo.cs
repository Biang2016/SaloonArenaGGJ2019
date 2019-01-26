using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Ammo : PoolObject
{
    public override void PoolRecycle()
    {
        base.PoolRecycle();Initialize();
    }
    public int player;
    public int damage;
    public Rigidbody2D Rigidbody2D;
    public Image Image;
    public Sprite[] Sprites;
    public GameObject garbage;
    public RectTransform transform;

    private void Awake()
    {
        //transform.localScale *= GameManager.Instance.AmmoScale;
        damage = GameManager.Instance.AmmoDamage;
        Image.sprite = Sprites[Random.Range(0, 3)];
    }
    public void Initialize()
    {
        damage = GameManager.Instance.AmmoDamage;
        transform.localScale *= GameManager.Instance.AmmoScale;
        Image.sprite = Sprites[Random.Range(0, 3)];
    }
    void Start()
    {
        Rigidbody2D.mass = GameManager.Instance.AmmoMass;
        Rigidbody2D.drag = GameManager.Instance.AmmoDrag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && player != (int) collision.gameObject.GetComponent<PlayerBody>().WhichPlayer && !collision.gameObject.GetComponent<PlayerBody>().Lying)
        {
            collision.gameObject.GetComponent<PlayerBody>().Hitted(damage);
            /*int temp = (int)Random.Range(6, 9);
            collision.gameObject.GetComponent<PlayerBody>().Loss_Garbage(temp);
            */
            Vector2 vector2 = new Vector2(-transform.up.x, -transform.up.y);
            Drop(vector2);
            SoundPlay("sfx/ShootHit");
            Rigidbody2D.velocity = Vector3.zero;
            PoolRecycle();
        }

        if (collision.transform.CompareTag("Wall"))
        {
            Vector2 vector2 = Vector2.zero;
            switch (collision.name)
            {
                case "Wall_left":vector2.x = 1;break;
                case "Wall_right":vector2.x = -1;break;
                case "Wall_up":vector2.y = -1;break;
                case "Wall_down":vector2.y = 1;break;
            default:break;
            }
            Drop(vector2);
            Rigidbody2D.velocity = Vector3.zero;
            PoolRecycle();
        }
    }

    void Drop(Vector2 vector2)
    {
        GarbageMain am = GameObjectPoolManager.Instance.Pool_Garbage.AllocateGameObject<GarbageMain>(GameBoardManager.Instance.GameBoardGarbagesCanvas.transform);
        am.Initialize();
        am.CanPick = true;
        am.transform.position = transform.position;       
        am.Rigidbody2D.velocity = vector2.normalized * Random.Range(100,200);
        
    }
}
