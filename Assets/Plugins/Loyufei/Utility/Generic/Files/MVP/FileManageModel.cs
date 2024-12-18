using Loyufei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Loyufei
{
    public class FileManageModel
    {
        [Inject]
        protected virtual void Construct(IEnumerable<ISaveSystem> saves) 
        {
            Saves = saves.ToDictionary(k => k.Identity);
        }

        public Dictionary<object, ISaveSystem> Saves { get; private set; }

        public void Save(object id) 
        {
            if (Saves.TryGetValue(id, out var save)) { save.Save(); }
        }
    }
}
