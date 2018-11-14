using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleClick : MonoBehaviour {

    void OnMouseDown()
    {
        if (GameObject.Find("WarControl").GetComponent<WarControl>().getState() == RoleState.SkillSelected)
        {
            //string s = this.name;
            //Debug.Log(s);
            GameObject.Find("WarControl").GetComponent<WarControl>().changeState();
            GameObject.Find("WarControl").GetComponent<WarControl>().FindTarget(this.gameObject);
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
