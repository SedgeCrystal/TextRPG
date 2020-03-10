
public class Item 
{
    public ItemDataList itemDataList;
    int id;
    string name;
    int value;
    string itemInfo;
    int atk;
    int heal;
    bool canEscape;
    private int v;

    public Item(ItemData itemData)
    {

        this.id = itemData.id;
        this.Name = itemData.name;
        this.value = itemData.value;
        this.itemInfo = itemData.itemInfo;
        this.atk = itemData.atk;
        this.heal = itemData.heal;

        this.CanEscape = itemData.canEscape;

    }

    public int Value { get => value; set => this.value = value; }
    public string ItemInfo { get => itemInfo; set => itemInfo = value; }
    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public bool CanEscape { get => canEscape; set => canEscape = value; }

    public string Function(Player player, Enemy enemy)
    {
        if(enemy == null && this.heal == 0)
        {
            return this.Name + "を投げ捨てた！";
        }
        player.Hp += this.heal;
        
        string s = player.Name + "は" + this.Name + "を使った！ ";
        if(this.heal > 0)
        {
            s += player.Name + "は回復した！";
        }
        
        if(this.atk > 0)
        {
            s += enemy.Name + "に" +this.atk +  "のダメージ";
            enemy.Hp -= this.atk;
        }

        if (CanEscape)
        {
            s += player + "は逃げ出した！";
        }

        return s;
    }
}



