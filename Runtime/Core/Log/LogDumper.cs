using System;
using System.Collections;
using System.Reflection;
using System.Text;

using UnityEngine;

namespace Com.BaiZe.GameBase
{
    public class LogDumper
    {
        private readonly StringBuilder _text = new StringBuilder("", 1024);

        private void AppendIndent(int num)
        {
            _text.Append(' ', num);
        }

        private void DoDump(object obj, int depth = 100, bool showField = true)
        {
            if (obj == null)
            {
                _text.Append("null");
                _text.Append(",");
                return;
            }

            if (depth == 0)
            {
                _text.Append("DEPTH_NULL,");
                return;
            }

            Type t = obj.GetType();

            if (obj is IList)
            {
                _text.Append("[");
                IList list = obj as IList;
                foreach (object v in list)
                {
                    DoDump(v, depth, showField);
                }

                _text.Append("]");
            }
            else if (t.IsValueType)
            {
                _text.Append(obj);
                _text.Append(",");
                AppendIndent(1);
            }
            else if (obj is string)
            {
                _text.Append("\"");
                _text.Append(obj);
                _text.Append("\"");
                _text.Append(",");
                AppendIndent(1);
            }
            else if (obj is byte[])
            {
                _text.Append("\"");
                _text.Append(Encoding.UTF8.GetString((byte[])obj));
                _text.Append("\"");
                _text.Append(",");
                AppendIndent(1);
            }
            else if (t.IsArray)
            {
                Array a = (Array)obj;
                _text.Append("[");
                for (int i = 0; i < a.Length; i++)
                {
                    _text.Append(i);
                    _text.Append(":");
                    DoDump(a.GetValue(i), depth, showField);
                }

                _text.Append("]");
            }
            else if (t.IsClass)
            {
                _text.Append($"<{t.Name}>");
                _text.Append("{");
                var props = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (props.Length > 0)
                {
                    foreach (PropertyInfo info in props)
                    {
                        _text.Append(info.Name);
                        _text.Append(":");
                        object value = info.GetGetMethod().Invoke(obj, null);
                        DoDump(value, depth - 1, showField);
                    }
                }
                var fields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (showField && fields.Length > 0)
                {
                    foreach (FieldInfo info in fields)
                    {
                        _text.Append(info.Name);
                        _text.Append(":");
                        object value = info.GetValue(obj);
                        DoDump(value, depth - 1, showField);
                    }
                }

                _text.Append("}");
            }
            else
            {
                Debug.LogWarning("unsupport type: " + t.FullName);
                _text.Append(obj);
                _text.Append(",");
                AppendIndent(1);
            }
        }

        public string DumpAsString(object obj, int depth = 100, bool showField = true, string hint = "")
        {
            _text.Clear();
            _text.Append(hint);
            DoDump(obj, depth, showField);
            return _text.ToString();
        }
    }
}