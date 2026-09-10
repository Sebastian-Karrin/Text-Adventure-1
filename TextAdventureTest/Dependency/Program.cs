using System;
using System.Reflection.Metadata.Ecma335;

namespace TextAdventure
{
    
  /*  class Items // Items Class
    {
        public string itemName;
        


        public Items(string name)
        {
            itemName = name;
        }
    }
    class BattleItems // Battle Items
        {
            public string itemName;



            public BattleItems(string name)
            {
                itemName = name;
            }
        } */

    
        class Program
        {

            static int Weapon(Hero hero, int amount)
            {

                Console.Clear();
                Console.WriteLine($"You used {hero.equip}");
                Console.ReadLine();

                if (hero.equip == "gun")
                {
                    switch (RollD6() >= 4)
                    {
                        case true:
                            amount = 15;
                            break;
                            
                            case false :
                            Console.WriteLine("The gun jammed.....");
                            Console.ReadLine();
                            amount = 0;
                            break;
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

                return amount;
            }

            


            static void Fight(Hero hero, Enemy enemy) // Fight
            {
                Console.Clear();
                Console.WriteLine($"You enter combat with {enemy.enemyName}");
                Console.ReadLine();
                
                hero.Items.Add("block");
                while (!hero.HeroIsDead() && !enemy.EnemyIsDead())
                {
                    Console.Clear();
                    Console.WriteLine($"Your Health:{hero.Health}\n" +
                                      $"{enemy.enemyName} Health: {enemy.enemyHealth}");
                    
                    
                    Console.WriteLine("Your Choices: ");
                    foreach (string BattleItem in hero.BattleItems)
                    {
                        Console.Write(BattleItem);
                    }

                    string battleitem = Ask("What do you choose?").Trim().ToLower();
                   
                    if (hero.BattleItems.Contains(hero.equip))
                    {
                        Console.Clear();
                        Console.WriteLine($"You used {battleitem}");
                        Console.Read();
                        if (battleitem == "wooden sword" || battleitem == "gun" || battleitem == "knife")
                        {
                            
                            Console.WriteLine($"{enemy.enemyName} took {hero.GiveDamage()} damage");
                            enemy.TakeDamage(hero.GiveDamage());
                            Console.Read();
                        }
                        else if (battleitem == "potion")
                        {
                            Console.WriteLine($"You used {battleitem}");
                            hero.Potion();
                            Console.WriteLine("You healed 30 Health");
                            hero.Items.Remove("potion");
                            Console.Read();
                        }
                        else if (battleitem == "block")
                        {
                            Console.WriteLine($"You used {battleitem}");
                            hero.block = true;
                            Console.WriteLine($"You are now ready the {enemy.enemyName}'s attack");
                        }
                    }

                    Console.Clear();
                    Console.WriteLine($"{enemy.enemyName}'s Turn");
                    Console.Read();
                    int monsterdecision = RollD6();
                    if (monsterdecision > 4)
                    {
                        Console.Clear();
                        Console.WriteLine($"The {enemy.enemyName} used Tackle");
                        Console.WriteLine("You took" + $"{hero.TakeDamage(enemy.GiveDamage())}" + "damage");
                        Console.ReadLine();
                    }
                    else if (monsterdecision > 2 && monsterdecision < 5)
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
                        Console.WriteLine($"The {enemy.enemyHealth} is resting");
                        enemy.Rest();
                        Console.WriteLine($"The {enemy.enemyName} healed 3 health");
                        Console.ReadLine();
                    }
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
                    hero.location = ("quit");

                }
            }

            static int RollD6() // RNG
            {
                Random random = new Random();
                int roll = random.Next(1, 7);
                return roll;
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
                string name = "";
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

                

                hero.BattleItems.Add("Wooden Sword");
                Console.WriteLine("You are equipped with a Wooden Sword");
                bool finished = false;
                do
                {
                    Console.WriteLine(" You are to slay the monster at the end of the dungeon" +
                                      "" +
                                      " In front of you is a stone table with two items on it," +
                                      $" Key and a Knife" +
                                      " You can only pick up one of these items.");

                    string tableroomItem =
                        Ask("Which one do you choose?" + "(key, knife, None)").Trim().ToLower();
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
                                Console.WriteLine("You didnt pick annything up!");
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
                string direktion = Ask("Which way do you want to go?" + "Left/Right ").Trim().ToLower();
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
                                  "But you only have enough space in your inventory for your wooden sword\n");
                if (AskYesOrNo("Would you like to get rid of your wooden sword and pick up the weathered gun? "))
                {
                    Console.Clear();
                    Console.WriteLine("you get rid of the sword and holster the new weapon");
                    hero.BattleItems.Remove("wooden sword");
                    hero.BattleItems.Add("Weathered Gun");
                    Console.ReadLine();
                }

                hero.location = "puzzleroom";


            }

            static void Puzzleroom(Hero hero) // Puzzleroom
            {

                Console.Clear();
                Console.WriteLine(" You enter the room and leaning against one of the walls you find a corpse\n" +
                                  " You approach the corpse and it looks like it has something in its pocket");
                if (AskYesOrNo("Do you want to loot the corpse? "))
                {
                    if (RollD6() >= 4)
                    {
                        if (hero.Items.Contains("gun"))
                        {
                            Console.Clear();
                            Console.WriteLine("You find some kind of oil\n" +
                                              "Maybe it's apliable to a gun?");
                            hero.Items.Add("Oil");
                            Console.ReadLine();
                            hero.location = "fightroom1";

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You find what looks to be a healing poition!");
                            hero.BattleItems.Add("Potion");
                            Console.ReadLine();
                            hero.location = "fightroom1";
                        }

                    }
                    else if (RollD6() <= 3)
                    {
                        Console.Clear();
                        Console.WriteLine(" You reach into the corpses pocket and you feel the sting of pain!\n" +
                                          "You pull your hand out of the corpses pocket and you see a snake" +
                                          " slithering out.\n" +
                                          "You lose 10hp");
                        hero.TakeDamage(10);
                        Console.WriteLine("Current health: " + hero.Health);
                        Console.ReadLine();
                        hero.location = "fightroom1";
                    }


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You ignore the corpse");
                    Console.ReadLine();
                    hero.location = "fightroom1";
                }

            }

            static void fightroom1(Hero hero) //Fightroom1
            {
                Enemy enemy1 = new Enemy("Sentient Rock", 20, 5);

                Console.Clear();
                Console.WriteLine("You enter to find a room dimly lit with torches lining the walls. \n" +
                                  "In the center of the room lies an unassuming looking rock\n" +
                                  "It looks small enough that you could lift it, but big enough that it might prove difficult!\n" +
                                  "As you approach the rock it lunges at you, and you find yourself barely able to dodge," +
                                  $"it is then you realize. Its not an ordinary rock, its a {enemy1.enemyName}");
                Console.ReadLine();

                Fight(hero, enemy1);


            }



            static void Main(string[] args) // Main String
            {
                Console.WriteLine("Lets play Dungeon Crawl 1!!!");

                Hero hero = new Hero();



                while (hero.location != "quit")
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
                        fightroom1(hero);
                    }
                    else
                    {
                        Console.Error.WriteLine($"You forgot to implement '{hero.location}'!");

                    }
                }
            }
        }
    }

