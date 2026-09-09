using system;

namespace TextAdventure
{
    public class Hero //Hero Class
    {
        public string name = "";
        public int Health = 100;
        public List<string> Items = new List<string>();
        public string location = "newgame";

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }