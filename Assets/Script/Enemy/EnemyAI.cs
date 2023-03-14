using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    public enum ZombieInitType { CrawlBite, IdelStand, WalkRandom };
    public GameObject player;
    public ZombieInitType zombieInitType;
    public float petrolArea;
    public float walkSpeed = 2;
    public float runSpeed = 4;
    private float minX;
    private float minZ;
    private float maxX;
    private float maxZ;
    private Vector3 petrolPoints;
    private Animator anim;
    private EnemyRandomMovement enemyRandomMovement;

    void Start()
    {
        minX = transform.position.x - petrolArea;
        maxX = transform.position.x + petrolArea;
        minZ = transform.position.y - petrolArea;
        maxZ = transform.position.y + petrolArea;
        // getRandomCoordinatestoMove();
        // StartCoroutine(Scheduler(4));
        anim = GetComponent<Animator>();
        enemyRandomMovement = GetComponent<EnemyRandomMovement>();
        setZombieType();

    }
    private void setZombieType()
    {
        /*
            * This function will set the zombie init stage in Game 
            * Crawl Bite zombie
            * Idel stage zombie
        */
        switch (zombieInitType)
        {
            case ZombieInitType.CrawlBite:
                anim.SetTrigger(constant.CRAWL_BITE_IDEL);
                enemyRandomMovement.setZombieIdel(true);
                break;

            case ZombieInitType.IdelStand:
                anim.SetTrigger(constant.IDEL_STAND);
                enemyRandomMovement.setZombieIdel(true);
                break;
            case ZombieInitType.WalkRandom:
                anim.SetTrigger(constant.WALK_RANDOM_ON_MAP);
                enemyRandomMovement.setZombieIdel(false);
                // gameObject.AddComponent<EnemyRandomMovement>();
                // GetComponent<EnemyRandomMovement>().setZombieSpeed(walkSpeed);
                break;

            default:
                Debug.LogError("ZombieInitType is out of range");
                break;
        }
    }


    void Update()
    {


    }

    IEnumerator Scheduler(float _time)
    {
        /*
        * @param int which show delay time
        */
        yield return new WaitForSeconds(_time);
        StartCoroutine(Scheduler(4));
        getRandomCoordinatestoMove();
    }



    private void getRandomCoordinatestoMove()
    {
        /*
            * This function is return petrol area
            * set petrol area in petrolPoints Vector3 variable
        */
        float _tempX = Random.Range(minX, maxX);
        float _tempZ = Random.Range(minZ, maxZ);
        petrolPoints = new Vector3(_tempX, transform.position.y, _tempZ);
    }
    public void SetChasePlayer(bool _chase)
    {
        /*
        * Set enemy to chasing state
        */
        anim.SetBool(constant.CHASE_PLAYER, _chase);
        anim.SetBool(constant.IS_ATTACKING,false);
    }

    

    public float getWalkSpeed()
    {
        /*
        * @return the current walk speed of enemy
        */
        return walkSpeed;
    }
    public float getRunSpeed()
    {
        /*
        * @return the current running speed of enemy
        */
        return runSpeed;
    }

    public void EnemyDead()
    {
        /*
        * enemy will dead now 
        */
        GetComponent<EnemyRandomMovement>().EnemyDead();
        anim.SetTrigger(constant.DEAD);
        StartCoroutine(DestroyEnemyAfterDelay(4));
    }
    IEnumerator DestroyEnemyAfterDelay(float _time)
    {
        /*
        * @param int which show delay time
        * This function will destroy gameObject after "@param _time" delay
        */
        enemyRandomMovement.setZombieIdel(true);
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
    public void attackOnPlayer(){
        /*
        * Player will start attack to the enemy
        */
        anim.SetBool(constant.IS_ATTACKING,true);
        anim.SetBool(constant.CHASE_PLAYER,false);
    }

}
