using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan penaltyTime;
    private TimeSpan bestTime;
    private bool racing;
    public delegate void TimerEvent();
    [SerializeField] private int penaltyTimeVal = 3;
    [SerializeField] private TMP_Text raceTimeText, bestTimeText;
    [SerializeField] private string bestTimeKey = "LVL1BestTime";
    
    private void OnEnable()
    {
        StartGate.StartRace += OnRaceStart;
        FinishGate.FinishRace += OnRaceEnd;
        SlalomFlag.RacePenalty += AddRacePenalty;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(bestTimeKey))
        {
            int bestTimeTicks = PlayerPrefs.GetInt(bestTimeKey);
            bestTime = new TimeSpan(bestTimeTicks);
            bestTimeText.text = "BEST: " + bestTime.ToString("ss\\:ff");
        }
        else
        {
            bestTime = new TimeSpan(int.MaxValue);
            bestTimeText.text = "BEST: --:--";
        }
        Debug.Log("Best time: "  + bestTime);
    }
    
    void AddRacePenalty()
    {
        penaltyTime += new TimeSpan(3, 0, penaltyTimeVal);
    }

    void OnRaceStart()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("Race started");
    }

    void OnRaceEnd()
    {
        racing = false;
        if (raceTime < bestTime)
        {
            bestTime = raceTime;
            bestTimeText.text = "BEST: " + bestTime.ToString("ss\\:ff");
            bestTimeText.color = Color.paleGoldenRod;
            PlayerPrefs.SetInt(bestTimeKey, (int)bestTime.Ticks);
            PlayerPrefs.Save();
        }
        Debug.Log("Race ended");
    }

    private void Update()
    {
        if(racing)
            raceTime = DateTime.Now - raceStart;
        //Debug.Log("Race time " + raceTime);
        raceTimeText.text = "TIME: " + raceTime.ToString("ss\\:ff");
    }
}
