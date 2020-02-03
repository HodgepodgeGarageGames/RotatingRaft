using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D body;
    private PlayerInput.PlayerInputReceiver input;
    private Animator anim = null;

    public float       accel {
        get { return GetComponentInParent<PlayersAssemblyBehavior>().playerAcceleration; }
    }
    public float       maxSpeed {
        get  { return GetComponentInParent<PlayersAssemblyBehavior>().playerMaxSpeed; }
    }
    public float       friction {
        get { return GetComponentInParent<PlayersAssemblyBehavior>().playerFriction; }
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        var p = GetComponentInParent<PlayersAssemblyBehavior>();
        input = PlayerInput.GetInputReceiver(p.GetPlayerIndex(transform));

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Friction();
        HandleControls();

        if (body.velocity.magnitude > 0.02f)
        {
            Vector2 vel = body.velocity.normalized;
            if (Mathf.Abs(vel.x) >= Mathf.Abs(vel.y))
            {
                if (vel.x > 0)
                    anim.SetInteger("Direction", 4);
                else
                    anim.SetInteger("Direction", 3);
            }
            else
            {
                if (vel.y > 0)
                    anim.SetInteger("Direction", 1);
                else
                    anim.SetInteger("Direction", 2);
            }
        }
        else
            anim.SetInteger("Direction", 0);
    }

    void HandleControls() {
        int x = 0, y = 0;
        if (Input.GetKey(KeyCode.Backspace)) {
            body.velocity = transform.position = new Vector2(0,0);
            return;
        }
        if (input.a) {
            body.velocity = new Vector2(0,0);
            return;
        }
        if (input.right) {
            ++x;
            Vector3 ov = transform.localScale;
        }
        if (input.left) {
            --x;
            Vector3 ov = transform.localScale;
        }
        if (input.down)
            --y;
        if (input.up)
            ++y;

        var v = body.velocity;
        float a = accel * Time.fixedDeltaTime;
        body.velocity = LimitMagnitude(new Vector2(v.x + x * a, v.y + y * a));
    }

    Vector2 LimitMagnitude(Vector2 v)
    {
        var mag = Mathf.Sqrt(v.x * v.x + v.y * v.y);
        if (mag < maxSpeed) {
            return v;
        }
        else {
            var theta = Mathf.Atan2(v.y, v.x);
            return new Vector2(
                maxSpeed * Mathf.Cos(theta),
                maxSpeed * Mathf.Sin(theta)
            );
        }
    }

    void Friction() {
        // Get direction and magnitude of current momentum
        var v = body.velocity;
        var mag = Mathf.Sqrt(v.x * v.x + v.y * v.y);
        var theta = Mathf.Atan2(v.y, v.x);
        var decel = friction * Time.fixedDeltaTime;
        if (mag < decel) {
            body.velocity = new Vector2(0, 0);
        }
        else {
            mag -= decel;
            body.velocity = new Vector2(
                mag * Mathf.Cos(theta),
                mag * Mathf.Sin(theta)
            );
        }
    }

    public void OnMotorStay(MotorBehavior motor) {
        if (input.a) motor.Thrust();
    }
}
