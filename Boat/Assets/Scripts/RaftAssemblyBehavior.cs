using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftAssemblyBehavior : MonoBehaviour
{
    public float            angularVelocity = 30;
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
