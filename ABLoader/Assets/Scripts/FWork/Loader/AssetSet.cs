using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一个ab包中的资源对象
/// </summary>
[SerializeField]
public class AssetSet
{
    public GameObject root;//ab包资源的对象
    public string abName = null;//ab包名字
    public string assetName = null;
    public AssetBundleReturnType returnType = AssetBundleReturnType.None;//ab包中的资源类型
    public Vector3 position;
    public Vector3 scale;
    public Vector3 rotation;
    public Type loadType;//从ab包加载的资源的类型
    public LoadTpye loadAssetTpye;//加载资源的方式
    public bool onlyLoad;
    public Action OnLoadBeign;//开始加载的事件
    public Action<GameObject> OnCloneOver;//gameobject克隆后的事件
    public Action<UnityEngine.Object> OnLoading; //加载时的事件
    public Action<UnityEngine.Object> OnLoadOver;//加载资源后的事件

    public void LoadBeign()
    {
        if (OnLoadBeign != null)
        {
            try
            {
                OnLoadBeign();
            }
            catch (Exception e)
            {

                Debug.Log(e.Message+"  "+e.StackTrace);
            }
            
        }
    }

    public void CloneOver(GameObject asset)
    {
        if (OnCloneOver != null)
        {
            try
            {
                OnCloneOver(asset);
            }
            catch (Exception e)
            {

                Debug.Log(e.Message + "  " + e.StackTrace);
            }

        }
    }
    public void Loading(UnityEngine.Object o)
    {
        if (OnLoading != null)
        {
            try
            {
                OnLoading(o);
            }
            catch (Exception e)
            {

                Debug.Log(e.Message + "  " + e.StackTrace);
            }

        }
    }
    public void LoadOver(UnityEngine.Object o)
    {
        if (OnLoadOver != null)
        {
            try
            {
                OnLoadOver(o);
            }
            catch (Exception e)
            {

                Debug.Log(e.Message + "  " + e.StackTrace);
            }

        }
    }

    
}
public enum AssetBundleReturnType
{
    None = 0,
    Object = 1,
    Prefab = 2,
    Scene = 3,
    Sprites = 4,
    AllObjects = 5,
}
public enum LoadTpye
{
    None=1,
}
