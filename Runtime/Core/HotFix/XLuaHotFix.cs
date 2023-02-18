using System;
using System.IO;
#if XLua
using XLua;

namespace Com.BaiZe.GameBase
{
    public class XLuaHotFix : Singleton<XLuaHotFix>
    {
        private LuaEnv luaEnv;

        public void LoadHotFix(Action callback)
        {
            if (luaEnv == null)
                luaEnv = new LuaEnv();
            string path = PathUtil.HotFixLuaFileSavePath_Editor;
            if (!Directory.Exists(path))
            {
                return;
            }
            string[] luaFiles = Directory.GetFiles(path);
            int filesCount = luaFiles.Length;
            for (int i = 0; i < filesCount; i++)
            {
                using (StreamReader reader = new StreamReader(luaFiles[i]))
                {
                    string buff = reader.ReadToEnd();
                    luaEnv.DoString(buff);
                }
            }
            luaEnv.Dispose();

            if (callback != null)
                callback.Invoke();
        }
    }
}
#endif