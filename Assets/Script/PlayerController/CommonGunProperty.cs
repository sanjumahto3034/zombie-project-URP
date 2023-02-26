/*
    This is the common class of gun created by Sanju
    All the Weapon common funcition are implement here
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGunProperty : MonoBehaviour
{
   public void PickGunByPlayer(){
    /*
    * Function will destry the rigidbody
    */
        Destroy(GetComponent<Rigidbody>());
   }
   public void DropGunByPlayer(){
    /*
    * Function will Add the rigidbody on item
    */
        gameObject.AddComponent<Rigidbody>();
   }
}
