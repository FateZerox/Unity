using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗单位类
public class CombatUnit : MonoBehaviour {

    public int type;
    public string playername;//名字
    public double speed;//迅速值
    public int attack;//攻击力
    public int AP;//AP点数
    public int san;//san值
    protected int maxSan;//最大san值
    public int hp;//生命
    protected int _maxHp;//最大生命
    protected int maxAttack;//最大攻击力
    public double move;//移动速率
    public int[] skillid =new int[3];//标注技能的id

    public List<Buff> buffs = new List<Buff>();//存放buff的list

    public int maxHp
    {
        get
        {
            return _maxHp;
        }
        private set
        {
            _maxHp = hp;
        }
    }
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
