using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemDataList:ScriptableObject
{

    public List<ItemData> dataList = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
    public string name;
    public int id;
    public int value;
    public string itemInfo;
    public int atk;
    public int heal;
    public bool canEscape;
    
}
