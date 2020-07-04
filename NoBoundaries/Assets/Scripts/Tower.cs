using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform gun;
    [SerializeField] Weapon startingWeaponData;
    [SerializeField] Projectile projectilePrefab;

    [Header("Settings")]
    [SerializeField] Vector2 gunExit;
    [SerializeField] WeaponData UpgradeAmount;

    [Header("Data")]
    [SerializeField] Team team;
    Collider2D[] targetsWithinRange = new Collider2D[10];
    Collider2D[] targetsWithinAOE = new Collider2D[10];
    int targetCount = 0;
    float lastFireTime = float.MinValue;
    [SerializeField, ReadOnly] WeaponData weaponData;

    Vector2 currentDirection = Vector3.zero;

    AIController nearestTarget = null;

    GameObject owner;

    private void Start()
    {
        weaponData = startingWeaponData.Data;
    }

    private void Update()
    {
        Aim();

        if (Time.time - lastFireTime > weaponData.cooldown)
            Fire();
    }

    public void Init(GameObject owner)
    {
        this.owner = owner;
    }

    void Fire()
    {
        if (nearestTarget)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.TransformPoint(gunExit);//Get gun exit position in global space
            projectile.Init(weaponData, currentDirection, team, gameObject);

            lastFireTime = Time.time;
        }
        else if (weaponData.areaOfEffect > 0.0f)
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, weaponData.range, targetsWithinAOE);
            for (int i = 0; i < count; ++i)
            {
                //Get target distance percent of aoe range
                float percent = Vector2.Distance(transform.position, targetsWithinAOE[i].ClosestPoint(transform.position)) / weaponData.areaOfEffect;

                //Hurt targets within aoe scaled by distance
                targetsWithinAOE[i].GetComponent<HealthComponent>().ChangeHealthBy(-weaponData.damage * percent);

                Rigidbody2D rigidbody = targetsWithinAOE[i].GetComponent<Rigidbody2D>();
                rigidbody.AddForce((targetsWithinAOE[i].transform.position - transform.position).normalized * percent * weaponData.force, ForceMode2D.Impulse);
            }
        }
    }

    void OnMeHurtingSomeoneElse(DamageEventData damageEvent)
    {
        owner.SendMessage("OnMeHurtingSomeoneElse", damageEvent);
    }

    void Aim()
    {
        targetCount = Physics2D.OverlapCircleNonAlloc(transform.position, weaponData.range, targetsWithinRange);
        float nearestTargetSqrDist = float.MaxValue;
        nearestTarget = null;
        for (int i = 0; i < targetCount; ++i)
        {
            AIController ai = targetsWithinRange[i].GetComponent<AIController>();
            if (ai)
            {
                float sqrDist = Vector2.SqrMagnitude(transform.position - ai.transform.position);
                if (sqrDist < nearestTargetSqrDist)
                {
                    nearestTargetSqrDist = sqrDist;
                    nearestTarget = ai;
                }
            }
        }

        if (nearestTarget)
        {
            Vector2 vecDif = nearestTarget.transform.position - transform.position;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Atan2(vecDif.y, vecDif.x) * Mathf.Rad2Deg);
            currentDirection = vecDif;
        }
    }

    private void OnMouseDown()
    {
        Upgrade();
    }

    void Upgrade()
    {
        weaponData += UpgradeAmount;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(gunExit), projectilePrefab.transform.localScale.x * 0.5f);
    }
#endif
}
