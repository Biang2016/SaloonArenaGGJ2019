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
    public int SolarChargeSpeed; //电力耗尽后电力恢复速度
    public int StartTrash; //起始携带垃圾量
    public int PowerConsume; //平时电量逐渐消耗速度
    public float Relife_speed; //充电桩充电速度
    public float Rotate_Speed; //旋转灵敏度
    public float Max_Speed; //移动最大速度
    public float Shoot_Speed; //子弹速度s
    public float Shoot_CD; //子弹CD
    public float RobotMass; //子弹速度
    public float RobotDrag; //机器人移动阻力
    public float RobotRotateDrag; //机器人转动阻力
    public float AmmoMass; //子弹质量
    public float AmmoDrag; //子弹阻力
    public float RobotScale;//机体尺寸
    public float AmmoScale;//子弹半径
    public int AmmoDamage;//子弹威力

    public int StarterFloorGarbage;

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
        StartTrash = int.Parse(valueNode.Attributes["StartTrash"].Value);
        PowerConsume = int.Parse(valueNode.Attributes["PowerConsume"].Value);
        Relife_speed = float.Parse(valueNode.Attributes["Relife_speed"].Value);
        Rotate_Speed = float.Parse(valueNode.Attributes["Rotate_Speed"].Value);
        Max_Speed = float.Parse(valueNode.Attributes["Max_Speed"].Value);
        Shoot_Speed = float.Parse(valueNode.Attributes["Shoot_Speed"].Value);
        Shoot_CD = float.Parse(valueNode.Attributes["Shoot_CD"].Value);
        RobotMass = float.Parse(valueNode.Attributes["RobotMass"].Value);
        RobotDrag = float.Parse(valueNode.Attributes["RobotDrag"].Value);
        RobotRotateDrag = float.Parse(valueNode.Attributes["RobotRotateDrag"].Value);
        AmmoMass = float.Parse(valueNode.Attributes["AmmoMass"].Value);
        AmmoDrag = float.Parse(valueNode.Attributes["AmmoDrag"].Value);
        StarterFloorGarbage = int.Parse(valueNode.Attributes["StarterFloorGarbage"].Value);
        RobotScale = float.Parse(valueNode.Attributes["RobotScale"].Value);
        AmmoScale = float.Parse(valueNode.Attributes["AmmoScale"].Value);
        AmmoDamage = int.Parse(valueNode.Attributes["AmmoDamage"].Value);
    }

    #endregion
}