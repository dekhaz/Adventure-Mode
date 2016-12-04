using AdventureMode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
    [FlagsAttribute] public enum Sphere
    {
        None = 0x0,
        Mortal = 0x1,
        Geotic = 0x2,
        Aquatic = 0x4,
        Industrial = 0x8,
        Ethereal = 0x10,
        Tempest = 0x20,
        Void = 0x40,
        Boreal = 0x80,
        Kindled = 0x100,
        Occult = 0x200,
        Primal = 0x400,
        Nemesis = 0x800,
        Esoteric = 0x1000,
        Celestial = 0x2000,
        Stellar = 0x4000,
        Death = 0x8000
    }

    public enum Spell
    {
        Magic_Bolt,Magic_Blast,Magic_Beam,

        Magic_Shield,Magic_Aura,Magic_Restoration,

        Magic_Haste, Magic_Courage, Magic_Fear,

        Pact_Spellstrike, Pact_Reprisal, Pact_Drain, Pact_Annihilate,

        Pact_Weapon,Pact_Armor,Pact_Shift

    } 


    class Magic: IObservable
    {
        public Sphere sphere { get; set; }
        public Spell spell { get; set; }
        public int cost { get; set; }
        public string description { get; set; }

        public void Describe()
        {
            switch (this.spell)
            {
                case Spell.Magic_Aura:
                    description = "Empower yourself with " + StringLogic.adjective_by_sphere(this.sphere) + " magic."; 
                    break;
                case Spell.Magic_Beam:
                    description = "Blast foes with a beam of " + StringLogic.adjective_by_sphere(this.sphere) + " magic.";
                    break;
                case Spell.Magic_Blast:
                    description = "Create a detonation of " + StringLogic.adjective_by_sphere(this.sphere) + " forces.";
                    break;
                case Spell.Magic_Bolt:
                    description = "Launch a " + StringLogic.adjective_by_sphere(this.sphere) + " bolt.";
                    break;
                case Spell.Magic_Courage:
                    description = "Rally yourself with the forces of " + StringLogic.adjective_by_sphere(this.sphere);
                    break;
                case Spell.Magic_Fear:
                    description = "Fill foes with the terror of a " + StringLogic.adjective_by_sphere(this.sphere) + " doom.";
                    break;
                case Spell.Magic_Haste:
                    description = "Bolster your movement with " + StringLogic.adjective_by_sphere(this.sphere) + " grace.";
                    break;
                case Spell.Magic_Restoration:
                    description = "The power of " + StringLogic.adjective_by_sphere(this.sphere) + " healing magic tends your flesh.";
                    break;
                case Spell.Magic_Shield:
                    description = "Protect yourself with a shield of " + StringLogic.adjective_by_sphere(this.sphere) + " force.";
                    break;
                case Spell.Pact_Annihilate:
                    description = "Destroy foes with a wave of " + StringLogic.adjective_by_sphere(this.sphere) + " destruction.";
                    break;
                case Spell.Pact_Armor:
                    description = "Enhance your armor to resist " + StringLogic.adjective_by_sphere(this.sphere) + " magic.";
                    break;
                case Spell.Pact_Drain:
                    description = "Steal " + StringLogic.adjective_by_sphere(this.sphere) + " essences from your foes to feed upon.";
                    break;
                case Spell.Pact_Reprisal:
                    description = "Lash out at offenders with " + StringLogic.adjective_by_sphere(this.sphere) + " rage!";
                    break;
                case Spell.Pact_Shift:
                    description = "Become the avatar of " + StringLogic.adjective_by_sphere(this.sphere) + " divinity.";
                    break;
                case Spell.Pact_Spellstrike:
                    description = "Strike foes with " + StringLogic.adjective_by_sphere(this.sphere) + " might and " + StringLogic.adjective_by_sphere(this.sphere) + " magic.";
                    break;
                case Spell.Pact_Weapon:
                    description = "Enhance your weapon to deal " + StringLogic.adjective_by_sphere(this.sphere) + " damage.";
                    break;

            }
        }

    }
}
