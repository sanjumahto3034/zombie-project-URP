using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    public float offsetStandFromPlayer = 5f;
    public float visionRange = 100;
    private NavMeshAgent agent;
    public float petrolArea = 30;
    private Vector3 startPoint;

    private bool isPetroling = true;
    private bool isChasing = false;
    private bool isOnAttackPosition = false;
    private bool isZombieAlive = true;

    private Vector3 petrolPoint;



    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    private EnemyAI enemyAI;

    private GameObject player;

    
    void Awake()
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
        /*
        * Set the enemy to petroling mode 
        */
        isPetroling = _petrol;
        agent.isStopped = true;
    }

    public void setZombieSpeed(float moveSpeed)
    {
        /*
        * Set zombie Nave Mesh Agent speed take
        * @param float speed
        */


        if(moveSpeed != agent.speed)agent.speed = moveSpeed;
    }


    void Update()
    {
        if(!isZombieAlive)return;


        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, visionRange))
        {
               
            if (hit.transform.gameObject.tag == constant.PLAYER)
            {
                enemyAI.SetChasePlayer(true);
                isPetroling = false;
                isChasing = true;
                isOnAttackPosition = false;
                setZombieIdel(false);
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
    public void setZombieIdel(bool zombieStatus){
        /*
        * Put the enemy in ideal stage
        */
        if(zombieStatus){
            agent.isStopped = zombieStatus;
            agent.speed = 0;
            isOnAttackPosition = false;
            isPetroling = false;
            isChasing = false;
        }
        else{
            agent.isStopped = zombieStatus;
        }
       
    }

    private void chasingPlayer(){
        /*
        * Put the enemy in casing state to player
        */
        agent.SetDestination(player.transform.position);
        if(agent.remainingDistance>=offsetStandFromPlayer){
            agent.isStopped = false;
            setZombieSpeed(enemyAI.getRunSpeed());
        }
        else{
            attackOnPlayer();
        }

    }
    private void petroling(){
        /*
        * Set the enemy to petroling stage at the fixed radius
        */
        agent.isStopped = false;
        Vector3 point;
       if (GetRandomPoints(out point) &&  (agent.remainingDistance <= agent.stoppingDistance)) agent.SetDestination(point);
        setZombieSpeed(enemyAI.getWalkSpeed());

    }

    private void attackOnPlayer(){
        /*
        * enemy will stop and start attack to player
        */
        agent.isStopped = true;
        enemyAI.attackOnPlayer();
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
        /*
        * Stop the enemy at current place and the enemy is dead now
        */
        agent.isStopped = true;
        agent.speed = 0;

        isOnAttackPosition = false;
        isChasing = false;
        isPetroling = false;
    }

    private const string TAG = "[ EnemyRandomMovement ] ";

}