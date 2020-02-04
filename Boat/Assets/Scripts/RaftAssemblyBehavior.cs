using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftAssemblyBehavior : MonoBehaviour
{
    public float            minAngularVelocity = 8;  // 1 player
    public float            maxAngularVelocity = 20; // 4 players
    private float           angularVelocity = 20;
    private Rigidbody2D     raftBody;
    PlayersAssemblyBehavior players;

    // Start is called before the first frame update
    void Start()
    {
        //// Set up physics body-based rotation
        //raftBody = transform.Find("Raft").GetComponent<Rigidbody2D>();
        //raftBody.angularVelocity = -spinPerSecond;
        //raftBody.angularDrag = 0f;

        players = GetComponentInChildren<PlayersAssemblyBehavior>();

        // Scale angularVelocity linearly from 8 (1 player) to 20 (4 playes)
        angularVelocity = minAngularVelocity
            + (GlobalGameData.numPlayers-1)*(maxAngularVelocity-minAngularVelocity)/3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        float rot = time*-angularVelocity;
        transform.Rotate(0f, 0f, rot);
        players.Straighten();
        players.RotateBy(rot);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("TRIGGERED");
        if (IsRiverCollider(col)) {
            var b = GetComponentInChildren<DamageGridBehavior>();
            b.IncurDamage();
        }
    }

    bool IsRiverCollider(Collider2D col)
    {
        return typeof(EdgeCollider2D).IsInstanceOfType(col)
            && col.GetComponentInParent<NuRiver>() != null;
    }
}
