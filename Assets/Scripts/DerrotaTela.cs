using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DerrotaTela : MonoBehaviour
{
    public Button yesBtn;
    public Button noBtn;

    private void Start()
    {
        yesBtn.onClick.AddListener(OnYesButtonClicked);
        noBtn.onClick.AddListener(OnNoButtonClicked);
    }

    private void OnNoButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void OnYesButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
