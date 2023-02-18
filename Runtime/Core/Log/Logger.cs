using UnityEngine;

namespace Com.BaiZe.GameBase
{
    public class Logger
    {
        private string owner;
        private bool logEnable = true;

        public Logger(string owner, bool logEnable = true)
        {
            this.owner = string.Format("{0}.cs", owner);
            this.logEnable = logEnable;
        }

        public void P(object info)
        {
            string infoData = new LogDumper().DumpAsString(info);
            string msg = PrintFormat($"[{owner}]: {infoData}");
            Log(msg);
        }

        public void W(object info)
        {
            string infoData = new LogDumper().DumpAsString(info);
            string msg = WarningFormat($"[{owner}]: {infoData}");
            Log(msg);
        }

        public void E(object info)
        {
            string infoData = new LogDumper().DumpAsString(info);
            string msg = ErrorFormat($"[{owner}]: {infoData}");
            Log(msg);
        }

        private string PrintFormat(string msg)
        {
            msg = ColorFormat("white", msg);
            return msg;
        }

        private string WarningFormat(string msg)
        {
            msg = ColorFormat("yellow", msg);
            return msg;
        }

        private string ErrorFormat(string msg)
        {
            msg = ColorFormat("red", msg);
            return msg;
        }

        private string ThrowFormat(string msg)
        {
            return $"<color=purple>{msg}</color>";
        }

        private string ColorFormat(string color, string msg)
        {
            return $"<color={color}>{msg}</color>";
        }

        private void Log(string msg)
        {
            if (!logEnable) return;
            Debug.Log(msg);
        }
    }
}