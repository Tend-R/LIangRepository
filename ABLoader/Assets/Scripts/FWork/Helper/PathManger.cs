using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FWork
{
    public class PathManger
    {
        /*需要打包制作成ab资源的文件夹*/
        public const string ABResoursesPath = "AB_Resourses";
        /*生成的ab资源的上一层文件夹名*/
        public const string ABProjectName = "AB";//打包出的bundle文件夹的名字
        /*ab资源包的后缀*/
        public static string abSuffix = ".ab";

        public static string jsonInfoPath = "ABInfoJson.json";//存储bundle名与对应资源名的json文件
        //本地测试（使用xampp搭的本地服务器）
        public   static string assetBundleWebUrl = "http://localhost:81/UnityAssets/";//需要下载的ab资源的地址



        #region publicMethod
        /// <summary>
        /// 获取到需要加载的ab包路径
        /// </summary>
        /// <returns></returns>
        public static string GetLoaderPath()
        {
            string re = assetBundleWebUrl;
#if UNITY_EDITOR
            re = Application.dataPath + "/StreamingAssets/";
            re += ABProjectName + "/";
#elif UNITY_STANDALONE_WIN
			re+="IOS/";
#elif UNITY_IPHONE
			re+="IOS/";
#elif UNITY_ANDROID
			re+="Android/";
#elif UNITY_WEBGL
        
#endif
            return re;
        }
 
          




        /// <summary>
        /// 获取到打包出的ab包路径
        /// </summary>
        /// <returns></returns>
        public static string GetABOutFile()
        {
            return GetPlatformPath() + "/" + GetPlatformName();
        }
       
        /// <summary>
        /// 获取打ab资源存储的文件夹（ab_resourses根目录）
        /// </summary>
        /// <returns></returns>
        public static string GetABResoursesPath()
        {
            return Application.dataPath + "/" + ABResoursesPath;
        }
        #endregion
        #region privateMethod
        /// <summary>
        /// 获取平台路径
        /// </summary>
        /// <returns></returns>
        private static string GetPlatformPath()
        {
            string strReturnPlatformPath = "";

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformPath = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strReturnPlatformPath = Application.persistentDataPath;
                    break;
                default:
                    break;
            }

            return strReturnPlatformPath;
        }



        /// <summary>
        /// 获取平台名称
        /// </summary>
        /// <returns></returns>
        private static string GetPlatformName()
        {
            string strReturnPlatformName = "";

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformName = ABProjectName;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    strReturnPlatformName = "Iphone";
                    break;
                case RuntimePlatform.Android:
                    strReturnPlatformName = "Android";
                    break;
                default:
                    break;
            }

            return strReturnPlatformName;
        }
        #endregion





     
    }
}

