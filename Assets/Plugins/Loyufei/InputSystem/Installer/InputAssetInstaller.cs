using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    [CreateAssetMenu(fileName = "InputInstaller", menuName = "Loyufei/InputSystem/InputInstaller")]
    public class InputAssetInstaller : FileInstallAsset<InputAssetInstaller.Saveable, InputAssetInstaller.Channel>
    {
        [SerializeField]
        private DefaultInputAsset _DefaultInput;
        [SerializeField]
        private InputIconAsset    _InputIconAsset;
        [SerializeField]
        private InputOptionAsset  _InputOptions;
        
        protected override void BindingAssets()
        {
            Container
                .Bind<IInputCollection>()
                .To(_DefaultInput.GetType())
                .FromInstance(_DefaultInput)
                .AsCached();

            Container
                .Bind<IInputIcons>()
                .To(_InputIconAsset.GetType())
                .FromInstance(_InputIconAsset)
                .AsSingle();

            Container
                .Bind<IInputOptions>()
                .To(_InputOptions.GetType())
                .FromInstance(_InputOptions)
                .AsSingle();
        }

        protected override bool BindChannel(int index, Channel channel)
        {
            channel.Default = _DefaultInput;

            var instance = channel.GetOrCreate(index, out var hasCreate);
            
            Container
                .Bind<IInputCollection>()
                .WithId(channel.Identity)
                .To(instance.GetType())
                .FromInstance(instance)
                .AsCached();

            return hasCreate;
        }

        [Serializable]
        public class Channel : Channel<IInputCollection>
        {
            public DefaultInputAsset Default { get; set; }

            public override object GetOrCreate(int index, out bool hasCreate) 
            {
                var added = false;
                
                var instance = _Saveable.GetOrAdd(index, () =>
                {
                    added = true;
                    
                    return new InputCollection();
                });

                foreach (var entity in Default)
                {
                    instance.Reset(entity.Identity, entity.Data.Select(e => e.Data.Copy()));
                }

                hasCreate = added;

                return instance;
            }
        }

        [Serializable]
        public class Saveable : IAdjustableSaveable<IInputCollection>
        {
            [SerializeField]
            private InputCollection[] _Collections = new InputCollection[0];

            public IInputCollection GetOrAdd(int index, Func<IInputCollection> add)
            {
                if (index < _Collections.Length) { return _Collections[index]; }

                if (index > _Collections.Length) { return default; }

                var collection = add.Invoke().To<InputCollection>();

                _Collections = _Collections.Append(collection).ToArray();

                return collection;
            }
        }
    }
}
