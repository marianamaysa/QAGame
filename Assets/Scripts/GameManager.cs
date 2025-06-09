using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject creditsBtn;
    [SerializeField] GameObject quitBtn;
    public void LoadScenes(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    void MenuButton()
    {

    }
}
