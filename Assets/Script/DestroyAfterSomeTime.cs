using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSomeTime : MonoBehaviour
{
    public float destroyDelay = 1f;
    void Start()
    {
        StartCoroutine(DestroyDelay());
    }

       IEnumerator DestroyDelay()
    {
        /*
        * @param int which show delay time
        */
        yield return new WaitForSeconds(destroyDelay);
         Destroy(gameObject); 

    }
}
