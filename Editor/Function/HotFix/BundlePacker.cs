using System.IO;
using System.Text;

using Com.BaiZe.SharpToolSet;

using UnityEditor;

using UnityEngine;

namespace Com.BaiZe.GameBase.Editor
{
    public class AssetBundlePacker
    {
        private static string resPath;
        private static string buildPath;
        private static string versionPath;
        private static string md5Path;

        public static void ExecuteBuild()
        {
            resPath = EditorCache.Get<string>(EnumEditorCacheIndex.PathABFrom);
            buildPath = EditorCache.Get<string>(EnumEditorCacheIndex.PathABTo);
            versionPath = EditorCache.Get<string>(EnumEditorCacheIndex.PathVersion);
            md5Path = EditorCache.Get<string>(EnumEditorCacheIndex.PathMd5);
            // 检查是否存在需要打包AB的路径
            if (!Directory.Exists(resPath))
            {
                EditorTips.ShowErrorTips($"不存在AssetBundle打包路径{resPath}");
                return;
            }
            // 更新版本文件
            using (FileStream fs = new FileStream(versionPath, FileMode.Create))
            {
                fs.SetLength(0);
                fs.Position = 0;
                string nextVersion = File.ReadAllText(versionPath);
                byte[] buffer = Encoding.UTF8.GetBytes(nextVersion);
                fs.Write(buffer, 0, buffer.Length);
                Debug.Log("Build version file success");
            }
            // 清空旧的校验文件
            using (FileStream fs = new FileStream(md5Path, FileMode.Create))
            {
                fs.SetLength(0);
                fs.Position = 0;
            }
            ClearOldAssetBundles();
            Pack(resPath);
            BuildAssetBundles(buildPath, BuildTarget.StandaloneWindows);
            Debug.Log("Build AssetBundle success");
            BuildVerifyFile(buildPath);
            Debug.Log("Build verify file success");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 清除之前保留的AB名
        /// </summary>
        private static void ClearOldAssetBundles()
        {
            string[] assetBundles = AssetDatabase.GetAllAssetBundleNames();
            foreach (string assetBundle in assetBundles)
            {
                AssetDatabase.RemoveAssetBundleName(assetBundle, true);
            }
        }

        /// <summary>
        /// 递归遍历打包AB
        /// </summary>
        /// <param name="path"></param>
        private static void Pack(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] is DirectoryInfo)
                {
                    Pack(files[i].FullName);
                }
                else
                {
                    if (!files[i].FullName.EndsWith(".meta"))
                    {
                        SetName(files[i].FullName);
                    }
                }
            }
        }

        // TODO:路径名存在问题
        /// <summary>
        /// 设置AB名称：文件路径名
        /// </summary>
        /// <param name="file"></param>
        private static void SetName(string file)
        {
            string path = file.PathFormat();
            string assetPath = "Assets" + path.Substring(Application.dataPath.Length);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);

            string namePath = path.Substring(Application.dataPath.Length + 1);
            namePath = namePath.Substring(namePath.IndexOf("/") + 1);
            string name = namePath.Replace(namePath.FileName(), "unity3d");
            assetImporter.assetBundleName = name;
        }

        /// <summary>
        /// 打包AB
        /// </summary>
        /// <param name="buildPath"></param>
        /// <param name="target"></param>
        private static void BuildAssetBundles(string buildPath, BuildTarget target)
        {
            if (Directory.Exists(buildPath))
            {
                Directory.Delete(buildPath, true);
            }
            Directory.CreateDirectory(buildPath);
            BuildPipeline.BuildAssetBundles(buildPath, BuildAssetBundleOptions.None, target);
        }

        private static void BuildVerifyFile(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] is DirectoryInfo)
                {
                    BuildVerifyFile(files[i].FullName);
                }
                else
                {
                    if (!files[i].FullName.EndsWith(".meta"))
                    {
                        string fullName = files[i].FullName.PathFormat();
                        string assetPath = fullName.Substring(Application.dataPath.Length + 1);
                        assetPath = assetPath.Substring(assetPath.IndexOf('/') + 1);
                        string md5 = MD5Helper.Encode(fullName);
                        using (FileStream fs = new FileStream(md5Path, FileMode.OpenOrCreate))
                        {
                            byte[] bytes = new byte[fs.Length];
                            fs.Read(bytes, 0, bytes.Length);
                            string Log = string.Format("{0}|{1}\n", assetPath, md5);
                            byte[] buffer = Encoding.UTF8.GetBytes(Log);
                            fs.Position = bytes.Length;
                            fs.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
        }
    }
}
