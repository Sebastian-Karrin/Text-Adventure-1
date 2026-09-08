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
                    hero.location = "nyckelrum";
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
                }
                else
                {
                    Console.Error.WriteLine($"You forgot to implement '{hero.location}'!");

                }
            }
        }
    }
}