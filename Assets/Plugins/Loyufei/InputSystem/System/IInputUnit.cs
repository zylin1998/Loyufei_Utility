using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace Loyufei
{
    public interface IInputUnit
    {
        public float  Axis        { get; }
        public float  AxisRaw     { get; }
        public bool   GetKeyDown  { get; }
        public bool   GetKey      { get; }
        public bool   GetKeyUp    { get; }

        #region Default

        public static IInputUnit Default { get; } = new DefaultInputUnit();

        private struct DefaultInputUnit : IInputUnit
        {
            public float  Axis        => 0;
            public float  AxisRaw     => 0;
            public bool   GetKeyDown  => false;
            public bool   GetKey      => false;
            public bool   GetKeyUp    => false;
        }

        #endregion
    }

    public class InputUnit : IInputUnit
    {
        public InputUnit(InputEntity entity, int player)
        {
            Reset(entity, player);
        }

        public void Reset(InputEntity entity, int player)
        {
            _Entity = entity;
            _Player = player;
        }

        private InputEntity _Entity;
        private int         _Player;

        private float _HoldTime     = 0f;
        private float _ReleasedTime = 0f;
        private float _DeltaTime    = 0f;

        public float Axis
        {
            get
            {
                var axisRaw = AxisRaw;
                
                return Hold(!Equals(axisRaw, 0f)).Clamp01() * axisRaw;
            }
        }

        public float AxisRaw
        {
            get
            {
                var positive = _Entity.Positive;
                var negative = _Entity.Negative;

                if (GetValue(positive)) { return 1; }

                if (GetValue(negative)) { return -1; }

                return 0;
            }
        }

        public bool GetKeyDown
        {
            get
            {
                if (TryGetKeyCode(_Entity.Positive, out var key)) { return Input.GetKeyDown(key); }

                if (GetValue(_Entity.Positive))
                {
                    (_ReleasedTime, _DeltaTime) = _ReleasedTime == 0 ? (0, _DeltaTime) : (0, 0);

                    _DeltaTime = _DeltaTime == 0 ? Time.deltaTime : _DeltaTime;

                    return Hold(true) <= _DeltaTime;
                }

                return false;
            }
        }

        public bool GetKey 
        {
            get 
            {
                if (TryGetKeyCode(_Entity.Positive, out var key)) { return Input.GetKey(key); }

                if (GetValue(_Entity.Positive))
                {
                    return Hold(true) >= _DeltaTime && !GetKeyDown;
                }

                return false;
            }
        }

        public bool GetKeyUp 
        {
            get 
            {
                if (TryGetKeyCode(_Entity.Positive, out var key)) { return Input.GetKeyUp(key); }

                if (_HoldTime == 0 && _ReleasedTime == 0) { return false; }

                if (!GetValue(_Entity.Positive))
                {
                    (_HoldTime, _DeltaTime) = _HoldTime == 0 ? (0, _DeltaTime) : (0, 0);

                    _DeltaTime = _DeltaTime == 0 ? Time.deltaTime : _DeltaTime;

                    return Release(true) <= _DeltaTime;
                }

                return false;
            }
        }

        private bool GetValue(EInputKey key) 
        {
            var index = (int)key;

            if (index <  600) 
            {
                return Input.GetKey((KeyCode)key);
            }

            if (index >= 600) 
            {
                return GetValue(GamePad.GetState((PlayerIndex)_Player), key);
            }

            return false;
        }

        private float Hold(bool isHold) 
        {
            if (!isHold) { return (_HoldTime = 0f); }

            if (_HoldTime == 0) { _HoldTime = Time.realtimeSinceStartup;  }

            return Time.realtimeSinceStartup - _HoldTime;
        }

        private float Release(bool isRelease)
        {
            if (!isRelease) { return (_ReleasedTime = 0f); }

            if (_ReleasedTime == 0) { _ReleasedTime = Time.realtimeSinceStartup; }

            return Time.realtimeSinceStartup - _ReleasedTime;
        }

        private bool GetValue(GamePadState state, EInputKey key) 
        {
            if (key == EInputKey.JoystickA) { return state.Buttons.A == ButtonState.Pressed; }
            if (key == EInputKey.JoystickB) { return state.Buttons.B == ButtonState.Pressed; }
            if (key == EInputKey.JoystickX) { return state.Buttons.X == ButtonState.Pressed; }
            if (key == EInputKey.JoystickY) { return state.Buttons.Y == ButtonState.Pressed; }
            
            if (key == EInputKey.Start) { return state.Buttons.Start == ButtonState.Pressed; }
            if (key == EInputKey.Back)  { return state.Buttons.Back  == ButtonState.Pressed; }
            
            if (key == EInputKey.LS_B)  { return state.Buttons.LeftStick  == ButtonState.Pressed; }
            if (key == EInputKey.RS_B)  { return state.Buttons.RightStick == ButtonState.Pressed; }

            if (key == EInputKey.LB) { return state.Buttons.LeftShoulder  == ButtonState.Pressed; }
            if (key == EInputKey.RB) { return state.Buttons.RightShoulder == ButtonState.Pressed; }

            if (key == EInputKey.DPADUp)    { return state.DPad.Up    == ButtonState.Pressed; }
            if (key == EInputKey.DPADDown)  { return state.DPad.Down  == ButtonState.Pressed; }
            if (key == EInputKey.DPADLeft)  { return state.DPad.Left  == ButtonState.Pressed; }
            if (key == EInputKey.DPADRight) { return state.DPad.Right == ButtonState.Pressed; }

            if (key == EInputKey.LT) { return state.Triggers.Left  > 0; }
            if (key == EInputKey.RT) { return state.Triggers.Right > 0; }

            if (key == EInputKey.LSUp)    { return state.ThumbSticks.Left.Y > 0; }
            if (key == EInputKey.LSDown)  { return state.ThumbSticks.Left.Y < 0; }
            if (key == EInputKey.LSLeft)  { return state.ThumbSticks.Left.X < 0; }
            if (key == EInputKey.LSRight) { return state.ThumbSticks.Left.X > 0; }

            if (key == EInputKey.RSUp)    { return state.ThumbSticks.Right.Y > 0; }
            if (key == EInputKey.RSDown)  { return state.ThumbSticks.Right.Y < 0; }
            if (key == EInputKey.RSLeft)  { return state.ThumbSticks.Right.X < 0; }
            if (key == EInputKey.RSRight) { return state.ThumbSticks.Right.X > 0; }

            return false;
        }

        private bool TryGetKeyCode(EInputKey key, out KeyCode keyCode) 
        {
            var result = (int)key <= 329;

            keyCode = (KeyCode)key;

            return result;
        }
    }
}