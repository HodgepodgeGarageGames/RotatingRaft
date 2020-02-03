using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AButton : MonoBehaviour
{
    [SerializeField] private Transform image = null;
    [SerializeField] private int playerNumber = -1;
    [SerializeField] private GameObject toon = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalGameData.playersIn[playerNumber])
        {
            image.localScale = Vector3.one * (0.75f + (Mathf.Sin(Time.time * 2.0f) * 0.25f));

            if (SNES.gamePad[playerNumber].ADown())
            {
                GlobalGameData.playersIn[playerNumber] = true;
                image.gameObject.SetActive(false);
                toon.SetActive(true);
            }
        }
        else
        {
            if (SNES.gamePad[playerNumber].ADown())
            {
                GlobalGameData.playersIn[playerNumber] = false;
                image.gameObject.SetActive(true);
                toon.SetActive(false);
            }
        }
        
    }
}
