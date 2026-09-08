using System;

namespace TextAdventure
{
    class Hero  //Hero Class
    {
        public string name = "";
        public int Health = 100;
        public List<string> Items = new List<string>();
        public string location = "newgame";
    }

    class Enemy //Enemy Class
    {
        public string enemyName;
        public int enemyHealth;
        public int enemyDamage;

        public Enemy(string name, int health, int damage)
        {
            enemyName = name;
            enemyHealth = health;
            enemyDamage  = damage;
        } 
        
    }
    
   /* class Items // Items Class
    {
        public string itemName;
        public int itemQuantity;

        public Items(string name, int quantity)
        {
            itemName = name;
            itemQuantity = quantity;
        }
    } */

    class Program
    {
        static int RollD6() // RNG
        {
            Random random = new Random();
            int roll = random.Next(1, 7);
            return roll;
        }
        
        static string Ask(string question)
        {
            string response;
            do
            {
                Console.Write(question);
                response = Console.ReadLine().Trim();
            } while (response == "");

            return response;
        } // FrågeFunktion

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


        static void NewGame(Hero hero)
        {
            Console.Clear();
            string name = "";
            do
            {
                name = Ask("What is your name? ");
            } while (!AskYesOrNo($"So your name is {name}? "));

            hero.name = name;
            hero.location = "Tableroom";
        } // New Game

        static void Tableroom(Hero hero) // Tableroom rum
        {
            do
            {
                Console.Clear();
                

                hero.Items.Add("Wooden Sword");
                Console.WriteLine("You are equipped with a Wooden Sword!" +
                                  " You are to slay the monster at the end of the dungeon" +
                                  "" +
                                  " In front of you is a stone table with two items on it," +
                                  " a Key and a Knife" +
                                  " You can only pick up one of these items.");
                string tableroomItem = Ask("Which one do you choose?" + " (Key, Knife, None)").Trim().ToLower();
                if (tableroomItem == "key" || tableroomItem == "knife")
                {
                    Console.Clear();
                    while (!AskYesOrNo($"Do you want to choose the {tableroomItem}?")) ;
                    {
                        hero.Items.Add($"{tableroomItem}");
                        Console.WriteLine($"You picked up the {tableroomItem}!");
                        hero.location = "Corridor";
                        break; 
                    }
                }
                else if (tableroomItem == "none")
                {
                    Console.Clear();
                    while (!AskYesOrNo("Do you want to choose none?")) ;
                    {
                        Console.WriteLine("You didnt pick annything up!");
                        hero.location = "Corridor";
                        break;
                    } 
                }
                
            } while (true);
        }

        static void Corridor(Hero hero) // Corridor
        {
            Console.Clear();

            Console.WriteLine("You exit the room with the table and you find yourself standing in the dark" +
                              "hallway. You kan either enter the room to the right, or the room to the left");
            string direktion = Ask("Which way do you want to go?" + "Left/Right " ).Trim().ToLower();
            if (direktion == "left")
            {
                if (hero.Items.Contains("key"))
                {
                    Console.WriteLine("You enter the key into the keyhole and the door opens");
                    Console.Read();
                    hero.location = "Nyckelrum";
                    hero.Items.Remove("key");
                } 
                else
                {
                    Console.WriteLine("You didnt have the key so you enter the other room");
                    Console.Read();
                    hero.location = "puzzleroom";
                }
            }

            if (direktion == "right")
            {
                Console.WriteLine("You enter the room to the right");
                Console.Read();
                hero.location = "puzzleroom";
            }
        }

        static void Nyckelrum(Hero hero)  // NyckelRum
        {
            Console.Clear();
            Console.WriteLine("You enter a gloomy room \n" +
                              "You can make out a faint sillhouette of a oxidized copper chest\n" +
                              "As you make your way to it, you feel the presence of something mighty\n" +
                              "Inside of the chest you find a weathered gun\n" +
                              "But you only have enough space in your inventory for your wooden sword\n");
                if (AskYesOrNo("Would you like to get rid of your wooden sword and pick up the weathered gun? "))
            { Console.Clear();
            Console.WriteLine("you get rid of the sword and holster the new weapon");
            hero.Items.Remove("wooden sword");
            hero.Items.Add("gun");
            Console.Read();
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
                        Console.Read();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You find what looks to be a healing poition!");
                        hero.Items.Add("Potion");
                        Console.Read();
                    }
                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" You reach into the corpses pocket and you feel the sting of pain!\n" +
                                      "You pull your hand out of the corpses pocket and you see a snake" +
                                      " slithering out.\n" +
                                      "You lose 10hp");
                    hero.Health -= 10;
                    Console.WriteLine("Current health: " + hero.Health);
                    Console.Read();
                }

                
            }

        }
    


    static void Main(string[] args) // Huvudspel
        {
            Console.WriteLine("Lets play Dungeon Crawl 1!!!");

            Hero hero = new Hero();
            while (hero.location != "quit")
            {
                if (hero.location == "newgame")
                {
                    NewGame(hero);
                } else if (hero.location == "Tableroom")
                {
                    Tableroom(hero);
                } else if (hero.location == "Corridor")
                {
                    Corridor(hero);
                } else if (hero.location == "Nyckelrum")
                {
                    Nyckelrum(hero);
                    
                } else if (hero.location == "puzzleroom")
                {
                    Puzzleroom(hero);
                }
                else
                {
                    Console.Error.WriteLine($"You forgot to implement '{hero.location}'!");

                }
            }
        }
    }
}