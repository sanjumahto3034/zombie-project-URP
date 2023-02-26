using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
   public PlayerMovementManager movementManager;


    
    private void OnTriggerEnter(Collider other){
        if(other.gameObject){
           movementManager.SetGrounded(true); 
        }
    }

    
   
}
