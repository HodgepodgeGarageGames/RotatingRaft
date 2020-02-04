using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Transform image = null;
    [SerializeField] private AButton[] abuttons = new AButton[4];
    [SerializeField] private TitleScreen titleScreen = null;
    private PlayerInput.PlayerInputReceiver[] playerInput = new PlayerInput.PlayerInputReceiver[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i != 4; ++i) {
            playerInput[i] = PlayerInput.GetInputReceiver(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalGameData.playersIn[0] || GlobalGameData.playersIn[1] || GlobalGameData.playersIn[2] || GlobalGameData.playersIn[3])
        {
            image.gameObject.SetActive(true);
            image.localScale = Vector3.one * (0.9f + (Mathf.Sin(Time.time * 1.0f) * 0.1f));

            for (int i = 0; i < 4; ++i)
            {
                if (GlobalGameData.playersIn[i])
                {
                    if (playerInput[i].startDown)
                    {
                        titleScreen.GetGameStarted();
                    }
                }
            }
        }
        else
            image.gameObject.SetActive(false);
    }

    
}
