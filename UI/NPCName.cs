using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCName : MonoBehaviour
{
    private GameObject npc_Name;

    public enum NPCTypes
    {
        Brute,
        FireSword,
    }
    public NPCTypes npctype;
    public Renderer npcRenderer;

    private void Start()
    {
        var name = "Canvas/NPCName/[NPC]" + npctype.ToString();
        npc_Name = GameObject.Find(name);
        npc_Name.SetActive(false);

        npcRenderer = GetComponent<Renderer>();
    }
    public void Namefalse()
    {
        npc_Name.SetActive(true);
    }

    private void Update()
    {

        if (npcRenderer != null && npcRenderer.isVisible)
         {
             npc_Name.SetActive(true);
             npc_Name.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, 2.5f, 0));
         }
         else
         {
             npc_Name.SetActive(false);
         }
    }
}
