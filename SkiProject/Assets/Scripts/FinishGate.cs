using UnityEngine;

public class FinishGate : MonoBehaviour
{
    public static event GameManager.TimerEvent FinishRace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FinishRace();
            //You win
        }
    }
}
