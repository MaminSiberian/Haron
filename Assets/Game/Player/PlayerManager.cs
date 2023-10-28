using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int increaseHP;
    [SerializeField] private int reduceUpgradeHP;
    [SerializeField] private int minIinkreaseHP;
    [Space]
    [SerializeField] private float reduceCooldownDash;
    [SerializeField] private float increaseDurationDash;
    [SerializeField] private float reduceUpgradeDash;
    [SerializeField] private float minReduceCooldownDash;
    [Space]
    [SerializeField] private int increaseDamage;
    [SerializeField] private int reduceUpgradeDamage;
    [SerializeField] private int minIncreaseDamage;
    [SerializeField] private HaronController hc;
    [SerializeField] private GameplayUI UI;

    private void Start()
    {
        hc = FindObjectOfType<HaronController>();
        UI = FindObjectOfType<GameplayUI>();
    }
    private void OnEnable()
    {
        Shop.OnItemPurchasedEvent += Upgrade;
    }

    private void OnDisable()
    {
        Shop.OnItemPurchasedEvent -= Upgrade;
    }


    private void Upgrade(Item item)
    {
        if (item == Item.HP)
        {
            hc.maxHP += increaseHP;
            if (increaseHP > minIinkreaseHP)
                increaseHP -= reduceUpgradeHP;
            UI.SetHPValue(hc.maxHP);
            hc.CurrentHP = hc.maxHP;
        }
        else if (item == Item.Dash)
        {
            hc.cooldownDash -= reduceCooldownDash;
            hc.durationDash += increaseDurationDash;
            if (reduceCooldownDash > minReduceCooldownDash)
                reduceCooldownDash -= reduceUpgradeDash;            
        }
        else if (item == Item.Damage)
        {
            hc.damage += increaseDamage;
            if (increaseDamage > minIncreaseDamage)
                increaseDamage -= reduceUpgradeDamage;
        }
    }
}
