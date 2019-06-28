using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light dayLight;            // Directional light represents the sun light
    [SerializeField] private GameObject stars;          

    public AnimationCurve[] lightOvetTime;

    private WorldTime worldTime;

    void Start()
    {
        worldTime = GetComponent<WorldTime>();
        worldTime.OnTimeChangedEvent += Cycle;
    }

    public void Cycle()
    {
        // Set Light
        float timePeriod = worldTime.LocalTime * 1.0f / WorldTime.minutes;     // 0 in midnight, 0.5 in midday

        // timePeriod changes from 0 in midnight to 1 in midday
        timePeriod *= 2;
        if (timePeriod > 1f)
        {
            timePeriod = 2 - timePeriod;
        }

        SetLight(timePeriod);

        // Set Stars
        SetStars(timePeriod);
    }

    void SetLight(float timePeriod)
    {
        float colorR, colorG, colorB;           // DayLight colors

        colorR = lightOvetTime[0].Evaluate(timePeriod);
        colorG = lightOvetTime[1].Evaluate(timePeriod);
        colorB = lightOvetTime[2].Evaluate(timePeriod);

        dayLight.color = new Color(colorR, colorG, colorB);
    }

    void SetStars(float timePeriod)
    {
        if (timePeriod > 0.4f)          // Stars fades out in the morning
        {
            stars.GetComponent<ParticleSystem>().Stop();

            if(timePeriod > 0.6f)
                stars.SetActive(false);
        }
        else                            // Evening begins
        {
            stars.SetActive(true);
        }
    }
}
