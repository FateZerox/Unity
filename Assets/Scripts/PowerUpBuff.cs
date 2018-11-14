using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//增加攻击力的buff
public class PowerUpBuff : Buff
{
    private int powerUpValue;
    public PowerUpBuff(int buffId,int duration,GameObject obj,int data) : base(buffId,duration, obj,data)
    {
        powerUpValue = data;

    }

    public override void Activate()
    {
        
        this.obj.GetComponent<CombatUnit>().attack += powerUpValue;//攻击力增加2


    }

    public override void End()
    {
        this.obj.GetComponent<CombatUnit>().attack -= powerUpValue;//攻击力回到初始值
    }
}

//中毒的buff
public class PoisoningBuff : Buff
{
    private int poisoning;
    public PoisoningBuff(int buffId, int duration, GameObject obj,int data) : base(buffId, duration, obj,data)
    {
        poisoning = data;

    }

    public override void Activate()
    {

        this.obj.GetComponent<CombatUnit>().hp -= poisoning;//血量减少


    }

    public override void End()
    {
        
    }
}