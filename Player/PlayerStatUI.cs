using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class PlayerStatUI : MonoBehaviour
{
    public static PlayerStatUI Instance { get; private set; }

    [SerializeField]
    TextMeshProUGUI PlayerCurrentHp;
    [SerializeField]
    TextMeshProUGUI PlayerMaxHp;
    [SerializeField]
    TextMeshProUGUI PlayerAttack;
    [SerializeField]
    TextMeshProUGUI HpbarCurrent;


    PlayerStat playerStat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
            Initialze();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartStat();

    }
    void Initialze()
    {
        playerStat =  GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
    }

    public void StartStat()
    {

        PlayerCurrentHp.color = Color.white;
        PlayerMaxHp.color = Color.white;
        PlayerAttack.color = Color.white;
        HpbarCurrent.color = Color.white;

        PlayerCurrentHp.text = ""  + playerStat.HP.ToString();
        PlayerMaxHp.text =  "" + playerStat.MaxHp.ToString();
        PlayerAttack.text = ""  +  playerStat.Attack.ToString();
        HpbarCurrent.text = "" + playerStat.HP.ToString();
    }
    public void UpdateHp() { PlayerCurrentHp.text = "체력: " + playerStat.HP.ToString(); if (playerStat.HP <= 0) { playerStat.HP = 0; } }
    public void UpdateMaxHp() { PlayerMaxHp.text = "/ " + playerStat.MaxHp.ToString(); }
    public void UpdateAttack() { PlayerAttack.text = "공격력 : " + playerStat.Attack.ToString(); }

    public void UpdateCurrentHpbar() { HpbarCurrent.text = "" + playerStat.HP.ToString(); if (playerStat.HP <= 0) { playerStat.HP = 0; } }
}


