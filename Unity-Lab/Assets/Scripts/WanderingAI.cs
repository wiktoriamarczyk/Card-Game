using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class responsible for the wandering AI behavior.
/// </summary>
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

    /// <summary>
    /// Method that returns a random position on the NavMesh.
    /// </summary>
    /// <param name="origin"> The origin position </param>
    /// <param name="dist"> The distance from the origin </param>
    /// <param name="layermask"> The layer mask </param>
    /// <returns></returns>
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
