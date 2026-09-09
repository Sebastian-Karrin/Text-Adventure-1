using System;

namespace EnemyClassAdventure
{ 
    public class Enemy //Enemy Class
    {
        public string enemyName;
        public int enemyHealth;
        public int enemyDamage;

        public Enemy(string name, int health, int damage)
        {
            enemyName = name;
            enemyHealth = health;
            enemyDamage = damage;
        }

        public void TakeDamage(int damage)
        {
            enemyHealth -= damage;
        }

        public bool IsDead()
        {
            return enemyHealth <= 0;
        }

    }
    
}

