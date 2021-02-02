using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public bool CanMeleeAttack;
    public bool CanShoot;
    public float MeleeDamage;
    public float ShootDamage;

    public void GunHit(int damage)
    {
        Health = Health - damage;
    }
}
