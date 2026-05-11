using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float rotSpeed = 30, speed = 20;
    private Rigidbody rb;
    [SerializeField] private bool grounded = true;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 pushbackForce;
    [SerializeField] private bool disabledControl =  false;
    [SerializeField] private float disableTime = 1;
    private float lastCollisionTime;
    public static Transform playerTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move"); 
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += TakeDamage;
    }
    
    //Dzird obstacle skriptu
    void TakeDamage()
    {
        rb.AddForce(pushbackForce);
        disabledControl = true;
        lastCollisionTime = Time.timeSinceLevelLoad;
        Debug.Log("Player Got HURT!!!");
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.timeSinceLevelLoad > lastCollisionTime + disableTime)
            disabledControl = false;
        grounded = Physics.Linecast(transform.position, transform.position + Vector3.down, groundMask);
        
        // #1
        //Color lineColor;
        //if (grounded)
        //    lineColor = Color.green;
        //else
        //lineColor = Color.red;
        
        // #2
        Color lineColor = grounded ? Color.green : Color.red;
        
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red);
        if (grounded && !disabledControl)
        {
            //transform.forward = move.ReadValue<Vector2>();
            Vector2 moveInput = move.ReadValue<Vector2>();
            Debug.Log("x: " + moveInput.x + " y: " + moveInput.y);
            transform.Rotate(0, -moveInput.x * rotSpeed * Time.fixedDeltaTime, 0 );
            float turnAngle = Mathf.Abs(180 - transform.localEulerAngles.y );
            float speedMult = Mathf.Cos(turnAngle * Mathf.Deg2Rad); //Rad - radiant
            rb.AddForce(transform.forward * speed * speedMult * Time.fixedDeltaTime);
            Debug.Log(" turnAngle: " + turnAngle );
        }
    }
}
