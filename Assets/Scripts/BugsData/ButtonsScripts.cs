using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsScripts : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool isBugged = false;
    [SerializeField] private Sprite codeSpriteCorrect;
    [SerializeField] private Sprite codeSpriteBugged;
    [SerializeField] private GameObject answerImage;

    [SerializeField] private GameObject scritpPanel;
    [SerializeField] private Button closeScriptPanel;
    private float timeToAnswer;

   
    public void SetTimeToAnswer(float time)
    {
        timeToAnswer = time;
    }

    public BugsManagerData bugsManagerData;

    private bool isPointerHeld = false;
    private float pointerHoldTime = 0f;

    private void Start()
    {
        closeScriptPanel.onClick.AddListener(CloseScript);
    }
    public void SetBugged()
    {
        isBugged = true;
        GetComponent<SpriteRenderer>().sprite = codeSpriteBugged;
    }

    public void SetCorrect()
    {
        isBugged = false;
        GetComponent<SpriteRenderer>().sprite = codeSpriteCorrect;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerHeld = true;
        pointerHoldTime = 0f;
        Debug.Log("Botão pressionado");
        bugsManagerData.SelectButton(this.gameObject);
        OpenScript();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerHeld = false;
        pointerHoldTime = 0f;
        Debug.Log("Botão liberado");
    }

    private void Update()
    {
        if (isPointerHeld)
        {
            pointerHoldTime += Time.deltaTime;
            if (pointerHoldTime >= timeToAnswer)
            {
                Debug.Log("Botão pressionado tempo suficiente");
                OnPointerHeldEnough();
                isPointerHeld = false;
            }
        }
    }

    private void OnPointerHeldEnough()
    {
        if (isBugged)
        {
            if (answerImage != null)
            {
                answerImage.SetActive(true);
            }
            bugsManagerData.AddSliderPoints();
        }
    }

    public void OpenScript()
    {
        scritpPanel.SetActive(true);
    }

    public void CloseScript()
    {
        scritpPanel.SetActive(false);
        bugsManagerData.ResetButtons(this.gameObject);
    }
}