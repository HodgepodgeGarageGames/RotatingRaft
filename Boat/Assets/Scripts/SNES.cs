using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SNES
{
    public class GamePad
    {
        private string horizontal;
        private string vertical;
        private string a;

        public GamePad(string H, string V, string A)
        {
            horizontal = H;
            vertical = V;
            a = A;
        }

        public float Horizontal()
        {
            if (Input.GetAxis(horizontal) > 0.0f)
                Debug.Log(horizontal);

            return Input.GetAxis(horizontal);
        }

        public float Vertical()
        {
            if (Input.GetAxis(vertical) > 0.0f)
                Debug.Log(vertical);

            return Input.GetAxis(vertical);
        }

        public bool A()
        {
            if (Input.GetButton(a))
                Debug.Log(a);

            return Input.GetButton(a);
        }

        public bool ADown()
        {
            if (Input.GetButtonDown(a))
                Debug.Log(a + " down");

            return Input.GetButtonDown(a);
        }

        public bool AUp()
        {
            if (Input.GetButtonUp(a))
                Debug.Log(a + " up");

            return Input.GetButtonUp(a);
        }
    }

    public static GamePad[] gamePad = {
        new GamePad("P1Horizontal", "P1Vertical", "P1A"),
        new GamePad("P2Horizontal", "P2Vertical", "P2A"),
        new GamePad("P3Horizontal", "P3Vertical", "P3A"),
        new GamePad("P4Horizontal", "P4Vertical", "P4A")
    };
}
