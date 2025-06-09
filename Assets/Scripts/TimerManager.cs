using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float startTimer;
    private float currentTime;

    [SerializeField] private TMP_Text timerTxt;

    void Start()
    {
        currentTime = startTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if(currentTime > 60)
            {
                FormatTimer();
            }
            else
            {
                timerTxt.text = currentTime.ToString("0:00");
            }
        }
    }

    void FormatTimer()
    {
        float mins = Mathf.FloorToInt(currentTime / 60);
        float secs = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", mins, secs);
    }

    public void AddTime(float timer)
    {
        currentTime += timer;
    }
}
