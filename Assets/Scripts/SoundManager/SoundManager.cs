using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Singleton pattern para f�cil acesso
    public static SoundManager Instance { get; private set; }

    [Header("Configura��es de �udio")]
    [SerializeField] private bool isMuted = false;

    [Header("Refer�ncias do Bot�o")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite[] sprites; // Sprites[0] = som ligado, Sprites[1] = som mudo
    public AudioSource buttonAudioSource; // Fonte de �udio para o som do bot�o

    private void Awake()
    {
        // Implementa��o do singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Carrega o estado salvo do mute
        LoadMuteState();
        UpdateAudioAndButton();
    }

    // M�todo p�blico para alternar o mute
    public void ToggleMute()
    {
        isMuted = !isMuted;
        UpdateAudioAndButton();
        SaveMuteState();
    }

    public void PlayButtonSound()
    {
        buttonAudioSource.Play();
    }



    // Atualiza o volume e o sprite do bot�o
    private void UpdateAudioAndButton()
    {
        AudioListener.volume = isMuted ? 0 : 1;

        if (buttonImage != null && sprites != null && sprites.Length >= 2)
        {
            buttonImage.sprite = isMuted ? sprites[1] : sprites[0];
        }
    }

    // Salva o estado do mute em PlayerPrefs
    private void SaveMuteState()
    {
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Carrega o estado do mute de PlayerPrefs
    private void LoadMuteState()
    {
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
    }

    // M�todo para verificar se est� mudo
    public bool IsMuted()
    {
        return isMuted;
    }
}