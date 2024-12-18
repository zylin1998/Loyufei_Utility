using UnityEngine;
using Zenject;

namespace Loyufei
{
    public class FileInstallAsset<TSaveable, TChannel> : ScriptableObjectInstaller<FileInstallAsset<TSaveable, TChannel>>
        where TSaveable : ISaveable where TChannel : IChannel
    {
        [SerializeField]
        private SaveSystem<TSaveable> _File;
        [SerializeField]
        private IndependentFileAsset _IdependentFile;
        [SerializeField]
        private TChannel[] _Channels;

        public ISaveSystem<TSaveable> File
            => _IdependentFile.To<ISaveSystem<TSaveable>>() ?? _File;

        public override void InstallBindings()
        {
            var changed = File.FetchData(out var saveable);

            BindingAssets();
            
            var index = 0;
            foreach (var channel in _Channels)
            {
                channel.TrySet(File.Saveable);

                changed = BindChannel(index, channel) || changed;
                
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

        protected virtual bool BindChannel(int index, TChannel channel) 
        {
            return false;
        }

        public class Channel<TCreate> : IChannel 
        {
            [Header("¸s²Õ³]©w")]
            [SerializeField]
            private string _Identity;
            
            public object Identity => _Identity;

            protected IAdjustableSaveable<TCreate> _Saveable;

            public bool TrySet(ISaveable saveable)
            {
                if (saveable is IAdjustableSaveable<TCreate> adjust)
                {
                    _Saveable = adjust;

                    return true;
                }

                return false;
            }

            public virtual object GetOrCreate(int index, out bool hasCreate)
            {
                hasCreate = false;

                return default;
            }
        }
    }

    public interface IChannel : IIdentity
    {
        public object GetOrCreate(int index, out bool hasCreate);

        public bool TrySet(ISaveable saveable);
    }
}