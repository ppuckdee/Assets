using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{

    [SerializeField] private Vector3 pos;
    public float step = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
        pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, pos, step);

    }
}
