using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image fill;
    public float maxCooldown = 5f;
    public float currentCooldown = 5f;
    private bool isEnded = true;

    private void Start()
    {
    }
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
        isEnded= true;
    }
    void Trigger_Skill()
    {
        if (!isEnded) return;

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
