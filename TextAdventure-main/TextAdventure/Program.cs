using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using System.Net.Quic;
using System.Reflection.Metadata.Ecma335;

namespace TextAdventure
{

/*    class Items // Items Class // Ej funktionell
    {
        public string itemName;
        public int itemQuantity;


        public Items(string name, int quantity)
        {
            itemName = name;
            itemQuantity = quantity;
        }

    }
*/
/*    public class BattleItems // Battle Items // Ej Funktionell
    {
        public int itemQuantity;
        public int itemDamage;

        public string BattleItem(int quantity, int damage)
        {
            itemQuantity = quantity;
            itemDamage = damage;
        }
    }
*/
    class Program
    {

        static void Fight(Hero hero, Enemy enemy) // Fight
        {
            Console.Clear();
            Console.WriteLine($"You enter combat with {enemy.enemyName}");
            Console.ReadLine();
            hero.BattleItems.Add("block");
            while (!hero.HeroIsDead() && !enemy.EnemyIsDead())
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Your Health:{hero.Health}\n" +
                                      $"{enemy.enemyName} Health: {enemy.enemyHealth}");


                    Console.WriteLine("Your Choices: ");
                    foreach (string BattleItem in hero.BattleItems)
                    {
                        Console.Write("[" + BattleItem + "]" + ", ");
                    }

                    string battleitem = Ask("What do you choose?").Trim().ToLower();

                    if (hero.BattleItems.Contains(battleitem))
                    {
                        Console.Clear();
                        Console.WriteLine($"You used {battleitem}");
                        hero.equip = battleitem;
                        if (hero.equip == "wooden sword" || hero.equip == "gun" || hero.equip == "knife" ||
                            hero.equip == "fine gun")
                        {
                            enemy.TakeDamage(Weapon(hero, enemy));
                        }

                        if (battleitem == "potion")
                        {
                            Console.WriteLine("You healed 30 Health");
                            Console.Read();
                            hero.Potion();
                            hero.BattleItems.Remove("potion");
                        }

                        if (battleitem == "block")
                        {
                            Console.WriteLine($"You are ready for the {enemy.enemyName}'s attack this round");
                            Console.Read();
                            hero.block = true;
                        }

                        break;
                    }
                } while (true);

                Console.Clear();
                Console.WriteLine($"{enemy.enemyName}'s Turn");
                Console.ReadLine();
                int monsterdecision = RollD6();
                if (RollD6() >= 4)
                {
                    Console.Clear();
                    Console.WriteLine($"The {enemy.enemyName} used Tackle");
                    Console.WriteLine("You took " + $"{hero.TakeDamage(enemy.GiveDamage())}" + " damage");
                    Console.ReadLine();
                }
                else if (monsterdecision >= 2 && monsterdecision < 4)
                    switch (enemy.empower)
                    {
                        case false:
                            Console.Clear();
                            Console.WriteLine($"The {enemy.enemyName} used Empower");
                            enemy.empower = true;
                            Console.WriteLine("Next attack is going to be stronger!");
                            Console.ReadLine();
                            break;
                        case true:
                            break;
                    }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"The {enemy.enemyName} is resting");
                    Console.WriteLine($"The {enemy.enemyName} healed {enemy.Rest()} health");
                    Console.ReadLine();
                }

                hero.block = false;
            }

            if (enemy.EnemyIsDead())
            {
                Console.Clear();
                Console.WriteLine($"You defeated {enemy.enemyName}");
            }

            if (hero.HeroIsDead())
            {
                Console.Clear();
                Console.WriteLine($"You, brave {hero.name} were defeated in battle by {enemy.enemyName}");
                Console.ReadKey();
                hero.location = ("quit");

            }
        }

        static int RollD6() // RNG
        {
            Random random = new Random();
            int roll = random.Next(1, 7);
            return roll;
        }

        static int Weapon(Hero hero, Enemy enemy) // Weapons
        {

            Console.Clear();
            Console.WriteLine($"You used {hero.equip}");
            Console.ReadLine();
            int amount = 0;

            if (hero.equip == "gun")
            {
                if (RollD6() >= 4) amount = 15;
                else
                {
                    Console.WriteLine("The gun jammed.....");
                    Console.ReadLine();
                    amount = 0;
                }
            }

            if (hero.equip == "fine gun")
            {
                if (RollD6() >= 2) amount = 15;
                else
                {
                    Console.WriteLine("The gun jammed.....");
                    Console.ReadLine();
                    amount = 0;
                }
            }

            if (hero.equip == "wooden sword")
            {
                amount = 5;
            }

            if (hero.equip == "knife")
            {
                amount = 8;
            }

            hero.Damage = amount;
            Console.WriteLine($"You did {hero.Damage} against the {enemy.enemyName}");
            Console.ReadLine();
            return amount;
        }

        static string Ask(string question) // FrågeFunktion
        {
            string response;
            do
            {
                Console.Write(question);
                response = Console.ReadLine().Trim();
            } while (response == "");

            return response;
        }

        static bool AskYesOrNo(string question) // Frågebool
        {
            while (true)
            {
                string response = Ask(question).ToLower();
                switch (response)
                {
                    case "yes":
                    case "ok":
                        return true;
                    case "no":
                        return false;
                }
            }
        }

        static void NewGame(Hero hero) // New Game
        {
            Console.Clear();
            string name;
            do
            {
                name = Ask("What is your name? ");
            } while (!AskYesOrNo($"So your name is {name}? "));

            hero.name = name;
            hero.location = "Tableroom";
        }

        static void Tableroom(Hero hero) // Tableroom rum
        {

            Console.Clear();
            hero.BattleItems.Add("wooden sword");
            Console.WriteLine("You are equipped with a Wooden Sword!");
            bool finished = false;
            do
            {
                Console.WriteLine("You are to slay the monster at the end of the dungeon" +
                                  "" +
                                  " In front of you is a stone table with two items on it," +
                                  " A Key and a Knife" +
                                  " You can only pick up one of these items.");

                string tableroomItem = Ask("Which one do you choose?" + " ([Key],[Knife], [None])")
                    .Trim().ToLower();
                if (tableroomItem == "key" || tableroomItem == "knife")
                {
                    Console.Clear();
                    switch (AskYesOrNo($"Do you want to choose the {tableroomItem}?"))
                    {
                        case true:
                            if (tableroomItem == "key")
                            {
                                hero.Items.Add($"{tableroomItem}");
                            }
                            else
                            {
                                hero.BattleItems.Add($"{tableroomItem}");
                            }

                            Console.WriteLine($"You picked up the {tableroomItem}!");
                            hero.location = "Corridor";
                            Console.ReadLine();
                            finished = true;
                            break;
                        case false:
                            finished = false;
                            break;
                    }
                }
                else if (tableroomItem == "none")
                {
                    Console.Clear();
                    switch (AskYesOrNo("Do you want to choose none?"))
                    {
                        case true:
                            Console.WriteLine("You didnt pick anything up!");
                            hero.location = "Corridor";
                            Console.ReadLine();
                            finished = true;
                            break;
                        case false:
                            finished = false;
                            break;
                    }
                }

            } while (!finished);


        }

        static void Corridor(Hero hero) // Corridor
        {
            Console.Clear();

            Console.WriteLine("You exit the room with the table and you find yourself standing in the dark" +
                              "hallway. You kan either enter the room to the right, or the room to the left");
            string direktion = Ask("Which way do you want to go?" + " [Left] or [Right] ").Trim().ToLower();
            if (direktion == "left")
            {
                if (hero.Items.Contains("key"))
                {
                    Console.WriteLine("You enter the key into the keyhole and the door opens");
                    Console.ReadLine();
                    hero.location = "Nyckelrum";
                    hero.Items.Remove("key");
                }
                else
                {
                    Console.WriteLine("You didnt have the key so you enter the other room");
                    Console.ReadLine();
                    hero.location = "puzzleroom";
                }
            }

            if (direktion == "right")
            {
                Console.WriteLine("You enter the room to the right");
                Console.ReadLine();
                hero.location = "puzzleroom";
            }
        }

        static void Nyckelrum(Hero hero) // NyckelRum
        {
            Console.Clear();
            Console.WriteLine("You enter a gloomy room \n" +
                              "You can make out a faint sillhouette of a oxidized copper chest\n" +
                              "As you make your way to it, you feel the presence of something mighty\n" +
                              "Inside of the chest you find a Weathered Gun\n" +
                              "But you only have enough space in your inventory for one of the weapons.\n");
            if (AskYesOrNo("Would you like to get rid of your wooden sword and pick up the weathered gun? "))
            {
                Console.Clear();
                Console.WriteLine("you get rid of the sword and holster the new weapon");
                hero.BattleItems.Remove("wooden sword");
                hero.BattleItems.Add("gun");
                Console.ReadLine();
            }

            hero.location = "puzzleroom";


        }

        static void Puzzleroom(Hero hero) // Puzzleroom
        {

            Console.Clear();
            Console.WriteLine("You enter the room and leaning against one of the walls you find a corpse\n" +
                              "You approach the corpse and it looks like it has something in its pocket");
            if (AskYesOrNo("Do you want to loot the corpse? "))
            {
                switch (RollD6() >= 3)
                {
                    case true:
                    {
                        if (hero.BattleItems.Contains("gun"))
                        {
                            Console.Clear();
                            Console.WriteLine("You find some kind of oil\n" +
                                              "Maybe it's apliable to a gun?");
                            hero.Items.Add("oil");
                            Console.ReadLine();
                            hero.location = "fightroom1";

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You find what looks to be a healing potion!");
                            hero.BattleItems.Add("potion");
                            Console.ReadLine();
                            hero.location = "fightroom1";

                        }

                        break;
                    }
                    case false:
                        Console.Clear();
                        Console.WriteLine("You reach into the corpses pocket and you feel the sting of pain!\n" +
                                          "You pull your hand out of the corpses pocket and you see a snake" +
                                          " slithering out.\n" +
                                          "You lose 10hp");
                        hero.TakeDamage(10);
                        Console.WriteLine("Current health: " + hero.Health);
                        Console.ReadLine();
                        hero.location = "fightroom1";
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You ignore the corpse!");
                Console.ReadLine();
                hero.location = "fightroom1";
            }

        }

        static void Fightroom1(Hero hero) //Fightroom1
        {
            Enemy enemy1 = new Enemy("Sentient Rock", 20, 5, "stubbed your toe");

            Console.Clear();
            Console.WriteLine("You enter to find a room dimly lit with torches lining the walls. \n" +
                              "In the center of the room lies an unassuming looking rock\n" +
                              "It looks small enough that you could lift it, but big enough that it might prove difficult!\n" +
                              "As you approach the rock it lunges at you, and you find yourself barely able to dodge," +
                              $"it is then you realize. Its not an ordinary rock, its a {enemy1.enemyName}");
            Console.ReadLine();

            Fight(hero, enemy1);
            enemy1 = null;
            if (hero.HeroIsDead())
            {
                hero.location = "quit";

            }
            else
            {
                Console.Clear();

                Console.WriteLine("After a tiring battle with the rock you push onward");
                hero.location = "preproom";
            }
        }

        static void Preproom(Hero hero) //preproom
        {

            Console.Clear();
            Console.WriteLine("You walk into the new room, the first thing to catch your eye is" +
                              " the huge door made out of cast iron straight ahead. \n" +
                              "The second thing you see is what looks to be a workbench in the center of the room");
            if (AskYesOrNo("Do you want to interact with the workbench?"))
            {
                if (hero.BattleItems.Contains("gun") && hero.Items.Contains("oil"))
                {
                    Console.Clear();
                    Console.WriteLine(
                        "You approach the workbench and find that it has the exact tools you need to oil your gun\n" +
                        "Lucky you!!!\n" +
                        "After spending some time applying the oil to your gun,\n" +
                        " it seems like its suddenly more reliable!");
                    hero.BattleItems.Remove("gun");
                    hero.Items.Remove("oil");
                    hero.BattleItems.Add("fine gun");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(
                        "You approach the workbench and find that it has the exact tools you need to oil a gun!\n" +
                        "Too bad you dont have the items needed D:!\n" +
                        "You move on!");
                    Console.ReadLine();
                    hero.location = "bossroom";
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You ignore the workbench");
                hero.location = "bossroom";
            }

        }

        static void Bossroom(Hero hero) //Bossrum
        {
            Enemy enemy2 = new Enemy("Minotaur", 100, 10, "Slashed you");
            Console.Clear();
            Console.WriteLine("As you open the doors to the grand hall you feel great pressure from the within.\n" +
                              "You continue deeper as thick fog covers the sides and corners of the spacious hall.\n" +
                              "Suddenly, as you reach the dead center of the eerie room, something big lands in front of you with a thud.\n" +
                              "It got horns as the devil himself, equipped with a big, bloody Sword\n" +
                              "You feel your pulse increase as you ready yourself for what seems to be a dangerous battle");
            Console.ReadLine();

            Fight(hero, enemy2);

            Console.Clear();
            Console.WriteLine($"With a gurgling roar, the {enemy2.enemyName} falls lifeless to the ground.\n" +
                              "You feel the fog and pressure disappearing from the hall\n" +
                              "You finally feel at peace as you continue to the golden door in the end of the room,\n" +
                              "As you get closer you see a strobing green, brightly lit sign hanging from the roof.\n" +
                              "spelled on it is EMERGENCY EXIT\n");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("This is it...");
            Console.ReadLine();
        }

        static void Quit(Hero hero) // Quit
        {
            Console.Clear();
            Console.WriteLine(
                "As you feel unconsciousness approaching you get an ominous feeling at the back of your neck!\n" +
                "As you wake up not knowing how much time has passed,");
            Console.ReadLine();
            switch (!AskYesOrNo("Do you want to quit the game?"))
            {
                case true:
                    hero.Items.Clear();
                    hero.BattleItems.Clear();
                    hero.Health = 100;
                    hero.HeroIsDead();
                    hero.location = "newgame";
                    break;
                case false:
                    Environment.Exit(0);
                    break;
            }
        }

        static void Main(string[] args) // Main String
        {
            Console.WriteLine("Lets play Dungeon Crawl 1!!!");

            Hero hero = new Hero();


            do
            {
                if (hero.location == "newgame")
                {
                    NewGame(hero);
                }
                else if (hero.location == "Tableroom")
                {
                    Tableroom(hero);
                }
                else if (hero.location == "Corridor")
                {
                    Corridor(hero);
                }
                else if (hero.location == "Nyckelrum")
                {
                    Nyckelrum(hero);

                }
                else if (hero.location == "puzzleroom")
                {
                    Puzzleroom(hero);
                }
                else if (hero.location == "fightroom1")
                {
                    Fightroom1(hero);
                }
                else if (hero.location == "fightroom1")
                {
                    Fightroom1(hero);
                }
                else if (hero.location == "preproom")
                {
                    Preproom(hero);
                }
                else if (hero.location == "bossroom")
                {
                    Bossroom(hero);
                }
                else
                {
                    Console.Error.WriteLine($"You forgot to implement '{hero.location}'!");
                }

                if (hero.location == "quit")
                {
                    Quit(hero);
                }

            } while (!hero.HeroIsDead());
        }
    }
}
