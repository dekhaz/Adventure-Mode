using AdventureMode.Logic;
using AdventureMode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
    public enum Hero_Class
    {
        Adventurer, Mercenary, Scholar, Thief, Crusader
    }

    public enum Inventory_Slot
    {
        Armor,Weapon,Artifact,Bottle
    }

    class Hero : Character
    {

        public Hero_Class Profession { get; set; }
       


        public List<Item> Inventory { get; set; }
        public List<Magic> Spellbook { get; set; }
        
        public Hero(string name,Hero_Class prof, Sphere gft,Sphere pct)
        {
            this.Name = name;
            this.Alignment = Sphere.Mortal;
            this.Gift = gft;
            this.Pact = pct;
            this.Profession = prof;
            switch (this.Profession)
            {
                case Hero_Class.Adventurer:
                    this.maxHP = 10 + DiceRoller.roll_die(4, 8);
                    this.maxFP = 6 + DiceRoller.roll_die(1, 6);
                    this.combat = 1;
                    this.protect = 1;
                    this.spirit = 1;
                    this.luck = 1;
                    this.reactions = 1;
                    this.Weapon = new Armament();
                    this.Armor = new Defensive();
                    this.Artifact = new Enhancement();
                    this.Bottle = new Elixir();

                    break;
                case Hero_Class.Crusader:
                    break;
                case Hero_Class.Mercenary:
                    break;
                case Hero_Class.Scholar:
                    break;
                case Hero_Class.Thief:
                    break;

            }

        }



    }
}
