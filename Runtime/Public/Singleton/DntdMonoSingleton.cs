using UnityEngine;

namespace Com.BaiZe.MonoToolSet
{
    public abstract class DntdMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;

        private static readonly Object singletonGO = new Object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonGO)
                    {

                        instance = FindObjectOfType<T>();
                        if (FindObjectsOfType<T>().Length > 1)
                        {
                            Debug.LogError(string.Format("class {0} has more then one singleton gameobject instance in the scene", typeof(T)));
                        }

                        if (instance == null)
                        {
                            var singleton = new GameObject();
                            singleton.name = typeof(T) + "[singleton]";
                            singleton.hideFlags = HideFlags.None;
                            instance = singleton.AddComponent<T>();
                        }
                        DontDestroyOnLoad(instance.gameObject);
                    }
                    instance.hideFlags = HideFlags.None;
                }
                return instance;
            }
        }
    }
}