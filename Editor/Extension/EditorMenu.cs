using UnityEditor;

using UnityEngine;

namespace Com.BaiZe.GameBase.Editor
{
    public class EditorMenu
    {
        private const string ROOT_MENU = "U2/";

        [MenuItem(ROOT_MENU + "切换资源加载方式/Editor")]
        public static void DebugResourceLoadMode()
        {
            Menu.SetChecked(ROOT_MENU + "切换资源加载方式/Debug", true);
        }

        [MenuItem(ROOT_MENU + "切换资源加载方式/Runtime")]
        public static void ReleaseResourceLoadMode()
        {
            Menu.SetChecked(ROOT_MENU + "切换资源加载方式/Debug", true);
        }

        [MenuItem(ROOT_MENU + "更新 unity.d.lua _F4")]
        public static void ExportUnityLuaDeclarations()
        {
            UnityLuaDefineExporter.ExecuteExport();
        }

        [MenuItem(ROOT_MENU + "打包 AssetBundle", false)]
        public static void BuildAssetBundle()
        {
            AssetBundlePacker.ExecuteBuild();
        }
    }
}