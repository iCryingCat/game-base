using Com.BaiZe.SharpToolSet;

using UnityEngine;

namespace Com.BaiZe.GameBase.Editor
{
    public class EditorHelper
    {
        public static void OpenAssetByCodeEditor(string assetPath)
        {
            string pathCodeEditor = EditorCache.Get<string>(EnumEditorCacheIndex.PathCodeEditor);
            if (!pathCodeEditor.IsNullOrEmpty())
            {
                System.Diagnostics.Process.Start(pathCodeEditor, assetPath);
            }
        }

        public static bool IsAssetGameObjectIsInPrefabScene(GameObject go, out string assetPath)
        {
            assetPath = null;

            // 判断是否在prefab mode下
            bool isPreviewObj = UnityEditor.SceneManagement.EditorSceneManager.IsPreviewSceneObject(go);
            if (!isPreviewObj)
            {
                EditorTips.ShowErrorTips("请在Prefab Mode下操作！！！");
                return false;
            }

            // PrefabMode中的GameObject既不是Instance也不是Asset
            var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetPrefabStage(go);
            if (prefabStage != null)
            {
                // 预制体资源
                assetPath = prefabStage.assetPath.PascalFormat();
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                EditorTips.ShowErrorTips("Asset资源丢失！！！");
                return false;
            }

            return true;
        }
    }
}