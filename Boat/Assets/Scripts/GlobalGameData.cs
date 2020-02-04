using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGameData
{
    public static bool[] playersIn = new bool[] { false, false, false, false };
    public static uint   numPlayers {
        get {
            uint ret = 0;
            foreach (bool val in playersIn) {
                if (val) ++ret;
            }
            return ret;
        }
    }
}
