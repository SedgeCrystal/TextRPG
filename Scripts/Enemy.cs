
using UnityEngine;

public class Enemy : Charater
{

    public int distance{ get; set; }
    public bool IsRecognize { get => isRecognize; set => isRecognize = value; }

    int recognizeRatio;

    
    private bool isRecognize;

    public int itemId;

    public Enemy(EnemyData enemyData)
    {
        this.Name = enemyData.name;
        this.MaxHp = enemyData.maxHp;
        this.Hp = this.MaxHp;
        this.Atk = enemyData.atk;
        this.distance = Random.Range(1,4);
        this.recognizeRatio = 3;
        
        this.IsRecognize = (Random.Range(0,10) <3);
        this.itemId = enemyData.itemId;
        

    }

    public string Action(Player player)
    {
        if (!IsRecognize)
        {
            if (Random.Range(0,3) + this.distance <= recognizeRatio)
            {
                IsRecognize = true;
                return this.Name +  "はこちらに気づいた！";
            }
            else
            {
                return this.Name + "は気が付いていない";
            }
          
        }

        if(this.distance > 0)
        {
            distance--;
            return this.Name + "は近づいた";
        }

        return base.Attack(player);
    }
}
