using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{

    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        // Talk
        talkData.Add(1000, new string[] {"만져볼까?", "활활 타오르는 검이다 ..", "조심하자" });
        talkData.Add(2000, new string[] {"누구지?", "브루트 아저씨다" });


        //Quest Talk
        talkData.Add(10 + 2000, new string[] { "오호? 이게 누군가?, 레아 아닌가 ?", "마을의 자랑 불의 검에게 가봐라." });
        talkData.Add(11 + 1000, new string[] { "모험가여 , 힘을 찾는걸 도와주겠나 ? ", " 마을의 대장장이 브루트한테 다시 가봐라." });

        talkData.Add(20 + 2000, new string[] { "음 ..힘을 찾아 주기 위해선 ", "이 쪽지를 들고 불의 검에게 가보게나" });
        talkData.Add(21 + 1000, new string[] { "마을 어딘가 포탈이 숨겨져있다는데 " });

        talkData.Add(30 + 1000, new string[] { "포탈은 찾았나 ? . ", " 12시 방향으로 가보게 " });
    }

    public string GetTalk(int id,int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            else
                return GetTalk(id - id % 10, talkIndex);

        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
