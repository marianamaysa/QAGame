using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem.Composites;
using System.Linq;


public class BugsManagerData : MonoBehaviour
{
    [Header("Bugs Images")]
    public List<Sprite> bugsImages;
    [Header("Normal Images")]
    public List<Sprite> normalImages;


    public List<GameObject> buttonObjects;
    public Slider slider;

    public float pointsToWin = 10;
    public float currentPoints = 0;

    public int timerWaitPerButton = 1;

    private void Start()
    {
        foreach (var button in buttonObjects)
        {
            button.gameObject.GetComponent<ButtonsScripts>().bugsManagerData = this;
            button.gameObject.GetComponent<ButtonsScripts>().SetTimeToAnswer(timerWaitPerButton);
        }
        RandomImagesAndBugs();
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
}
