using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum ZombieInitType{CrawlBite,IdelStand,WalkRandom};

    public ZombieInitType zombieInitType;
    public float petrolArea;
    public float walkSpeed = 2;
    public float runSpeed = 4;
    private float minX;
    private float minZ;
    private float maxX;
    private float maxZ;

    public float EnemyMoveSpeed;


    private Vector3 petrolPoints;

    private Animator anim;

    void Start(){
        minX = transform.position.x - petrolArea;
        maxX = transform.position.x + petrolArea;
        minZ = transform.position.y - petrolArea;
        maxZ = transform.position.y + petrolArea;
        // getRandomCoordinatestoMove();
        // StartCoroutine(Scheduler(4));
        anim = GetComponent<Animator>();

        setZombieType();

    }
    private void setZombieType(){
        /*
            * This function will set the zombie init stage in Game 
            * Crawl Bite zombie
            * Idel stage zombie
        */
        switch(zombieInitType){
            case ZombieInitType.CrawlBite:
                anim.SetTrigger(constant.CRAWL_BITE_IDEL);
            break;

            case ZombieInitType.IdelStand:
                anim.SetTrigger(constant.IDEL_STAND);
            break;
                case ZombieInitType.WalkRandom:
                anim.SetTrigger(constant.WALK_RANDOM_ON_MAP);
                gameObject.AddComponent<EnemyRandomMovement>();
                GetComponent<EnemyRandomMovement>().setZombieSpeed(walkSpeed);
            break;

            default :
                Debug.LogError("ZombieInitType is out of range");
            break;
        }
    }


    void Update(){
        // transform.position+=petrolPoints * EnemyMoveSpeed * Time.deltaTime;
        // transform.position = Vector3.Lerp(transform.position,petrolPoints,EnemyMoveSpeed * Time.deltaTime);
        
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


    private void getRandomCoordinatestoMove(){
        /*
            * This function is return petrol area
            * set petrol area in petrolPoints Vector3 variable
        */
        float _tempX = Random.Range(minX,maxX);
        float _tempZ = Random.Range(minZ,maxZ);
        petrolPoints =  new Vector3(_tempX,transform.position.y,_tempZ);
    }
}