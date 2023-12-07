using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionScript : MonoBehaviour
{

    public AudioClip clip;
    public float volume = 0.2f;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject col){

        int numCollisionEvents = part.GetCollisionEvents(col, collisionEvents);

        int i = 0;

        while (i < numCollisionEvents) {

            AudioSource.PlayClipAtPoint(clip, collisionEvents[i].intersection, volume);

            i++;
        
        }

    }

}
