using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem.Composites;
using System.Linq;
using System.Collections;


public class BugsManagerData : MonoBehaviour
{
    public static BugsManagerData Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [Header("Bugs Images")]
    public List<Sprite> bugsImages;
    [Header("Normal Images")]
    public List<Sprite> normalImages;


    public List<GameObject> buttonObjects;
    public Slider slider;

    public float pointsToWin = 10;
    public float currentPoints = 0;

    public int timerWaitPerButton = 5;
    public int speedValue = 2;

    public bool canSpeed = false;
    private bool isUsedOnce = false;
    private bool isUsedRevealOnce = false;

    private void Start()
    {
        Debug.Log("Start: timerWaitPerButton = " + timerWaitPerButton);
        foreach (var button in buttonObjects)
        {
            Debug.Log("Start: timerWaitPerButton = " + timerWaitPerButton);
            ResetTimersForAllButtons();
            RandomImagesAndBugs();
            /*
            button.gameObject.GetComponent<ButtonsScripts>().bugsManagerData = this;
            button.gameObject.GetComponent<ButtonsScripts>().SetTimeToAnswer(timerWaitPerButton);
            */
        }
        RandomImagesAndBugs();
    }

    public void OnAcelerarPressed()
    {
        Debug.Log("[BugsManager] Permissão de acelerar concedida");
        canSpeed = true;
    }
    public void SpeedCode(float duration)
    {
        Debug.Log("SpeedCode called. isUsedOnce=" + isUsedOnce);
        if (!isUsedOnce && canSpeed)
        {
            Debug.Log("Applying speed for " + duration + "s.");
            StartCoroutine(ApplySpeed(duration));
            isUsedOnce = true;
            canSpeed = false;
        }
        else
        {
            Debug.Log("SpeedCode skipped — either already used or not permitted.");
        }
    }

    private IEnumerator ApplySpeed(float duration)
    {
        Debug.Log("ApplySpeed entered. Original = " + timerWaitPerButton);
        int original = timerWaitPerButton;
        timerWaitPerButton = speedValue;
        Debug.Log("timerWaitPerButton set to speedValue = " + speedValue);
        ResetTimersForAllButtons();

        Debug.Log("Waiting for " + duration + " seconds...");
        yield return new WaitForSeconds(duration);

        timerWaitPerButton = original;
        Debug.Log("Coroutine end: timerWaitPerButton restored = " + original);
        ResetTimersForAllButtons();
    }

    private void ResetTimersForAllButtons()
    {
        foreach (var button in buttonObjects)
            button.GetComponent<ButtonsScripts>().SetTimeToAnswer(timerWaitPerButton);
        Debug.Log("All ButtonsScripts updated to timerWaitPerButton = " + timerWaitPerButton);
    }

    private void UpdateButtonTimer()
    {
        foreach (var button in buttonObjects)
            button.GetComponent<ButtonsScripts>().SetTimeToAnswer(timerWaitPerButton);
        Debug.Log("All ButtonsScripts updated to timerWaitPerButton = " + timerWaitPerButton);
    }

    public void RandomImagesAndBugs()
    {
        List<GameObject> buttons = buttonObjects.OrderBy(x => UnityEngine.Random.value).ToList();

        int totaButtons = buttons.Count;
        int buttonsHalf = totaButtons / 2;

        for (int i = 0; i < totaButtons; i++)
        {
            var button = buttons[i];
            Sprite selectedImage = null;
            bool useBugImage = (i < buttonsHalf);

            if (useBugImage && bugsImages.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, bugsImages.Count);
                selectedImage = bugsImages[randomIndex];

                var buttonScript = button.gameObject.GetComponent<ButtonsScripts>();
                if (buttonScript != null)
                {
                    buttonScript.isBugged = true;
                    buttonScript.AttVerifierScript();
                }
            }
            else if (normalImages.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, normalImages.Count);
                selectedImage = normalImages[randomIndex];
            }

            var buttonScriptImage = button.gameObject.GetComponent<ButtonsScripts>();
            buttonScriptImage.SetImageScript(selectedImage);
        }

    }
    public void AddSliderPoints()
    {
        currentPoints++;
        VerifySliderPoints();
        if (slider.value < slider.maxValue)
        {
            slider.value += pointsToWin * 0.01f;
        }
        else
        {
            Debug.Log("Slider is already at maximum value.");
        }
    }

    private void VerifySliderPoints()
    {
        if (slider.value >= slider.maxValue * 0.75f)
        {
            slider.fillRect.GetComponent<Image>().color = Color.green;
        }
    }

    public void SelectButton(GameObject button)
    {
        foreach (var btn in buttonObjects)
        {
            if (btn == button)
            {
                btn.GetComponent<ButtonsScripts>().OpenScript();
            }
            else
            {
                btn.SetActive(false);
            }
        }
    }

    public void ResetButtons(GameObject button)
    {
        foreach (var btn in buttonObjects)
        {
            btn.SetActive(true);
        }
    }

    private bool hasRevealedBug = false;

    public void OnRevealPressed()
    {
        if (hasRevealedBug) return;
        hasRevealedBug = true;
        RevealBug();
    }

    private void RevealBug()
    {
        var btn = buttonObjects
            .FirstOrDefault(b => b.GetComponent<ButtonsScripts>().isBugged);

        if (btn != null)
            StartCoroutine(FlashButton(btn, 4, 0.3f));
    }

    private IEnumerator FlashButton(GameObject btn, int flashes, float flashDuration)
    {
        var img = btn.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError($"[RevealBug] Image component não encontrado em {btn.name}");
            yield break;
        }

        Color original = img.color;
        Color highlight = Color.red;

        for (int i = 0; i < flashes; i++)
        {
            img.color = highlight;
            yield return new WaitForSeconds(flashDuration);
            img.color = original;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
