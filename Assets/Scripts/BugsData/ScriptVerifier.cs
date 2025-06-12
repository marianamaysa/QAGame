using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScriptVerifier : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isBugged;
    public bool isPointerHeld = false;
    public float timeToAnswer;
    public float pointerHoldTime = 0f;
    public BugsManagerData bugsManagerData;
    public bool bugFound = false;

    private bool speedTriggered = false;

    private void Update()
    {
        if (isPointerHeld)
        {
            pointerHoldTime += Time.deltaTime;
            if (pointerHoldTime >= timeToAnswer)
            {
                OnPointerHeldEnough();
                isPointerHeld = false;
            }
        }
    }

    private void OnPointerHeldEnough()
    {
        Debug.Log("[Verifier] Held long enough. isBugged=" + isBugged);

        if (isBugged && !bugFound)
        {
            bugFound = true;
            bugsManagerData.AddSliderPoints();
        }

        if (!speedTriggered && BugsManagerData.Instance != null && BugsManagerData.Instance.canSpeed)
        {
            Debug.Log("[Verifier] Triggering speed-up now!");
            BugsManagerData.Instance.SpeedCode(timeToAnswer);
            speedTriggered = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerHeld = true;
        pointerHoldTime = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerHeld = false;
        pointerHoldTime = 0f;
    }
}
