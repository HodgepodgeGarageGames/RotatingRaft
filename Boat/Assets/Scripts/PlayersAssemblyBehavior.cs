using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAssemblyBehavior : MonoBehaviour
{
    public GameObject               playerPrefab;
    private List<Transform>         players = new List<Transform>();
    public float                    playerMaxSpeed = 1f;
    public float                    playerAcceleration = 1f; // secs per sec
    public float                    playerFriction = 4f; // secs per sec

    // Start is called before the first frame update
    void Start()
    {
        // Create players
        var obj = Instantiate(playerPrefab, new Vector3(-1,0,0), Quaternion.identity, transform);
        players.Add(obj.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Straighten()
    {
        foreach (var p in players) {
            //p.Rotate(0,0,0);
            p.rotation = Quaternion.identity;
        }
    }

    // Ensures that player velocities get rotated along with their positions (automatic)
    public void RotateBy(float degree)
    {
        float rads = degree * 2 * Mathf.PI / 360;
        foreach (var p in players) {
            var body = p.GetComponent<Rigidbody2D>();
            float x = body.velocity.x, y = body.velocity.y;
            body.velocity = new Vector2(
                x * Mathf.Cos(rads) - y * Mathf.Sin(rads),
                x * Mathf.Sin(rads) + y * Mathf.Cos(rads)
            );
        }
    }
}
