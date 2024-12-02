using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei
{
    public class OptionMonitor : IGetter<OptionInfo>
    {
        public IInputOptions    Options      { get; private set; }
        public IInputCollection Collection   { get; private set; }
        public IInputIcons      Icons        { get; private set; }
        public object           Mode         { get; private set; }

        protected virtual void Construct(IInputOptions options, IInputCollection collections, IInputIcons icons) 
        {
            Options    = options;
            Collection = collections;
            Icons      = icons;
        }

        public OptionInfo Get(object id)
        {
            var option = Options.Get(id);
            var entity = Collection.Get(Mode).Get(option.TargetId);
            var key    = option.Positive == EPositive.Positive ? entity.Positive : entity.Negative;
            var icon   = Icons.Get(key);

            var optionName = GetOptionName(option.Identity);

            return new(option.Identity, optionName, option.TargetId, option.Positive, icon);
        }

        public OptionInfo Get(int index)
        {
            var option = Options.Get(index);
            var entity = Collection.Get(Mode).Get(option.TargetId);
            var key    = option.Positive == EPositive.Positive ? entity.Positive : entity.Negative;
            var icon   = Icons.Get(key);

            var optionName = GetOptionName(option.Identity);

            return new(option.Identity, optionName, option.TargetId, option.Positive, icon);
        }

        public IEnumerable<OptionInfo> GetAll()
        {
            
            foreach (var option in Options.GetAll()) 
            {
                var entity = Collection.Get(Mode).Get(option.TargetId);
                var key    = option.Positive == EPositive.Positive ? entity.Positive : entity.Negative;
                var icon   = Icons.Get(key);

                var optionName = GetOptionName(option.Identity);

                yield return new(option.Identity, optionName, option.TargetId, option.Positive, icon);
            }
        }

        protected virtual string GetOptionName(object optionId) 
        {
            return string.Empty;
        }

        public void SetMode(object mode) 
        {
            Mode = mode;
        }
    }

    public struct OptionInfo 
    {
        public OptionInfo(object optionId, string name, object target, EPositive positive, Sprite icon) 
        {
            OptionId   = optionId;
            OptionName = name;
            Target     = target;
            Positive   = positive;
            Icon       = icon;
        }

        public object    OptionId   { get; }
        public string    OptionName { get; }
        public object    Target     { get; }
        public EPositive Positive   { get; }
        public Sprite    Icon       { get; }
    }
}
