using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent missing on" + gameObject.name);
            return;
        }
    }

    void Update()
    {
        if(player != null && agent != null)
        {
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) < 0.1f)
            {
                animator.SetBool("isColliding", true);
            }
            else
            {
                animator.SetBool("isColliding", false);
            }
        }  
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + health);
        if (health <= 0)
        {
            Die();
        }

        void Die()
        {
            Debug.Log(gameObject.name + "has died!");
            Destroy(gameObject);
        }
    }
}