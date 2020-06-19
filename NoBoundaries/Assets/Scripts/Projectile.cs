using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] new Rigidbody2D rigidbody;

    WeaponData data;
    public WeaponData Data { get => data; set => data = value; }

    Team team;

    public void Init(WeaponData data, Vector2 direction, Team team)
    {
        if (direction.sqrMagnitude > 0.0f)
        {
            this.team = team;
            this.data = data;
            renderer.sprite = data.projectileSprite;
            rigidbody.velocity = data.projectileSpeed * direction.normalized;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>().Team != team)
        {
            //Do impact damage
            ApplyDamage(collision.gameObject);

            //Do area of effect
            if (data.areaOfEffect > 0.0f)
            {
                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, data.areaOfEffect);
                for (int i = 0; i < cols.Length; i++)
                {
                    ApplyDamage(cols[i].gameObject);
                }
            }


        }
    }

    public void ApplyDamage(GameObject gameObject)
    {
        HealthComponent health = gameObject.GetComponent<HealthComponent>();
        if (health)
        {
            health.ChangeHealthBy(-data.damage);

            if (data.damageOverTime > 0.0f)
            {
                health.AddDamageOverTime(data.damageOverTime, data.damageOverTimeDurration);
            }
        }
    }
}