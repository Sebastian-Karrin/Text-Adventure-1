using System;

namespace TextAdventure
{
    class Hero
    {
        public string name = "";
        public int Health = 100;
        public List<string> Items = new List<string>();
        public string location = "newgame";
    } //Hero Class

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
       
       static void Tableroom (Hero hero) // Tableroom rum
       {
            Console.Clear();
            hero.Items.Add("Wooden Sword");
            Console.WriteLine("You are equipped with a Wooden Sword!" + 
                              " You are to slay the monster at the end of the dungeon" + 
                              "" + 
                              " In front of you is a stone table with two items on it," + 
                              " a key and a knife" + 
                              " You can only pick up one of these items.");
           string tableroomItem =  Ask("Which one do you choose?" + " (Key, Knife, None)").Trim().ToLower();
            if(tableroomItem == "key")
            { 
                Console.Clear();
                while (!AskYesOrNo("Do you want to choose the key?"));
                {
                    hero.Items.Add("Key");
                    Console.WriteLine("You picked up the key!");
                    hero.location = "Corridor";
                    
                }
            }
            else if (tableroomItem == "knife")
            {
                Console.Clear();
                while (!AskYesOrNo("Do you want to choose the knife?")) ;
                {
                    hero.Items.Add("knife");
                    Console.WriteLine("You picked up the knife!");
                    hero.location = "Corridor";
                }
            }
            else if (tableroomItem == "none") {
                    Console.Clear();
                    while (!AskYesOrNo("Do you want to choose none?"));
                    {
                        Console.WriteLine("You didnt pick annything up!");
                        hero.location = "Corridor";
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
                }
                else
                {
                    Console.Error.WriteLine($"You forgot to implement '{hero.location}'!");

                }
            }
        }
    }
}