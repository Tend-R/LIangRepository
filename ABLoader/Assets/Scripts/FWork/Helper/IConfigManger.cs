using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FWork
{
    public interface IConfigManger
    {
        /// <summary>
        /// 获取到到键值对存储
        /// </summary>
        Dictionary<string, string> AppJsonSetting { get; }
        /// <summary>
        /// 获取配置的最大长度
        /// </summary>
        /// <returns></returns>
        int maxAppJsonSettingNum();
    }
    [SerializeField]
    public class KeyValueInfo
    {   //json配置列表
        public List<KeyValueNode> KeyValueList;
    }
    [SerializeField]
    public class KeyValueNode
    {  
        //键(资源名)
        public string Key="";
        //值（ab包名）
        public string Value="";
       
    }

}

