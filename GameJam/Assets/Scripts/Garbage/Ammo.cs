using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Ammo : PoolObject
{
    public int player;
    public int damage;
    public Rigidbody2D Rigidbody2D;
    public Image Image;
    public Sprite[] Sprites;
    public GameObject garbage;

    private void Awake()
    {
        Image.sprite = Sprites[Random.Range(0, 3)];
    }

    void Start()
    {
        Rigidbody2D.mass = GameManager.Instance.ammoMass;
        Rigidbody2D.drag = GameManager.Instance.ammoDrag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && player != (int) collision.gameObject.GetComponent<PlayerBody>().WhichPlayer)
        {
            collision.gameObject.GetComponent<PlayerBody>().Hitted(damage);
            SoundPlay("sfx/ShootHit");
            Rigidbody2D.velocity = Vector3.zero;
            PoolRecycle();
        }

        if (collision.transform.CompareTag("Wall"))
        {
            Rigidbody2D.velocity = Vector3.zero;
            PoolRecycle();
        }
    }

    void Drop()
    {
        GarbageMain am = GameObjectPoolManager.Instance.Pool_GarbageLitter.AllocateGameObject<GarbageMain>(GameBoardManager.Instance.GameBoardGarbagesCanvas.transform);
        Instantiate(garbage, transform).GetComponent<Ammo>();
        Vector3 temp = new Vector3(0, 0, Random.Range(0, 360));
        //am.CanPick = false;
        am.CanPick = false;
        am.transform.position = transform.position;
        am.transform.rotation = transform.rotation;
        am.Rigidbody2D.velocity = temp * Random.Range(500, 800);
    }
}