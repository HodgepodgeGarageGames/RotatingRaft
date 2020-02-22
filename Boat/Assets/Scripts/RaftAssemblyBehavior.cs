using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftAssemblyBehavior : MonoBehaviour
{
    public AudioSource      crashSound;
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
        uint numPlayers = GlobalGameData.numPlayers;
        if (numPlayers == 0) numPlayers = 1; // for testing (no players)
        angularVelocity = minAngularVelocity + (numPlayers-1)*(maxAngularVelocity-minAngularVelocity)/3;
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

    /*void OnTriggerStay2D(Collider2D col)
    {
        if (IsRiverCollider(col)) {
            var b = GetComponentInChildren<DamageGridBehavior>();
            if (b.IncurDamage()) PlayCrash();
        }
    }*/

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (IsRiverCollider(collision.collider))
        {
            var b = GetComponentInChildren<DamageGridBehavior>();
            if (b.IncurDamage()) PlayCrash();
        }
    }

    bool IsRiverCollider(Collider2D col)
    {
        return typeof(EdgeCollider2D).IsInstanceOfType(col)
            && col.GetComponentInParent<NuRiver>() != null;
    }

    void PlayCrash()
    {
        crashSound.Play();
    }
}
