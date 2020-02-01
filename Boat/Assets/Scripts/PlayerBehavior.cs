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
        speed = transform.Find("PlayersAssembly")
            .GetComponent<PlayersAssemblyBehavior>().playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        int x = 0, y = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            x = 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            x = -1;

        var body = GetComponent<Rigidbody2D>();
        var v = body.velocity;
        float factor = .01f;
        body.velocity = new Vector2(v.x + x * factor, v.y);
    }
}
