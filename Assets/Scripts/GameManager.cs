using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Buttons Part")]
    [SerializeField] Button playBtn;
    [SerializeField] Button creditsBtn;
    [SerializeField] Button closeCreditsBtn;
    [SerializeField] Button quitBtn;
    [Header("Panels Part")]
    [SerializeField] GameObject creditsPanel;

    public void Awake()
    {
        playBtn.onClick.AddListener(PlayGame);
        creditsBtn.onClick.AddListener(OpenCredits);
        closeCreditsBtn.onClick.AddListener(CloseCredits);
        quitBtn.onClick.AddListener(QuitGame);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
