using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class WeaponController : MonoBehaviour
{
    public TMP_Text primaryGunText;
    public TMP_Text secondaryGunText;


    private bool pickGunCall = false;
    public Transform gunHoldingPoint;

    private Vector3 gunPointVector;
    private Vector3 gunPointVectorRot;
    public PlayerAnimationController animationController;
    private Vector3 ResetPoints;

    private GameObject primaryGun;
    private GameObject SecondaryGun;
    
    
    /*
        * 1 Set primary gun as current gun
        * 2 Set Seconday gun as current gun
    */
    private int currentGun; 



    void Start()
    {
        Debug.Log("[Weapon Controller INIT]");
        ResetPoints = new Vector3(0, 0, 0);

    }
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            if (hit.transform.gameObject.tag == "gun" && pickGunCall)
            {
                AddGunInPlayerHand(hit.transform.gameObject, gunHoldingPoint);
            }
        }


    }

    /*
        * Get Input From Unity new Input System
        * @param callback context show phases of input
    */
    public void PickGun(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
            * Pick Item from ground
            * Take input from F-Key
        */
        if (context.started) pickGunCall = true;
    }
    public void DropGun(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
            * Pick Drop Item on ground
            * Take input from E-Key
        */
        if (context.started) RemoveGunFromPlayerHand();
    }
       public void EquiptPrimaryGun(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
            * Set primary gun on Player hand
            * Take input from 1-Key
        */
        if (context.started && primaryGun) {
            SetPrimaryGun(1);

        }
    }
       public void EquiptSecondaryGun(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
            * Set Secondary gun on Player hand
            * Take input from 2-Key
        */
        if (context.started && SecondaryGun){
          SetPrimaryGun(2);
        } 
    }
    private void SetPrimaryGun(int _gunIndex){
        if(_gunIndex == 1 && primaryGun){
            primaryGun.transform.localScale = Vector3.zero;
            SecondaryGun.transform.localScale = new Vector3(1f,1f,1f);
            currentGun = _gunIndex;
        }
        else if(_gunIndex == 2 && SecondaryGun){
            primaryGun.transform.localScale = new Vector3(1f,1f,1f);
            SecondaryGun.transform.localScale = Vector3.zero;
            currentGun = _gunIndex;
        }
    }

    private void AddGunInPlayerHand(GameObject _gun, Transform _holdingPoints)
    {
        /*
        * This funciton is used add Gun in player hand
        */
        Debug.Log(_holdingPoints.childCount + " < Child Count");
        if (_holdingPoints.childCount == 0)
        {
            PutGunOnHand(_gun,_holdingPoints);
            primaryGun = _gun;
            primaryGunText.text = _gun.name;
            SetPrimaryGun(currentGun);
            currentGun = 1;
        }
        else if(_holdingPoints.childCount == 1){
            PutGunOnHand(_gun,_holdingPoints);
            SecondaryGun = _gun;
            secondaryGunText.text = _gun.name;
            SetPrimaryGun(currentGun);
        }
    }
    private void PutGunOnHand(GameObject _gun, Transform _holdingPoints){
            _gun.GetComponent<Rigidbody>().isKinematic = true;
            _gun.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _gun.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            _gun.GetComponent<CommonGunProperty>().PickGunByPlayer();
            pickGunCall = false;
            _gun.transform.parent = _holdingPoints.transform;
            _gun.transform.position = Vector3.zero;
            _gun.transform.rotation = Quaternion.Euler(Vector3.zero);
            animationController.SetPlayerHandHasGun(true);
            _gun.transform.localPosition = Vector3.zero;
            _gun.transform.localEulerAngles = Vector3.zero;
            _holdingPoints.transform.SetParent(_gun.transform);
    }

    private void RemoveGunFromPlayerHand()
    {
        /*
        * This funciton is used to remove gun from player hand 
        */
        if (primaryGun)
        {
            primaryGun.GetComponent<CommonGunProperty>().DropGunByPlayer();
            primaryGun.transform.parent = null;
            primaryGun = null;
        }

    }

    

}
