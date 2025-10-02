using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    public Transform target;
    PlayerController player;
    [Header("Logic")]
    private NavMeshAgent agent;

    public bool isFollowing = false;

    [Header("Enemy Stats")]
    public int health = 5;
    public int maxHealth = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        else
        {
            if (isFollowing)
                agent.destination = player.transform.position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "proj")
        {
            print("I'm Hit");
            health--;
            Destroy(collision.gameObject);
        }
    }
}
