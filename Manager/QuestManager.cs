using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static UnityEditor.SceneView;


public class QuestManager : MonoBehaviour
{
    [SerializeField]
    GameObject questwindow;
    public CameraMove cameramove;
    public TalkManager talkManager;
    public ProgressManager progressManager;
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI QuestText;
    public GameObject scanobj;
    [SerializeField]
    GameObject[] npcMark;
    public int talkIndex;
    bool isAction;
    public int questTalkIndex;
    private void Start()
    {
        NpcMarkreset();
        Debug.Log(progressManager.CheckQuest());
        questTalkIndex = 10;
    }
    public void Action(GameObject scanobject)
    {
        scanobj = scanobject;
        ObjData objdata = scanobj.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNPC);
        questwindow.SetActive(isAction);
    }
    void NpcMarkreset()
    {
        for(int i = 0; i < npcMark.Length; i++)
        {
            npcMark[i].SetActive(false);
        }
    }
    void Talk(int id, bool isNpc)
    {
        questTalkIndex = progressManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            cameramove.TypePlay();
            Debug.Log(progressManager.CheckQuest(id));
            QuestText.text = progressManager.CheckQuest(id);
            return;
        }
        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true;
        cameramove.TypeUI();
        talkIndex++;
    }
    private void Update()
    {
        switch (questTalkIndex)
        {
            case 10:
                NpcMarkreset();
                npcMark[0].SetActive(true);
                break;
            case 21:
                NpcMarkreset();
                npcMark[1].SetActive(true);
                break;
        }
    }
}
