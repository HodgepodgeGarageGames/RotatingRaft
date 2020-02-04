using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    private DamageGridBehavior damageGrid = null;
    private PlayersAssemblyBehavior pab = null;
    private Transform damagedTile = null;
    private float timeUntilRepair = 0.0f;
    private PlayerInput.PlayerInputReceiver playerInput;

    // Start is called before the first frame update
    void Start()
    {
        damagedTile = null;
        pab = transform.parent.GetComponent<PlayersAssemblyBehavior>();
        damageGrid = transform.parent.parent.GetComponentInChildren<DamageGridBehavior>();
        playerInput = PlayerInput.GetInputReceiver(pab.GetPlayerIndex(transform));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.bDown)
        {
            damagedTile = damageGrid.getClosestDamagedTile(transform.position, 0.7f);

            timeUntilRepair = 1.0f;
        }

        if (playerInput.b)
        {
            if (damagedTile)
            {
                if (timeUntilRepair <= 0.0f)
                {
                    damageGrid.repairTile(damagedTile);
                    damagedTile = null;
                }
                else
                {
                    timeUntilRepair -= Time.deltaTime;
                }
            }
        }
    }
}
