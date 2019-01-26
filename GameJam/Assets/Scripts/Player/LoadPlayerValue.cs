using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class LoadPlayerValue : MonoBehaviour
{
    public TextAsset value;

    public float mass, rota_s, trans_s;
    XmlNodeList valueNodeList;
    private void Awake()
    {
        XmlDocument ValueDoc = new XmlDocument();
        ValueDoc.LoadXml(value.text);
        valueNodeList = ValueDoc.SelectNodes("PlayerValues/Value");
        mass = (float)XmlConvert.ToDouble(valueNodeList[0].Value);
        rota_s = (float)XmlConvert.ToDouble(valueNodeList[1].Value);
        trans_s = (float)XmlConvert.ToDouble(valueNodeList[2].Value);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
