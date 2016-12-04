using AdventureMode.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode.Model
{
    class Elixir : Item, IObservable
    {
        public override int effect() { return DiceRoller.roll_die(Potency_E,8); }
        public override int bonus() { return DiceRoller.roll_die(Potency_B, 8); }
        public string description { get; set; }
        public string color { get; set; }
        public int Doses { get; set; }
        private int max_doses { get; set; }

        public void Describe() {
            description = "This is a bottle of " + this.color + " liquid.\n It has " + Doses + " remaining.";
            if (Potency_E > 0)
            { description += "\n It is a level " + Potency_E + " restorative."; }
            if (Potency_B > 0)
            { description += "\n It is a level " + Potency_B + " stimulant."; }
        }


    }
}
