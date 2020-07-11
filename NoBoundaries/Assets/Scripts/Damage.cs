using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct DamageOverTime
{
    public float damagePerTick;
    public float timeLeft;
    public GameObject attacker;
}

public class Damage : MonoBehaviour
{
    // Dictionary.key = interaction between damage component and a health component
    // Dictionary.value = time interaction occured
    // This is used to ensure that damage does not occur faster that cooldown between interactions, but multiple cooldowns can be applied to the same damage component
    // For example a fire will be able to hurt 2 entities simultaniosuly without having to cooldown in between entities. But it will still cool down inbetween iterations.
    static Dictionary<Tuple<HealthComponent, Damage>, float> lastDamageTimes = new Dictionary<Tuple<HealthComponent, Damage>, float>();

    //value 1 = object, value 2 = time of last damage to object
    List<Tuple<HealthComponent, float>> currentObjsInMe = new List<Tuple<HealthComponent, float>>();

    [SerializeField] float damage;
    [SerializeField] bool canDoContinuousDamage;
    [SerializeField] float damageCooldown;

    public float DamageValue { get => damage; set => damage = value; }
    public bool CanDoContinuousDamage { get => canDoContinuousDamage; set => canDoContinuousDamage = value; }
    public float DamageCooldown { get => damageCooldown; set => damageCooldown = value; }

    bool AddObjectToObjList(HealthComponent obj)
    {
        // If currentObjsInMe doesn't already contain object, add it to the list, with current time. 
        if (currentObjsInMe.Find(tuple => tuple.Item1 == obj) == null)
        {
            currentObjsInMe.Add(new Tuple<HealthComponent, float>(obj, Time.time));
            return true;
        }
        return false;
    }

    bool RemoveObjectFromObjList(HealthComponent obj)
    {
        // If currentObjsInMe contains object, remove it
        Tuple<HealthComponent, float> healthObj = currentObjsInMe.Find(tuple => tuple.Item1 == obj);
        if (healthObj != null)
        {
            currentObjsInMe.Remove(healthObj);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (CanDoContinuousDamage)
        {
            float currentTime = Time.time;
            for (int i = 0; i < currentObjsInMe.Count; i++)
            {
                if (currentTime - currentObjsInMe[i].Item2 > DamageCooldown)
                {
                    currentObjsInMe[i].Item1.ChangeHealthBy(-damage);
                    currentObjsInMe[i] = new Tuple<HealthComponent, float>(currentObjsInMe[i].Item1, Time.time);
                }
            }
        }
    }

    //Object Enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health)
            HandleCollision(health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthComponent health = collision.transform.GetComponent<HealthComponent>();
        if (health)
            HandleCollision(health);
    }

    //private void OnDisable()
    //{
    //    lastDamageTimes.Clear();
    //    currentObjsInMe.Clear();
    //}

    void HandleCollision(HealthComponent health)
    {
        if (AddObjectToObjList(health))
        {
            Tuple<HealthComponent, Damage> interaction = new Tuple<HealthComponent, Damage>(health, this);

            // If this damage component has hurt this health component in the past
            if (lastDamageTimes.ContainsKey(interaction))
            {
                // And if time past since interaction is longer than cooldown
                if (Time.time - lastDamageTimes[interaction] > damageCooldown)
                {
                    //Damage health object and add interaction time
                    health.ChangeHealthBy(-damage);
                    lastDamageTimes[interaction] = Time.time;
                }
            }
            else // else just damage the object and add interaction time
            {
                health.ChangeHealthBy(-damage);
                lastDamageTimes[interaction] = Time.time;
            }
        }
    }

    //Object exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health)
            RemoveObjectFromObjList(health);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        HealthComponent health = collision.transform.GetComponent<HealthComponent>();
        if (health)
            RemoveObjectFromObjList(health);
    }
}
