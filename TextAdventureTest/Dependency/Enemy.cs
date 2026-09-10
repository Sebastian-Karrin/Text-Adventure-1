namespace TextAdventure;

class Enemy //Enemy Class
{
    public string enemyName;
    public int enemyHealth;
    public int enemyDamage;
        
    public bool empower;
        
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
      
    public void Rest()
    {
        enemyHealth += 3;
    }

    public bool EnemyIsDead()
    {
        return enemyHealth <= 0;
    }

}
