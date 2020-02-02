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
        private string b;
        private string start;
        private string select;

        public GamePad(string H, string V, string A, string B, string START, string SELECT)
        {
            horizontal = H;
            vertical = V;
            a = A;
            b = B;
            start = START;
            select = SELECT;
        }

        public float Horizontal()
        {
            return Input.GetAxis(horizontal);
        }

        public float Vertical()
        {
            return Input.GetAxis(vertical);
        }

        public bool A()
        {
           return Input.GetButton(a);
        }

        public bool ADown()
        {
            return Input.GetButtonDown(a);
        }

        public bool AUp()
        {
            return Input.GetButtonUp(a);
        }

        public bool Start()
        {
            return Input.GetButton(start);
        }

        public bool StartDown()
        {
            return Input.GetButtonDown(start);
        }

        public bool StartUp()
        {
            return Input.GetButtonUp(start);
        }

        public bool Select()
        {
            return Input.GetButton(select);
        }

        public bool SelectDown()
        {
            return Input.GetButtonDown(select);
        }

        public bool SelecttUp()
        {
            return Input.GetButtonUp(select);
        }

        public bool B()
        {
            return Input.GetButton(b);
        }

        public bool BDown()
        {
            return Input.GetButtonDown(b);
        }

        public bool BUp()
        {
            return Input.GetButtonUp(b);
        }
    }

    public static GamePad[] gamePad = {
        new GamePad("P1Horizontal", "P1Vertical", "P1A", "P1B", "P1Start", "P1Select"),
        new GamePad("P2Horizontal", "P2Vertical", "P2A", "P2B", "P2Start", "P2Select"),
        new GamePad("P3Horizontal", "P3Vertical", "P3A", "P3B", "P3Start", "P3Select"),
        new GamePad("P4Horizontal", "P4Vertical", "P4A", "P4B", "P4Start", "P4Select")
    };
}
