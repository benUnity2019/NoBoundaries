using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    //value 1 = object, value 2 = time of last damage to object
    List<Tuple<HealthComponent, float>> currentObjsInMe = new List<Tuple<HealthComponent, float>>();

    [SerializeField] float damage;
    [SerializeField] bool canDoContinuousDamage;
    [SerializeField] float damageRate;

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
        if (canDoContinuousDamage)
        {
            float currentTime = Time.time;
            for (int i = 0; i < currentObjsInMe.Count; i++)
            {
                if (currentTime - currentObjsInMe[i].Item2 > damageRate)
                {
                    currentObjsInMe[i].Item1.Health -= damage;
                }
            }
        }
    }

    //Object Enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health && AddObjectToObjList(health))
        {
            health.Health -= damage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthComponent health = collision.transform.GetComponent<HealthComponent>();
        if (health && AddObjectToObjList(health))
        {
            health.Health -= damage;
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
