using UnityEngine;
//using static GameManager;

public class StartGate : MonoBehaviour
{
    public static event GameManager.TimerEvent StartRace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            StartRace.Invoke();
        }
    }
}
