using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Woadcraft : MonoBehaviour
{


    public float goblinHP;
    public float goblinHPMax;

    public List<Sprite> balthazars = new List<Sprite>();
    public List<Sprite> loots = new List<Sprite>();

    public List<string> balthazarNames = new List<string>();
    public List<AbilityButton> abilityButtons = new List<AbilityButton>();


    public Slider hpBar;
    public Image actionBar;
    public Image currentEnemyImage;
    public TextMeshProUGUI displayText;
    public Button claimLoot;


    int combo;
    public List<int> previous = new List<int>();

    AbilityButton randButton;

    void Start()
    {
        goblinHP = goblinHPMax;
        hpBar.value = goblinHP / goblinHPMax;
        claimLoot.gameObject.SetActive(false);
        for(int i = 0; i < abilityButtons.Count; i++)
        {
            abilityButtons[i].Initialise(this, i);
        }

        foreach (AbilityButton ab in abilityButtons)
        {
            previous.Add(ab.index);
            ab.Highlight(-1);
        }

        NewRound();
    }

    
    void Update()
    {
        
    }

    public void DamageGoblin(int index)
    {
        if(randButton.index == index)
        {
            previous.Remove(index);
            combo += 1;
            goblinHP -= combo;
        }
        else
        {
            combo = 0;
            goblinHP -= 1;
        }

        hpBar.value = goblinHP / goblinHPMax;

        NewRound();

    }

    void NewRound()
    {

        if(EnemyIsDead())
        {
            DoLootScreen();
            return;
        }

        randButton = abilityButtons[previous[Random.Range(0, previous.Count)]];
       
        foreach(AbilityButton ab in abilityButtons)
        {
            ab.Highlight(randButton.index);
        }
    }


    void DoLootScreen()
    {
        //LOOT HERE
        actionBar.gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
        displayText.text = "LOOT!";
        claimLoot.gameObject.SetActive(true);

        currentEnemyImage.sprite = loots[Random.Range(0, loots.Count)];

    }

    public void NewEnemy()
    {

        previous.Clear();
        combo = 0;

        foreach(AbilityButton ab in abilityButtons)
        {
            previous.Add(ab.index);
            ab.Highlight(-1);
        }

        actionBar.gameObject.SetActive(true);
        hpBar.gameObject.SetActive(true);

        claimLoot.gameObject.SetActive(false);

        currentEnemyImage.sprite = balthazars[Random.Range(0, balthazars.Count)];
        displayText.text = balthazarNames[Random.Range(0, balthazarNames.Count)];
        goblinHP = goblinHPMax;
        hpBar.value = goblinHP / goblinHPMax;
    }

    bool EnemyIsDead()
    {
        return goblinHP <= 0;
    }
}
