using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Vector3 currentPosition;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float scale = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPosition + new Vector3(0.0f, Mathf.Sin(Time.time * speed), 0.0f) * scale;
    }
}
