using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftBehavior : MonoBehaviour
{
    public float            angularVelocity = 30;
    private Rigidbody2D     raftBody;

    // Start is called before the first frame update
    void Start()
    {
        //// Set up physics body-based rotation
        //raftBody = transform.Find("Raft").GetComponent<Rigidbody2D>();
        //raftBody.angularVelocity = -spinPerSecond;
        //raftBody.angularDrag = 0f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        transform.Rotate(0f, 0f, time*-angularVelocity);
        var playBeh = transform.Find("PlayersAssembly").GetComponent<PlayersAssemblyBehavior>();
        playBeh.Straighten();
    }
}
