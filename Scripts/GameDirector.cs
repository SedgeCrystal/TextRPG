using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    int distance;
    int maxD = 20;
    int leftTime;

    int money;
    int goalMoney = 100;
    
    float waitTime;
    float wTMax = 1f;
    bool isPlayerTurn;

    bool isKO_golem;
    public bool isClear;

    int canEscapeD = 5;

    List<string> logStringList;
    int maxLog = 5;
    char[] separator = new char[] { ' '};

    Player player;
    Enemy enemy;
    List<Item> items;
    int index_item;
    int itemMax;


    Text logText;
    Text playerText;
    Text enemyText;
    Text qInfoText;
    Text AtkButtonText;
    Text EscapeButtonText;
    Text ExpButtonText;
    Text ItemText;
    Text ItemInfoText;

    int encountRatio;
    public ItemDataList itemDataList;
    public EnemyDataList enemyDataList;
    
    // Start is called before the first frame update
    void Awake()
    {
        this.distance = 0;
        this.leftTime = 100;
        this.isPlayerTurn = true;
        this.isKO_golem = false;
        this.isClear = false;

        this.logStringList = new List<string>();
        this.logStringList.Add("あなたは出発した！");

        this.player = new Player("あなた");
        
        this.items = new List<Item>();
        this.index_item = 0;
        this.itemMax = 14;
        this.encountRatio = 2;
        this.logText = GameObject.Find("LogPanel").GetComponentInChildren<Text>();
        this.playerText = GameObject.Find("PlayerPanel").GetComponentInChildren<Text>();
        this.enemyText = GameObject.Find("EnemyPanel").GetComponentInChildren<Text>();
        this.qInfoText = GameObject.Find("QuestInfoPanel").GetComponentInChildren<Text>();
        this.ItemText = GameObject.Find("ItemPanel").GetComponentInChildren<Text>();
        this.ItemInfoText = GameObject.Find("ItemInfoPanel").GetComponentInChildren<Text>();
        this.AtkButtonText = GameObject.Find("AtkButton").GetComponentInChildren<Text>();
        this.EscapeButtonText = GameObject.Find("EscapeButton").GetComponentInChildren<Text>();
        this.ExpButtonText = GameObject.Find("ExpButton").GetComponentInChildren<Text>();



        DontDestroyOnLoad(this);
    }

    

    public void OnClick_Atk()
    {
        
        if (isPlayerTurn)
        {
            if (this.enemy == null)
            {
                if (this.distance < maxD)
                {
                    distance++;

                    this.logStringList.Add(this.player.Name + "は前進した");


                    if (this.distance > 0)
                    {
                        if (distance == (maxD - 1) &&!isKO_golem) {
                            this.enemy = new Enemy(enemyDataList.dataList[2]);
                        }
                        else if (Random.Range(0, 10) < (this.encountRatio + distance / 10))
                        {
                            this.enemy = new Enemy(enemyDataList.dataList[Random.Range(0, 2)]);
                        }

                        if (this.enemy != null)
                        {
                            logStringList.Add(enemy.Name + "を発見した！");
                        }

                    }
                    this.leftTime--;
                }
                else
                {
                    this.logStringList.Add("これ以上進めない");
                }
            }
            else
            {
                string s = this.player.Attack(enemy);
                string[] logs = s.Split(separator);

                foreach(string log in logs)
                {
                    this.logStringList.Add(log);
                }
                isPlayerTurn = false;
                this.waitTime = 0;
            }
            
            
        }
    }

    public void OnClick_Esc()
    {
        if (isPlayerTurn)
        {
            if (this.enemy == null)
            {
                if (distance > 0)
                {
                    distance--;
                    this.logStringList.Add(this.player.Name + "は後退した");
                    if (this.distance > 0 && Random.Range(0, 10) < (this.encountRatio + distance / 10))
                    {
                        this.enemy = new Enemy(enemyDataList.dataList[Random.Range(0, 2)]);
                        logStringList.Add(enemy.Name + "を発見した！");

                    }
                    this.leftTime--;
                }
                else
                {
                    if (items.Count > 0)
                    {
                        this.money += items[index_item].Value;
                        this.logStringList.Add(string.Format("{0}を売却した", items[index_item].Name));
                        items.RemoveAt(index_item);
                        if (index_item >= items.Count)
                        {
                            index_item = (items.Count - 1);
                            if (index_item < 0)
                            {
                                index_item++;
                            }
                        }
                    }
                    else
                    {
                        this.logStringList.Add("アイテムを持っていない");
                    }
                }
            }
            else
            {
                
                if(enemy.distance < canEscapeD)
                {
                    enemy.distance++;
                    this.logStringList.Add(this.player.Name + "は後退した");
                    isPlayerTurn = false;
                    this.waitTime = 0;
                }
                else
                {
                    this.logStringList.Add(this.player.Name + "は逃げ出した！");
                    enemy = null;
                    this.distance--;
                }
                
                
            }


        }
    }

    public void OnClick_Item()
    {
        if (items.Count > 0)
        {


            string s = items[index_item].Function(player, enemy);
            string[] logs = s.Split(separator);

            foreach (string log in logs)
            {
                this.logStringList.Add(log);

            }
            if (items[index_item].CanEscape)
            {
                distance--;
            }
            
            if (enemy != null)
            {
                
                isPlayerTurn = false;
                this.waitTime = 0;
            }
            items.RemoveAt(index_item);

        }
        else
        {
            this.logStringList.Add("アイテムを持っていない！");
        }
        if(index_item >= items.Count)
        {
            index_item =(items.Count - 1);
            if(index_item < 0)
            {
                index_item++;
            }
        }
    }

    public void OnClick_Exp()
    {
        if (isPlayerTurn)
        {
            if (this.enemy == null)
            {

                this.logStringList.Add(string.Format("{0}は周囲を探索した", player.Name));
                int r = Random.Range(0, 10);
                if (distance == maxD)
                {
                    this.logStringList.Add(this.AddItem(new Item(itemDataList.dataList[5])));
                }
                else if (r == 0 && distance > 10)
                {
                    this.logStringList.Add(this.AddItem(new Item(itemDataList.dataList[4])));
                }
                else if(this.distance > 5)
                {
                    r = (r - 1) / 3;
                    if(distance > 10)
                    {
                        r++;   
                    }
                    this.logStringList.Add(this.AddItem(new Item(itemDataList.dataList[r])));
                }
                else
                {
                    this.logStringList.Add(this.AddItem(new Item(itemDataList.dataList[0])));
                }

                if (this.distance > 0 && Random.Range(0, 10) < (this.encountRatio + distance / 10))
                {
                    this.enemy = new Enemy(enemyDataList.dataList[Random.Range(0, 2)]);
                    logStringList.Add(enemy.Name + "を発見した！");

                }

                this.leftTime--;
            }
            else
            {
                
                
                this.logStringList.Add(string.Format("{0}はその場に留まった",player.Name));
                
                isPlayerTurn = false;
                this.waitTime = 0;
            }


        }

    }

    public void OnClick_CI()
    {
        this.index_item++;
        if(this.index_item >= items.Count)
        {
            index_item = 0;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if(this.money >= this.goalMoney)
        {
            this.isClear = true;
            SceneManager.LoadScene("ResultScene");
        }

        if(leftTime < 0)
        {
            SceneManager.LoadScene("ResultScene");
        }

        if (enemy != null)
        {
            this.ExpButtonText.text = "待機"; 
            if(enemy.distance == 0)
            {
                this.AtkButtonText.text = "攻撃";
            }
            else
            {
                this.AtkButtonText.text = "前進";
            }
            if(enemy.distance == canEscapeD)
            {
                this.EscapeButtonText.text = "逃げる";
            }
            else
            {
                this.EscapeButtonText.text = "後退";
            }
        }
        else
        {
            this.AtkButtonText.text = "前進";
            this.EscapeButtonText.text = "後退";
            this.ExpButtonText.text = "探索";
        }

        if (this.distance == 0)
        {
            EscapeButtonText.text = "売却";
        }



        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("村から{0}/{1}里",this.distance,this.maxD));
        
        sb.Append("\n期限まであと");
        sb.Append(this.leftTime);
        sb.Append(string.Format("回\n残金/目標:{0}/{1}", this.money, this.goalMoney));
        this.qInfoText.text = sb.ToString();
        sb.Clear();

        if(enemy != null && enemy.Hp <= 0)
        {
            this.logStringList.Add(enemy.Name + "を倒した");
            if(enemy.Name == enemyDataList.dataList[2].name)
            {
                this.isKO_golem = true;
            }
            this.isPlayerTurn = true;
            Item item = new Item(itemDataList.dataList[enemy.itemId]);
            this.logStringList.Add(this.AddItem(item));
            this.enemy = null;
            
        }
        if(enemy == null)
        {
            this.enemyText.text = "敵は見当たらない...";

        }
        else
        {
            sb.Append(enemy.Name);
            sb.Append(string.Format("\nHp:{0}/{1}",enemy.Hp,enemy.MaxHp));
            
            sb.Append("\n距離:");
            sb.Append(enemy.distance);
            
            sb.Append(string.Format("\n状態:{0}", enemy.IsRecognize?"興奮":"警戒"));

            this.enemyText.text = sb.ToString();
            sb.Clear();
        }

        if(player.Hp <=0)
        {
            SceneManager.LoadScene("ResultScene");
        }
        sb.Append(player.Name);
        sb.Append(string.Format("\nHp:{0}/{1}",player.Hp,player.MaxHp));
        

        this.playerText.text = sb.ToString();
        sb.Clear();

        if (!isPlayerTurn)
        {
            this.waitTime += Time.deltaTime;
            if (this.waitTime >= this.wTMax)
            {


                string s = this.enemy.Action(this.player);
                string[] logs = s.Split(separator);

                foreach (string log in logs)
                {
                    this.logStringList.Add(log);
                }
                this.isPlayerTurn = true;
            }
        }


        if(logStringList.Count > maxLog)
        {
            int num = logStringList.Count - maxLog;
            for(int i = 0;i < num; i++)
            {
                logStringList.RemoveAt(0);
            }


        }

        foreach(string logStr in logStringList)
        {
            sb.Append(logStr);
            sb.Append("\n");
        }
        logText.text = sb.ToString();
        sb.Clear();

        sb.Append("アイテム一覧\n");
        if (items.Count > 0)
        {
            for(int i = 0; i <items.Count;i++)
            {
                if(i == index_item)
                {
                    sb.Append("▶");
                }
                else
                {
                    sb.Append("　");
                }
                sb.Append(items[i].Name + "\n");
            }
           
        }
        this.ItemText.text = sb.ToString();
        sb.Clear();

        sb.Append("アイテム情報\n");
        if(items.Count == 0) {

             sb.Append("なし");
        }
        else
        {
            sb.Append(items[index_item].ItemInfo);
            sb.Append(string.Format("\n\n売値:{0}",items[index_item].Value));

        }
        this.ItemInfoText.text = sb.ToString();
        sb.Clear();

    }

    string AddItem(Item item)
    {
        if(this.items.Count < this.itemMax)
        {
            items.Add(item);
            return item.Name + "を持ち物に加えた";
        }
        else
        {
            return   "持ち物がいっぱいだ";
        }
    }

    public void OnClick_home()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
