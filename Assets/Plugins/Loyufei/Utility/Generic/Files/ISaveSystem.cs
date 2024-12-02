using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei
{
    public interface ISaveSystem : IIdentity
    {
        public bool      LocatePreserve { get; set; }
        public string    SavePath       { get; }
        public string    FileName       { get; }
        public ISaveable Saveable       { get; }

        public bool FetchData(out ISaveable saveable);
    }

    public interface ISaveSystem<TSaveable> : ISaveSystem where TSaveable : ISaveable 
    {
        public bool FetchData(out TSaveable saveable);

        bool ISaveSystem.FetchData(out ISaveable saveable) => FetchData(out saveable);
    }
}
