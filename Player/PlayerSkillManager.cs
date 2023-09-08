using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerSkillManager : MonoBehaviour
{
    public Skill[] skills;
    public Animator anim;
    bool skill4active;
    int skill4count = 1;
    int potioncount = 3;
    [SerializeField]
    GameObject[] skilllist;
    private void Awake()
    {

        if (FindObjectsOfType(GetType()).Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Skill[] skillcomponent = FindObjectsOfType<Skill>();

        skills = new Skill[skillcomponent.Length];

        for (int i = 0; i < skillcomponent.Length; i++)
        {
            int skillIndex = skillcomponent[i].skillNumber - 1;
            skills[skillIndex] = skillcomponent[i];
            //skills[i] = skillcomponent[i].GetComponent<Skill>();
        }
        skill4active = false;
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
  
    IEnumerator Potion()
    {
        if (skilllist[2] != null)
            skilllist[2].SetActive(true);

       yield return new WaitForSeconds(1f);
        skilllist[2].SetActive(false);
    }
    private void Update()
    {
        PlayerStat playerStat = anim.GetComponent<PlayerStat>();
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
        else if (Input.GetKeyDown(KeyCode.Alpha4) && skill4count > 0)
        {
            UseSkill(3);
            skilllist[3].SetActive(true);
            skill4count -= 1;
            skill4active = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && potioncount > 0)
        {
            //potioncount를 인벤에 포션 개수
            UseSkill(8);
            if (playerStat.HP < 200)
            {
                potioncount -= 1;
                playerStat.HP += 50;
                StartCoroutine(Potion());
                if (playerStat.HP > 200)
                {
                    playerStat.HP = 200;
                }
                PlayerStatUI.Instance.UpdateHp();
                PlayerStatUI.Instance.UpdateCurrentHpbar();
            }
        }

        if (skill4active)
        { 
            playerStat.Attack = playerStat.Attack * 3 / 2;
            skill4active= false;
        }
        else if(!skill4active)
        {
            playerStat.Attack = playerStat.Attack;
        }
       
    }
}
