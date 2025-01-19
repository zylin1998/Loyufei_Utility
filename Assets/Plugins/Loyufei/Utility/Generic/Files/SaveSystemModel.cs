using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei
{
    public class SaveSystemModel
    {
        public SaveSystemModel(IEnumerable<ISaveSystem> saves) 
        {
            Saves = saves.ToDictionary(k => k.Identity);
        }

        public Dictionary<object, ISaveSystem> Saves { get; }

        public void Save(object key) 
        {
            if (Saves.TryGetValue(key, out var save)) 
            {
                save.Save();
            }
        }
    }
}
