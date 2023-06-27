using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{

    Woadcraft woadcraft;
    public Image highlight;
    public Color baseColour;
    public Color highlightColour;

    public int index;

    public void Initialise(Woadcraft game, int index)
    {
        this.index = index;
        woadcraft = game;
        Highlight(-1);
    }


    public void Highlight(int index)
    {
        highlight.color = index == this.index ? highlightColour : baseColour;
    }

    public void OnClick()
    {
        woadcraft.DamageGoblin(index);
    }
    

}
