using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] float wanderRadius;

    NavMeshAgent agent;
    float wanderTimer;

    const float minWanderTime = 1f;
    const float maxWanderTime = 5f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(minWanderTime, maxWanderTime);
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);

        yield return new WaitForSeconds(wanderTimer);

        wanderTimer = Random.Range(minWanderTime, maxWanderTime);

        StartCoroutine(Wander());
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
