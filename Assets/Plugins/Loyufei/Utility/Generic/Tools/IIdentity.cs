using System;
using System.Collections;
using System.Collections.Generic;

namespace Loyufei
{
    public interface IIdentity
    {
        public object Identity { get; }

        public static object NULLId { get; } = new NULL();

        protected class NULL
        {

        }
    }
}