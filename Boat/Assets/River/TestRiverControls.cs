using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRiverControls : MonoBehaviour
{
    public NuRiver river = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            river.moveRiver(Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            river.moveRiver(Vector3.up * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            river.moveRiver(Vector3.down * Time.deltaTime);
        }
    }
}
