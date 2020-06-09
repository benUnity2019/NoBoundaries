using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float health;

    public float Health
    {
        get => health;
        set
        {
            float oldHealth = health;

            health = value;

            if (health < oldHealth)
            {
                BroadcastMessage("OnHurt", health);
            }
            else if (health > oldHealth)
            {
                BroadcastMessage("OnHeal", health);
            }

            if (health <= 0.0f)
            {
                BroadcastMessage("OnDead", health);
            }
        }
    }
}
