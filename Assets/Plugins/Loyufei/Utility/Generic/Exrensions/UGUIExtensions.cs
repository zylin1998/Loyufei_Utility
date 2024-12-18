using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Loyufei
{
    public static class UGUIExtensions
    {
        public static void Set(this Image self, Sprite sprite, Color color) 
        {
            self.sprite = sprite;
            self.color  = color;
        }
    }
}
