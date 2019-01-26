using UnityEngine;
using UnityEngine.UI;

public class Ammo : PoolObject
{
    public int player;
    public int damage;
    public Rigidbody2D Rigidbody2D;
    public Image Image;
    public Sprite[] Sprites;
    public GameObject garbage;
    public RectTransform transform;

    private void Awake()
    {
        Initialize(Players.Player1);
    }

    public void Initialize(Players _player)
    {
        GameManager.RobotParameter rp = GameManager.Instance.RobotParameters[(Robots) _player];
        damage = rp.AmmoDamage;
        Rigidbody2D.mass = rp.AmmoMass;
        Rigidbody2D.drag = rp.AmmoDrag;
        transform.localScale *= rp.AmmoScale;
        Image.sprite = Sprites[Random.Range(0, 3)];
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
                case "Wall_left":
                    vector2.x = 1;
                    break;
                case "Wall_right":
                    vector2.x = -1;
                    break;
                case "Wall_up":
                    vector2.y = -1;
                    break;
                case "Wall_down":
                    vector2.y = 1;
                    break;
                default: break;
            }

            Drop(vector2);
            Rigidbody2D.velocity = Vector3.zero;
            PoolRecycle();
        }
    }

    void Drop(Vector2 vector2)
    {
        GarbageMain am = GameObjectPoolManager.Instance.Pool_Garbage.AllocateGameObject<GarbageMain>(GameBoardManager.Instance.GameBoardGarbagesCanvas.transform);
        am.CanPick = true;
        am.transform.position = transform.position;
        am.Rigidbody2D.velocity = vector2.normalized * Random.Range(100, 200);
        am.Initialize();
    }
}