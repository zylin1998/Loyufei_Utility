using System;
using UnityEngine;
using Zenject;

namespace Loyufei
{
    public class FileInstallAsset<TData, TChannel> : ScriptableObjectInstaller<FileInstallAsset<TData, TChannel>>where TChannel : IChannel
    {
        [SerializeField]
        private SaveSystem<TemplateData<TData>> _File;
        [SerializeField]
        private bool _UseIndependentFile;
        [SerializeField]
        private IndependentFileAsset _IdependentFile;
        [SerializeField]
        private TChannel[] _Channels;

        public ISaveSystem File => _UseIndependentFile ? _IdependentFile : _File;

        public override void InstallBindings()
        {
            var changed = File.FetchData(out var saveable);

            BindingAssets();
            
            var index = 0;
            foreach (var channel in _Channels)
            {
                channel.TrySet(File.Saveable);

                changed = BindChannel(channel) || changed;
                
                index++;
            }
            
            if (changed) { File.Save(); }

            if (File is IndependentFileAsset asset) { asset.Bind(Container); }
            
            else 
            {
                Container
                    .Bind<ISaveSystem>()
                    .To(File.GetType())
                    .FromInstance(File)
                    .AsCached();
            }
        }

        protected virtual void BindingAssets() 
        {

        }

        protected virtual bool BindChannel(TChannel channel) 
        {
            var instance = (TData)channel.GetOrCreate(out var hasCreate);

            Container
                .Bind<TData>()
                .WithId(channel.Identity)
                .FromInstance(instance)
                .AsCached();

            return hasCreate;
        }

        public class Channel : IChannel 
        {
            [Header("¸s²Õ³]©w")]
            [SerializeField]
            private string _Identity;
            
            public object Identity => _Identity;
            
            protected IAdjustableSaveable<TData> _Saveable;

            public bool TrySet(ISaveable saveable)
            {
                if (saveable is IAdjustableSaveable<TData> adjust)
                {
                    _Saveable = adjust;

                    return true;
                }

                return false;
            }

            public virtual object GetOrCreate(out bool hasCreate)
            {
                var added = false;

                var result = _Saveable.GetOrAdd(Identity, () =>
                {
                    added = true;

                    return Activator.CreateInstance<TData>();
                });

                hasCreate = added;

                return result;
            }
        }
    }

    public interface IChannel : IIdentity
    {
        public object GetOrCreate(out bool hasCreate);

        public bool TrySet(ISaveable saveable);
    }
}