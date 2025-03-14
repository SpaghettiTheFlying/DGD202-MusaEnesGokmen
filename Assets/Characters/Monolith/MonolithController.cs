using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonolithController : MonoBehaviour
{
    #region MONOLITH STATS
    public float attackRange = 15f;
    public float attackRate = 1f;
    public int damage = 10;
    #endregion

    #region ATTACK SETTINGS
    public GameObject projectilePrefab;
    public Transform firePoint;
    #endregion

    private float attackCD = 0f;

    void Update()
    {
        attackCD -= Time.deltaTime;

        GameObject target = FindNearestEnemy();
        if (target != null && attackCD <= 0)
        {
            Attack(target);
            attackCD = attackRate;
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
        attackCD = Mathf.Max(attackCD - CDreduction);
    }

}