using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public static PlayerStatUI Instance { get; private set; }

    [SerializeField]
    TextMeshProUGUI PlayerCurrentHp;
    [SerializeField]
    TextMeshProUGUI PlayerMaxHp;
    [SerializeField]
    TextMeshProUGUI PlayerAttack;


    PlayerStat playerStat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

        PlayerCurrentHp.color = Color.red;
        PlayerMaxHp.color = Color.red;
        PlayerAttack.color = Color.red;

        PlayerCurrentHp.text = "CurrentHp :"  + playerStat.HP.ToString();
        PlayerMaxHp.text =  "MaxHp : " + playerStat.MaxHp.ToString();
        PlayerAttack.text = "Attack : "  +  playerStat.Attack.ToString();
    }
    public void UpdateHp() { PlayerCurrentHp.text = "CurrentHp :" + playerStat.HP.ToString(); }
    public void UpdateAttack() { PlayerAttack.text = "Attack : " + playerStat.Attack.ToString(); }

}
