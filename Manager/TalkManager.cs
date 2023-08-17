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
        talkData.Add(1000, new string[] {"��������?", "ȰȰ Ÿ������ ���̴� ..", "��������" });
        talkData.Add(2000, new string[] {"������?", "���Ʈ ��������" });


        //Quest Talk
        talkData.Add(10 + 2000, new string[] { "��ȣ? �̰� ������?, ���� �ƴѰ� ?" , "������ �ڶ� ���� �˿��� ������."});
        talkData.Add(11 + 1000, new string[] { "���谡�� , ���� ã�°� �����ְڳ� ? ", " ������ �������� ���Ʈ���� �ٽ� ������."});

        talkData.Add(20 + 2000, new string[] { "�� ..���� ã�� �ֱ� ���ؼ� " , "�� ������ ��� ���� �˿��� �����Գ�" });
        talkData.Add(21 + 1000, new string[] { "�ź��̸� ��Ƽ� ������ ���Ķ� " });

        talkData.Add(30 + 1000, new string[] { "���谡 . ", " �����ΰ� ?? " });
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
