using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Display")]
    [SerializeField] Sprite icon;
    [SerializeField] Sprite projectileSprite;

    [Header("Damage")]
    [SerializeField] float damage;
    [SerializeField] float damageOverTime;
    [SerializeField] float damageOverTimeDurration;
    [SerializeField] float areaOfEffect;

    [Header("Speed")]
    [SerializeField] float cooldown;
    [SerializeField] float projectileSpeed;

    [Header("Distance")]
    [SerializeField] float reach;
    [SerializeField] float range;

    //Interface
    public Sprite Icon { get => icon; }
    public Sprite ProjectileSprite { get => projectileSprite; }
    public float Damage { get => damage; }
    public float DamageOverTime { get => damageOverTime; }
    public float DamageOverTimeDurration { get => damageOverTimeDurration; }
    public float AreaOfEffect { get => areaOfEffect; }
    public float Cooldown { get => cooldown; }
    public float ProjectileSpeed { get => projectileSpeed; }
    public float Reach { get => reach; }
    public float Range { get => range; }
}