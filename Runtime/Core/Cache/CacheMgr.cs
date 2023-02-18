using System;

using Newtonsoft.Json;

using UnityEditor;

using UnityEngine;

namespace Com.BaiZe.GameBase
{
    public static class CacheMgr
    {
        public static T Get<T>(string key)
        {
#if UNITY_EDITOR
            if (!EditorPrefs.HasKey(key))
                return default(T);
            string json = EditorPrefs.GetString(key);
#else
            if (!PlayerPrefs.HasKey(key))
                return default(T);
            string json = PlayerPrefs.GetString(key);
#endif
            T data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public static void Save<T>(string key, T data)
        {
            if (data == null) throw new ArgumentNullException();
            string json = JsonConvert.SerializeObject(data);
#if UNITY_EDITOR
            EditorPrefs.SetString(key, json);
#else
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
#endif
        }

        public static void Delete(string key)
        {
#if UNITY_EDITOR
            if (!EditorPrefs.HasKey(key)) return;
            EditorPrefs.DeleteKey(key);
#else
            if (!PlayerPrefs.HasKey(key)) return;
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
#endif
        }

        public static void DeleteAll()
        {
#if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
#else
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
#endif
        }
    }
}