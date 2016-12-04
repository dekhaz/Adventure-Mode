using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdventureMode
{
    class GameLogic
    {
        public static Key Attack_Binding = new Key();
        public static Key Defend_Binding = new Key();
        public static Key Evade_Binding = new Key();
        public static Key Incant_Binding = new Key();
        public static Key Gesture_Binding = new Key();
        public static Key Use_Item_Binding = new Key();
        public static Key Use_Tool_Binding = new Key();
        public static Key Catalyst_Binding = new Key();

        public static void debug_controls()
        {
            Attack_Binding = Key.A;
            Defend_Binding = Key.D;
            Evade_Binding = Key.E;
            Incant_Binding = Key.I;
            Gesture_Binding = Key.G;
            Use_Item_Binding = Key.U;
            Use_Tool_Binding = Key.T;
            Catalyst_Binding = Key.C;
        }
    

        public static Action_Type verify_action(Action_Type allowed, Action_Type attempted)
        {
            if ((allowed & attempted) != 0)
            {
                return attempted;
            }
            else { attempted &= 0x0; }
            return attempted;
        }


        public static Action_Type read_keyboard_combat()
        {
            Action_Type act;
            if (Keyboard.IsKeyDown(Attack_Binding))
            {
                if (Keyboard.IsKeyDown(Catalyst_Binding))
                {
                    act = Action_Type.Power_Attack;
                }
                else if (Keyboard.IsKeyDown(Defend_Binding))
                {
                    act = Action_Type.Counter_Attack;
                }
                else if (Keyboard.IsKeyDown(Evade_Binding))
                {
                    act = Action_Type.Skirmish;
                }
                else if (Keyboard.IsKeyDown(Incant_Binding))
                {
                    act = Action_Type.Spell_Attack;
                }
                else if (Keyboard.IsKeyDown(Gesture_Binding))
                {
                    act = Action_Type.Enchant_Weapon;
                }
                else
                {
                    act = Action_Type.Attack;
                }
            }
            else if (Keyboard.IsKeyDown(Defend_Binding))
            {
                if (Keyboard.IsKeyDown(Catalyst_Binding))
                {
                    act = Action_Type.Heavy_Defense;
                }
                else if (Keyboard.IsKeyDown(Evade_Binding))
                {
                    act = Action_Type.Fall_Back;
                }
                else if (Keyboard.IsKeyDown(Incant_Binding))
                {
                    act = Action_Type.Magic_Defense;
                }
                else if (Keyboard.IsKeyDown(Gesture_Binding))
                {
                    act = Action_Type.Enchant_Armor;
                }
                else if (Keyboard.IsKeyDown(Use_Tool_Binding))
                {
                    act = Action_Type.Tool_Defensive;
                }
                else if (Keyboard.IsKeyDown(Use_Item_Binding))
                {
                    act = Action_Type.Item_Defensive;
                }
                else
                {
                    act = Action_Type.Defend;

                }
            }
            else if (Keyboard.IsKeyDown(Evade_Binding))
            {
                if (Keyboard.IsKeyDown(Catalyst_Binding))
                {
                    act = Action_Type.Flee;
                }
                else if (Keyboard.IsKeyDown(Incant_Binding))
                {
                    act = Action_Type.Magic_Evade;
                }
                else if (Keyboard.IsKeyDown(Gesture_Binding))
                {
                    act = Action_Type.Blink_Step;
                }
                else if (Keyboard.IsKeyDown(Use_Tool_Binding))
                {
                    act = Action_Type.Tool_Evasion;
                }
                else if (Keyboard.IsKeyDown(Use_Item_Binding))
                {
                    act = Action_Type.Item_Evasion;
                }
                else
                {
                    act = Action_Type.Evade;
                }
            }
            else if (Keyboard.IsKeyDown(Incant_Binding))
            {
                if (Keyboard.IsKeyDown(Catalyst_Binding))
                {
                    act = Action_Type.Power_Shout;
                }
                else if (Keyboard.IsKeyDown(Gesture_Binding))
                {
                    act = Action_Type.Arcane_Magic;
                }
                else if (Keyboard.IsKeyDown(Use_Tool_Binding))
                {
                    act = Action_Type.Tool_Magic;
                }
                else if (Keyboard.IsKeyDown(Use_Item_Binding))
                {
                    act = Action_Type.Item_Magic;
                }
                else
                {
                    act = Action_Type.Incant;
                }
            }
            else if (Keyboard.IsKeyDown(Gesture_Binding))
            {
                if (Keyboard.IsKeyDown(Catalyst_Binding))
                {
                    act = Action_Type.Power_Fist;
                }
                else if (Keyboard.IsKeyDown(Use_Tool_Binding))
                {
                    act = Action_Type.Tool_Offensive;
                }
                else if (Keyboard.IsKeyDown(Use_Item_Binding))
                {
                    act = Action_Type.Item_Offensive;
                }
                else
                {
                    act = Action_Type.Gesture;
                }
            }
            else if (Keyboard.IsKeyDown(Use_Tool_Binding))
            {
                if (Keyboard.IsKeyDown(Catalyst_Binding))
                {
                    act = Action_Type.Tool_Power;
                }
                else
                {
                    act = Action_Type.Use_Tool;
                }
            }
            else if (Keyboard.IsKeyDown(Use_Item_Binding))
            {
                if (Keyboard.IsKeyDown(Catalyst_Binding))
                {
                    act = Action_Type.Double_Dose;
                }
                else
                {
                    act = Action_Type.Use_Item;
                }
            }
            else { act = Action_Type.Observe; }


            return act;
        }
    }
}
