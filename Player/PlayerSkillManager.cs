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

        for (int i = 0; i < skillcomponent.Length; i++)
        {
            int skillIndex = skillcomponent[i].skillNumber - 1;
            skills[skillIndex] = skillcomponent[i];
            //skills[i] = skillcomponent[i].GetComponent<Skill>();
        }
    }
    void UseSkill(int skillIndex)
    {
       
        if (skillIndex >= 0 && skillIndex < skills.Length)
        {
            Skill skillToUse = skills[skillIndex];

            if (skillToUse != null)
            {
                skills[skillIndex].Trigger_Skill();
                Debug.Log(skills[skillIndex]);      
            }
        }
    }
    public void OnSkillButtonClicked(int skillIndex)
    {
        CooldownUI.skillcool(); 
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
