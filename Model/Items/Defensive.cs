using AdventureMode.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode.Model
{
    class Defensive : Item, IObservable 
    {
        public override int effect() { return Potency_B; }
        public override int bonus() { return Potency_E; }
        public string description { get; set; }
        public string armor_type { get; set; }

        public Sphere Resistance { get; set; }
         
        public void Describe()
        {
            description = ItemName + " is a " + armor_type + ". \n   It provides " + Potency_B + " protection die,\n   and resists " + Potency_E + " damage.";
                if (this.Resistance != Sphere.None) { description += "\n   Grants resistance to " + this.Resistance + " powers."; }
        }

        public int Defensive_Bonus() { return DiceRoller.roll_die(Potency_B, Potency_E); }

    }
}
