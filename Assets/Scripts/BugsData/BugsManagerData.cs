using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class BugsManagerData : MonoBehaviour
{

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
