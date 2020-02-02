using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SNES.gamePad[0].Horizontal();
        SNES.gamePad[1].Horizontal();
        SNES.gamePad[2].Horizontal();
        SNES.gamePad[3].Horizontal();

        SNES.gamePad[0].Vertical();
        SNES.gamePad[1].Vertical();
        SNES.gamePad[2].Vertical();
        SNES.gamePad[3].Vertical();

        SNES.gamePad[0].A();
        SNES.gamePad[1].A();
        SNES.gamePad[2].A();
        SNES.gamePad[3].A();
    }
}
