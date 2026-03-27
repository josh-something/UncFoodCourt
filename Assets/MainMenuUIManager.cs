using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneController.Instance.LoadLevelByName("MainScreen");
    }


}
