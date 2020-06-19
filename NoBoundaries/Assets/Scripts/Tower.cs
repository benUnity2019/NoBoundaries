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
    Collider2D[] enemiesNearMe = new Collider2D[10];
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
        Fire();
    }


    void Fire()
    {
        if (nearestEnemy &&
            Time.time - lastFireTime > weaponData.cooldown)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.TransformPoint(gunExit);//Get gun exit position in global space
            projectile.Init(weaponData, currentDirection, team);

            lastFireTime = Time.time;
        }
    }

    void Aim()
    {
        enemyCount = Physics2D.OverlapCircleNonAlloc(transform.position, weaponData.range, enemiesNearMe);
        float nearestEnemySqrDist = float.MaxValue;
        nearestEnemy = null;
        for (int i = 0; i < enemyCount; ++i)
        {
            AIController ai = enemiesNearMe[i].GetComponent<AIController>();
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
