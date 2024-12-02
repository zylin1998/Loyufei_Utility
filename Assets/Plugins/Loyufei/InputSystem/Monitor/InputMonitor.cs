using System;
using System.Collections.Generic;
using System.Linq;

namespace Loyufei
{
    public class InputMonitor : IGetter<IInput>
    {
        public IInput[] Inputs { get; private set; }

        public object InputMode { get; private set; }

        protected virtual void Construct(IEnumerable<IInput> inputs) 
        {
            Inputs = inputs.ToArray();
        }

        public IInput Get(object id)
        {
            return Inputs.FirstOrDefault(i => Equals(i.Index, id)) ?? IInput.Default;
        }

        public IInput Get(int index)
        {
            return index < Inputs.Length ? Inputs[index] : IInput.Default;
        }

        public IEnumerable<IInput> GetAll()
        {
            return Inputs;
        }

        public void SetMode(object mode) 
        {
            InputMode = mode;

            foreach(var input in Inputs) 
            {
                input.Reset(mode);
            }
        }
    }
}
