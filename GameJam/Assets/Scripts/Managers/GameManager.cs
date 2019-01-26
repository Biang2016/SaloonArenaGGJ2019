using System;
using System.IO;
using System.Xml;
using UnityEngine;

public partial class GameManager : MonoSingleton<GameManager>
{
    private GameManager()
    {
    }

    void Awake()
    {
        LoadXMLConfig();
        Awake_Xue();
        Awake_Li();
    }

    void Start()
    {
        Start_Xue();
        Start_Li();
    }

    public Camera BattleGroundCamera;

    #region 游戏全局参数

    public float GarbageBulletBeLitterSpeedThreshold; //子弹减速阈值（速度低于这个值阻力大幅增大然后停下消失）
    public float MaxEnergy; //最大电量
    public float StartEnergy; //起始电量
    public int SolarChargeSpeed; //电力耗尽后复活速度
    public int Trash; //携带垃圾量
    public int PowerConsume; //电量消耗速度
    public float relife_speed;
    public float Rotate_Speed;
    public float max_speed;
    public float shoot_speed;
    public float robotMass;
    public float robotDrag;
    public float ammoMass;
    public float ammoDrag;

    private string playerValueXMLPath = Application.streamingAssetsPath + "/Config/PlayerValues.xml";

    public void LoadXMLConfig()
    {
        string text;
        using (StreamReader sr = new StreamReader(playerValueXMLPath))
        {
            text = sr.ReadToEnd();
        }

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlElement allValues = doc.DocumentElement;

        XmlNode valueNode = allValues.ChildNodes.Item(0);
        GarbageBulletBeLitterSpeedThreshold = float.Parse(valueNode.Attributes["GarbageBulletBeLitterSpeedThreshold"].Value);
        MaxEnergy = float.Parse(valueNode.Attributes["MaxEnergy"].Value);
        StartEnergy = float.Parse(valueNode.Attributes["StartEnergy"].Value);
        SolarChargeSpeed = int.Parse(valueNode.Attributes["SolarChargeSpeed"].Value);
        Trash = int.Parse(valueNode.Attributes["Trash"].Value);
        PowerConsume = int.Parse(valueNode.Attributes["PowerConsume"].Value);
        relife_speed = float.Parse(valueNode.Attributes["relife_speed"].Value);
        Rotate_Speed = float.Parse(valueNode.Attributes["Rotate_Speed"].Value);
        max_speed = float.Parse(valueNode.Attributes["max_speed"].Value);
        shoot_speed = float.Parse(valueNode.Attributes["shoot_speed"].Value);
        robotMass = float.Parse(valueNode.Attributes["robotMass"].Value);
        robotDrag = float.Parse(valueNode.Attributes["robotDrag"].Value);
        ammoMass = float.Parse(valueNode.Attributes["ammoMass"].Value);
        ammoDrag = float.Parse(valueNode.Attributes["ammoDrag"].Value);
    }

    #endregion
}