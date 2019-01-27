using System;
using System.Collections.Generic;
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

    public Dictionary<Robots, RobotParameter> RobotParameters = new Dictionary<Robots, RobotParameter>();

    public class RobotParameter
    {
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
        public float AmmoScale; //子弹尺寸
        public int AmmoDamage; //子弹伤害==消耗垃圾数==爆出垃圾数
        public float RobotScale; //机体尺寸
        public float wake;//重新恢复百分比
        public float ContactDamage;//相撞造成的伤害值
        public float ContactX;//相撞造成伤害的相对速度最小值
        public float Move_Speed;//移动速度
        public int Do_num;//偷垃圾量的百分比

    }

    public int StarterFloorGarbage; //起始地面垃圾
    public float GarbageBulletBeLitterSpeedThreshold; //子弹减速阈值（速度低于这个值阻力大幅增大然后停下消失）
    public float LevelTime; //关卡时间
    

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
        for (int i = 0; i < allValues.ChildNodes.Count; i++)
        {
            XmlNode valueNode = allValues.ChildNodes.Item(i);
            switch (valueNode.Name)
            {
                case "Robots":
                {
                    for (int j = 0; j < valueNode.ChildNodes.Count; j++)
                    {
                        XmlNode robotNode = valueNode.ChildNodes.Item(j);
                        RobotParameter rp = new RobotParameter();
                        Robots robot = (Robots) Enum.Parse(typeof(Robots), robotNode.Attributes["name"].Value);
                        rp.MaxEnergy = float.Parse(robotNode.Attributes["MaxEnergy"].Value);
                        rp.StartEnergy = float.Parse(robotNode.Attributes["StartEnergy"].Value);
                        rp.SolarChargeSpeed = int.Parse(robotNode.Attributes["SolarChargeSpeed"].Value);
                        rp.StartTrash = int.Parse(robotNode.Attributes["StartTrash"].Value);
                        rp.PowerConsume = int.Parse(robotNode.Attributes["PowerConsume"].Value);
                        rp.Relife_speed = float.Parse(robotNode.Attributes["Relife_speed"].Value);
                        rp.Rotate_Speed = float.Parse(robotNode.Attributes["Rotate_Speed"].Value);
                        rp.Max_Speed = float.Parse(robotNode.Attributes["Max_Speed"].Value);
                        rp.Shoot_Speed = float.Parse(robotNode.Attributes["Shoot_Speed"].Value);
                        rp.Shoot_CD = float.Parse(robotNode.Attributes["Shoot_CD"].Value);
                        rp.RobotMass = float.Parse(robotNode.Attributes["RobotMass"].Value);
                        rp.RobotDrag = float.Parse(robotNode.Attributes["RobotDrag"].Value);
                        rp.RobotRotateDrag = float.Parse(robotNode.Attributes["RobotRotateDrag"].Value);
                        rp.AmmoMass = float.Parse(robotNode.Attributes["AmmoMass"].Value);
                        rp.AmmoDrag = float.Parse(robotNode.Attributes["AmmoDrag"].Value);
                        rp.RobotScale = float.Parse(robotNode.Attributes["RobotScale"].Value);
                        rp.AmmoScale = float.Parse(robotNode.Attributes["AmmoScale"].Value);
                        rp.AmmoDamage = int.Parse(robotNode.Attributes["AmmoDamage"].Value);
                        rp.wake = float.Parse(robotNode.Attributes["wake"].Value);
                        rp.ContactDamage = float.Parse(robotNode.Attributes["ContactDamage"].Value);
                        rp.ContactX = float.Parse(robotNode.Attributes["ContactX"].Value);
                        rp.Move_Speed = float.Parse(robotNode.Attributes["Move_Speed"].Value);
                        rp.Do_num= int.Parse(robotNode.Attributes["Do_num"].Value);

                            if (!RobotParameters.ContainsKey(robot))
                        {
                          RobotParameters.Add(robot, rp);
                        }
                    }

                    break;
                }
                case "Map":
                {
                    GarbageBulletBeLitterSpeedThreshold = float.Parse(valueNode.Attributes["GarbageBulletBeLitterSpeedThreshold"].Value);
                    StarterFloorGarbage = int.Parse(valueNode.Attributes["StarterFloorGarbage"].Value);
                    LevelTime = float.Parse(valueNode.Attributes["LevelTime"].Value);
                    break;
                }
            }
        }
    }

    #endregion
}