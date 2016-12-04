using AdventureMode.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode.Model
{
    public enum Morale
    {
        Foolish = 0x10, Brave = 0x8, Cautious = 0x4, Shaken = 0x2, Terrified = 0x1
    }

     class Character
    {

        [Key] public int Chara_ID { get;set;}
        public string Name { get; set; }


        protected int combat { get; set; }

        public int Combat_Dice { get; set; }
        protected int protect { get; set; }

        public int Protection_Dice { get; set; }
        protected int spirit { get; set; }

        public int Spirit_Dice { get; set; }
            public int Magic_Force { get; set; }
            public int Magic_Control { get; set; }


        protected int luck { get; set; }
        public int Luck_Dice { get; set; }


        public int Health { get; set; }
        protected int maxHP { get; set; }
        public int Focus { get; set; }
        protected int maxFP { get; set; }
        public int Reaction { get; set; }
        protected int reactions { get; set; }
        public void Refill()
        {
            this.Health = this.maxHP;
            this.Focus = this.maxFP;
            this.Reaction = this.reactions;
            this.Health = this.maxHP;
            this.Combat_Dice = this.combat;
            this.Protection_Dice = this.protect;
            this.Spirit_Dice = this.spirit;
            this.Luck_Dice = this.luck;
            this.Channel();
        }

        public void Channel()
        {
            this.Magic_Force = this.rollSpirit() + this.rollLuck() + this.rollCombat();
            this.Magic_Control = this.rollSpirit() + this.rollLuck() + this.rollProtection();
        }


        public Sphere Alignment { get; set; }
        public Sphere Gift { get; set; }
        public Sphere Pact { get; set; }

        public Logic.Action Plan { get; set; }

        public Armament Weapon { get; set; }
        public Defensive Armor { get; set; }
        public Enhancement Artifact { get; set; }
        public Elixir Bottle { get; set; }


        public delegate void resolveEffects();

        public void amAlive() { this.reFocus(1); }
        public void amBurning() { this.receiveHarm(6); }
        public void amShocked() { this.loseFocus(6); }
        public void amFrozen() { this.overwhelmed(6); }
        public void amPoisoned() { this.receiveHarm(2); this.loseFocus(2); this.overwhelmed(1); }
        public void amGross() { this.Luck_Dice = this.luck - 1; }
        public void amCrazy() { this.Spirit_Dice = this.spirit - 1; }
        public void amRaging() { this.Combat_Dice = this.combat + 2; this.Protection_Dice = this.protect - 2; }
        public void amDepressed() {this.Spirit_Dice -= 1; this.Luck_Dice -= 1; }
        public void amTired() { this.Combat_Dice -= 1; this.Protection_Dice -= 1; }





        public void receiveHarm(int dam) { this.Health -= dam; }
        public void loseFocus(int burn) { this.Focus -= burn; }
        public void overwhelmed(int loss) { this.Reaction -= loss; }



        public  int rollCombat() { return DiceRoller.roll_die(this.Combat_Dice, 6); }
        public int rollProtection() { return DiceRoller.roll_die(this.Protection_Dice, 6); }
        public int rollSpirit() { return DiceRoller.roll_die(this.Spirit_Dice, 6); }
        public int rollLuck() { return DiceRoller.roll_die(this.Luck_Dice, 6); }




        public  void beHealed(int heal) { this.Health += heal; if (this.Health > this.maxHP) { this.Health = this.maxHP; } }
        public  void reFocus(int foci) { this.Focus += maxFP; if (this.Focus > this.maxFP) { this.Focus = this.maxFP; } }




        public Morale morale { get; set; }
        public void moraleBreak()
        {
            if (this.morale != Morale.Terrified)
            {
                this.morale = (Morale)((int)this.morale / 2);
            }
        }
        public  void moraleRally()
        {
            if (this.morale != Morale.Foolish)
            {
                this.morale = (Morale)((int)this.morale * 2);
            }
        }

        public resolveEffects resolve;

        public Character()
        {
            resolve = amAlive;
            resolveEffects efkt_burn = amBurning;
            resolveEffects efkt_shok = amShocked;
            resolveEffects efkt_frez = amFrozen;
            resolveEffects efkt_posn = amPoisoned;
            resolveEffects efkt_grss = amGross;
            resolveEffects efkt_crzy = amCrazy;
            resolveEffects efkt_ragn = amRaging;
            resolveEffects efkt_dprs = amDepressed;
            resolveEffects efkt_tird = amTired;
            this.maxHP = 20;
            this.maxFP = 10;
            this.combat = 1;
            this.protect = 1;
            this.spirit = 1;
            this.luck = 1;
            

        }


    }
}
