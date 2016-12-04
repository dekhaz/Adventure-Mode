using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode
{
    class StringLogic
    {
        public static string adjective_by_sphere(Sphere sph)
        {
            string word = "";
            Random selector = new Random();
            switch (sph)
            {
                case Sphere.None:
                    switch (selector.Next(0, 6)){
                        case 0:
                            word = "erroneous";
                            break;
                        case 1:
                            word = "awesome";
                            break;
                        case 2:
                            word = "really neat";
                            break;
                        case 3:
                            word = "righteous";
                            break;
                        case 4:
                            word = "cool, cool";
                            break;
                        case 5:
                            word = "outrageous";
                            break;
                        case 6:
                            word = "surprising";
                            break;
                    }
                    break;
                case Sphere.Death:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "cursed";
                            break;
                        case 1:
                            word = "virulent";
                            break;
                        case 2:
                            word = "vile";
                            break;
                        case 3:
                            word = "malevolent";
                            break;
                        case 4:
                            word = "deadly";
                            break;
                        case 5:
                            word = "pestilent";
                            break;
                        case 6:
                            word = "toxic";
                            break;
                    }
                    break;
                case Sphere.Stellar:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "stellar";
                            break;
                        case 1:
                            word = "astral";
                            break;
                        case 2:
                            word = "starry";
                            break;
                        case 3:
                            word = "cosmic";
                            break;
                        case 4:
                            word = "alien";
                            break;
                        case 5:
                            word = "strange";
                            break;
                        case 6:
                            word = "meteoric";
                            break;
                    }
                    break;
                case Sphere.Celestial:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "celestial";
                            break;
                        case 1:
                            word = "divine";
                            break;
                        case 2:
                            word = "solar";
                            break;
                        case 3:
                            word = "radiant";
                            break;
                        case 4:
                            word = "angelic";
                            break;
                        case 5:
                            word = "sacred";
                            break;
                        case 6:
                            word = "blessed";
                            break;
                    }
                    break;
                case Sphere.Esoteric:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "esoteric";
                            break;
                        case 1:
                            word = "arcane";
                            break;
                        case 2:
                            word = "hidden";
                            break;
                        case 3:
                            word = "secret";
                            break;
                        case 4:
                            word = "magical";
                            break;
                        case 5:
                            word = "wizardly";
                            break;
                        case 6:
                            word = "magely";
                            break;
                    }
                    break;
                case Sphere.Nemesis:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "nemesis";
                            break;
                        case 1:
                            word = "fated";
                            break;
                        case 2:
                            word = "ominous";
                            break;
                        case 3:
                            word = "dreadful";
                            break;
                        case 4:
                            word = "terrible";
                            break;
                        case 5:
                            word = "inevitable";
                            break;
                        case 6:
                            word = "destined";
                            break;
                    }
                    break;
                case Sphere.Primal:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "primal";
                            break;
                        case 1:
                            word = "ancient";
                            break;
                        case 2:
                            word = "savage";
                            break;
                        case 3:
                            word = "wrathful";
                            break;
                        case 4:
                            word = "bloody";
                            break;
                        case 5:
                            word = "beastly";
                            break;
                        case 6:
                            word = "feral";
                            break;
                    }
                    break;
                case Sphere.Occult:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "occult";
                            break;
                        case 1:
                            word = "ritualistic";
                            break;
                        case 2:
                            word = "bizarre";
                            break;
                        case 3:
                            word = "eldritch";
                            break;
                        case 4:
                            word = "chaotic";
                            break;
                        case 5:
                            word = "insane";
                            break;
                        case 6:
                            word = "necromantic";
                            break;
                    }
                    break;
                case Sphere.Kindled:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "kindled";
                            break;
                        case 1:
                            word = "burning";
                            break;
                        case 2:
                            word = "warm";
                            break;
                        case 3:
                            word = "glowing";
                            break;
                        case 4:
                            word = "flaming";
                            break;
                        case 5:
                            word = "molten";
                            break;
                        case 6:
                            word = "charred";
                            break;
                    }
                    break;
                case Sphere.Boreal:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "boreal";
                            break;
                        case 1:
                            word = "chilly";
                            break;
                        case 2:
                            word = "frosty";
                            break;
                        case 3:
                            word = "icy";
                            break;
                        case 4:
                            word = "auroral";
                            break;
                        case 5:
                            word = "subzero";
                            break;
                        case 6:
                            word = "arctic";
                            break;
                    }
                    break;
                case Sphere.Void:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "voidtouched";
                            break;
                        case 1:
                            word = "horrific";
                            break;
                        case 2:
                            word = "aberrant";
                            break;
                        case 3:
                            word = "formless";
                            break;
                        case 4:
                            word = "chaotic";
                            break;
                        case 5:
                            word = "calamitous";
                            break;
                        case 6:
                            word = "empty";
                            break;
                    }
                    break;
                case Sphere.Tempest:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "tempestuous";
                            break;
                        case 1:
                            word = "charged";
                            break;
                        case 2:
                            word = "electric";
                            break;
                        case 3:
                            word = "shocking";
                            break;
                        case 4:
                            word = "thunderous";
                            break;
                        case 5:
                            word = "windswept";
                            break;
                        case 6:
                            word = "torrential";
                            break;
                    }
                    break;
                case Sphere.Ethereal:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "ethereal";
                            break;
                        case 1:
                            word = "misty";
                            break;
                        case 2:
                            word = "floating";
                            break;
                        case 3:
                            word = "phasing";
                            break;
                        case 4:
                            word = "aerial";
                            break;
                        case 5:
                            word = "shadowed";
                            break;
                        case 6:
                            word = "dusky";
                            break;
                    }
                    break;
                case Sphere.Industrial:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "industrial";
                            break;
                        case 1:
                            word = "mechanical";
                            break;
                        case 2:
                            word = "cybernetic";
                            break;
                        case 3:
                            word = "hydraulic";
                            break;
                        case 4:
                            word = "technological";
                            break;
                        case 5:
                            word = "mechanized";
                            break;
                        case 6:
                            word = "robotic";
                            break;
                    }
                    break;
                case Sphere.Aquatic:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "aquatic";
                            break;
                        case 1:
                            word = "amphibious";
                            break;
                        case 2:
                            word = "moist";
                            break;
                        case 3:
                            word = "pressurized";
                            break;
                        case 4:
                            word = "drowned";
                            break;
                        case 5:
                            word = "sunken";
                            break;
                        case 6:
                            word = "tidal";
                            break;
                    }
                    break;
                case Sphere.Geotic:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "geotic";
                            break;
                        case 1:
                            word = "earthen";
                            break;
                        case 2:
                            word = "stoned";
                            break;
                        case 3:
                            word = "petrified";
                            break;
                        case 4:
                            word = "subterranean";
                            break;
                        case 5:
                            word = "volcanic";
                            break;
                        case 6:
                            word = "tectonic";
                            break;
                    }
                    break;
                case Sphere.Mortal:
                    switch (selector.Next(0, 6))
                    {
                        case 0:
                            word = "strong";
                            break;
                        case 1:
                            word = "quick";
                            break;
                        case 2:
                            word = "tough";
                            break;
                        case 3:
                            word = "cerebral";
                            break;
                        case 4:
                            word = "wise";
                            break;
                        case 5:
                            word = "fine";
                            break;
                        case 6:
                            word = "heroic";
                            break;
                    }
                    break;
            }
            return word;
        //end function adjective by sphere
        }

 
        //end class string logic
    }
}
