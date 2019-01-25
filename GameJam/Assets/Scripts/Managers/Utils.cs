using UnityEngine;
using UnityEngine.Rendering;

public class Utils
{
    public static Color HTMLColorToColor(string htmlColor)
    {
        Color cl = new Color();
        ColorUtility.TryParseHtmlString(htmlColor, out cl);
        return cl;
    }

    public static string ColorToHTMLColor(Color color)
    {
        string res = "#" + ColorUtility.ToHtmlStringRGBA(color).ToLower();
        return res;
    }
}