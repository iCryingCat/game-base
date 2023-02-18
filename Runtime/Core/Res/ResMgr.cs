using System.IO;

using Com.BaiZe.SharpToolSet;

using UnityEditor;

using UnityEngine;

using UObject = UnityEngine.Object;

namespace Com.BaiZe.GameBase
{
    public class ResMgr
    {
        public const string LOAD_PATH_EDITOR = "Assets/Res";
        public const string LOAD_PATH_RUNTIME = "Res";

        public static GameObject Instantiate(GameObject asset)
        {
            return GameObject.Instantiate(asset);
        }

        public static void Destroy(GameObject instance)
        {
            GameObject.Destroy(instance);
        }

        public static GameObject LoadPrefab(string slug)
        {
            GameObject go = Load<GameObject>(slug);
            return go;
        }

        public static TextAsset LoadTextAsset(string slug)
        {
            TextAsset go = Load<TextAsset>(slug);
            return go;
        }

        public static Sprite LoadSprite(string slug)
        {
            Sprite go = Load<Sprite>(slug);
            return go;
        }

        public static Texture LoadTexture(string slug)
        {
            Texture go = Load<Texture>(slug);
            return go;
        }

        private static T Load<T>(string slug) where T : UObject
        {
            T asset = null;
#if UNITY_EDITOR
            string resLoadMode = "AssetDatabase";
            switch (resLoadMode)
            {
                case "AssetDatabase":
                    var path = Path.Combine(LOAD_PATH_EDITOR, slug).PathFormat();
                    Debug.Log("AssetDatabase load..." + path);
                    asset = AssetDatabase.LoadAssetAtPath<T>(path);
                    if (asset != null)
                    {
                        Debug.Log("AssetDatabase load Succ..." + asset);
                        return asset as T;
                    }
                    else
                    {
                        Debug.Log("AssetDatabase load Failed..." + path);
                    }
                    return asset;
            }
#else
            var path = Path.Combine(LOAD_PATH_RUNTIME, slug).PathFormat();
            Debug.Log("AssetBundle load..." + asset);
            var abAsset = AssetBundleManager.Load<T>(path);
            if (asset != null)
            {
                return asset as T;
            }
            Debug.LogError("资源丢失：" + path);
            return asset;
#endif
            return null;
        }

        public static string ReadAllText(string slug)
        {
#if UNITY_EDITOR
            var path = Path.GetFullPath(Path.Combine(Application.dataPath, slug));
            return File.ReadAllText(path);
#else
            var txtAsset = LoadTextAsset(slug);
            return txtAsset.text;
#endif
        }
    }
}