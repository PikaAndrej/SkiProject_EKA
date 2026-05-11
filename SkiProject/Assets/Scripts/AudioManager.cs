using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip collisionSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnEnable()
    {
        Obstacle.OnPlayerHit += PlayCollisionSound;
    }
    
    private void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
