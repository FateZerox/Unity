using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Buff
{
    protected int buffId;//buff的id
    protected int duration;//buff持续时间
    protected int data;//储存伤害或者治疗量
    protected GameObject obj;
    public bool isFinished {
        get{ return duration <= 0 ? true : false; }
    }

    public Buff(int buffId,int duration,GameObject obj,int data)
    {
        this.buffId = buffId;
        this.duration = duration;
        this.obj = obj;
        this.data = data;
    }

    //持续时间减1
    public void Tick()
    {
        duration -= 1;
        if(duration <= 0)
        {
            End();
        }
    }

    public abstract void Activate();//buff生效,更新角色属性信息S
    public abstract void End();//buff失效
}
