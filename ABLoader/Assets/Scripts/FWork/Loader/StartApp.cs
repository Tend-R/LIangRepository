using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FWork
{
    public class StartApp : MonoBehaviour
    {
        private void Awake()
        {
            InitReadJson();
            LoadLoadingAni();
        }
        private void Start()
        {

            //test
            GameManger.Instance.ShowToGame(SystemDefine.sphereprefabName);
            GameManger.Instance.ShowToGame(SystemDefine.cubeprefabName);
        }
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.K))
            {
                //test
                //GameManger.Instance.ShowABAsset(SystemDefine.uuprefab, (s) =>
                //{
                //    SystemDefine.uu = s as GameObject;
                //    Instantiate<GameObject>(SystemDefine.uu);
                //}, typeof(GameObject));
                //GameManger.Instance.ShowToGame(SystemDefine.uuprefab);
            }
#endif
        }
        /// <summary>
        /// 读取json配置 存储到系统常量类
        /// </summary>
        private void InitReadJson()
        {
            SystemDefine.jsonDic = new Dictionary<string, string>();
            IConfigManger jsonConfig = new JsonConfigManger(SystemDefine.jsonName);
            if (jsonConfig!=null)
            {
                SystemDefine.jsonDic = jsonConfig.AppJsonSetting;
            }
        }
        /// <summary>
        /// loading界面
        /// </summary>
        private void LoadLoadingAni()
        {
            GameObject loadingPrefab = Resources.Load<GameObject>("Loading/LoadingPanel");
            GameObject load = Instantiate(loadingPrefab, GameObject.Find("Canvas").transform);
            load.SetActive(false);
            SystemDefine.loadingPanel = load;
        }

    }
}

