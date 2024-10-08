using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class Ink 
{
    public enum Level { DIM = 0, FULL}

    [HorizontalGroup("Group 1"), LabelWidth(100)]
    public Level currentLevel;
    public Color color { get; set; }
    [HorizontalGroup("Group 1"), LabelWidth(100)]
    public ColorMode colorMode;
    private int intensity;
    public int Intensity {
        get { return intensity; }
        set {
            if(value >= 1)
            {
                currentLevel = Level.FULL;
            }
            else
            {
                currentLevel = Level.DIM;
            }
            intensity = value;
        }
    }

    public Ink(bool canTraverse = false)
    {
        if (canTraverse) color = Color.white;
        else color = Services.ColorManager.GetColor(ColorMode.BLACK);
        colorMode = ColorMode.NONE;
        Intensity = 0;
    }
    /*
    public Ink()
    {
        color = Color.white;
        colorMode = ColorMode.NONE;
        Intensity = 0;
    }
    */

    public Ink(Color c, ColorMode mode, int i)
    {
        color = c;
        colorMode = mode;
        Intensity = i;
        if(colorMode == ColorMode.BLACK)
        {
            if (currentLevel == Level.FULL)
                color = Services.ColorManager.ColorScheme.GetColor(ColorMode.BLACK)[0];
            else
                color = Services.ColorManager.ColorScheme.GetColor(ColorMode.BLACK)[1];
        }
    }

    public Ink(ColorMode mode)
    {
        colorMode = mode;
        color = Services.ColorManager.ColorScheme.GetColor(mode)[0];
        Intensity = Player.MAX_INTENSITY_LEVEL;
    }
}
