using System;

using Newtonsoft.Json;

using UnityEditor;

namespace Com.BaiZe.GameBase.Editor
{
    public enum EnumEditorCacheIndex
    {
        PathCodeEditor,
        PathABFrom,
        PathABTo,
        PathMd5,
        PathVersion,
        PathUnityLuaDefine,
        PathLuaScripts
    }

    public class EditorCache
    {
        public static T Get<T>(EnumEditorCacheIndex index)
        {
            var key = index.ToString();
            if (!EditorPrefs.HasKey(key))
                return default(T);
            var json = EditorPrefs.GetString(key);
            T data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public static void Save<T>(EnumEditorCacheIndex index, T data)
        {
            var key = index.ToString();
            if (data == null) throw new ArgumentNullException();
            var json = JsonConvert.SerializeObject(data);
            EditorPrefs.SetString(key, json);
        }

        public static void Delete(EnumEditorCacheIndex index)
        {
            var key = index.ToString();
            if (!EditorPrefs.HasKey(key)) return;
            EditorPrefs.DeleteKey(key);
        }

        public static void DeleteAll()
        {
            EditorPrefs.DeleteAll();
        }
    }
}