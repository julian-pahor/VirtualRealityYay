using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Woadcraft : MonoBehaviour
{



    public float goblinHP;
    public float goblinHPMax;

    public Image actionBar;
    public Image currentEnemyImage;
    public List<Sprite> balthazars = new List<Sprite>();

    public Slider hpBar;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void DamageGoblin(int damg)
    {
        goblinHP -= damg;
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

    }


    void DoLootScreen()
    {

    }

    void NewEnemy()
    {
        goblinHP = goblinHPMax;
        hpBar.value = goblinHP / goblinHPMax;
    }

    bool EnemyIsDead()
    {
        return goblinHP <= 0;
    }
}
