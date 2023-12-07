using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{

    private Light light;

    public float intensity;
    public float defIntensity;

    public float minTime;
    public float maxTime;
    [SerializeField] private float _nextTime;              // when it will flash next
    public float flashTime;             // how long it will flash for
    public float flashTime_between;     // time between flashes

    public AudioClip thunder;
    public float minTime_thunder;
    public float maxTime_thunder;
    [SerializeField] private float _thunderTime;           // how long for thunder to sound
    public float volume;

    public GameObject center;
    public float distanceStep;
    [SerializeField] private Vector3 _dir;
    [SerializeField] private float _xPos;
    [SerializeField] private float _yPos;

    [SerializeField] private bool _flashAvail;

    // Start is called before the first frame update
    void Start()
    {
        
        light = GetComponent<Light>();
        defIntensity = light.intensity;

        _flashAvail = true;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (_flashAvail) {

            StartCoroutine(Flash());

        }

    }

    IEnumerator Flash() {

        _flashAvail = false;

        _nextTime = Random.Range(minTime, maxTime);
        _thunderTime = Random.Range(minTime_thunder, maxTime_thunder);

        _xPos = Random.Range(-1, 1);
        _yPos = Random.Range(-1, 1);
        var myDir = new Vector3(_xPos, 0, _yPos);

        _dir = (myDir * (_thunderTime * distanceStep));

        var savedNextTime = _nextTime;
        var savedThunderTime = _thunderTime;
        var savedDir = _dir;

        yield return new WaitForSeconds(savedNextTime);

        light.intensity = intensity;

        yield return new WaitForSeconds(flashTime);

        light.intensity = defIntensity;

        yield return new WaitForSeconds(flashTime_between);

        light.intensity = intensity;

        yield return new WaitForSeconds(flashTime);

        light.intensity = defIntensity;

        _flashAvail = true;

        yield return new WaitForSeconds(savedThunderTime);

        AudioSource.PlayClipAtPoint(thunder, savedDir + center.transform.position);

    }

}
