using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEditor;

namespace Com.BaiZe.GameBase.Editor
{
    public class EditorTips
    {
        public static bool ShowCommonTips(string tips)
        {
            return UnityEditor.EditorUtility.DisplayDialog("Tips", tips, "确认");
        }

        public static bool ShowWarningTips(string tips)
        {
            return UnityEditor.EditorUtility.DisplayDialog("Warning", tips, "确认", "取消");
        }

        public static void ShowErrorTips(string tips)
        {
            UnityEditor.EditorUtility.DisplayDialog("Error", tips, "确认");
        }
    }
}