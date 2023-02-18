using System.Collections.Generic;

using Com.BaiZe.MonoToolSet;

using UnityEngine;

namespace Com.BaiZe.GameBase
{
    public class MonoPoolController : DntdMonoSingleton<MonoPoolController>
    {
        private Dictionary<EPoolID, MonoPool> poolMap = new Dictionary<EPoolID, MonoPool>();

        public MonoPool New(EPoolID poolID, GameObject pref, int cap)
        {
            if (poolMap == null)
            {
                poolMap = new Dictionary<EPoolID, MonoPool>();
            }
            if (!poolMap.ContainsKey(poolID))
            {
                poolMap.Add(poolID, new MonoPool(pref, cap));
            }
            return poolMap[poolID];
        }

        public MonoPool GetPool(EPoolID poolID)
        {
            if (poolMap == null)
            {
                poolMap = new Dictionary<EPoolID, MonoPool>();
            }
            if (!poolMap.ContainsKey(poolID))
            {
                return null;
            }
            return poolMap[poolID];
        }

        public void Delete(EPoolID poolID)
        {
            if (poolMap == null)
            {
                poolMap = new Dictionary<EPoolID, MonoPool>();
            }
            if (!poolMap.ContainsKey(poolID))
            {
                return;
            }
            poolMap.Remove(poolID);
        }
    }
}