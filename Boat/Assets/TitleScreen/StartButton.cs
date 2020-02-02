using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Transform image = null;
    [SerializeField] private AButton[] abuttons = new AButton[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (abuttons[0].checkedIn || abuttons[1].checkedIn || abuttons[2].checkedIn || abuttons[3].checkedIn)
        {
            image.gameObject.SetActive(true);
            image.localScale = Vector3.one * (0.9f + (Mathf.Sin(Time.time * 1.0f) * 0.1f));

            for (int i = 0; i < 4; ++i)
            {
                if (abuttons[i].checkedIn)
                {
                    if (SNES.gamePad[i].StartDown())
                    {
                        SceneManager.LoadScene("RiverTest", LoadSceneMode.Single);
                    }
                }
            }
        }
        else
            image.gameObject.SetActive(false);
    }
}
