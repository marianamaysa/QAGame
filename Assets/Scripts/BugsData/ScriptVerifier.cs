using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScriptVerifier : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isBugged;
    public bool isPointerHeld = false;
    public float timeToAnswer = 5f;
    public float pointerHoldTime = 0f;
    public BugsManagerData bugsManagerData;
    public bool bugFound = false;

    private bool speedTriggered = false;
    private float originalTimeToAnswer;
    private float acceleratedTimeToAnswer;
    private void Start()
    {
        originalTimeToAnswer = timeToAnswer;
        acceleratedTimeToAnswer = BugsManagerData.Instance.speedValue;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerHeld = true;
        pointerHoldTime = 0f;

        if (!speedTriggered && BugsManagerData.Instance.canSpeed)
        {
            timeToAnswer = acceleratedTimeToAnswer;
            BugsManagerData.Instance.SpeedCode();
            speedTriggered = true;  
            Debug.Log("aceleracao aplicada: timeToAnswer = " + timeToAnswer);
        }
    }

    private void Update()
    {
        if (isPointerHeld)
        {
            pointerHoldTime += Time.deltaTime;
            if (pointerHoldTime >= timeToAnswer)
            {
                isPointerHeld = false;
                OnPointerHeldEnough();
            }
        }
    }

    private void OnPointerHeldEnough()
    {
        Debug.Log("resolucao completa em: " + pointerHoldTime + "s");

        timeToAnswer = originalTimeToAnswer;
        Debug.Log("restaurado timeToAnswer = " + timeToAnswer);

        if (isBugged && !bugFound)
        {
            bugFound = true;
            BugsManagerData.Instance.AddSliderPoints();

            var bs = GetComponent<ButtonsScripts>();
            if (bs != null)
                bs.bugFound = true;
        }
        else
        {
            Debug.Log("codigo correto");
        }

        timeToAnswer = originalTimeToAnswer;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerHeld = false;
        pointerHoldTime = 0f;
    }
}
