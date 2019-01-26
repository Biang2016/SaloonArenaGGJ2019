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
    public float Energy; //当前电量
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

    public Image UI_P;
    public Sprite[] UI_sps;
    public Text Trash_Text;
    public Text Hp_Text;
    public Slider Hp;

    public static Vector2 default_arrow_sizeDelta;
    public static Vector2 default_self_sizeDelta;

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
        PlayerImage.sprite = sps[(int) WhichRobot];
        MaxEnerg = rp.MaxEnergy;
        SolarChargeSpeed = rp.SolarChargeSpeed;
        Trash = rp.StartTrash;
        Power = rp.PowerConsume;
        relife_speed = rp.Relife_speed;
        Energy = rp.StartEnergy;
        transform.sizeDelta = default_self_sizeDelta * rp.RobotScale;
        transform.GetComponent<CircleCollider2D>().radius = transform.sizeDelta.x / 2;
        arrow.sizeDelta = default_arrow_sizeDelta * rp.RobotScale;
        UpdateHp();
        UpdateTrash();
        Charging = false;
    }

    private void FixedUpdate()
    {
        if (GameBoardManager.Instance.M_StateMachine.GetState() == GameBoardManager.StateMachine.States.Hide) return;
        if (Lying)
        {
            Add_Energy(SolarChargeSpeed * Time.deltaTime);
            if ((Energy / MaxEnerg) > 0.2)
                Lying = false;
        }
        else if (!Lying && !Charging)
        {
            Hitted(Power * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (GameBoardManager.Instance.M_StateMachine.GetState() == GameBoardManager.StateMachine.States.Hide) return;
        UI_P.gameObject.transform.position = transform.position + Vector3.up * (100 + transform.sizeDelta.x / 2);
        UI_P.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
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
        //Hp_Text.text = (Hp.value * 100).ToString("##0") + "%";
        Hp_Text.text = MaxEnerg.ToString("##0");
    }

    public Image EmojiImage;

    public void ShowEmoji(Emojis emoji)
    {
        EmojiImage.sprite = EmojiSprites[(int) emoji];
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
}