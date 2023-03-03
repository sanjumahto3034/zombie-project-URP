using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private float petrolArea = 30;
    private Vector3 startPoint;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPoint = transform.position;
    }


    public void setZombieSpeed(float moveSpeed){
        /*
        * Set zombie Nave Mesh Agent speed take
        * @param float speed
        */
        agent.speed = moveSpeed;
    }


    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (GetRandomPoints(out point)) agent.SetDestination(point);
        }

    }
    private bool GetRandomPoints(out Vector3 result)
    {
        /*
            * This function is used to get the random points in NavMeshPoints
            * @param Vector3 out 
            * @return bool
        */

        Vector3 randomPoint = startPoint + Random.insideUnitSphere * petrolArea;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }


}