using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardDummy : MonoBehaviour
{

    public Camera cam;

    public float cooldown_time = 0.5f;
    public float cooldown_currTime;
    public float cooldown_nextTime = 0f;
    public bool particle_available = true;

    public ParticleSystem particleSystem;

    [SerializeField] private Rigidbody rb;
    public float speed = 15f;
    public float maxSpeed = 10f;
    public float slowdown = 5f;

    private AudioSource audio;
    public AudioClip grass;
    public AudioClip wood;
    [SerializeField] private bool grassPlaying;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audio = gameObject.GetComponent<AudioSource>();

        cam = Camera.main;

    }

    void Update()
    {

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
         
        //camera forward and right vectors:
        var forward = cam.transform.forward;
        var right = cam.transform.right;
 
        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
 
        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;
 
        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * speed * Time.deltaTime);

        // JUICE

        if (cooldown_currTime > cooldown_nextTime) {
            
            particle_available = true;

        }

        if (particle_available) {

            particle_available = false;
            cooldown_nextTime = Time.time + cooldown_time;
            particleSystem.Play();

        }

            
        /* if (rb.velocity.magnitude > maxSpeed) {

            rb.velocity = rb.velocity.normalized * maxSpeed;

        }

        if (Input.GetKeyDown(KeyCode.W)) {

            audio.Play();

        } else if (Input.GetKeyUp(KeyCode.W)) {

            audio.Stop();

        } */
    }
    
}
