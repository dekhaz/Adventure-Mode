using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureMode.Logic
{
    public enum Action
    {
        Idle,Attack,Defend,Magic,Potion,Cower
    }


    class ActionHandler
    {

        public static void combat_turn(ref Hero player, ref Foe enemy)
        {
            int CombatP, CombatF;
            CombatP = 0;
            CombatF = 0;

            int ProtectP, ProtectF;
            ProtectP = 0;
            ProtectF = 0;

            Sphere off_sphere;
            Sphere def_sphere;

            switch (player.Plan)
            {
                case Action.Idle:
                    CombatP = player.rollLuck();
                    ProtectP = player.rollLuck() + player.Armor.Defensive_Bonus();
                    break;

                case Action.Attack:
                    CombatP = player.rollLuck() + player.rollCombat() + player.Weapon.Combat_Modifier(enemy.Alignment);
                    if (player.Weapon.Enchanted) { CombatP += player.Weapon.Enchanted_Modifier(enemy.Alignment); }
                    ProtectP = player.rollLuck() + player.Armor.Defensive_Bonus();
                    break;

                case Action.Defend:
                    CombatP = player.Weapon.Combat_Modifier(enemy.Alignment);
                    ProtectP = player.rollProtection() + player.Armor.Defensive_Bonus();
                    off_sphere = enemy.Weapon.Elemental | enemy.Weapon.Enchantment | enemy.Pact;
                    def_sphere = player.Alignment | player.Gift | player.Pact | player.Armor.Resistance;
                    if ((off_sphere & def_sphere) > 0)
                    {
                        ProtectP += player.rollProtection() + player.Armor.Defensive_Bonus();
                    }

                    break;
                case Action.Magic:
                    player.Channel();
                    if (player.Magic_Force > player.Magic_Control)
                    {
                        CombatP = (player.Magic_Force * ActionHandler.sphere_comparison(player.Gift, enemy.Alignment)) / 4;
                        ProtectP = player.rollLuck() + player.Armor.Defensive_Bonus();
                        player.receiveHarm(player.Magic_Force - player.Magic_Control);
                    }
                    else
                    {
                        CombatP = (player.Magic_Force * ActionHandler.sphere_comparison(player.Gift, enemy.Alignment)) / 4;
                        ProtectP = player.Magic_Control + player.rollLuck() + player.Armor.Defensive_Bonus();
                        bool casting = true;
                        while (casting)
                        {
                            int magicka = DiceRoller.spell_selection(player.Spellbook.Count);
                            switch (player.Spellbook[magicka].spell)
                            {
                                case Spell.Magic_Aura:
                                    CombatP += player.rollLuck();
                                    ProtectP += player.rollLuck();
                                    break;
                                case Spell.Magic_Beam:
                                    CombatP += ((player.Magic_Force + player.rollSpirit()) * ActionHandler.sphere_comparison(player.Spellbook[magicka].sphere, enemy.Alignment)) / 4;
                                    break;
                                case Spell.Magic_Blast:
                                    CombatP += (player.Magic_Force * ActionHandler.sphere_comparison(player.Spellbook[magicka].sphere, enemy.Alignment)) / 4;
                                    break;
                                case Spell.Magic_Bolt:
                                    CombatP += (player.rollSpirit() * ActionHandler.sphere_comparison(player.Spellbook[magicka].sphere, enemy.Alignment)) / 4;
                                    break;
                                case Spell.Magic_Courage:
                                    player.moraleRally();
                                    break;
                                case Spell.Magic_Fear:
                                    enemy.moraleBreak();
                                    break;
                                case Spell.Magic_Haste:
                                    player.Reaction++;
                                    break;
                                case Spell.Magic_Restoration:
                                    player.beHealed(player.Magic_Force);
                                    break;
                                case Spell.Magic_Shield:
                                    ProtectP += (player.Magic_Control);
                                    break;
                                case Spell.Pact_Annihilate:
                                    CombatP += (((player.Magic_Control) * ActionHandler.sphere_comparison(player.Spellbook[magicka].sphere, enemy.Gift)) / 4);
                                    break;
                                case Spell.Pact_Armor:
                                    ProtectP += player.rollSpirit() + player.rollProtection();
                                    player.Armor.Resistance = player.Armor.Resistance | player.Spellbook[magicka].sphere;
                                    break;
                                case Spell.Pact_Drain:
                                    CombatP += ((player.Magic_Force + player.rollSpirit()) * ActionHandler.sphere_comparison(player.Spellbook[magicka].sphere, enemy.Alignment)) / 4;
                                    player.beHealed(CombatP);
                                    break;
                                case Spell.Pact_Reprisal:
                                    CombatP += ProtectP;
                                    ProtectP = CombatP;
                                    break;
                                case Spell.Pact_Shift:
                                    if (player.Weapon.Enchanted == true)
                                    {
                                        player.Weapon.Enchantment = player.Pact;
                                    }
                                    player.Armor.Resistance = player.Armor.Resistance | player.Pact;
                                    break;
                                case Spell.Pact_Spellstrike:
                                    CombatP += (((player.rollCombat() + player.Weapon.Combat_Modifier(enemy.Alignment)) * ActionHandler.sphere_comparison(player.Spellbook[magicka].sphere, enemy.Alignment)) / 4);
                                    if (player.Weapon.Enchanted) { CombatP += player.Weapon.Enchanted_Modifier(enemy.Alignment); }
                                    break;
                                case Spell.Pact_Weapon:
                                    player.Weapon.Enchanted = true;
                                    player.Weapon.Enchantment = player.Spellbook[magicka].sphere;
                                    break;
                            }
                            player.Magic_Force = (player.Magic_Force / 2) + player.rollSpirit();
                            player.Magic_Control = (player.Magic_Control / 2) + player.rollSpirit();
                            if ((player.Magic_Force < player.Magic_Control) || (player.Magic_Force < 0) || (player.Magic_Control < 0)) { casting = false; }
                        }
                    }
                    off_sphere = player.Gift | player.Pact;
                    def_sphere = enemy.Alignment | enemy.Gift | enemy.Pact | enemy.Armor.Resistance;
                    if ((off_sphere & def_sphere) > 0)
                    {
                        CombatP -= (enemy.rollProtection() + enemy.Armor.Defensive_Bonus());
                    }
                    break;
                case Action.Potion:
                    if (player.Bottle.Doses > 0)
                    {
                        player.Bottle.Doses--;
                        player.beHealed(player.Bottle.effect());
                        player.resolve -= player.amPoisoned;
                        if (player.Bottle.bonus() > 0)
                        {
                            player.reFocus(player.Bottle.bonus());
                            player.resolve -= player.amTired;
                        }
                    }
                    break;
            }

            switch (enemy.Plan)
            {
                case Action.Idle:
                    CombatF = enemy.rollLuck();
                    ProtectF = enemy.rollLuck() + enemy.Armor.Defensive_Bonus();
                    break;

                case Action.Attack:
                    CombatF = enemy.rollLuck() + enemy.rollCombat() + enemy.Weapon.Combat_Modifier(player.Alignment);
                    if (enemy.Weapon.Enchanted) { CombatF += enemy.Weapon.Enchanted_Modifier(player.Alignment); }
                    ProtectF = enemy.rollLuck() + enemy.Armor.Defensive_Bonus();
                    break;

                case Action.Defend:
                    CombatF = enemy.Weapon.Combat_Modifier(player.Alignment);
                    ProtectF = enemy.rollProtection() + enemy.Armor.Defensive_Bonus();
                    off_sphere = player.Weapon.Elemental | player.Weapon.Enchantment | player.Pact;
                    def_sphere = enemy.Alignment | enemy.Gift | enemy.Pact | enemy.Armor.Resistance;
                    if ((off_sphere & def_sphere) > 0)
                    {
                        ProtectF += enemy.rollProtection() + enemy.Armor.Defensive_Bonus();
                    }

                    break;
                case Action.Magic:
                    enemy.Channel();
                    if (enemy.Magic_Force > enemy.Magic_Control)
                    {
                        CombatF = (enemy.Magic_Force * ActionHandler.sphere_comparison(enemy.Gift, player.Alignment)) / 4;
                        ProtectF = enemy.rollLuck() + enemy.Armor.Defensive_Bonus();
                        enemy.receiveHarm(enemy.Magic_Force - enemy.Magic_Control);
                    }
                    else
                    {
                        CombatF = (enemy.Magic_Force * ActionHandler.sphere_comparison(enemy.Gift, player.Alignment)) / 4;
                        ProtectF = enemy.Magic_Control + enemy.rollLuck() + enemy.Armor.Defensive_Bonus();
                        bool casting = true;
                        while (casting)
                        {
                            int magicka = DiceRoller.spell_selection(enemy.Spellbook.Count);
                            switch (enemy.Spellbook[magicka].spell)
                            {
                                case Spell.Magic_Aura:
                                    CombatF += enemy.rollLuck();
                                    ProtectF += enemy.rollLuck();
                                    break;
                                case Spell.Magic_Beam:
                                    CombatF += ((enemy.Magic_Force + enemy.rollSpirit()) * ActionHandler.sphere_comparison(enemy.Spellbook[magicka].sphere, player.Alignment)) / 4;
                                    break;
                                case Spell.Magic_Blast:
                                    CombatF += (enemy.Magic_Force * ActionHandler.sphere_comparison(enemy.Spellbook[magicka].sphere, player.Alignment)) / 4;
                                    break;
                                case Spell.Magic_Bolt:
                                    CombatF += (enemy.rollSpirit() * ActionHandler.sphere_comparison(enemy.Spellbook[magicka].sphere, player.Alignment)) / 4;
                                    break;
                                case Spell.Magic_Courage:
                                    enemy.moraleRally();
                                    break;
                                case Spell.Magic_Fear:
                                    player.moraleBreak();
                                    break;
                                case Spell.Magic_Haste:
                                    enemy.Reaction++;
                                    break;
                                case Spell.Magic_Restoration:
                                    enemy.beHealed(enemy.Magic_Force);
                                    break;
                                case Spell.Magic_Shield:
                                    ProtectF += (enemy.Magic_Control);
                                    break;
                                case Spell.Pact_Annihilate:
                                    CombatF += (((enemy.Magic_Control) * ActionHandler.sphere_comparison(enemy.Spellbook[magicka].sphere, player.Gift)) / 4);
                                    break;
                                case Spell.Pact_Armor:
                                    ProtectF += enemy.rollSpirit() + enemy.rollProtection();
                                    enemy.Armor.Resistance = enemy.Armor.Resistance | enemy.Spellbook[magicka].sphere;
                                    break;
                                case Spell.Pact_Drain:
                                    CombatF += ((enemy.Magic_Force + enemy.rollSpirit()) * ActionHandler.sphere_comparison(enemy.Spellbook[magicka].sphere, player.Alignment)) / 4;
                                    enemy.beHealed(CombatF);
                                    break;
                                case Spell.Pact_Reprisal:
                                    CombatF += ProtectF;
                                    ProtectF = CombatF;
                                    break;
                                case Spell.Pact_Shift:
                                    if (enemy.Weapon.Enchanted == true)
                                    {
                                        enemy.Weapon.Enchantment = enemy.Pact;
                                    }
                                    enemy.Armor.Resistance = enemy.Armor.Resistance | enemy.Pact;
                                    break;
                                case Spell.Pact_Spellstrike:
                                    CombatF += (((enemy.rollCombat() + enemy.Weapon.Combat_Modifier(player.Alignment)) * ActionHandler.sphere_comparison(enemy.Spellbook[magicka].sphere, player.Alignment)) / 4);
                                    if (enemy.Weapon.Enchanted) { CombatF += enemy.Weapon.Enchanted_Modifier(player.Alignment); }
                                    break;
                                case Spell.Pact_Weapon:
                                    enemy.Weapon.Enchanted = true;
                                    enemy.Weapon.Enchantment = enemy.Spellbook[magicka].sphere;
                                    break;
                            }
                            enemy.Magic_Force = (enemy.Magic_Force / 2) + enemy.rollSpirit();
                            enemy.Magic_Control = (enemy.Magic_Control / 2) + enemy.rollSpirit();
                            if ((enemy.Magic_Force < enemy.Magic_Control) || (enemy.Magic_Force < 0) || (enemy.Magic_Control < 0)) { casting = false; }
                        }
                    }
                    off_sphere = enemy.Gift | enemy.Pact;
                    def_sphere = player.Alignment | player.Gift | player.Pact | player.Armor.Resistance;
                    if ((off_sphere & def_sphere) > 0)
                    {
                        CombatF -= (player.rollProtection() + player.Armor.Defensive_Bonus());
                    }
                    break;
                case Action.Potion:
                    if (enemy.Bottle.Doses > 0)
                    {
                        enemy.Bottle.Doses--;
                        enemy.beHealed(enemy.Bottle.effect());
                        enemy.resolve -= enemy.amPoisoned;
                        if (enemy.Bottle.bonus() > 0)
                        {
                            enemy.reFocus(enemy.Bottle.bonus());
                            enemy.resolve -= enemy.amTired;
                        }
                    }
                    break;
            }

            if (player.Reaction > enemy.Reaction)
            {
                if (CombatP > ProtectF)
                {
                    if (ProtectP > CombatF)
                    { //fast total success: player
                        enemy.receiveHarm(CombatP);
                    }
                    else
                    { //fast minor success: player
                        enemy.receiveHarm(CombatP - ProtectF);
                    }
                }
                if (CombatF > ProtectP)
                {
                    if (ProtectF > CombatP)
                    { //slow total success: foe
                        player.receiveHarm(CombatF);
                    }
                    else
                    { //slow minor success: foe
                        player.receiveHarm(CombatF - ProtectP);
                    }
                }
            }
            else
            {
                if (CombatF > ProtectP)
                {
                    if (ProtectF > CombatP)
                    { //fast total success: foe
                        player.receiveHarm(CombatF);
                    }
                    else
                    { //fast minor success: foe
                        player.receiveHarm(CombatF - ProtectP);
                    }
                }
                if (CombatP > ProtectF)
                {
                    if (ProtectP > CombatF)
                    { //slow total success: player
                        enemy.receiveHarm(CombatP);
                    }
                    else
                    { //slow minor success: player
                        enemy.receiveHarm(CombatP - ProtectF);
                    }
                }
            }
        }


        public static int sphere_comparison(Sphere off, Sphere def)
        {
            switch (off)
            {
                case Sphere.Aquatic:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 4;
                            break;
                        case Sphere.Boreal:
                            return 1;
                            break;
                        case Sphere.Celestial:
                            return 1;
                            break;
                        case Sphere.Death:
                            return 4;
                            break;
                        case Sphere.Esoteric:
                            return 8;
                            break;
                        case Sphere.Ethereal:
                            return 8;
                            break;
                        case Sphere.Geotic:
                            return 4;
                            break;
                        case Sphere.Industrial:
                            return 8;
                            break;
                        case Sphere.Kindled:
                            return 8;
                            break;
                        case Sphere.Mortal:
                            return 4;
                            break;
                        case Sphere.Nemesis:
                            return 4;
                            break;
                        case Sphere.None:
                            return 2;
                            break;
                        case Sphere.Occult:
                            return 4;
                            break;
                        case Sphere.Primal:
                            return 4;
                            break;
                        case Sphere.Stellar:
                            return 1;
                            break;
                        case Sphere.Tempest:
                            return 4;
                            break;
                        case Sphere.Void:
                            return 2;
                            break;
                    }
                    break;
                case Sphere.Boreal:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 8;
                            break;
                        case Sphere.Boreal:
                            return 2;
                            break;
                        case Sphere.Celestial:
                            return 2;
                            break;
                        case Sphere.Death:
                            return 4;
                            break;
                        case Sphere.Esoteric:
                            return 2;
                            break;
                        case Sphere.Ethereal:
                            return 1;
                            break;
                        case Sphere.Geotic:
                            return 2;
                            break;
                        case Sphere.Industrial:
                            return 4;
                            break;
                        case Sphere.Kindled:
                            return 4;
                            break;
                        case Sphere.Mortal:
                            return 4;
                            break;
                        case Sphere.Nemesis:
                            return 4;
                            break;
                        case Sphere.None:
                            return 2;
                            break;
                        case Sphere.Occult:
                            return 2;
                            break;
                        case Sphere.Primal:
                            return 4;
                            break;
                        case Sphere.Stellar:
                            return 2;
                            break;
                        case Sphere.Tempest:
                            return 8;
                            break;
                        case Sphere.Void:
                            return 2;
                            break;
                    }
                    break;
                case Sphere.Celestial:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 1;
                            break;
                        case Sphere.Boreal:
                            return 4;
                            break;
                        case Sphere.Celestial:
                            return 2;
                            break;
                        case Sphere.Death:
                            return 8;
                            break;
                        case Sphere.Esoteric:
                            return 1;
                            break;
                        case Sphere.Ethereal:
                            return 1;
                            break;
                        case Sphere.Geotic:
                            return 1;
                            break;
                        case Sphere.Industrial:
                            return 1;
                            break;
                        case Sphere.Kindled:
                            return 1;
                            break;
                        case Sphere.Mortal:
                            return 2;
                            break;
                        case Sphere.Nemesis:
                            return 4;
                            break;
                        case Sphere.None:
                            return 8;
                            break;
                        case Sphere.Occult:
                            return 8;
                            break;
                        case Sphere.Primal:
                            return 4;
                            break;
                        case Sphere.Stellar:
                            return 4;
                            break;
                        case Sphere.Tempest:
                            return 2;
                            break;
                        case Sphere.Void:
                            return 8;
                            break;
                    }
                    break;
                case Sphere.Death:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 4;
                            break;
                        case Sphere.Boreal:
                            return 4;
                            break;
                        case Sphere.Celestial:
                            return 1;
                            break;
                        case Sphere.Death:
                            return -1;
                            break;
                        case Sphere.Esoteric:
                            return 2;
                            break;
                        case Sphere.Ethereal:
                            return 4;
                            break;
                        case Sphere.Geotic:
                            return 4;
                            break;
                        case Sphere.Industrial:
                            return 1;
                            break;
                        case Sphere.Kindled:
                            return 4;
                            break;
                        case Sphere.Mortal:
                            return 8;
                            break;
                        case Sphere.Nemesis:
                            return 1;
                            break;
                        case Sphere.None:
                            return 8;
                            break;
                        case Sphere.Occult:
                            return 4;
                            break;
                        case Sphere.Primal:
                            return 8;
                            break;
                        case Sphere.Stellar:
                            return 2;
                            break;
                        case Sphere.Tempest:
                            return 4;
                            break;
                        case Sphere.Void:
                            return 1;
                            break;
                    }
                    break;
                case Sphere.Esoteric:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 4;
                            break;
                        case Sphere.Boreal:
                            return 2;
                            break;
                        case Sphere.Celestial:
                            return 4;
                            break;
                        case Sphere.Death:
                            return 1;
                            break;
                        case Sphere.Esoteric:
                            return 8;
                            break;
                        case Sphere.Ethereal:
                            return 8;
                            break;
                        case Sphere.Geotic:
                            return 4;
                            break;
                        case Sphere.Industrial:
                            return 4;
                            break;
                        case Sphere.Kindled:
                            return 2;
                            break;
                        case Sphere.Mortal:
                            return 4;
                            break;
                        case Sphere.Nemesis:
                            return 1;
                            break;
                        case Sphere.None:
                            return 1;
                            break;
                        case Sphere.Occult:
                            return 8;
                            break;
                        case Sphere.Primal:
                            return 2;
                            break;
                        case Sphere.Stellar:
                            return 4;
                            break;
                        case Sphere.Tempest:
                            return 2;
                            break;
                        case Sphere.Void:
                            return 8;
                            break;
                    }
                    break;
                case Sphere.Ethereal:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 2;
                            break;
                        case Sphere.Boreal:
                            return 4;
                            break;
                        case Sphere.Celestial:
                            return 1;
                            break;
                        case Sphere.Death:
                            return 1;
                            break;
                        case Sphere.Esoteric:
                            return 4;
                            break;
                        case Sphere.Ethereal:
                            return 8;
                            break;
                        case Sphere.Geotic:
                            return 4;
                            break;
                        case Sphere.Industrial:
                            return 4;
                            break;
                        case Sphere.Kindled:
                            return 4;
                            break;
                        case Sphere.Mortal:
                            return 8;
                            break;
                        case Sphere.Nemesis:
                            return 4;
                            break;
                        case Sphere.None:
                            return 1;
                            break;
                        case Sphere.Occult:
                            return 2;
                            break;
                        case Sphere.Primal:
                            return 4;
                            break;
                        case Sphere.Stellar:
                            return 1;
                            break;
                        case Sphere.Tempest:
                            return 8;
                            break;
                        case Sphere.Void:
                            return 2;
                            break;
                    }
                    break;
                case Sphere.Geotic:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 8;
                            break;
                        case Sphere.Boreal:
                            return 8;
                            break;
                        case Sphere.Celestial:
                            return 1;
                            break;
                        case Sphere.Death:
                            return 1;
                            break;
                        case Sphere.Esoteric:
                            return 4;
                            break;
                        case Sphere.Ethereal:
                            return 2;
                            break;
                        case Sphere.Geotic:
                            return 4;
                            break;
                        case Sphere.Industrial:
                            return 8;
                            break;
                        case Sphere.Kindled:
                            return 4;
                            break;
                        case Sphere.Mortal:
                            return 4;
                            break;
                        case Sphere.Nemesis:
                            return 2;
                            break;
                        case Sphere.None:
                            return 1;
                            break;
                        case Sphere.Occult:
                            return 4;
                            break;
                        case Sphere.Primal:
                            return 4;
                            break;
                        case Sphere.Stellar:
                            return 1;
                            break;
                        case Sphere.Tempest:
                            return 1;
                            break;
                        case Sphere.Void:
                            return 2;
                            break;
                    }
                    break;
                case Sphere.Industrial:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 8;
                            break;
                        case Sphere.Boreal:
                            return 8;
                            break;
                        case Sphere.Celestial:
                            return 1;
                            break;
                        case Sphere.Death:
                            return 1;
                            break;
                        case Sphere.Esoteric:
                            return 2;
                            break;
                        case Sphere.Ethereal:
                            return 8;
                            break;
                        case Sphere.Geotic:
                            return 8;
                            break;
                        case Sphere.Industrial:
                            return 4;
                            break;
                        case Sphere.Kindled:
                            return 2;
                            break;
                        case Sphere.Mortal:
                            return 8;
                            break;
                        case Sphere.Nemesis:
                            return 2;
                            break;
                        case Sphere.None:
                            return 4;
                            break;
                        case Sphere.Occult:
                            return 2;
                            break;
                        case Sphere.Primal:
                            return 2;
                            break;
                        case Sphere.Stellar:
                            return 1;
                            break;
                        case Sphere.Tempest:
                            return 1;
                            break;
                        case Sphere.Void:
                            return 1;
                            break;
                    }
                    break;
                case Sphere.Kindled:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            return 1;
                            break;
                        case Sphere.Boreal:
                            return 8;
                            break;
                        case Sphere.Celestial:
                            return 2;
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            return 2;
                            break;
                        case Sphere.Ethereal:
                            return 4;
                            break;
                        case Sphere.Geotic:
                            return 2;
                            break;
                        case Sphere.Industrial:
                            return 4;
                            break;
                        case Sphere.Kindled:
                            return 2;
                            break;
                        case Sphere.Mortal:
                            return 4;
                            break;
                        case Sphere.Nemesis:
                            return 4;
                            break;
                        case Sphere.None:
                            return 4;
                            break;
                        case Sphere.Occult:
                            return 2;
                            break;
                        case Sphere.Primal:
                            return 4;
                            break;
                        case Sphere.Stellar:
                            return 2;
                            break;
                        case Sphere.Tempest:
                            return 2;
                            break;
                        case Sphere.Void:
                            return 2;
                            break;
                    }
                    break;
                case Sphere.Mortal:
                    switch (def)
                    {
                        case Sphere.Aquatic:

                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;
                case Sphere.Nemesis:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;
                case Sphere.None:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;
                case Sphere.Occult:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;
                case Sphere.Primal:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;
                case Sphere.Stellar:
                    switch (def)
                    {
                        case Sphere.Aquatic:

                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;
                case Sphere.Tempest:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;
                case Sphere.Void:
                    switch (def)
                    {
                        case Sphere.Aquatic:
                            break;
                        case Sphere.Boreal:
                            break;
                        case Sphere.Celestial:
                            break;
                        case Sphere.Death:
                            break;
                        case Sphere.Esoteric:
                            break;
                        case Sphere.Ethereal:
                            break;
                        case Sphere.Geotic:
                            break;
                        case Sphere.Industrial:
                            break;
                        case Sphere.Kindled:
                            break;
                        case Sphere.Mortal:
                            break;
                        case Sphere.Nemesis:
                            break;
                        case Sphere.None:
                            break;
                        case Sphere.Occult:
                            break;
                        case Sphere.Primal:
                            break;
                        case Sphere.Stellar:
                            break;
                        case Sphere.Tempest:
                            break;
                        case Sphere.Void:
                            break;
                    }
                    break;

            }


        } 


    }
}
