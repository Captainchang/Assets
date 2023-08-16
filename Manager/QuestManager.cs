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
    public TextMeshProUGUI talkText;
    public GameObject scanobj;
    public int talkIndex;
    bool isAction;

    public void Action(GameObject scanobject)
    {
        scanobj = scanobject;
        ObjData objdata = scanobj.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNPC);
        questwindow.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData =  talkManager.GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            cameramove.TypePlay();
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
