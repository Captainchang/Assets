using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public Image fill;
    private bool isEnded = true;
    [Header("스킬정보")]
    [Space (10)]  
    public string skillName;
    public int skillNumber;
    public int damage;
    public float maxCooldown = 5f;
    public float currentCooldown = 5f;

    private void Update()
    {
        if (isEnded)
            return;
        Check_CoolTime();
    }

    void Check_CoolTime()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown < maxCooldown)
        {
            Set_FillAmount(currentCooldown);
        }
        else if (!isEnded)
        {
            End_Cooltime();
        }
    }
    void End_Cooltime()
    {
        Set_FillAmount(maxCooldown);
        isEnded = true;
    }
    public void Trigger_Skill()
    {
        if (!isEnded) return; // todo 나중에 플레이어 레벨 안되면 안눌리게

        Reset_CoolTime();
    }
    void Reset_CoolTime()
    {
        currentCooldown = 0;
        Set_FillAmount(0);
        isEnded = false;
    }
    void Set_FillAmount(float value)
    {
        fill.fillAmount = value / maxCooldown;
    }
    public void On_Btn()
    {
        Trigger_Skill();
    }
}

