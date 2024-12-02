using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Loyufei.MapManagement
{
    [CreateAssetMenu(fileName = "EnviromentInstaller", menuName = "Loyufei/Map/Installer")]
    public class EnviromentInstaller : ScriptableObjectInstaller<EnviromentInstaller>
    {
        [SerializeField]
        private ObjectStatesFile _File;
        [SerializeField]
        private Channel[]        _Channels;

        public override void InstallBindings()
        {
            var changed = _File.FetchData(out var saveable);

            var index = 0;
            foreach(var channel in _Channels) 
            {
                var instance = saveable.GetOrAdd(index++, () =>
                {
                    changed = true;

                    return Create(channel);
                });

                Container
                    .Bind<IObjectStates>()
                    .To(instance.GetType())
                    .FromInstance(instance)
                    .AsCached();
            }

            if (changed) { _File.Save(); }

            _File.Save();
        }

        protected virtual IObjectStates Create(Channel channel) 
        {
            return new ObjectStates(channel.Category);
        }

        [Serializable]
        protected class Channel 
        {
            [SerializeField]
            private string _Category;

            public string Category => _Category;
        }
    }

    [Serializable]
    public class ObjectStatesFile : SaveSystem<ObjectStatesSaveable> 
    {

    }

    [Serializable]
    public class ObjectStatesSaveable : IAdjustableSaveable<IObjectStates> 
    {
        [SerializeField]
        private ObjectStates[] _ObjectStates = new ObjectStates[0];

        public IObjectStates GetOrAdd(int index, Func<IObjectStates> add)
        {
            if (index < _ObjectStates.Length) { return _ObjectStates[index]; }

            if (index > _ObjectStates.Length) { return default; }

            var append = add.Invoke().To<ObjectStates>();

            _ObjectStates = _ObjectStates.Append(append).ToArray();

            return append;
        }
    }
}