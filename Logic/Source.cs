using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
    class Source<T>
    {
        public Source(T t)
        {
            data = t;
        }

        private T data;
        public T Data { get { return data; } set { data = value; } }
    }
}
