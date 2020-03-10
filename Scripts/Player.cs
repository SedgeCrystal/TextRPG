

public class Player :Charater
{
    
    

    public Player(string name)
    {
        this.Name = name;
        this.MaxHp = 10;
        this.Hp = this.MaxHp;
        this.Atk = 3;

    }
    public  string Attack(Enemy enemy)
    {
        if(enemy.distance > 0)
        {
            enemy.distance--;
            return enemy.Name + "に近づいた";
            
        }
        return base.Attack(enemy);
    }

    
}
