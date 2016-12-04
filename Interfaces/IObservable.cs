using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
    interface IObservable
    {
        string description { get; set; }
        void Describe();
    }
}
