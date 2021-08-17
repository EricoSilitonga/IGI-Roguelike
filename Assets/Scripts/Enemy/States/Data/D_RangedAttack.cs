using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttack : ScriptableObject
{
    public GameObject bulletProjectile;
    public float bulletSpeed = 12f;
    public float bulletDamage = 5f;
    public float projectileTravelDistance = 10f;

    public LayerMask whatIsPlayer;
}
