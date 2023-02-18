using System;
using System.Collections;

using UnityEngine;

namespace Com.BaiZe.GameBase
{
    // 定时回调
    public class Timer
    {
        public Timer(float duration, Action callback)
        {
            MonoLoop.Instance.StartCoroutine(Timing(duration, callback));
        }

        IEnumerator Timing(float duration, Action callback)
        {
            yield return new WaitForSeconds(duration);
            callback?.Invoke();
        }
    }

}