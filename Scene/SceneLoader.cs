using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    GameObject startimg;
    [SerializeField]
    GameObject loadingimg;
    [SerializeField]
    Slider slider;

    private void Start()
    {
        loadingimg.SetActive(false);
    }
    IEnumerator LoadMainScene()
    {
        startimg.SetActive(false);
        loadingimg.SetActive(true);

        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");
        asyncLoad.allowSceneActivation = false;

        while(!asyncLoad.isDone)
        {
            yield return null;

            slider.value = asyncLoad.progress;
            asyncLoad.allowSceneActivation=true;
        }

    }
    private void Update()
    {
        if(Input.anyKey)
        {
            StartCoroutine(LoadMainScene());
        }
    }
}
