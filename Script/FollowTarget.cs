using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public GameObject target;

    [SerializeField] private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {

        distance = transform.position - target.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = target.transform.position + distance;

    }
}
