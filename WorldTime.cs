using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for measuring time
/// </summary>
public class WorldTime : MonoBehaviour
{
    public const int minutes = 1440;                    // Minutes in day

    [Range(0, 1439)]
    [SerializeField] private int localTime;             // Local time in players current position
    public int LocalTime                                 
    {
        get { return localTime; }
        set { localTime = value; localTime %= minutes; }
    }

    [SerializeField] private int timeScale;             // 1 real minute = 2n game minutes
    public int TimeScale                  
    {
        get { return timeScale; }
        set { if (value > 0) timeScale = value; }
    }

    public bool TimeStop { get; set; }                  // Pause

    public delegate void OnTimeChanged();
    public event OnTimeChanged OnTimeChangedEvent;

    void Start()
    {
        StartCoroutine("MeasureTime");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))                // Take a pause
        {
            TimeStop = !TimeStop;
        }
    }

    public IEnumerator MeasureTime()
    {
        while (true)
        {
            if (TimeStop)                   // Wait if pause
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            yield return new WaitForSeconds(0.5f);
            LocalTime += TimeScale;

            if (OnTimeChangedEvent != null)
                OnTimeChangedEvent.Invoke();
        }
    }
}
