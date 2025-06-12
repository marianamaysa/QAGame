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
        if (isBugged && !bugFound)
        {
            bugFound = true;
            bugsManagerData.AddSliderPoints();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerHeld = false;
        pointerHoldTime = 0f;
    }
}
