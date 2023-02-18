using System.Collections.Generic;
using System.IO;
using Com.BaiZe.SharpToolSet;
using UnityEngine;

namespace Com.BaiZe.GameBase
{
    public class BundleMgr
    {
        // ab缓存映射，ab包只能加载一次
        public static Dictionary<string, AssetBundle> cacheMap = new Dictionary<string, AssetBundle>();

        private static (string, string) GetBundleAndAssetNameByPath(string path)
        {
            return (path.DirectoryPath(), path.FileName());
        }

        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            var assetPair = GetBundleAndAssetNameByPath(path);
            string bundleName = assetPair.Item1;
            string assetName = assetPair.Item2;
            if (null == cacheMap)
                cacheMap = new Dictionary<string, AssetBundle>();

            AssetBundle ab = null;
            if (cacheMap.ContainsKey(bundleName))
            {
                ab = cacheMap[bundleName];
            }
            else
            {
                ab = AssetBundle.LoadFromFile(bundleName);
            }

            AssetBundleManifest manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] dependencies = manifest.GetAllDependencies(bundleName);
            for (int i = 0; i < dependencies.Length; ++i)
            {
                var dep = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, dependencies[i]));
            }
            var asset = ab.LoadAsset<T>(assetName);
            return asset;
        }
    }
}