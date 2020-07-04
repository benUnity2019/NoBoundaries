using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] GameObject objectThatShotMe;

    int peirceCount = 0;

    WeaponData weaponData;
    public WeaponData Data { get => weaponData; set => weaponData = value; }

    Team team;

    public void Init(WeaponData data, Vector2 direction, Team team, GameObject objectThatShotMe)
    {
        if (direction.sqrMagnitude > 0.0f)
        {
            this.team = team;
            this.weaponData = data;
            renderer.sprite = data.projectileSprite;
            rigidbody.velocity = data.projectileSpeed * direction.normalized;
            this.objectThatShotMe = objectThatShotMe;
            peirceCount = weaponData.peirceCount;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController character = collision.GetComponent<CharacterController>();

        if (character &&
            (weaponData.targetIsOwnTeam && character.Team == team ||
            !weaponData.targetIsOwnTeam && character.Team != team))
        {
            //Do impact damage
            ApplyDamage(collision.gameObject, 1.0f);

            //Do area of effect
            if (weaponData.areaOfEffect > 0.0f)
            {
                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, weaponData.areaOfEffect);
                for (int i = 0; i < cols.Length; i++)
                {
                    //Get enemy distance percent of aoe range
                    float percent = Vector2.Distance(transform.position, cols[i].ClosestPoint(transform.position)) / weaponData.areaOfEffect;

                    ApplyDamage(cols[i].gameObject, percent);
                }
            }

            //Handle pierce
            peirceCount--;
            if (peirceCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Attempts to take health from object
    /// </summary>
    /// <param name="gameObject">Object to damage</param>
    /// <param name="percent">Damage multiplier</param>
    /// <returns>Returns true if object had health</returns>
    public bool ApplyDamage(GameObject gameObject, float percent)
    {
        //Add force
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody)
            rigidbody.AddForce(rigidbody.velocity.normalized * percent * weaponData.force, ForceMode2D.Impulse);

        //Hurt
        HealthComponent health = gameObject.GetComponent<HealthComponent>();
        if (health)
        {
            health.ChangeHealthBy(-weaponData.damage * percent);

            if (weaponData.damageOverTime > 0.0f)
            {
                health.AddDamageOverTime(weaponData.damageOverTime, weaponData.damageOverTimeDurration, gameObject);
            }

            DamageEventData damageEvent = new DamageEventData()
            {
                attacker = this.gameObject,
                victim = gameObject,
                victimsResultingHealth = health.Health
            };
            objectThatShotMe.SendMessage("OnMeHurtingSomeoneElse", damageEvent);

            return true;
        }
        return false;
    }
}