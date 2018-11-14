
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoleState
{
    NoAction = 0,
    SkillSelected = 1,
    TargetSelected = 2,

}

public class WarControl : MonoBehaviour
{

    public GameObject ActionPanel;//左下角玩家操作面板，旗下有三个按钮
    public GameObject Button_Attack;
    public GameObject Button_Skill1;
    public GameObject Button_Skill2;
    public GameObject Button_Ok;
    public GameObject PlayerHPinfo;//左下角的玩家的HP信息文本Text  
    public GameObject WarinfoText;//右上角的战斗信息文本Text   


    private List<GameObject> battleUnits;           //所有参战对象的列表    
    private GameObject[] playerUnits;           //所有参战玩家的列表    
    private GameObject[] monsterUnits;            //所有参战敌人的列表    
    private GameObject[] remainingMonsterUnits;           //剩余参战对敌人的列表    
    private GameObject[] remainingPlayerUnits;           //剩余参战对玩家的列表     
    private GameObject currentActUnit;          //当前行动的单位    
    private GameObject currentActUnitTarget;            //当前行动的单位的目标

    private int currentUnit = 0;//当前单位编号
    private RoleState roleState = RoleState.NoAction;//当前人物的状态
    private Skill currentSkill = null;//当前的技能

    // Use this for initialization
    void Start()
    {

        //Time.timeScale = 1;//打破时间结界，主要是配合下面update()中结算时的布置的时间结界Time.timeScale = 0;  
        /*定义玩家和敌人的属性，这部分在实际中，可以从记载游戏状态的xml等地方取，这里粗暴定义为100*/



        //创建参战列表
        battleUnits = new List<GameObject>();

        //初始化所有技能
        SkillManager.Instance.initializeSkillList();

        //添加玩家单位
        playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject playerUnit in playerUnits)
        {
            battleUnits.Add(playerUnit);
        }

        //添加怪物单位
        monsterUnits = GameObject.FindGameObjectsWithTag("MonsterUnit");
        foreach (GameObject monsterUnit in monsterUnits)
        {
            battleUnits.Add(monsterUnit);
        }

        WarinfoText.GetComponent<Text>().text = "战斗开始！\n";//战斗信息更新 

        //开始战斗
        Debug.Log("开始战斗");
        listSort();
        battle();
    }

    // Update is called once per frame
    void Update()
    {

    }


    /*检查战斗是否结束,暂时未使用*/
    public void isEnd()
    {
        
        remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        remainingMonsterUnits = GameObject.FindGameObjectsWithTag("MonsterUnit");
        //检查存活敌人单位        
        if (remainingMonsterUnits.Length == 0)
        {
            Debug.Log("敌人全灭，战斗胜利");
            
        } //检查存活玩家单位        
        else if (remainingPlayerUnits.Length == 0)
        {
            Debug.Log("我方全灭，战斗失败");
            
        }
        Debug.Log("开始该回合");
        
    }

    /*是否一轮行动所有角色都行动结束，如果结束则需要重新排序*/
    public void isRoundOver()
    {
        if(currentUnit == battleUnits.Count - 1)
        {
            Debug.Log("一轮游戏所有角色行动已经结束，准备开始下一轮");
            listSort();
        }
    }

    /*按照速度排序*/
    public void listSort()
    {
        Debug.Log("人物按照速度顺序已排序");
        battleUnits.Sort((x, y) => -x.GetComponent<CombatUnit>().speed.CompareTo(y.GetComponent<CombatUnit>().speed));
    }

    /*战斗*/
    public void battle()
    {
        //首先检查isRoundOver()，若一轮结束，重新调用listSort排序

        //取出list中当前该行动的角色
        currentActUnit = battleUnits[currentUnit];

        //结算buff
        BuffEffect();

        //更新UI,加载角色信息
        Debug.Log("现在是" + currentActUnit.GetComponent<CombatUnit>().name + "行动中");
        PlayerHPinfo.GetComponent<Text>().text = "名字:" + currentActUnit.GetComponent<CombatUnit>().name + " HP:" + currentActUnit.GetComponent<CombatUnit>().hp;//玩家HP信息文本的更新 
        WarinfoText.GetComponent<Text>().text = "现在是:" + currentActUnit.GetComponent<CombatUnit>().name + "行动";//战斗信息更新
        roleState = RoleState.NoAction;
        //结算buff，UI动画，包括更新buff的动画和更新角色信息的buff

    }

    /*更新角色信息*/
    public void UpdateRole()
    {
        //更新所有和角色相关的UI部分，如HP变化，不能释放的技能按钮变灰
    }

    /*结算buff*/
    public void BuffEffect()
    {
        Debug.Log("人物行动前buff结算");
        foreach (Buff buff in currentActUnit.GetComponent<CombatUnit>().buffs.ToArray())
        {
            //buff产生效果，计时减1
            buff.Tick();
        }
    }

    /*技能按钮根据当前角色加载角色的技能信息,图片等,以及需要加载的一些角色信息*/
    public void SkillButtonLoad()
    {

    }

    /*改变人物的状态*/
    public void changeState()
    {
        roleState = RoleState.TargetSelected;
    }

    /*读取人物的状态*/
    public RoleState getState()
    {
        return roleState;
    }

    /*0、移动按钮，触发人物移动*/
    public void Button_MoveClick()
    {
        //1、点击移动后，要能显示当前角色可以移动的范围（UI），鼠标悬停显示消耗多少AP
        //2、选择目的地之后，有个确认或取消按钮
        //3、确认之后，调用角色移动UI，更新角色信息
    }

    /*1、技能按钮响应，触发技能*/
    public void Button_SkillClick()
    {

        if (roleState == RoleState.NoAction)
        {
            //获得点击按钮对应的技能信息
            int skillid = currentActUnit.GetComponent<CombatUnit>().skillid[0];
            currentSkill = SkillManager.Instance.getSkillByID(skillid);//角色第一个技能
            Debug.Log(currentSkill.name + " " + currentSkill.desc+"选中");
            if (currentSkill != null)
            {
                WarinfoText.GetComponent<Text>().text = currentSkill.name + " " + currentSkill.desc;//显示技能信息
            }
            else
            {
                WarinfoText.GetComponent<Text>().text = "无技能";//显示技能信息
            }
            roleState = RoleState.SkillSelected;
        }

    }


    /*2、检查是否满足技能释放要求，如AP和san值*/
    public void CheckSkill()
    {

    }

    /*3、选择技能目标并释放技能*/
    public void FindTarget(GameObject obj)
    {
        Debug.Log("目标角色" + obj.name);
        currentActUnitTarget = obj;
    }

    /*4、技能释放结算，跟新角色属性*/
    public void SkillEffect()
    {
        if (roleState != RoleState.TargetSelected) { return; }
        Debug.Log("技能生效");
        roleState = RoleState.NoAction;
        if (currentSkill.skillId == 11)
        {
            //医生的普攻，id=11
            currentActUnitTarget.GetComponent<CombatUnit>().hp -= (int)(currentActUnit.GetComponent<CombatUnit>().attack * currentSkill.multiply);
            //检查目标生命是否小于0
            if (currentActUnitTarget.GetComponent<CombatUnit>().hp < 0)
            {
                currentActUnitTarget.GetComponent<CombatUnit>().hp = 0;
            }

        }
        else if (currentSkill.skillId == 21)
        {
            //吸血鬼的普攻，id=21
            currentActUnitTarget.GetComponent<CombatUnit>().hp -= (int)(currentActUnit.GetComponent<CombatUnit>().attack * currentSkill.multiply);
            //检查目标生命是否小于0
            if (currentActUnitTarget.GetComponent<CombatUnit>().hp < 0)
            {
                currentActUnitTarget.GetComponent<CombatUnit>().hp = 0;
            }
            //吸血
            currentActUnit.GetComponent<CombatUnit>().hp += (int)(currentActUnit.GetComponent<CombatUnit>().attack * currentSkill.multiply * 0.2);
            //检查生命是否超过最大值
            if(currentActUnit.GetComponent<CombatUnit>().hp >= currentActUnit.GetComponent<CombatUnit>().maxHp)
            {
                currentActUnit.GetComponent<CombatUnit>().hp = currentActUnit.GetComponent<CombatUnit>().maxHp;
            }
        }
        else if (currentSkill.skillId == 31)
        {
            currentActUnitTarget.GetComponent<CombatUnit>().hp -= (int)(currentActUnit.GetComponent<CombatUnit>().attack * currentSkill.multiply);

        }else if (currentSkill.skillId == 41)
        {
            currentActUnitTarget.GetComponent<CombatUnit>().hp -= (int)(currentActUnit.GetComponent<CombatUnit>().attack * currentSkill.multiply);

        }
    }

    //取消按钮,返回上个状态
    public void Button_CancelClick()
    {
        if(roleState == RoleState.TargetSelected)
        {
            Debug.Log("目标选择取消，请重新选择目标");
            roleState = RoleState.SkillSelected;
        }else if(roleState == RoleState.SkillSelected)
        {
            Debug.Log("技能选择取消，请重新选择技能");
            roleState = RoleState.NoAction;
        }
        
    }

    /*切换操作角色按钮*/
    public void NextOneButtonClick()
    {
        Debug.Log("下一个");
        isRoundOver();
        currentUnit += 1;
        if (currentUnit >= battleUnits.Count)
        {
            currentUnit = 0;
        }
        battle();
    }

    /*测试技能预加载，后期删除*/
    [ContextMenu("TestLoadSkill")]
    private void TestLoadSkill()
    {
        SkillManager.Instance.initializeSkillList();
    }

    /*测试增加攻击力的buff,后期删除*/
    [ContextMenu("TestPowerUpBuff")]
    private void TestPowerUpBuff()
    {
        Buff powerUpBuff = new PowerUpBuff(1, 2, currentActUnit,2);
        powerUpBuff.Activate();
    }
}
