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
    public int talkIndex;
    bool isAction;

    private void Start()
    {
        Debug.Log(progressManager.CheckQuest());
    }
    public void Action(GameObject scanobject)
    {
        scanobj = scanobject;
        ObjData objdata = scanobj.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNPC);
        questwindow.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = progressManager.GetQuestTalkIndex(id);
        string talkData =  talkManager.GetTalk(id + questTalkIndex, talkIndex);

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
}
