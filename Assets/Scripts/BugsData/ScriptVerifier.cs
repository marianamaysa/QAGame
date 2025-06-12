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
            Debug.Log("[Verifier] Aceleração aplicada: timeToAnswer = " + timeToAnswer);
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
        Debug.Log("[Verifier] Resolução completa em: " + pointerHoldTime + "s");
        // Restaurar tempo padrão
        timeToAnswer = originalTimeToAnswer;
        Debug.Log("[Verifier] Restaurado timeToAnswer = " + timeToAnswer);

        if (isBugged && !bugFound)
        {
            bugFound = true;
            BugsManagerData.Instance.AddSliderPoints();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerHeld = false;
        pointerHoldTime = 0f;
    }
}
