using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsScripts : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool isBugged = false;
    [SerializeField] private Sprite codeSpriteCorrect;
    [SerializeField] private Sprite codeSpriteBugged;
    [SerializeField] private GameObject answerImage;
    private float timeToAnswer;

    public void SetTimeToAnswer(float time)
    {
        timeToAnswer = time;
    }

    public BugsManagerData bugsManagerData;

    private bool isPointerHeld = false;
    private float pointerHoldTime = 0f;

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
            if(answerImage != null)
            {
                answerImage.SetActive(true);
            }
            bugsManagerData.AddSliderPoints();
        }
    }
}