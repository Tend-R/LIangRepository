using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class JsonManger {
    public static Dictionary<string, string> jsonInfo=null;
   
    /// <summary>
    /// 构造函数，构造时获取json数据分析存储
    /// </summary>
    /// <param name="jsonPath"></param>
    public JsonManger(string jsonPath)
    {
        jsonInfo = new Dictionary<string, string>();
        InitAndAnalysisJson(jsonPath);
    }
    /// <summary>
    /// 加载json配置文件存储到
    /// </summary>
    /// <param name="jsonPath"></param>
    private static void InitAndAnalysisJson(string jsonPath)
    {
        if (string.IsNullOrEmpty(jsonPath)) return;
        TextAsset jsonAllText = null;
        jsonAllText = Resources.Load<TextAsset>(jsonPath);
        KeyValueInfo keyValueInfo=  JsonUtility.FromJson<KeyValueInfo>(jsonAllText.text);
        foreach (KeyValue item in keyValueInfo.KeyValueList)
        {
            jsonInfo.Add(item.Key,item.Value);
        }
    }
/// <summary>
/// 将打包的ab数据配置存储更新到json
/// </summary>
/// <param name="path"></param>
/// <param name="keyValue"></param>
   public static void DataToJson(string path, KeyValueInfo kvInfo)
    {
        if (string.IsNullOrEmpty(path) || kvInfo == null) return;
        string abstr = JsonUtility.ToJson(kvInfo);
        try
        {
            //覆盖写入
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + path, abstr);
        }
        catch (Exception e)
        {
           Debug.LogError(e.GetType()+"-"+e.Message);
        }

    }
}
[Serializable]
public class KeyValueInfo
{
    public List<KeyValue> KeyValueList=null;
    public KeyValueInfo()
    {
        KeyValueList = new List<KeyValue>();
    }
}
[Serializable]
public class KeyValue
{
    public string Key = "";
    public string Value = "";
    public KeyValue(string _key,string _value)
    {
        Key = _key;
        Value = _value;
    }
}