using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : PoolObject
{
    public Image PlayerImage;
    public Players WhichPlayer;
    public Robots WhichRobot;

    internal string Index_name;
    public Sprite[] sps;
    float MaxEnerg = 100; //最大电量
    int SolarChargeSpeed; //电力耗尽后复活速度
    private float energy; //当前电量

    public float Energy
    {
        get { return energy; }
        set
        {
            if (!value.Equals(energy))
            {
                energy = value;
                if (energy.Equals(MaxEnerg))
                {
                    ShowEmoji(Emojis.PowerFull, 2f);
                }
                else if (energy <= wake * MaxEnerg)
                {
                    ShowEmoji(Emojis.Low, 2f);
                }
            }
        }
    }

    private int trash; //携带垃圾量

    private Vector3 defaultPos;

    public int Trash
    {
        get { return trash; }
        set
        {
            if (trash != value)
            {
                trash = value;
                switch (WhichPlayer)
                {
                    case Players.Player1:
                    {
                        BattleScorePanelManager.Instance.ScorePlayer1 = trash;
                        break;
                    }
                    case Players.Player2:
                    {
                        BattleScorePanelManager.Instance.ScorePlayer2 = trash;
                        break;
                    }
                    case Players.Player3:
                    {
                        BattleScorePanelManager.Instance.ScorePlayer3 = trash;
                        break;
                    }
                    case Players.Player4:
                    {
                        BattleScorePanelManager.Instance.ScorePlayer4 = trash;
                        break;
                    }
                }
            }
        }
    }

    public int Power; //电量消耗速度
    public int Scores; //最后的分数
    public bool Lying; //电量耗尽
    public float relife_speed;
    public bool Charging;
    public RectTransform transform;
    public RectTransform arrow;
    public float wake; //重新恢复百分比
    public float Move_Speed;

    public float ContactX; //相撞造成伤害的相对速度最小值
    public float ContactDamage; //相撞造成的伤害值

    public int Do_num; //偷垃圾量的百分比

    public Image UI_P;
    public Sprite[] UI_sps;
    public Text Trash_Text;
    public Text Hp_Text;
    public Slider Hp;

    public CircleCollider2D circleCollider;
    public Move move;
    public Shoot shoot;

    public static Vector2 default_arrow_sizeDelta;
    public static Vector2 default_self_sizeDelta;

    public Rigidbody2D rb;
    public CircleCollider2D LootedArea;
    public CircleCollider2D DoArea;

    public float do_time;

    void Awake()
    {
        UI_P.sprite = UI_sps[(int) WhichPlayer];
        Index_name = "P" + ((int) WhichPlayer + 1) + "_";
        default_arrow_sizeDelta = arrow.sizeDelta;
        default_self_sizeDelta = transform.sizeDelta;
        defaultPos = transform.position;
    }

    public void Initialize()
    {
        GameManager.RobotParameter rp = GameManager.Instance.RobotParameters[WhichRobot];
        transform.position = defaultPos;
        Do_num = rp.Do_num;
        if (Do_num > 100) Do_num = 100;
        do_time = 0;
        Move_Speed = rp.Move_Speed;
        ContactX = rp.ContactX;
        ContactDamage = rp.ContactDamage;
        wake = rp.wake;
        PlayerImage.sprite = sps[(int) WhichRobot];
        MaxEnerg = rp.MaxEnergy;
        SolarChargeSpeed = rp.SolarChargeSpeed;
        Trash = rp.StartTrash;
        Power = rp.PowerConsume;
        relife_speed = rp.Relife_speed;
        Energy = rp.StartEnergy;
        transform.sizeDelta = default_self_sizeDelta * rp.RobotScale;
        arrow.sizeDelta = default_arrow_sizeDelta * rp.RobotScale;
        circleCollider.radius = transform.sizeDelta.x / 2;
        LootedArea.radius = (transform.sizeDelta.x / 2) * 1.1f;
        DoArea.radius = (transform.sizeDelta.x / 2) * 1.5f;
        UpdateHp();
        UpdateTrash();
        Charging = false;
        EmojiImage.enabled = false;
        move.Initialize();
        shoot.Initialize();
    }

    void Update()
    {
        if (GameBoardManager.Instance.M_StateMachine.GetState() == GameBoardManager.StateMachine.States.Hide) return;
        if (Charging)
        {
            ShowEmoji(Emojis.Charging, 2f);
        }
        else if (Lying)
        {
            ShowEmoji(Emojis.Not, 2f);
        }
    }

    private void FixedUpdate()
    {
        if (GameBoardManager.Instance.M_StateMachine.GetState() == GameBoardManager.StateMachine.States.Hide) return;
        if (Lying)
        {
            Add_Energy(SolarChargeSpeed * Time.deltaTime);
            if ((Energy / MaxEnerg) > wake)
                Lying = false;
        }
        else if (!Lying && !Charging)
        {
            Hitted(Power * Time.deltaTime);
        }
        else if (!Lying)
        {
            /*foreach (PlayerBody player in GameBoardManager.Instance.Players)
            {
                if (player.isActiveAndEnabled && player.WhichPlayer!= WhichPlayer)
                {
                    Vector3 pos3 = player.transform.position - transform.position;
                    Vector2 pos = new Vector2(pos3.x, pos3.y);
                    Debug.Log(pos.magnitude);
                    Vector2 spe = player.rb.velocity - rb.velocity;
                    if (pos.magnitude<(player.circleCollider.radius * player.transform.localScale.x +circleCollider.radius* transform.localScale.x + 4.7f))
                    {
                       
                        float temp = spe.magnitude;
                        Debug.Log(temp + "||" + (player.move.max_speed + move.max_speed) * ContactX);
                        if(temp > (player.move.max_speed + move.max_speed) * ContactX  && Vector2.Dot(pos,spe)<0)
                        {
                            player.Hitted(ContactDamage);
                            SoundPlay("sfx/Collision");
                        }
                        
                    }
                }
            }
        */
        }
    }

    private void LateUpdate()
    {
        if (GameBoardManager.Instance.M_StateMachine.GetState() == GameBoardManager.StateMachine.States.Hide) return;
        UI_P.transform.position = transform.position + Vector3.up * (100 + transform.sizeDelta.x / 2);
        UI_P.transform.rotation = Quaternion.Euler(Vector3.zero);
        EmojiImage.transform.position = transform.position + Vector3.up * (transform.sizeDelta.x / 2) * 1.5f + Vector3.right * (transform.sizeDelta.x / 2) * 1.5f;
        GameManager.RobotParameter rp = GameManager.Instance.RobotParameters[WhichRobot];
        EmojiImage.transform.rotation = Quaternion.Euler(Vector3.zero);
        EmojiImage.transform.localScale = Vector3.one * rp.RobotScale;
    }

    public void Hitted(float damage) //受到伤害
    {
        if (Energy > damage)
            Energy -= damage;
        else
        {
            Energy = 0;
            Lying = true;
        }

        UpdateHp();
    }

    public void Pick_Garbage(int num)
    {
        AudioManager.Instance.SoundPlay("sfx/Sweeping");
        Trash += num;
        UpdateTrash();
    }

    public void Loss_Garbage(int num)
    {
        if (Trash >= num)
            Trash -= num;
        else
            Trash = 0;

        UpdateTrash();
    }

    public void Add_Energy(float n)
    {
        if ((Energy + n) <= MaxEnerg)
            Energy += n;
        else
            Energy = MaxEnerg;

        UpdateHp();
    }

    void PowerDown()
    {
        if (Energy > Power * Time.deltaTime)
            Energy -= Power * Time.deltaTime;
        else
        {
            Energy = 0;
            Lying = true;
        }

        UpdateHp();
    }

    void UpdateTrash()
    {
        Trash_Text.text = Trash.ToString();
    }

    void UpdateHp()
    {
        Hp.value = Energy / MaxEnerg;
        Hp_Text.text = (Hp.value * 100).ToString("##0") + "%";
    }

    public Image EmojiImage;
    private Coroutine co_showEmoji;

    public void ShowEmoji(Emojis emoji, float duration = 2f)
    {
        if (co_showEmoji != null)
        {
            StopCoroutine(co_showEmoji);
        }

        co_showEmoji = StartCoroutine(Co_ShowEmoji(emoji, duration));
    }

    public void HideEmoji()
    {
        if (co_showEmoji != null)
        {
            StopCoroutine(co_showEmoji);
        }

        EmojiImage.enabled = false;
    }

    IEnumerator Co_ShowEmoji(Emojis emoji, float duration)
    {
        EmojiImage.enabled = true;
        EmojiImage.sprite = EmojiSprites[(int) emoji];
        yield return new WaitForSeconds(duration);
        EmojiImage.enabled = false;
    }

    public Sprite[] EmojiSprites;

    public enum Emojis
    {
        Charging,
        PowerFull,
        Han,
        Hitted,
        Low,
        Not,
        Shot,
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBody pb = collision.gameObject.GetComponent<PlayerBody>();
        if (pb != null)
        {
            if (pb.WhichPlayer != WhichPlayer)
            {
                if (collision.CompareTag("Player") && !pb.Lying)
                {
                    Vector3 pos3 = collision.transform.position - transform.position;
                    Vector2 pos = new Vector2(pos3.x, pos3.y);
                    Vector2 spe = pb.rb.velocity - rb.velocity;
                    float temp = spe.magnitude;

                    if (temp > (pb.move.max_speed + move.max_speed) * ContactX && Vector2.Dot(pos, spe) < 0)
                    {
//                        Debug.Log(temp + "||" + (pb.move.max_speed + move.max_speed) * ContactX);
                        pb.Hitted(ContactDamage);
                        pb.ShowEmoji(Emojis.Hitted, 0.3f);
                        SoundPlay("sfx/Collision");
                    }
                }
            }
        }
    }
}