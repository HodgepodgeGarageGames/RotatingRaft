using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorsAssemblyBehavior : MonoBehaviour
{
    public GameObject   motor;
    public float        thrust;
    private Transform[] motors;
    private const int numMotors = 4;

    // Start is called before the first frame update
    void Start()
    {
        motors = new Transform[numMotors];
        var origin = new Vector3(0,0,0);
        for (int i=0; i != numMotors; ++i) {
            var m = Instantiate(motor, origin, Quaternion.Euler(0,0,90f*i));
            m.name = $"Motor {i}";
            m.transform.parent = transform;
            motors[i] = m.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
