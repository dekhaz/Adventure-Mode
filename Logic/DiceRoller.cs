using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode.Logic
{
    class DiceRoller
    {


        public static int roll_die(int pool, int sides)
        {
            Random roller = new Random();
            int result = 0;
            for (int i = 0; i < pool; i++)
            {
                result += roller.Next(sides);

            }
            return result;
        }

        public static int spell_selection(int spells_learned)
        {
            Random roller = new Random();
            int result = 0;
            result = roller.Next(spells_learned);
            return result; 
        }
    }
}
