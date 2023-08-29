using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public Skill[] skills;

    private void Awake()
    {
        Skill[] skillcomponent = FindObjectsOfType<Skill>();

        skills = new Skill[skillcomponent.Length];

        for (int i = skillcomponent.Length - 1; i >= 0; i--)
        {
           
            skills[skillcomponent.Length -1 - i] = skillcomponent[i].GetComponent<Skill>();
        }
    }
    void UseSkill(int skillIndex)
    {
       
        if (skillIndex >= 0 && skillIndex < skills.Length)
        {
            Skill skillToUse = skills[skillIndex];

            if (skillToUse != null)
            {
                CooldownUI.skillcool();
                Debug.Log(skills[skillIndex]);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSkill(0); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseSkill(3);
        }
    }
}
