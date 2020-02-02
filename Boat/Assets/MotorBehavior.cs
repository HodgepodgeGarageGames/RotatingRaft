using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBehavior : MonoBehaviour
{
    public float thrust {
        get {
            // Assumes parent is a MotorsAssembly
            return GetComponentInParent<MotorsAssemblyBehavior>().thrust;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider) {
        var pb = collider.gameObject.GetComponent<PlayerBehavior>();
        if (pb) pb.OnMotorStay(this);
    }

    public void Thrust() {
        NuRiver river = null;
        foreach (var obj in gameObject.scene.GetRootGameObjects()) {
            river = obj.GetComponent<NuRiver>();
            if (river != null) break;
        }
        string r = river == null? "not ": "";

        Debug.Log($"Thrust! Motor {gameObject.name} (river {r}found)");

        var rb2d = river.gameObject.GetComponent<Rigidbody2D>();
        var vec3 = transform.TransformDirection(0,Time.fixedDeltaTime*thrust,0);
        var vec = new Vector2(vec3.x,vec3.y);
        rb2d.AddForce(vec);
    }
}
