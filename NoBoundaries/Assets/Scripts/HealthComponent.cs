using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    float tickLength = 0.1f;//TODO: move to settings or time manager

    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] Fillbar fillbar;

    List<DamageOverTime> currentDamageOverTime = new List<DamageOverTime>();

    public float Health
    {
        get => health;
        private set
        {
            float oldHealth = health;

            health = value;

            if (health < oldHealth)
            {
                BroadcastMessage("OnHurt", health, SendMessageOptions.DontRequireReceiver);
            }
            else if (health > oldHealth)
            {
                BroadcastMessage("OnHeal", health, SendMessageOptions.DontRequireReceiver);
            }

            if (health <= 0.0f)
            {
                BroadcastMessage("OnDead", health, SendMessageOptions.DontRequireReceiver);
            }

            if (fillbar)
                fillbar.Value = health / maxHealth;
        }
    }

    private void Update()
    {
        ApplyDamageOverTime();
    }

    public void ApplyDamageOverTime()
    {
        for (int i = 0; i < currentDamageOverTime.Count; ++i)
        {
            DamageOverTime dot = currentDamageOverTime[i];

            dot.timeLeft -= Time.deltaTime;
            currentDamageOverTime[i] = dot;

            if (dot.timeLeft <= 0.0f)
            {
                currentDamageOverTime.RemoveAt(i);
                continue;
            }
            Health -= dot.damagePerTick;

            DamageEventData damageEvent = new DamageEventData()
            {
                attacker = currentDamageOverTime[i].attacker,
                victim = gameObject,
                victimsResultingHealth = health
            };
            currentDamageOverTime[i].attacker.SendMessage("OnMeHurtingSomeoneElse", damageEvent);
        }
    }

    public void ChangeHealthBy(float amount)
    {
        Health += amount;
    }

    public void AddDamageOverTime(float damagePerTick, float time, GameObject attackingObject)
    {
        currentDamageOverTime.Add(new DamageOverTime() { damagePerTick = damagePerTick, timeLeft = time, attacker = attackingObject });
    }
}
