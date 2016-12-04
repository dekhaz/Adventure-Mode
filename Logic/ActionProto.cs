using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{

    /*
    public enum Result
    {
        Failure = 0,
        Absorbed = 1,
        Resisted = 2,
        Diminished = 3,
        Successful = 4,
        Effective = 5,
        Critical = 6
    }


    [FlagsAttribute] public enum Action_Type
    {
        Observe = 0x0,
        Attack = 0x1,
        Incant = 0x2,
        Gesture = 0x4,
        Defend = 0x8,
        Evade = 0x10,
        Use_Item = 0x20,
        Use_Tool = 0x40,
        Catalyst = 0x80,
        Fail = 0x100,

        Ultimate_Magic = Attack|Catalyst|Defend|Evade|Incant|Gesture,
        Power_Attack = Attack | Catalyst,
        Counter_Attack = Attack | Defend,
        Skirmish = Attack | Evade,
        Spell_Attack = Attack | Incant,
        Enchant_Weapon = Attack | Gesture,
        Heavy_Defense = Defend | Catalyst,
        Fall_Back = Defend | Evade,
        Magic_Defense = Defend | Incant,
        Enchant_Armor = Defend | Gesture,
        Tool_Defensive = Defend | Use_Tool,
        Item_Defensive = Defend | Use_Item,
        Flee = Evade | Catalyst,
        Magic_Evade = Evade | Incant,
        Blink_Step = Evade | Gesture,
        Tool_Evasion = Evade | Use_Tool,
        Item_Evasion = Evade | Use_Item,
        Power_Shout = Incant | Catalyst,
        Arcane_Magic = Incant | Gesture,
        Tool_Magic = Incant | Use_Tool,
        Item_Magic = Incant | Use_Item,
        Power_Fist = Gesture | Catalyst,
        Tool_Offensive = Gesture | Use_Tool,
        Item_Offensive = Gesture | Use_Item,
        Tool_Power = Use_Tool | Catalyst,
        Double_Dose = Use_Item | Catalyst

    }

    public enum Behavior_Type
    {
        Mindless,Aggressive,Cautious,Witty,Sorcerous
    }

    class ActionProto : IObservable
    {
        public Action_Type Action_Type { get; set; }
        public Sphere Sphere { get; set; }
        public string description { get; set; }
        public Action(Action_Type act, Sphere sph, int cost)
        {
            this.Action_Type = act;
            StringBuilder untitled = new StringBuilder();
            switch (this.Action_Type)
            {
                case Action_Type.Ultimate_Magic:
                    untitled.Append("Break past your limits! Unleash " + StringLogic.adjective_by_sphere(this.Sphere) + " doom!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Power_Attack:
                    untitled.Append("Attack with all of your might!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Counter_Attack:
                    untitled.Append("Wait for an opening and strike!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Skirmish:
                    untitled.Append("Strike quickly and fall back!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Spell_Attack:
                    untitled.Append("Channel "+ StringLogic.adjective_by_sphere(this.Sphere) +" magic into your attack!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Enchant_Weapon:
                    untitled.Append("Enchant your weapon with " + StringLogic.adjective_by_sphere(this.Sphere)+" Magic!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Attack:
                    untitled.Append("Attack your foe!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Heavy_Defense:
                    untitled.Append("Brace for impact!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Fall_Back:
                    untitled.Append("Fall back!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Magic_Defense:
                    untitled.Append("Summon a magical "+ StringLogic.adjective_by_sphere(this.Sphere)+" shield!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Enchant_Armor:
                    untitled.Append("Enchant your armor with "+ StringLogic.adjective_by_sphere(this.Sphere)+" magic!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Tool_Defensive:
                    untitled.Append("Use tools for defense!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Item_Defensive:
                    untitled.Append("Use defensive items!");
                    untitled.AppendLine(" -- uses " + cost + " focus.");
                    break;
                case Action_Type.Defend:
                    untitled.Append("Defend!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Flee:
                    untitled.Append("Flee!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Magic_Evade:
                    untitled.Append("Evade in a burst of "+ StringLogic.adjective_by_sphere(this.Sphere)+" magic!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Blink_Step:
                    untitled.Append("Teleport with "+ StringLogic.adjective_by_sphere(this.Sphere)+" magic!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Tool_Evasion:
                    untitled.Append("Use tools for evasion!");
                    untitled.AppendLine(" -- uses " + cost + " durability.");
                    break;
                case Action_Type.Item_Evasion:
                    untitled.Append("Use tactical items!");
                    untitled.AppendLine(" -- uses " + cost + " charges.");
                    break;
                case Action_Type.Evade:
                    untitled.Append("Evade attacks!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Power_Shout:
                    untitled.Append("Shout with all of your might!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Arcane_Magic:
                    untitled.Append("Wield the forces of "+ StringLogic.adjective_by_sphere(this.Sphere)+" magic!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Tool_Magic:
                    untitled.Append("Use tools for magic!");
                    untitled.AppendLine(" -- uses " + cost + " durability.");
                    break;
                case Action_Type.Item_Magic:
                    untitled.Append("Use magical items!");
                    untitled.AppendLine(" -- uses " + cost + " charges.");
                    break;
                case Action_Type.Incant:
                    untitled.Append("Speak a "+ StringLogic.adjective_by_sphere(this.Sphere)+" word!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Power_Fist:
                    untitled.Append("Strike with a fist of "+ StringLogic.adjective_by_sphere(this.Sphere)+ " force!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Tool_Offensive:
                    untitled.Append("Use tools for offense!");
                    untitled.AppendLine(" -- uses " + cost + " durability.");
                    break;
                case Action_Type.Item_Offensive:
                    untitled.Append("Use offensive items!");
                    untitled.AppendLine(" -- uses " + cost + " charges.");
                    break;
                case Action_Type.Gesture:
                    untitled.Append("Evoke a "+StringLogic.adjective_by_sphere(this.Sphere)+" bolt!");
                    untitled.AppendLine(" -- costs " + cost + " focus.");
                    break;
                case Action_Type.Tool_Power:
                    untitled.Append("Use a "+ StringLogic.adjective_by_sphere(this.Sphere)+" artifact!");
                    untitled.AppendLine(" -- uses " + cost + " durability.");
                    break;
                case Action_Type.Use_Tool:
                    untitled.Append("Use a utility tool!");
                    untitled.AppendLine(" -- uses " + cost + " durability.");
                    break;
                case Action_Type.Double_Dose:
                    untitled.Append("Chug a potion!");
                    untitled.AppendLine(" -- drink " + cost + " doses.");
                    break;
                case Action_Type.Use_Item:
                    untitled.Append("Sip a potion!");
                    untitled.AppendLine(" -- drink " + cost + " doses.");
                    break;
                case Action_Type.Observe:
                    untitled.Append("Observe the situation");
                    untitled.AppendLine(" -- restores " + cost + " focus");
                    break;
            }
        }

        public void Describe(string desc)
        {
            this.description = desc;
        }
    }
*/
}
