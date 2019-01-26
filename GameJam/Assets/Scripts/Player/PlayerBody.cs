using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBody : PoolObject
{
    public Image PlayerImage;
    public Players WhichPlayer;
    internal string Index_name;
    public Sprite[] sps;
    static float MaxEnerg = 100; //最大电量
    static int SolarChargeSpeed; //电力耗尽后复活速度
    public float Energy; //当前电量
    public int Trash; //携带垃圾量
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

    void Awake()
    {
        UI_P.sprite = UI_sps[(int) WhichPlayer];
        PlayerImage.sprite = sps[(int) WhichPlayer];
        Index_name = "P" + ((int) WhichPlayer + 1) + "_";
    }

    void Start()
    {
        MaxEnerg = GameManager.Instance.MaxEnergy;
        SolarChargeSpeed = GameManager.Instance.SolarChargeSpeed;
        Trash = GameManager.Instance.StartTrash;
        Power = GameManager.Instance.PowerConsume;
        relife_speed = GameManager.Instance.Relife_speed;
        Energy = GameManager.Instance.StartEnergy;
        transform.sizeDelta *= GameManager.Instance.RobotScale;
        transform.GetComponent<CircleCollider2D>().radius = transform.sizeDelta.x / 2;
        arrow.sizeDelta *= GameManager.Instance.RobotScale;
        UpdateHp();
        UpdateTrash();
        Charging = false;
    }

    private void FixedUpdate()
    {
        if (Lying)
        {
            Add_Energy(SolarChargeSpeed * Time.deltaTime);
            if ((Energy / MaxEnerg) > 0.2)
                Lying = false;
        }
        else if(!Lying && !Charging)
        {
            Hitted(Power * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        UI_P.gameObject.transform.position = transform.position + Vector3.up * (100+ transform.sizeDelta.x / 2);
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
}