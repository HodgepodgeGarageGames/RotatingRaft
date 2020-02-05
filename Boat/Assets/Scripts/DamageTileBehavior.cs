using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTileBehavior : MonoBehaviour
{
    private bool targeted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targeted) {
            var mom = GetComponentInParent<DamageGridBehavior>();
            float cycles = mom.repairFlashesPerSec;
            float alpha = 1 - Mathf.Cos(Time.time * 2 * Mathf.PI * cycles);
            SetOpacity(alpha);
        }
    }

    public void SetRepairing(bool repairing)
    {
        targeted = repairing;
        if (!targeted) {
            SetOpacity(1f);
        }
    }

    void SetOpacity(float alpha)
    {
        var spr = GetComponent<SpriteRenderer>();
        var c = spr.color;
        spr.color = new Color(c.r, c.g, c.b, alpha);
    }
}
