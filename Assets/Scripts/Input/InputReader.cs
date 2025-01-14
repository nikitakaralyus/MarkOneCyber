﻿using UnityEngine;
using UnityEngine.Events;

namespace MarkOne.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Input/Input Reader")]
    public class InputReader : ScriptableObject
    {
        public event UnityAction<MoveDirection> MoveEvent = delegate { };
        public event UnityAction JumpEvent = delegate { };
        public event UnityAction SlideEvent = delegate { };

        public void InvokeMoveEvent(MoveDirection direction) => MoveEvent?.Invoke(direction);
        public void InvokeJumpEvent() => JumpEvent?.Invoke();
        public void InvokeSlideEvent() => SlideEvent?.Invoke();

        public enum MoveDirection
        {
            Left = -1,
            None = 0,
            Right = 1
        }
    }
}