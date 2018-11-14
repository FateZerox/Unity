using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//定义基本的技能类
public class Skill {

    //附加状态，可以用与操作表示几种状态共存，后期修改成buff类
    public enum State
    {
        None = 1 << 0,
        Paralyzed = 1 << 1,//闪电麻痹
        Ether = 1 << 2,//以太的
        Burning = 1 << 3,//燃烧的
        Poisoning = 1 << 4,//中毒的
    }

    //技能类型枚举，暂时未定如何使用
    public enum SkillType
    {
        normal = 1,
        attack,
        heal,

    }

    public int skillId;//技能ID
    public string name = "";//技能名字
    public string desc = "";//技能描述
    public string ItemIcon;//技能图标
    public SkillType type;//技能类型
    public int cd = 0;//技能cd
    public int costAP = 0;//技能消耗AP
    public double multiply = 1.0;//技能伤害倍率
    public double attackRange = 1.0;//技能攻击范围
    public State state;//技能附加状态


    public Skill(int skillId,string name,string desc, string ItemIcon,SkillType type,int cd,int costAP,double multiply,double attackRange,State state)
    {
        this.skillId = skillId;
        this.name = name;
        this.desc = desc;
        this.ItemIcon = ItemIcon;
        this.type = type;
        this.cd = cd;
        this.costAP = costAP;
        this.multiply = multiply;
        this.attackRange = attackRange;
        this.state = state;
    }


}
