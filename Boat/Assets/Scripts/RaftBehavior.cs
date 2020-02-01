using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftBehavior : MonoBehaviour
{
    public float    spinPerSecond = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        transform.Rotate(0f, 0f, time*-spinPerSecond);
    }
}
