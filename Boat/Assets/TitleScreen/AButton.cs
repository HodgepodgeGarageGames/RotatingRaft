using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AButton : MonoBehaviour
{
    [SerializeField] private Transform image = null;
    [SerializeField] private int playerNumber = -1;
    [SerializeField] private TitleToon toon = null;
    private PlayerInput.PlayerInputReceiver playerInput;

    // Update is called once per frame
    void Update()
    {
        // This should go in Start, but I'm not sure playerNumber
        // will be assigned in time.
        if (playerNumber == -1)
            return;
        else if (playerInput == null)
            playerInput = PlayerInput.GetInputReceiver(playerNumber);

        if (!GlobalGameData.playersIn[playerNumber])
        {
            image.localScale = Vector3.one * (0.75f + (Mathf.Sin(Time.time * 2.0f) * 0.25f));

            if (playerInput.aDown)
            {
                GlobalGameData.playersIn[playerNumber] = true;
                image.gameObject.SetActive(false);
                toon.gameObject.SetActive(true);
                toon.playASound();
            }
        }
        else
        {
            if (playerInput.aDown)
            {
                GlobalGameData.playersIn[playerNumber] = false;
                image.gameObject.SetActive(true);
                toon.gameObject.SetActive(false);
            }
        }
        
    }
}
