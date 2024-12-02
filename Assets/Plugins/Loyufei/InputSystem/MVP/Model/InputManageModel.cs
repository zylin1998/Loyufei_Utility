using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace Loyufei
{
    public class InputManageModel
    {
        protected virtual void Construct(IInputCollection sample, IInputCollection collection) 
        {
            Default    = sample;
            Collection = collection;
        }

        public IInputCollection Default    { get; private set; }
        public IInputCollection Collection { get; private set; }

        public virtual int Index { get; }
        
        private static readonly KeyCode[] KeyCodes = Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().ToArray();

        public EInputKey GetOrigin(object storageId, object inputId, EPositive positive) 
        {
            
            var entity = Collection.Get(storageId).Get(inputId);
            var origin = positive == EPositive.Positive ? entity.Positive : entity.Negative;

            return origin;
        }

        public void Change(object storageId, object inputId, EInputKey key, EPositive positive) 
        {
            var entity  = Collection.Get(storageId).Get(inputId);
            
            if (positive == EPositive.Positive) { entity.Positive = key; }
            if (positive == EPositive.Negative) { entity.Negative = key; }
        }

        public void Reset(object storageId) 
        {
            Collection.Reset(storageId, Default.Copy(storageId));
        }

        public bool GetInput(EInputKey origin, out EInputKey key) 
        {
            if (!Input.anyKeyDown && (int)origin < 600) 
            {
                key = EInputKey.None; 
                
                return true; 
            }

            if (KeyCodes.FirstOrDefault(k => Input.GetKeyDown(k)).ToKey(out key) && key != EInputKey.None)  
            {
                return key == origin; 
            }

            var gamePad = GamePad.GetState((PlayerIndex)Index);

            if (!gamePad.IsConnected)  { return true; }

            if (gamePad.ToKey(out key) && key != EInputKey.None) { return key == origin; }

            return true;
        }
    }

    public static class InputSystemExtensions
    {
        public static bool ToKey(this KeyCode self, out EInputKey key) 
        {
            var index = (int)self;

            if (index < 330) { key = (EInputKey)index; return true; }

            key = (index % 20).IsClamp(0, 11) ? (EInputKey)(600 + index % 20) : EInputKey.None;

            return key != EInputKey.None;
        }

        public static bool ToKey(this GamePadState self, out EInputKey key) 
        {
            key = EInputKey.None;

            if (!self.IsConnected) { return false; }

            if (self.Buttons    .ToKey(out key)) { return true; }
            if (self.DPad       .ToKey(out key)) { return true; }
            if (self.Triggers   .ToKey(out key)) { return true; }
            if (self.ThumbSticks.ToKey(out key)) { return true; }

            return false;
        }

        public static bool ToKey(this GamePadButtons self, out EInputKey key) 
        {
            if (self.A == ButtonState.Pressed) { key = EInputKey.JoystickA; return true; }
            if (self.B == ButtonState.Pressed) { key = EInputKey.JoystickB; return true; }
            if (self.X == ButtonState.Pressed) { key = EInputKey.JoystickX; return true; }
            if (self.Y == ButtonState.Pressed) { key = EInputKey.JoystickY; return true; }
            
            if (self.Start == ButtonState.Pressed) { key = EInputKey.Start; return true; }
            if (self.Back  == ButtonState.Pressed) { key = EInputKey.Back;  return true; }
            
            if (self.LeftStick  == ButtonState.Pressed) { key = EInputKey.LS_B;  return true; }
            if (self.RightStick == ButtonState.Pressed) { key = EInputKey.RS_B;  return true; }

            if (self.LeftShoulder  == ButtonState.Pressed) { key = EInputKey.LB; return true; }
            if (self.RightShoulder == ButtonState.Pressed) { key = EInputKey.RB; return true; }

            key = EInputKey.None;

            return false;
        }

        public static bool ToKey(this GamePadThumbSticks self, out EInputKey key)
        {
            if (self.Left.X <= -1) { key = EInputKey.LSLeft;  return true; }
            if (self.Left.X >=  1) { key = EInputKey.LSRight; return true; }
            if (self.Left.Y >=  1) { key = EInputKey.LSUp;    return true; }
            if (self.Left.Y <= -1) { key = EInputKey.LSDown;  return true; }

            if (self.Right.X <= -1) { key = EInputKey.RSLeft;  return true; }
            if (self.Right.X >=  1) { key = EInputKey.RSRight; return true; }
            if (self.Right.Y >=  1) { key = EInputKey.RSUp;    return true; }
            if (self.Right.Y <= -1) { key = EInputKey.RSDown;  return true; }

            key = EInputKey.None;

            return false;
        }

        public static bool ToKey(this GamePadDPad self, out EInputKey key)
        {
            if (self.Left  == ButtonState.Pressed) { key = EInputKey.DPADLeft;  return true; }
            if (self.Right == ButtonState.Pressed) { key = EInputKey.DPADRight; return true; }
            if (self.Up    == ButtonState.Pressed) { key = EInputKey.DPADUp;    return true; }
            if (self.Down  == ButtonState.Pressed) { key = EInputKey.DPADDown;  return true; }

            key = EInputKey.None;

            return false;
        }

        public static bool ToKey(this GamePadTriggers self, out EInputKey key)
        {
            if (self.Left  >= 1) { key = EInputKey.LT; return true; }
            if (self.Right >= 1) { key = EInputKey.RT; return true; }

            key = EInputKey.None;

            return false;
        }
    }
}
