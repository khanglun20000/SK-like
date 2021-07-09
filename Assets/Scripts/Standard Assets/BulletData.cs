using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bullet", menuName ="Bullet")]
public class BulletData : ScriptableObject
{
    public int critChance;

    public bool canExplode;
    public bool canBounce;
    public bool canBurn;
    public bool canFreeze;
    public bool canPoison;
}
