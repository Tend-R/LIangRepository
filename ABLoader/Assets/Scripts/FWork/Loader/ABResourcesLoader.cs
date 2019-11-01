using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace FWork
{
    public class ABResourcesLoader : MonoBehaviour
    {
        private static ABResourcesLoader instance;
        public static ABResourcesLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject loader = new GameObject("ABResourcesLoader");
                    instance= loader.AddComponent<ABResourcesLoader>();
                }
                return instance;
            }
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                Destroy(this.gameObject);
            ABundleDic.Clear();
            DontDestroyOnLoad(this.gameObject);
        }
        AssetSet thisLoader = null;//目前正在需要加载的资源
        bool isLoadingAsset = false;
        /*开始加载时的事件*/
        Action beignAction=null;
        public  Action overAction=null;
        /* ab资源的队列*/
        Queue<AssetSet> assetBundleQueue = new Queue<AssetSet>();
        /*存放下载的ab包 key：ab的包名，value：ab包*/
        public Dictionary<string, AssetBundle> ABundleDic = new Dictionary<string, AssetBundle>();

        private void Start()
        {
            //添加开始加载资源动画事件
            beignAction = () => {
                SystemDefine.loadingPanel.SetActive(true);
            };
            //添加加载完后的停止动画事件
            overAction = () => {
                SystemDefine.loadingPanel.SetActive(false);
            };
        }
        #region privateMethon
        /// <summary>
        /// 加载单个ab包到缓存字典
        /// </summary>
        /// <param name="abName">ab包名字</param>
        /// <returns></returns>
        private IEnumerator LoadABundle(string abName, Action ac)
        {
            //编辑器下test使用
             string url = System.IO.Path.Combine(PathManger.GetLoaderPath(), abName);
            //外网链接
           // string url = PathManger.assetBundleWebUrl + PathManger.ABProjectName+"/"+abName;
            Debug.Log("url:" + url);
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError(GetType() + "需要下载的" + abName + "资源的url为空！");
            }
            UnityWebRequest webRequest = UnityWebRequest.GetAssetBundle(url);
            yield return webRequest.SendWebRequest();
            if (!string.IsNullOrEmpty(webRequest.error))
            {
                Debug.LogError(webRequest.error + "URL: " + url);
            }
            else
            {
                // AssetBundle ab = (webRequest.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(webRequest);
                if (ab != null)
                {
                    ABundleDic[abName] = ab;
                    Debug.Log(ab.name + "包加载完成");
                    ac();
                }
            }
        }
        /// <summary>
        /// 将assetSet对象加入队列
        /// </summary>
        /// <param name="assetSet"></param>
        private void AddSetToAssetBundleQueue(AssetSet assetSet)
        {
            assetBundleQueue.Enqueue(assetSet);
        }
        /// <summary>
        /// 尝试加载ab包，ab包中的文件
        /// </summary>
        /// <param name="abName">ab包名</param>
        /// <param name="assetName">ab包中的资源名</param>
        /// <param name="overAction">加载后的事件</param>
        /// <param name="loadType">加载的资源的类型</param>
        private void TryLoadAsset(string abName, string assetName, Action<UnityEngine.Object> _overAction, Type loadType)
        {
            //确保在添加事件时事件已赋值（在调用时脚本还未执行start方法）
            if (beignAction==null||overAction==null)
            {
                Start();
            }
            AssetSet abset = new AssetSet//其中的一些配置（位置，旋转都能）
            {
                abName = abName.ToLower(),
                assetName = assetName,
                returnType = AssetBundleReturnType.Prefab,
                loadType = loadType,
                OnLoadBeign =Instance.beignAction,
                OnLoadOver = _overAction
            };
            AddSetToAssetBundleQueue(abset);
            Load();

        }
        //private void TryLoadAsset(string abName, string assetName, Action<UnityEngine.Object> _beignAction, Action<UnityEngine.Object> _loadingAction, Action<UnityEngine.Object> _overAction, Type loadType)
        //{
        //    AssetSet abset = new AssetSet
        //    {
        //        abName = abName.ToLower(),
        //        assetName = assetName,
        //        returnType = AssetBundleReturnType.Prefab,
        //        loadType = loadType,
        //        OnLoadBeign = _beignAction,
        //        OnLoading = _loadingAction,
        //        OnLoadOver = _overAction
        //    };
        //    AddSetToAssetBundleQueue(abset);
        //}
/// <summary>
/// 查询资源包（加载）
/// </summary>
        private void Load()
        {
            if (assetBundleQueue.Count > 0)
            {
                AssetSet assetSet = assetBundleQueue.Dequeue();
                assetSet.OnLoadBeign();//开始加载动画界面
                if (ABundleDic.ContainsKey(assetSet.abName))
                {
                    Debug.Log("加载到ab包：" + assetSet.abName);
                    AssetBundle assetBundle = ABundleDic[assetSet.abName];
                    if (assetBundle != null)
                    {
                        GameObject obj = assetBundle.LoadAsset<GameObject>(assetSet.assetName);
                        assetSet.OnLoadOver(obj);
                    }
                }
                else
                {
                    Debug.Log("开始加载:"+ assetSet.abName);
                    StartCoroutine(LoadABundle(assetSet.abName, () => {
                        AddSetToAssetBundleQueue(assetSet);
                        Load();
                    }));
                }
            }
        }
        #endregion


        /// <summary>
        /// 加载资源名对应的ab包
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="overAction"></param>
        /// <param name="loadType"></param>
        public void TryLoadAssetAndSave(string assetName, Action<UnityEngine.Object> overAction, Type loadType)
        {
            if (SystemDefine.jsonDic.Count>0)
            {
                if (SystemDefine.jsonDic.ContainsKey(assetName))
                {
                    string abname = SystemDefine.jsonDic[assetName];
                    TryLoadAsset(abname,assetName , overAction, loadType);
                }
                else
                {
                    Debug.LogError("Json配置列表无"+assetName+",请检查！");
                }               
            }
            else
            {
                Debug.LogError("Json配置未被写入");
            }
        }
    }
}

