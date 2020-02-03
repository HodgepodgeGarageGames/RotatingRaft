using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AButton : MonoBehaviour
{
    public bool checkedIn = false;
    [SerializeField] private Transform image = null;
    [SerializeField] private int playerNumber = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkedIn)
        {
            image.localScale = Vector3.one * (0.75f + (Mathf.Sin(Time.time * 2.0f) * 0.25f));

            if (SNES.gamePad[playerNumber].ADown())
            {
                checkedIn = true;
                image.gameObject.SetActive(false);
                GlobalGameData.playersIn[playerNumber] = true;
            }
        }
        else
        {
            if (SNES.gamePad[playerNumber].ADown())
            {
                checkedIn = false;
                image.gameObject.SetActive(true);
                GlobalGameData.playersIn[playerNumber] = false;
            }
        }
        
    }
}
