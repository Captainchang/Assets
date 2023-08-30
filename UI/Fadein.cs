using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadein : MonoBehaviour
{
    public float FadeTime = 2f;
    [SerializeField]
    Image fadeImg;
    float start;
    float end;
    float time = 0f;
    bool isPlaying = false;

    private void Awake()
    {
        
        fadeImg = GetComponent<Image>();
        start = 1f;
        end = 0f;
        InStartFadeanim();
    }
    public void OutStartFadeAnim()
    {
        if(isPlaying == true)
            return;
        start = 1f;
        end = 0f;

        //StartCoroutine("");
    }
    public void InStartFadeanim()
    {
        if (isPlaying == true) 
            return;
        StartCoroutine("fadeinplay");
    }

    IEnumerator fadeinplay()
    {
        isPlaying = true;

        Color fadecolor = fadeImg.color;
        time = 0f;
        fadecolor.a = Mathf.Lerp(start,end,time);

        while(fadecolor.a > 0f)
        {
            time += Time.deltaTime/FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);
            fadeImg.color= fadecolor;
            yield return null;
        }
        isPlaying= false;
    }
}
