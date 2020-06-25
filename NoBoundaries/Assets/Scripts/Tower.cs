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
    Collider2D[] enemiesWithinRange = new Collider2D[10];
    Collider2D[] enemiesWithinAOE = new Collider2D[10];
    int enemyCount = 0;
    float lastFireTime = float.MinValue;
    [SerializeField, ReadOnly] WeaponData weaponData;

    Vector2 currentDirection = Vector3.zero;

    AIController nearestEnemy = null;

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


    void Fire()
    {
        if (nearestEnemy)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.TransformPoint(gunExit);//Get gun exit position in global space
            projectile.Init(weaponData, currentDirection, team);

            lastFireTime = Time.time;
        }
        else if (weaponData.areaOfEffect > 0.0f)
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, weaponData.range, enemiesWithinAOE);
            for (int i = 0; i < count; ++i)
            {
                //Get enemy distance percent of aoe range
                float percent = Vector2.Distance(transform.position, enemiesWithinAOE[i].ClosestPoint(transform.position)) / weaponData.areaOfEffect;

                //Hurt enemies within aoe scaled by distance
                enemiesWithinAOE[i].GetComponent<HealthComponent>().ChangeHealthBy(-weaponData.damage * percent);

                Rigidbody2D rigidbody = enemiesWithinAOE[i].GetComponent<Rigidbody2D>();
                rigidbody.AddForce((enemiesWithinAOE[i].transform.position - transform.position).normalized * percent * weaponData.force, ForceMode2D.Impulse);
            }
        }
    }

    void Aim()
    {
        enemyCount = Physics2D.OverlapCircleNonAlloc(transform.position, weaponData.range, enemiesWithinRange);
        float nearestEnemySqrDist = float.MaxValue;
        nearestEnemy = null;
        for (int i = 0; i < enemyCount; ++i)
        {
            AIController ai = enemiesWithinRange[i].GetComponent<AIController>();
            if (ai)
            {
                float sqrDist = Vector2.SqrMagnitude(transform.position - ai.transform.position);
                if (sqrDist < nearestEnemySqrDist)
                {
                    nearestEnemySqrDist = sqrDist;
                    nearestEnemy = ai;
                }
            }
        }

        if (nearestEnemy)
        {
            Vector2 vecDif = nearestEnemy.transform.position - transform.position;
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
