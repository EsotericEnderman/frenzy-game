using UnityEngine;

using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    private NavMeshAgent agent;
    private Transform targetLocation;

    public Transform[] patrolPoints;

    private readonly float chaseDistancePerSecond = 5F;
    private readonly float patrolSpeedPerSecond = 3F;
    private readonly float chaseSpeedPerSecond = 5F;

    private int currentPatrolPointIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isChasing)
        {
            ChaseTarget();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        if (Vector3.Distance(transform.position, targetLocation.position) <= chaseDistancePerSecond)
        {
            isChasing = true;
            agent.speed = chaseSpeedPerSecond;
        }
    }

    private void ChaseTarget()
    {
        agent.SetDestination(targetLocation.position);

        if (Vector3.Distance(transform.position, targetLocation.position) > chaseDistancePerSecond)
        {
            isChasing = false;
            agent.speed = patrolSpeedPerSecond;
        }
    }
}
