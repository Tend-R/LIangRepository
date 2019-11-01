using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FWork
{
    public class SingletonT<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static object _lock = new object();
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            return instance;
                        }

                        if (instance == null)
                        {
                            GameObject singleton = new GameObject(typeof(T).Name);
                            instance = singleton.AddComponent<T>();
                            DontDestroyOnLoad(singleton);
                        }
                    }
                }
                return instance;
            }
        }
    }

}
