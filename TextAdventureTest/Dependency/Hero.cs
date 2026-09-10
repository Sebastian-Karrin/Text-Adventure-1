namespace TextAdventure
{

    class Hero //Hero Class
    {
        public string name = "";
        public int Health = 100;
        public List<string> Items = new List<string>();
        public List<string> BattleItems = new List<string>();
        public string location = "newgame";
        public bool block;
        public int Damage;
        public string equip;


        public void Potion()
        {
            Health += 30;
        }

        public int TakeDamage(int amount)
        {
            if (block)
            {
                amount -= amount / 2;
            }

            if (amount > 0)
            {
                Health -= amount;
            }

            block = false;
            return amount;
        }

        public int GiveDamage()
        {
            return Damage;
        }

        public bool HeroIsDead()
        {
            return Health <= 0;
        }
    }
}