using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
 
    public enum Rarity
    {
        Common,Rare,Obscure,Dimensional
    }

    public abstract class Item
    {

        public int ID { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public Rarity Tier { get; set; }

        public int Potency_E { get; set; }
        public abstract int effect();

        public int Potency_B { get; set; }
        public abstract int bonus();



    }
}

