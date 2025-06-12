using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsScripts : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isBugged = false;
    public bool bugFound = false;
    [SerializeField] private Sprite codeSpriteCorrect;
    [SerializeField] private Sprite codeSpriteBugged;
    [SerializeField] private GameObject answerImage;

     public GameObject scriptPanel;
    [SerializeField] private Button closeScriptPanel;
    private ScriptVerifier scriptVerifier;
    private float timeToAnswer;

   
    public void SetTimeToAnswer(float time)
    {
        timeToAnswer = time;
        AttVerifierScript();
    }

    public BugsManagerData bugsManagerData;


    private void Start()
    {
        closeScriptPanel.onClick.AddListener(CloseScript);
    }

    public void AttVerifierScript()
    {
        scriptVerifier = scriptPanel.GetComponent<ScriptVerifier>();
        scriptVerifier.isBugged = isBugged;
        scriptVerifier.timeToAnswer = timeToAnswer;
        scriptVerifier.bugsManagerData = bugsManagerData;

    }
    public void SetImageScript(Sprite image)
    {
        scriptPanel.GetComponent<Image>().sprite = image;
        AttVerifierScript();
    }


    public void SetBugged()
    {
        if (!isBugged)
        {
            isBugged = true;
            GetComponent<SpriteRenderer>().sprite = codeSpriteBugged;
        }
    }

    public void SetCorrect()
    {
        isBugged = false;
        GetComponent<SpriteRenderer>().sprite = codeSpriteCorrect;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        bugsManagerData.SelectButton(this.gameObject);
        OpenScript();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OpenScript()
    {
        scriptPanel.SetActive(true);
    }

    public void CloseScript()
    {
        scriptPanel.SetActive(false);
        bugsManagerData.ResetButtons(this.gameObject);
    }
}