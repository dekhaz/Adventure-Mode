using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
    class Program
    {
        static void Main(string[] args)
        {
            new_Game();

            


        }

        private static Boolean new_Game()
        {
            
            try
            {
                using (var ctx = new GameContext())
                {


                    ctx.SaveChanges();
                }
            } catch (Exception e) {
                Console.WriteLine(e);
            } 

        }
            




    }
}
