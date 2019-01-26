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
        Image.sprite = Sprites[Random.Range(0,3)];


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && player != (int)collision.gameObject.GetComponent<PlayerBody>().WhichPlayer)
        {
            collision.gameObject.GetComponent<PlayerBody>().Hitted(damage);
            int temp = (int)Random.Range(6, 9);
            collision.gameObject.GetComponent<PlayerBody>().Loss_Garbage(temp);
            SoundPlay("sfx/ShootHit");
            Rigidbody2D.velocity = Vector3.zero;
            PoolRecycle();

        }
        if(collision.transform.CompareTag("Wall"))
        {
            Drop();
            Rigidbody2D.velocity = Vector3.zero;
            PoolRecycle();
        }
    }


    void Drop()
    {
        GarbageMain am = GameObjectPoolManager.Instance.Pool_Garbage.AllocateGameObject<GarbageMain>(GameBoardManager.Instance.GameBoardGarbagesCanvas.transform);
        Vector3 temp = new Vector3(0, 0, Random.Range(0, 360));
        //am.CanPick = false;
        am.CanPick = false;
        am.transform.position = transform.position;
        am.transform.rotation = transform.rotation;
        am.Rigidbody2D.velocity = temp.normalized * Random.Range(500,800);
    }

}
