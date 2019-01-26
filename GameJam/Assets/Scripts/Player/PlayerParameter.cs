using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameter : PoolObject
{
    static int MaxEnerg;		//最大电量
    static int Weight;              //重量
    static int SolarChargeSpeed;    //电力耗尽后复活速度
    public float Energy;                     //当前电量
    public int Trash;                      //携带垃圾量
    public int Power;                      //电量消耗速度
    public float MaxSpeed;                 //最大移动速度
    public int Scores;						//最后的分数
    public bool Lying;                      //电量耗尽
    GameObject garbage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hitted(int damage)//受到伤害
    {
        if (Energy >= damage)
            Energy -= damage;
        else
        {
            Energy = 0;
        }
        int temp = (int)Random.Range(6, 9);
        Loss_Garbage(temp);
       for(int i = 0;i<temp;i++)
        {
           
        }

    }
    public void Pick_Garbage(int num)
    {
        Trash += num;
    }
    public void Loss_Garbage(int num)
    {
        if (Trash >= num)
            Trash -= num;
        else
            Trash = 0;
    }

    void PowerDown()
    {
        if (Energy >= Power * Time.deltaTime)
            Energy -= Power * Time.deltaTime;
        else
        {
            Energy = 0;
            Lying = true;
        }
    }

}
