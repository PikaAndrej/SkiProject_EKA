using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    private enum Direction {Left, Right};
    [SerializeField] private Direction direction;
    [SerializeField] private Material goodMat, badMat;
    private bool flagPassed = false;
    public static event GameManager.TimerEvent RacePenalty;

    void Update()
    {
        if (PlayerControl.playerTransform != null && PlayerControl.playerTransform.position.z < transform.position.z && flagPassed == false)
        {
            Direction passingDirection = Direction.Right;
            if(PlayerControl.playerTransform.position.x < transform.position.x)
                passingDirection = Direction.Left;
            
            flagPassed = true;
            Debug.LogError("Player passed on: " + passingDirection);
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (passingDirection == direction)
            {
                Debug.LogError("passed on correct side");
                renderer.material = goodMat;
            }
            else
            {
                Debug.LogError("passed on the incorrect side");
                renderer.material = badMat;
            }
        }
    }
}
