﻿using UnityEngine;

namespace Com.BaiZe.GameBase
{
    public enum EPoolID
    {
        DamageShowTxt,
    }

    public class MonoPool : BasePool<GameObject>
    {
        GameObject prefGo;

        public MonoPool(GameObject pref, int count)
        {
            prefGo = pref;
            MaxCap = count;
            for (int i = 0; i < count; i++)
            {
                LoadPref();
            }
        }

        protected override void LoadPref()
        {
            GameObject instance = GameObject.Instantiate<GameObject>(prefGo);
            instance.SetActive(false);
            base.objectMap.Push(instance);
        }

        public GameObject Borrow()
        {
            GameObject go = base.Get();
            go.SetActive(true);
            return go;
        }

        public void Return(GameObject go)
        {
            go.SetActive(false);
            base.PutBack(go);
        }
    }
}