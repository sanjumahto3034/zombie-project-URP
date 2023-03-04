using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    
    public Transform gunPoint;

    private string IS_WALKING = "isWalking";
    private string IS_RUNNING = "isRunning";
    private string IS_IDEL = "isIdeal";
    

    private string animationType = "Pistol";
    private bool isPlayerHaveGun = false;
    void Start(){
        anim = GetComponent<Animator>();
        SetPlayerHandHasGun(false);
    }
    void FixedUpdate(){
        isPlayerHaveGun = gunPoint.childCount>0?true:false;
    }

    public void SetWalkAnimation(bool state){ // Depreciated
    /*  
        * Set Walk Animation
        * @param bool show player is Waking or not
    */
        anim.SetBool(IS_IDEL,!state);
        anim.SetBool(IS_RUNNING,!state);
        anim.SetBool(IS_WALKING,state);
    }
    public void SetRunAnimation(bool state){ // Depreciated
    /*  
        * Set Running Animation
        * @param bool show player is running or not
    */
        anim.SetBool(IS_IDEL,!state);
        anim.SetBool(IS_RUNNING,state);
        anim.SetBool(IS_WALKING,!state);
    }
    public void SetIdealAnimation(bool state){ // Depreciated
    /*  
        * Set Ideal Animation
        * @param bool show player is ideal or not
    */
        anim.SetBool(IS_IDEL,state);
        anim.SetBool(IS_RUNNING,!state);
        anim.SetBool(IS_WALKING,!state);
    }
    public void SetPlayerHandHasGun(bool haveGun){ // Depreciated
    /*  
        * Set Player Animation for Gun/Hand
        * @param bool having gun or not
    */
        if(haveGun){
            anim.SetBool(animationType,true);
            anim.SetBool("noGun",false);
        }
        else{
            anim.SetBool(animationType,false);
            anim.SetBool("noGun",true);
        }
    }
}


