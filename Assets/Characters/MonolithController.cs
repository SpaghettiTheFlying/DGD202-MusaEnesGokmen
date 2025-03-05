using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonolithController : MonoBehaviour
{
    #region MONOLITH STATS
    public float attackRange = 15f;
    public float attackCooldown = 2f;
    public int damage = 10;
    #endregion

    private float CDtimer = 0f;

    #region ATTACK SETTINGS
    public GameObject projectilePrefab;
    public Transform firePoint;
    #endregion

    void Update()
    {
        CDtimer -= Time.deltaTime;

        GameObject target = FindNearestEnemy();
        if (target != null && CDtimer <= 0)
        {
            Attack(target);
            CDtimer = attackCooldown;
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    public void Attack(GameObject enemy)
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectileScript = projectile.GetComponent<Projectile>();

            if (projectileScript != null)
            {
                projectileScript.Initialize(enemy.transform, damage);
            }
        }
    }

    public void UpgradeMonolith(int extraDamage, float extraRange, float CDreduction)
    {
        damage += extraDamage;
        attackRange += extraRange;
        attackCooldown = Mathf.Max(attackCooldown - CDreduction);
    }

}