using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    [Serializable]
    public class SaveSystem<TSaveable> : ISaveSystem<TSaveable> where TSaveable : ISaveable
    {
        [SerializeField]
        protected bool           _LocatePreserve; 
        [SerializeField]
        protected EUnitySavePath _UnitySavePath;
        [SerializeField]
        protected string         _SavePath;
        [SerializeField]
        protected string         _FileName;
        [SerializeField]
        protected string         _Identity;

        public object Identity => _Identity;

        public bool LocatePreserve 
        {
            get => _LocatePreserve; 
            
            set => _LocatePreserve = value; 
        }

        public string SavePath
        {
            get
            {
                var unitySavePath = string.Empty;

                switch (_UnitySavePath)
                {
                    case EUnitySavePath.PersistanceDataPath:
                        unitySavePath = Application.persistentDataPath;
                        break;
                    case EUnitySavePath.DataPath:
                        unitySavePath = Application.dataPath;
                        break;
                    default:
                        unitySavePath = string.Empty;
                        break;
                }

                return Path.Combine(unitySavePath, _SavePath);
            }
        }

        public string FileName => _FileName + ".json";

        public virtual ISaveable Saveable { get; protected set; }

        public virtual bool FetchData(out TSaveable saveable) 
        {
            Saveable = this.Load();

            var shouldCreate = Saveable.IsDefault();

            if (shouldCreate) { Saveable = Activator.CreateInstance<TSaveable>(); }

            saveable = Saveable.To<TSaveable>();

            return shouldCreate;
        }
    }
}
