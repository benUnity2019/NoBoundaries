using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject
{
    //Interface
    public Sprite Icon;
    public Sprite ProjectileSprite;
    public float Health;
    public string[] Target;
    public float Damage;
    public float DamageOverTime;
    public float DamageOverTimeDurration;
    public float AreaOfEffect;
    public float Cooldown;
    public float ProjectileSpeed;
    public float Reach;
    public float Range;
    public float Force;
}
