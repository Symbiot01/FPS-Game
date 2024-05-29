using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Load game scene by index
    }
}