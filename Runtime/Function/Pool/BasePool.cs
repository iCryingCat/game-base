using System.Collections.Generic;

namespace Com.BaiZe.GameBase
{
    // 对象池基类
    public abstract class BasePool<T>
    {
        // 最大容量
        private int maxCap;

        // 储存栈
        protected Stack<T> objectMap = new Stack<T>();

        // 激活的对象列表
        protected List<T> goMap = new List<T>();

        public int MaxCap { get => maxCap; protected set => maxCap = value; }

        protected abstract void LoadPref();

        public void Expand(int extraCap = 5)
        {
            MaxCap += extraCap;
            for (int i = 0; i < extraCap; i++)
            {
                LoadPref();
            }
        }

        // 取出对象
        protected T Get()
        {
            if (objectMap == null)
            {
                objectMap = new Stack<T>();
            }
            if (objectMap.Count == 0)
            {
                Expand();
            }
            T go = objectMap.Pop();
            if (goMap == null)
            {
                goMap = new List<T>();
            }
            goMap.Add(go);
            return go;
        }

        // 归还对象
        protected void PutBack(T go)
        {
            if (objectMap == null)
            {
                objectMap = new Stack<T>();
            }
            if (goMap == null)
            {
                goMap = new List<T>();
            }
            if (goMap.Count > 0)
            {
                goMap.Remove(go);
            }
            objectMap.Push(go);
        }
    }
}