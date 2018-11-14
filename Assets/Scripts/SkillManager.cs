using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此脚本用来初始化所有技能
public class SkillManager{

    private static SkillManager instance;

    //单例对象
    public static SkillManager Instance
    {
        get
        {
            if (instance == null) instance = new SkillManager();
            return instance;
            
        }
    }

    //存储所有技能的列表
    public List<Skill> skillList;

    //初始化所有技能，只执行一次
    public void initializeSkillList()
    {
        Skill Skill_Doctor_Attack = new Skill(11, "doctor_attack", "普通攻击，攻击范围2,附带毒素","jpg",Skill.SkillType.attack,0,2,1.0,2.0,Skill.State.Poisoning);
        Skill Skill_Vampire_Attack = new Skill(21, "vampire_attack", "普通攻击，攻击范围2,吸血", "jpg", Skill.SkillType.attack, 0, 2, 1.0, 2.0, Skill.State.None);
        Skill Skill_Archer_Attack = new Skill(31, "archer_attack", "普通攻击，攻击范围1", "jpg", Skill.SkillType.attack, 0, 2, 1.0, 1.0, Skill.State.None);
        Skill Skill_Scientist_Attack = new Skill(41, "scientist_attack", "普通攻击，攻击范围1,耗电1", "jpg", Skill.SkillType.attack, 0, 2, 1.0, 1.0, Skill.State.Paralyzed);

        if(this.skillList == null)
        {
            this.skillList = new List<Skill>();
        }
        skillList.Add(Skill_Doctor_Attack);
        skillList.Add(Skill_Vampire_Attack);
        skillList.Add(Skill_Archer_Attack);
        skillList.Add(Skill_Scientist_Attack);
        Debug.Log("初始化所有技能");
        foreach (Skill skill in skillList)
        {
            Debug.Log(skill.skillId + " " + skill.name + " " + skill.desc);
        }
    }

    //根据技能id查询技能
    public Skill getSkillByID(int skillId)
    {
        foreach(Skill skill in skillList)
        {
            if (skill.skillId == skillId)
                return skill;
        }
        return null;
    }
}
