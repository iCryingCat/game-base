using System;
using System.IO;
using System.Text;
using Com.BaiZe.SharpToolSet;
using UnityEngine;
using UnityEngine.UI;

namespace Com.BaiZe.GameBase.Editor
{
    public class UnityLuaDefineExporter
    {
        private const string DECLARE_DEFINE = "{0} = CS.{1}.{2}";
        private const string UNITY_D_LUA = "unity.d.lua";
        private const string CS_D_LUA = "cs.d.lua";

        public static void ExecuteExport()
        {
            var slugDirUnityLuaDefine = EditorCache.Get<string>(EnumEditorCacheIndex.PathUnityLuaDefine);
            if (slugDirUnityLuaDefine.IsNullOrEmpty()) return;

            var dirPath = Path.GetFullPath(Path.Combine(Application.dataPath, slugDirUnityLuaDefine));
            if (!Directory.Exists(dirPath))
            {
                var confirm = EditorTips.ShowWarningTips("是否创建文件夹：{0}".Format(dirPath));
                if (confirm)
                    Directory.CreateDirectory(dirPath);
                else
                    return;
            }

            var unity2LuaSB = new StringBuilder();
            var cs2LuaSB = new StringBuilder();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var nameSpace = type.Namespace;
                    var isMatchCustomNamespace = nameSpace == "Com.BaiZe.U2Framework" || nameSpace == "Com.BaiZe.U2Framework.UI";
                    var isUnityComponent = (nameSpace == "UnityEngine" || nameSpace == "UnityEngine.UI")
                    && (type.IsClass && type.IsSubclassOf(typeof(Component)) || type.IsEnum || !type.IsPrimitive && type.IsValueType);

                    if (type.IsPublic && !type.IsGenericTypeDefinition)
                    {
                        if (isUnityComponent)
                        {
                            var define = DECLARE_DEFINE.Format(type.Name, type.Namespace, type.Name);
                            unity2LuaSB.AppendLine(define);
                        }
                        else if (isMatchCustomNamespace)
                        {
                            var define = DECLARE_DEFINE.Format(type.Name, type.Namespace, type.Name);
                            cs2LuaSB.AppendLine(define);
                        }
                    }
                }
            }

            var unity2Lua = Path.Combine(dirPath, UNITY_D_LUA).PathFormat();
            var unity2LuaTxt = unity2LuaSB.ToString();
            using (FileStream fs = new FileStream(unity2Lua, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0);
                var bytes = Encoding.UTF8.GetBytes(unity2LuaTxt);
                fs.Write(bytes, 0, bytes.Length);
            }

            var cs2Lua = Path.Combine(dirPath, CS_D_LUA).PathFormat();
            var cs2LuaTxt = cs2LuaSB.ToString();
            using (FileStream fs = new FileStream(cs2Lua, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0);
                var bytes = Encoding.UTF8.GetBytes(cs2LuaTxt);
                fs.Write(bytes, 0, bytes.Length);
            }
            EditorTips.ShowCommonTips("Generate Finish!");
        }
    }
}