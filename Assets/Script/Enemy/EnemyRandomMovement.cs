using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    public float visionRange = 100;
    private NavMeshAgent agent;
    public float petrolArea = 30;
    private Vector3 startPoint;

    private bool isPetroling = true;
    private bool isChasing = false;
    private bool isOnAttackPosition = false;

    private Vector3 petrolPoint;



    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    private int testInt = 0;
    private EnemyAI enemyAI;

    private GameObject player;
    void Start()
    {

        minX = transform.position.x - petrolArea;
        maxX = transform.position.x + petrolArea;
        minZ = transform.position.y - petrolArea;
        maxZ = transform.position.y + petrolArea;

        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAI>();
        startPoint = transform.position;
        player = GameObject.FindGameObjectWithTag(constant.PLAYER);
    }

    public void setPetroling(bool _petrol)
    {
        isPetroling = _petrol;
        agent.isStopped = true;
    }

    public void setZombieSpeed(float moveSpeed)
    {
        /*
        * Set zombie Nave Mesh Agent speed take
        * @param float speed
        */
                Debug.Log(TAG+"Remaining Distance of enemy "+moveSpeed+"  :  "+agent.speed);


        if(moveSpeed != agent.speed)agent.speed = moveSpeed;
    }


    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, visionRange))
        {
               
            if (hit.transform.gameObject.tag == constant.PLAYER)
            {
                Debug.DrawRay(transform.position,transform.forward,Color.green,1f);
                enemyAI.SetChasePlayer(true);
                isPetroling = false;
                isChasing = true;
                isOnAttackPosition = false;
            }

        }



        if(isOnAttackPosition && !isPetroling && !isChasing){
            attackOnPlayer();
        }
        else if(!isOnAttackPosition && isPetroling && !isChasing){
            petroling();
        }
        else if(!isOnAttackPosition && !isPetroling && isChasing){
            chasingPlayer();
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

    private void chasingPlayer(){
        agent.SetDestination(player.transform.position);
        if(agent.remainingDistance>5f){
            agent.isStopped = false;
            setZombieSpeed(enemyAI.getRunSpeed());
        }
        else{
            attackOnPlayer();
        }

    }
    private void petroling(){
        agent.isStopped = false;
        Vector3 point;
       if (GetRandomPoints(out point) &&  (agent.remainingDistance <= agent.stoppingDistance)) agent.SetDestination(point);
        setZombieSpeed(enemyAI.getWalkSpeed());

    }

    private void attackOnPlayer(){
        agent.isStopped = true;
        Debug.Log("Attacking");
    }

    private void getRandomCoordinatestoMove()
    {

        /*
            * This function is return petrol area
            * set petrol area in petrolPoints Vector3 variable
        */
        float _tempX = Random.Range(minX, maxX);
        float _tempZ = Random.Range(minZ, maxZ);
        petrolPoint = new Vector3(_tempX, transform.position.y, _tempZ);
    }
    public void EnemyDead(){
        agent.isStopped = true;
        agent.speed = 0;

        isOnAttackPosition = false;
        isChasing = false;
        isPetroling = false;
    }

    private const string TAG = "[ EnemyRandomMovement ] ";

}