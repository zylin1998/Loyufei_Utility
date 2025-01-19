using System;
using UnityEngine;
using Zenject;

namespace Loyufei
{
    public class FileInstallAsset<TData, TChannel> : ScriptableObjectInstaller<FileInstallAsset<TData, TChannel>>where TChannel : IChannel<TData>
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
                channel.TrySet(saveable);

                changed = BindChannel(channel) || changed;
                
                index++;
            }
            
            if (changed) { File.Save(); }

            if (File is IndependentFileAsset asset) { return; }
            
            Container
                .Bind<ISaveSystem>()
                .To(File.GetType())
                .FromInstance(File)
                .AsCached();
        }

        protected virtual void BindingAssets() 
        {

        }

        protected virtual bool BindChannel(TChannel channel) 
        {
            var created = channel.GetOrCreate(out var instance);

            Container
                .Bind<TData>()
                .WithId(channel.Identity)
                .FromInstance(instance)
                .AsCached();
            
            return created;
        }
    }

    [Serializable]
    public class Channel<TData> : IChannel<TData> 
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

        public virtual bool GetOrCreate(out TData result)
        {
            var created = false;

            result = _Saveable.GetOrAdd(Identity, () =>
            {
                created = true;

                return Activator.CreateInstance<TData>();
            });

            return created;
        }
    }

    public interface IChannel<T> : IIdentity
    {
        public bool GetOrCreate(out T result);

        public bool TrySet(ISaveable saveable);
    }
}