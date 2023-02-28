using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float petrolArea;

    private float minX;
    private float minZ;
    private float maxX;
    private float maxZ;

    public float EnemyMoveSpeed;


    private Vector3 petrolPoints;

    void Start(){
        minX = transform.position.x - petrolArea;
        maxX = transform.position.x + petrolArea;
        minZ = transform.position.y - petrolArea;
        maxZ = transform.position.y + petrolArea;
        getRandomCoordinatestoMove();
        StartCoroutine(Scheduler(4));

    }



    void Update(){
        // transform.position+=petrolPoints * EnemyMoveSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position,petrolPoints,EnemyMoveSpeed * Time.deltaTime);
        
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
        float _tempX = Random.Range(minX,maxX);
        float _tempZ = Random.Range(minZ,maxZ);
        petrolPoints =  new Vector3(_tempX,transform.position.y,_tempZ);
    }
}
