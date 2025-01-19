using System;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using Zenject;

namespace Loyufei
{
    public class IndependentFileAsset<TSaveable> : IndependentFileAsset, ISaveSystem<TSaveable> where TSaveable : ISaveable
    {
        public virtual bool FetchData(out TSaveable saveable)
        {
            Saveable = this.Load();

            var shouldCreate = Saveable.IsDefault();

            if (shouldCreate) { Saveable = Activator.CreateInstance<TSaveable>(); }

            saveable = Saveable.To<TSaveable>();

            return shouldCreate;
        }

        public override bool FetchData(out ISaveable saveable)
        {
            var save = default(TSaveable);

            var result = FetchData(out save);

            saveable = save;

            return result;
        }
    }

    public class IndependentFileAsset : ScriptableObjectInstaller<IndependentFileAsset>, ISaveSystem
    {
        [SerializeField]
        protected bool _LocatePreserve;
        [SerializeField]
        protected EUnitySavePath _UnitySavePath;
        [SerializeField]
        protected string _SavePath;
        [SerializeField]
        protected string _FileName;
        [SerializeField]
        protected string _Identity;

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

        public ISaveable Saveable { get; protected set; }

        public virtual bool FetchData(out ISaveable saveable) 
        {
            saveable = default;

            return false;
        }

        public override void InstallBindings()
        {
            Container
                .Bind<ISaveSystem>()
                .To(GetType())
                .FromInstance(this)
                .AsCached();
        }
    }
}