using AdventureMode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
    class Foe : Character
    {
        public List<Magic> Spellbook { get; set; }

        public Logic.Action Decision()
        {
            Logic.Action choice = Logic.Action.Idle;
            if (this.Health < (this.maxHP / 4))
            {
                this.moraleBreak();
                choice = Logic.Action.Potion;
            }
            else if (this.Health < (this.maxHP / 2))
            {
                switch (this.morale)
                {
                    case Morale.Foolish:
                        if (this.Focus > (this.maxFP / 2))
                        {
                            if (this.rollSpirit() > this.rollCombat())
                            {
                                choice = Logic.Action.Magic;
                            }
                            else
                            {
                                choice = Logic.Action.Attack;
                            }
                        } else
                        {
                            choice = Logic.Action.Magic;
                        }
                        break;
                    case Morale.Brave:
                        if (this.Focus > (this.maxFP / 2))
                        {
                            if (this.rollSpirit() > this.rollCombat())
                            {
                                choice = Logic.Action.Magic;
                            }
                            else
                            {
                                choice = Logic.Action.Attack;
                            }
                        } else
                        {
                            choice = Logic.Action.Attack;
                        }
                        break;
                    case Morale.Cautious:
                        if (this.Focus > (this.maxFP / 2))
                        {
                            if (this.rollCombat() > this.rollProtection())
                            {
                                choice = Logic.Action.Attack;
                            }
                            else
                            {
                                choice = Logic.Action.Defend;
                            }
                        } else
                        {
                            choice = Logic.Action.Potion;
                        }
                        break;
                    case Morale.Shaken:
                        if (this.Focus > (this.maxFP / 2))
                        {
                            if (this.rollSpirit() > this.rollProtection())
                            {
                                choice = Logic.Action.Magic;
                            }
                            else
                            {
                                choice = Logic.Action.Potion;
                            }
                        }
                        else
                        {
                            choice = Logic.Action.Potion;
                        }
                        break;
                    case Morale.Terrified:
                        if (this.Focus > (this.maxFP / 2))
                        {
                            if (this.rollLuck() < this.rollLuck())
                            {
                                this.moraleRally();
                                choice = Logic.Action.Magic;
                            }
                            else
                            {
                                this.moraleRally();
                                choice = Logic.Action.Potion;
                            }
                        }
                        else
                        {
                            choice = Logic.Action.Defend;
                        }
                        break;
                }
            } else
            {
                switch (this.morale)
                {
                    case Morale.Foolish:
                        choice = Logic.Action.Magic;
                        break;
                    case Morale.Brave:
                        choice = Logic.Action.Attack;
                        break;
                    case Morale.Cautious:
                        if (this.rollCombat() > this.rollProtection())
                        {
                            choice = Logic.Action.Attack;
                        } else { choice = Logic.Action.Defend; }
                        break;
                    case Morale.Shaken:
                        choice = Logic.Action.Defend;
                        break;
                    case Morale.Terrified:
                        choice = Logic.Action.Potion;
                        break;
                }
            }
            return choice;
        }


    }
}
