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
    public static Dictionary<ColorType, string> ColorDict = new Dictionary<ColorType, string>();
    public static Dictionary<string, ColorType> ColorDict_Rev = new Dictionary<string, ColorType>();

    public enum ColorType
    {
        Wall,
        Floor,
        InsideBlock,
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
            ColorType colorType = (ColorType) Enum.Parse(typeof(ColorType), colorNode.Attributes["name"].Value);
            string color_str = colorNode.Attributes["color"].Value;
            if (!ColorDict.ContainsKey(colorType)) ColorDict.Add(colorType, color_str);
            if (!ColorDict_Rev.ContainsKey(color_str)) ColorDict_Rev.Add(color_str, colorType);
        }
    }
}