using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

/// <summary>
/// 关卡灰度图的颜色配置文件
/// </summary>
public static class AllLevelMapColors
{
    public static Dictionary<MapBlockType, string> ColorDict = new Dictionary<MapBlockType, string>();
    public static Dictionary<string, MapBlockType> ColorDict_Rev = new Dictionary<string, MapBlockType>();

    public enum MapBlockType
    {
        Wall,
        Floor,
        InsideBlock,
        Player1Slot,
        Player2Slot,
        Player3Slot,
        Player4Slot,
    }

    public static void AddAllColors(string colorXMLPath)
    {
        string text;
        using (StreamReader sr = new StreamReader(colorXMLPath))
        {
            text = sr.ReadToEnd();
        }

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlElement allColors = doc.DocumentElement;

        for (int i = 0; i < allColors.ChildNodes.Count; i++)
        {
            XmlNode colorNode = allColors.ChildNodes.Item(i);
            MapBlockType colorType = (MapBlockType) Enum.Parse(typeof(MapBlockType), colorNode.Attributes["name"].Value);
            string color_str = colorNode.Attributes["color"].Value;
            if (!ColorDict.ContainsKey(colorType)) ColorDict.Add(colorType, color_str);
            if (!ColorDict_Rev.ContainsKey(color_str)) ColorDict_Rev.Add(color_str, colorType);
        }
    }
}