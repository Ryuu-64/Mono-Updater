using System;
using UnityEngine;

namespace Ryuu
{
    public sealed class MonoUpdater : MonoBehaviour
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private void Update() => OnUpdate?.Invoke();

        private void FixedUpdate() => OnFixedUpdate?.Invoke();

        private void LateUpdate() => OnLateUpdate?.Invoke();
        
        public void Subscribe(Action action, UpdateMode updateMode)
        {
            Unsubscribe(action);
            switch (updateMode)
            {
                case UpdateMode.None:
                    break;
                case UpdateMode.Update:
                    OnUpdate += action;
                    break;
                case UpdateMode.FixedUpdate:
                    OnFixedUpdate += action;
                    break;
                case UpdateMode.LateUpdate:
                    OnLateUpdate += action;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(updateMode), updateMode, null);
            }
        }

        public void Unsubscribe(Action action)
        {
            OnUpdate -= action;
            OnFixedUpdate -= action;
            OnLateUpdate -= action;
        }
    }
}