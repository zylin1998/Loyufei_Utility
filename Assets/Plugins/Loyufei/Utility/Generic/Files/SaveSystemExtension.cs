using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    public static class SaveSystemExtensions
    {
        public static void Save(this ISaveSystem self, bool prettyPrint = true)
        {
            if (!self.LocatePreserve) { return; }

            if (!Directory.Exists(self.SavePath))
            {
                Directory.CreateDirectory(self.SavePath);
            }

            var json = JsonUtility.ToJson(self.Saveable, prettyPrint);
            var path = Path.Combine(self.SavePath, self.FileName);

            File.WriteAllText(path, json);
        }

        public static T Load<T>(this ISaveSystem self) where T : ISaveable
        {
            if (!self.LocatePreserve) { return default; }

            var path = Path.Combine(self.SavePath, self.FileName);
            var exist = File.Exists(path);

            if (!exist) { return default; }

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);

            return data;
        }

        public static T Load<T>(this ISaveSystem<T> self) where T : ISaveable
        {
            return ((ISaveSystem)self).Load<T>();
        }
    }
}
