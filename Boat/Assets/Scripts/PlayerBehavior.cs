using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D body;
    float       speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(-1,0);
        speed = GetComponentInParent<PlayersAssemblyBehavior>().playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        int x = 0, y = 0;
        var body = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown(KeyCode.Space)) {
            body.velocity = new Vector2(0,0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ++x;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            --x;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            --y;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ++y;

        var v = body.velocity;
        body.velocity = new Vector2(v.x + x * speed, v.y + y * speed);
    }
}
