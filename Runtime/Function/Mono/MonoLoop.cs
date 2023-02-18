using System;

using Com.BaiZe.MonoToolSet;

namespace Com.BaiZe.GameBase
{
    public class MonoLoop : DntdMonoSingleton<MonoLoop>
    {
        private event Action onUpdate;
        private event Action onFixedUpdate;
        private event Action onLateUpdate;

        private void Start()
        {
            Loom.Setup();
        }

        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }

        private void FixedUpdate()
        {
            if (onFixedUpdate != null) onFixedUpdate();
        }

        private void LateUpdate()
        {
            if (onLateUpdate != null) onLateUpdate();
        }

        public void AddUpdateListener(Action action)
        {
            onUpdate += action;
        }
        public void AddFixedUpdateListener(Action action)
        {
            onFixedUpdate += action;
        }

        public void AddLateUpdateListener(Action action)
        {
            onLateUpdate += action;
        }

        public void RemoveUpdateListener(Action action)
        {
            onUpdate -= action;
        }

        public void RemoveFixedUpdateListener(Action action)
        {
            onFixedUpdate -= action;
        }

        public void RemoveLateUpdateListener(Action action)
        {
            onLateUpdate -= action;
        }
    }
}