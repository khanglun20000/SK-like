using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    SuperRare,
    Epic,
    Legendary
}
public enum Type
{
    Bow,
    SMG,
    Sword
}
public enum CombatStyle
{
    Ranged,
    Melee,
    Both
}
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public Rarity rarity;
    public Type type;
    public CombatStyle cbStyle;
    public float holdRotaion; // rotation difference between sprite and holding
    public float timeBtwShots;
    public float chargeTime; // time charging before release bullet
    public int damage,crit,mana;
    public int bulletsEachShot;
    public int timesEachShot;
    public float intervalBtwTimes; // interval between each time in a shot
    public float delayTime; // Time delayed between starting animation and starting timeBtwAttack

    public Sprite artwork;
    public Sprite display;
    public RuntimeAnimatorController shootAniCtrl;
    public GameObject bulletType;   
}
