using Newtonsoft.Json;

namespace Com.BaiZe.GameBase
{
    public enum EnumCacheKey
    {

    }

    public class LocalCache
    {
        public static T Get<T>(EnumCacheKey index) => CacheMgr.Get<T>(index.ToString());
        public static void Save<T>(EnumCacheKey index, T data) => CacheMgr.Save<T>(index.ToString(), data);
        public static void Delete(EnumCacheKey index) => CacheMgr.Delete(index.ToString());
        public static void DeleteAll() => CacheMgr.DeleteAll();
    }
}