using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadMiniScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Sundae";

    private void Start()
    {
        StartCoroutine(LoadSceneAdditive());
    }

    private IEnumerator LoadSceneAdditive()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Preview Scene Loaded!");
    }
}