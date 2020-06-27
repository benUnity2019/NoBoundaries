using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponData
{
    [Header("Display")]
    public Sprite icon;
    public Sprite projectileSprite;

    [Header("Health")]
    public float health;

    [Header("Targets")]
    public string[] targets;

    [Header("Damage")]
    public float damage;
    public float damageOverTime;
    public float damageOverTimeDurration;
    public float areaOfEffect;

    [Header("Speed")]
    public float cooldown;
    public float projectileSpeed;

    [Header("Distance")]
    public float reach;
    public float range;
    public float force;

    static public WeaponData operator +(WeaponData lhs, WeaponData rhs)
    {
        return new WeaponData()
        {
            icon = lhs.icon,
            projectileSprite = lhs.projectileSprite,
            health = lhs.health + rhs.health,
            targets = lhs.targets,
            damage = lhs.damage + rhs.damage,
            damageOverTime = lhs.damageOverTime + rhs.damageOverTime,
            damageOverTimeDurration = lhs.damageOverTimeDurration + rhs.damageOverTimeDurration,
            areaOfEffect = lhs.areaOfEffect + rhs.areaOfEffect,
            cooldown = lhs.cooldown + rhs.cooldown,
            projectileSpeed = lhs.projectileSpeed + rhs.projectileSpeed,
            reach = lhs.reach + rhs.reach,
            range = lhs.range + rhs.range,
            force = lhs.force + rhs.force
        };
    }
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] WeaponData data;

    //Interface
    public Sprite Icon { get => Data.icon; }
    public Sprite ProjectileSprite { get => Data.projectileSprite; }
    public float Health { get => Data.health; }
    public string[] Targets { get => Data.targets; }
    public float Damage { get => Data.damage; }
    public float DamageOverTime { get => Data.damageOverTime; }
    public float DamageOverTimeDurration { get => Data.damageOverTimeDurration; }
    public float AreaOfEffect { get => Data.areaOfEffect; }
    public float Cooldown { get => Data.cooldown; }
    public float ProjectileSpeed { get => Data.projectileSpeed; }
    public float Reach { get => Data.reach; }
    public float Range { get => Data.range; }
    public float Force { get => Data.force; }
    public WeaponData Data { get => data; }
}