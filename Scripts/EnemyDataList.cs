using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class EnemyDataList : ScriptableObject
{

    public List<EnemyData> dataList = new List<EnemyData>();
}

[System.Serializable]
public class EnemyData
{
    public string name;
    public int maxHp;
    public int atk;
    public int itemId;
    public int distance;
    public int recognizeRatio;


}
