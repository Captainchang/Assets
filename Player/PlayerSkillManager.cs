using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public Skill[] skills;
    public Animator anim;
    bool skill4active;

    [SerializeField]
    GameObject[] skilllist;
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
        skill4active = true;
    }
    void UseSkill(int skillIndex)
    {
       
        if (skillIndex >= 0 && skillIndex < skills.Length)
        {
            Skill skillToUse = skills[skillIndex];

            float maxcool = skills[skillIndex].maxCooldown;
            float currentcool = skills[skillIndex].currentCooldown;

            if (currentcool >= maxcool )//skillToUse != null)
            {
                skills[skillIndex].Trigger_Skill();

                Debug.Log(skills[skillIndex]);      
                switch(skillIndex)
                {
                    case 0:
                        anim.SetTrigger("Skill1");
                        break;
                    case 1:
                        anim.SetTrigger("Skill2");
                        break;
                    case 2:
                        anim.SetTrigger("Skill3");
                        break;
                }    
            }
            else
            {
                Debug.Log("스킬 쿨다운중.");
            }
        }
    }
    public void Skill2()
    {
        skilllist[1].SetActive(true);
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
            skilllist[3].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            UseSkill(8);
        }

        if (skill4active)
        {
            PlayerStat playerStat = anim.GetComponent<PlayerStat>();
            playerStat.Attack = playerStat.Attack * 3/2;
            skill4active = false;
        }
    }
}
