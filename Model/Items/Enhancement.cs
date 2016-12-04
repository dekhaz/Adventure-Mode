using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode.Model
{
    class Enhancement : Item, IObservable
    {
        public override int effect() { return Potency_E; }
        public override int bonus() { return Potency_B; }
        public string description { get; set; }
        
        
        public string iType { get; set; }
        public string iPairof { get; set; }
        public string material { get; set; }

        public int Ward { get; set; }
        public int Hope { get; set; }

        public void Describe()
        {
            description = "This is a "+this.iPairof+this.material+" "+this.iType + ".";
            if (this.Potency_E > 0)
            { description += "\n Enchantments grant you ease and speed."; }
            if (this.Potency_B > 0)
            { description += "\n Runes of power bolster your might."; }
            if (this.Ward > 0)
            { description += "\n This protects you from harm"; }
            if (this.Hope > 0)
            { description += "\n You feel lucky, or blessed."; }
        }

    }
}
