using UnityEngine;
using System.Linq;


    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance = null;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = Resources.FindObjectsOfTypeAll(typeof(T)).Select(s => s as T).FirstOrDefault();
                    if (instance == null)
                        instance = new GameObject("@" + typeof(T).ToString(), typeof(T)).AddComponent<T>();
                }
                return instance;
            }
        }
    }


