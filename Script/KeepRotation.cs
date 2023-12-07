using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{

    [SerializeField] private Vector3 setRotation;

    // Start is called before the first frame update
    void Start()
    {

        setRotation = transform.localEulerAngles;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.localEulerAngles = setRotation;

    }
}
