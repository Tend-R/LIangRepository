using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FWork;
using System;

public class GameManger : MonoBehaviour
{
    private static GameManger instance;

    public static GameManger Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject game = new GameObject("GameManger");
                instance = game.AddComponent<GameManger>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }   
    /// <summary>
    /// 可以自定义加载ab包中资源的action操作
    /// </summary>
    /// <param name="prefabName">要加载的ab包中的资源名称</param>
    /// <param name="action">加载到资源后的事件函数（参数为加载到的资源）</param>
    /// <param name="type">加载到的资源的类型</param>
    public void ShowABAsset(string prefabName,Action<UnityEngine.Object> action,Type type)
    {
        action += (s) => { SystemDefine.loadingPanel.SetActive(false); };
        ABResourcesLoader.Instance.TryLoadAssetAndSave(prefabName, action, type);

    }
    /// <summary>
    /// 将ab包中资源加载到场景
    /// </summary>
    /// <param name="assetName">ab包中资源的名称</param>
    public void ShowToGame(string assetName)
    {
        ShowABAsset(assetName, (s) => {
            GameObject o = s as GameObject;
            Instantiate<GameObject>(o);
            Debug.Log("加载到ab中资源并克隆到到场景："+o.name);
            //TODO  这里可以给资源给定初始数据（旋转，位置等），可以在外部加载配置文件或者写在资源对象的挂在脚本中，加载时获取
            //
            ABResourcesLoader.Instance.overAction();//停止加载动画
        }, typeof(GameObject));
    }
}
