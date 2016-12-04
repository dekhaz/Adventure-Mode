using AdventureMode.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode.Model
{
    class Armament : Item, IObservable
    {

        public override int effect() { return Potency_B; }
        public override int bonus() { return Potency_E; }
        public string description { get; set; }
        public string weapon_type { get; set; }
        public Sphere Elemental { get; set; }
        public bool Enchanted { get; set; }
        public Sphere Enchantment { get; set; }
        public void Describe()
        {
            description = ItemName + " is a " + weapon_type + ". \n   It grants " + Potency_B + " combat die,\n   and deals " + Potency_E + " " + this.Elemental + " damage.";
        }


        public int Combat_Modifier(Sphere tSph) { return ((DiceRoller.roll_die((this.Potency_B), (this.Potency_E)) * (ActionHandler.sphere_comparison(this.Elemental, tSph))) / 4); }
        public int Enchanted_Modifier(Sphere tSph)
        {
            return ((DiceRoller.roll_die((this.Potency_B), (this.Potency_E)) * (ActionHandler.sphere_comparison(this.Enchantment, tSph))) / 4);
        }
    }
}
