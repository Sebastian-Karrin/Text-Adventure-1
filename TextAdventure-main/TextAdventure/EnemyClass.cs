namespace TextAdventure
{ 
    class Enemy //Enemy Class
    {
        public string enemyName;
        public int enemyHealth;
        public int enemyDamage;
        private int enemyMax;
        public string enemyAttack;
        public bool empower;
        
        public Enemy(string name, int health, int damage, string attack)
        {
            enemyName = name;
            enemyHealth = health;
            enemyDamage = damage;
            enemyMax = health;
            enemyAttack = attack;
        }

        public void TakeDamage(int damage)
        {
            enemyHealth -= damage;
        }
       
        public int GiveDamage()
        {
            int amount = enemyDamage;
            if (empower)
            {
                amount += 2;
            }

            empower = false;
            return amount;
        }
      
        public int Rest()
        {
            enemyHealth += enemyMax/10;
            return enemyMax / 10;
        }

        public bool EnemyIsDead()
        {
            return enemyHealth <= 0;
        }

    }
}

