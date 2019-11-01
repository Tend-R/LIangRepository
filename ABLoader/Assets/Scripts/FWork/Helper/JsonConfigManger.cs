using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace FWork
{
    public class JsonConfigManger : IConfigManger
    {
        //保存（键值对）应用设置集合
        private static Dictionary<string, string> _AppJsonSetting;
        /// <summary>
        /// 获取到有读取后的json配置存储的集合
        /// </summary>
        public Dictionary<string, string> AppJsonSetting
        {
            get
            {
                return _AppJsonSetting;
            }
        }
        //集合的做大长度
        public int maxAppJsonSettingNum()
        {
            if (_AppJsonSetting != null && _AppJsonSetting.Count >= 1)
            {
                return _AppJsonSetting.Count;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonPath">json文件的路径</param>
        public JsonConfigManger(string jsonName)
        {
            _AppJsonSetting = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(jsonName)) return;
            AnalysisJsonAndSave(jsonName);
        }

        /// <summary>
        /// 将打包的ab数据配置存储更新
        /// </summary>
        /// <param name="keyValue"></param>
        public static void DataToJson(string path, KeyValueInfo kvInfo)
        {
            if (string.IsNullOrEmpty(path) || kvInfo == null) return;
            string abstr = JsonMapper.ToJson(kvInfo);
            try
            {
                //覆盖写入
                System.IO.File.WriteAllText(Application.dataPath + "/Resources/" + path, abstr);
            }
            catch (Exception e)
            {

                Debug.LogError(e.GetType() + "-" + e.Message);
            }
        }

        /// <summary>
        /// 读取到json配置 并存储起来
        /// </summary>
        /// <param name="jsonPath">json的路径</param>
        private void AnalysisJsonAndSave(string jsonName)
        {
            TextAsset jsonstring = null;
            KeyValueInfo keyValueInfo = null;
            if (string.IsNullOrEmpty(jsonName)) return;
            jsonstring = Resources.Load<TextAsset>(jsonName);
            keyValueInfo = JsonMapper.ToObject<KeyValueInfo>(jsonstring.text);
            foreach (var item in keyValueInfo.KeyValueList)
            {
                _AppJsonSetting.Add(item.Key, item.Value);
            }
        }


    }

}
