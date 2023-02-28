/*
    This is the common class of gun created by Sanju
    All the Weapon common function are implement here
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGunProperty : MonoBehaviour
{
   public void PickGunByPlayer(){
    /*
    * Function will destroy the rigidbody
    */
        Destroy(GetComponent<Rigidbody>());
   }
   public void DropGunByPlayer(){
    /*
    * Function will Add the rigidbody on item
    */
        gameObject.AddComponent<Rigidbody>();
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.transform.parent = null;
   }
}
