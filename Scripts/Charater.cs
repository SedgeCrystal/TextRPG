

public abstract class Charater
{
    private string _name;
    private int maxHp;
    private int _hp;
    private int _atk;

    public string Name { get => _name; set => _name = value; }
    public int Hp { get => _hp;
        set {
            if (value < 0)
            {
                _hp = 0;
            }
            else if(value > this.MaxHp)
            {
                _hp = this.MaxHp;
            }
            else
            {
                _hp = value;
            }
        }
    }
    public int Atk { get => _atk; set => _atk = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }

    public string Attack(Charater charater)
    {
        charater.Hp = charater.Hp - this.Atk;
        return  this.Name + "の攻撃 "+ charater.Name + "に" + this.Atk + "のダメージ";
    }


}
