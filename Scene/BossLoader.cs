using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLoader : MonoBehaviour
{
    IEnumerator LoadMainScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Dungeon");
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;

            asyncLoad.allowSceneActivation = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadMainScene());
        }
    }
}
