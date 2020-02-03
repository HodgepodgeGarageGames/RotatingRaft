using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject instructions = null;

    // Start is called before the first frame update
    void Start()
    {
        GlobalGameData.playersIn[0] = false;
        GlobalGameData.playersIn[1] = false;
        GlobalGameData.playersIn[2] = false;
        GlobalGameData.playersIn[3] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalGameData.playersIn[0] ||
            GlobalGameData.playersIn[1] ||
            GlobalGameData.playersIn[2] ||
            GlobalGameData.playersIn[3])
        {
            instructions.SetActive(true);
        }
        else
        {
            instructions.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
