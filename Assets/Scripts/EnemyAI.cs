using UnityEngine;

using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float chaseDistance = 5F;
    public float patrolSpeed = 3F;
    public float chaseSpeed = 5F;

    private NavMeshAgent agent;
    private Transform target;
    private int currentPatrolPointIndex;
    private bool isChasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentPatrolPointIndex = 0;
        isChasing = false;
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

    private void Patrol ()
    {
        agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

        if (agent.remainingDistance <= agent.stoppingDistance )
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        if (Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
        }
    }

    private void ChaseTarget()
    {
        agent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            isChasing = false;
            agent.speed = patrolSpeed;
        }
    }
}
