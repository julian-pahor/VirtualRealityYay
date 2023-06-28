using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Woadcraft : MonoBehaviour
{


    public Sprite evilBalth;
    public string balthEvilName;

    public float goblinHP;
    public float goblinHPMax;

    public List<Sprite> balthazars = new List<Sprite>();
    public List<Sprite> loots = new List<Sprite>();

    public List<string> balthazarNames = new List<string>();
    public List<AbilityButton> abilityButtons = new List<AbilityButton>();

    public List<AudioClip> damageClips = new List<AudioClip>();


    public Slider hpBar;
    public Image actionBar;
    public Image currentEnemyImage;
    public TextMeshProUGUI displayText;
    public Button claimLoot;


    int combo;
    List<int> previous = new List<int>();

    public AudioClip buttonClick;
    public AudioClip balthazarDie;
    public AudioClip loot;

    public AudioClip evilClip;
    public AudioSource source;
    AbilityButton randButton;
    Fire fire;
    bool evil;

    void Start()
    {
        fire = FindObjectOfType<Fire>();
        

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
        if (evil)
            return;

        if (Vector3.Distance(transform.position, fire.transform.position) < fire.flameRadius)
        {
            TurnEvil();
        }

    }

    void TurnEvil()
    {
        source.clip = evilClip;
        source.loop = true;
     
        source.Play();
        
        evil = true;
        claimLoot.gameObject.SetActive(false);
        actionBar.gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
        currentEnemyImage.sprite = evilBalth;
        displayText.text = balthEvilName;
       
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

        source.PlayOneShot(damageClips[Random.Range(0,damageClips.Count)]);
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
        source.PlayOneShot(loot);
        source.PlayOneShot(balthazarDie);
        actionBar.gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
        displayText.text = "LOOT!";
        claimLoot.gameObject.SetActive(true);

        currentEnemyImage.sprite = loots[Random.Range(0, loots.Count)];

    }

    public void NewEnemy()
    {
        source.PlayOneShot(buttonClick);
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
