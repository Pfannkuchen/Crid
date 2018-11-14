using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorSettings
{
    public static Color getPlayerColor(int i)
    {
        Color[] allPlayerColors = new Color[]
        {
            Color.yellow,
            Color.blue,
            Color.cyan,
            Color.magenta
        };

        if (i >= allPlayerColors.Length)
        {
            return Color.white;
        }
        else
        {
            return allPlayerColors[i];
        }
    }
}