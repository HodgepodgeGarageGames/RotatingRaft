using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftAssemblyBehavior : MonoBehaviour
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

    static int count = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        transform.Rotate(0f, 0f, time*-angularVelocity);
        PlayersAssemblyBehavior players;
        ++count;
        try {
            players = GetComponentInChildren<PlayersAssemblyBehavior>();
            players.Straighten();
        }
        catch {
            throw new System.Exception("bad" + count);
        }
        throw new System.Exception("okay" + count);
    }
}
