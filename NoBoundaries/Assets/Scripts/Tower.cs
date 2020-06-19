using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform gun;
    [SerializeField] Weapon weaponData;
    [SerializeField] Projectile projectilePrefab;

    [Header("Settings")]
    [SerializeField] Vector2 gunExit;

    [Header("Data")]
    [SerializeField] Team team;
    Collider2D[] enemiesNearMe = new Collider2D[10];
    int enemyCount = 0;
    float lastFireTime = float.MinValue;

    Vector2 currentDirection = Vector3.zero;

    AIController nearestEnemy = null;

    private void Update()
    {
        Aim();
        Fire();
    }

    void Fire()
    {
        if (nearestEnemy &&
            Time.time - lastFireTime > weaponData.Cooldown)
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.TransformPoint(gunExit);//Get gun exit position in global space
            projectile.Init(weaponData, currentDirection, team);

            lastFireTime = Time.time;
        }
    }

    void Aim()
    {
        enemyCount = Physics2D.OverlapCircleNonAlloc(transform.position, weaponData.Range, enemiesNearMe);
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(gunExit), projectilePrefab.transform.localScale.x * 0.5f);
    }
#endif
}
