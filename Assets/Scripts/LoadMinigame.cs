using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadMinigame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string sceneToLoad = "Burger";
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject panel;

    private IEnumerator LoadSceneAdditive()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Preview Scene Loaded!");
        panel.SetActive(true);
        bg.SetActive(true);

        yield return new WaitForSeconds(20);
        SceneManager.UnloadSceneAsync(sceneToLoad);
        panel.SetActive(false);
        bg.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(LoadSceneAdditive());
        panel.SetActive(true);
        bg.SetActive(true);
    }
}
