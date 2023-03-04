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

    private GameObject primaryGun = null;
    private GameObject SecondaryGun = null;
    public GameObject primaryGunBG;
    public GameObject SecondaryGunBG;


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
        /*
            * This function is sending ray to the object by distance 5
            * This function is send ray
        */
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            if (hit.transform.gameObject.tag == "gun" && pickGunCall)
            {
                AddGunInPlayerHand(hit.transform.gameObject, gunHoldingPoint);
                pickGunCall = false;
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
        if (context.started) RemoveGunFromPlayerHand(currentGun);
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
            * Shoot Gun
            * Take input from Left-Key Mouse
        */
        if (context.started)
        {
            if (currentGun == 1 && primaryGun)
            {
                primaryGun.GetComponent<Shooting>().FireGun();
            }
            else if (currentGun == 2 && SecondaryGun)
            {
                SecondaryGun.GetComponent<Shooting>().FireGun();
            }
        }
    }

    public void EquiptPrimaryGun(InputAction.CallbackContext context)
    {
        /*
            * Get Input From Unity new Input System
            * @param callback context show phases of input
            * Set primary gun on Player hand
            * Take input from 1-Key
        */
        if (context.started && primaryGun)
        {
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
        if (context.started && SecondaryGun)
        {
            SetPrimaryGun(2);
        }
    }
    private void SetPrimaryGun(int _gunIndex)
    {
        /*
            * This function is used set gun to primary
            * @param gun index in 0 & 1
        */
        if (_gunIndex == 1 && primaryGun)
        {
            if (primaryGun) primaryGun.transform.localScale = new Vector3(1f, 1f, 1f);
            if (SecondaryGun) SecondaryGun.transform.localScale = Vector3.zero;
            primaryGunBG.SetActive(true);
            SecondaryGunBG.SetActive(false);
            currentGun = _gunIndex;
        }
        else if (_gunIndex == 2 && SecondaryGun)
        {
            if (primaryGun) primaryGun.transform.localScale = Vector3.zero;
            if (SecondaryGun) SecondaryGun.transform.localScale = new Vector3(1f, 1f, 1f);
            primaryGunBG.SetActive(false);
            SecondaryGunBG.SetActive(true);
            currentGun = _gunIndex;
        }
    }

    private void AddGunInPlayerHand(GameObject _gun, Transform _holdingPoints)
    {
        /*
        * This funciton is used add Gun in player hand
        */
        if (_holdingPoints.childCount == 0)
        {
            primaryGun = _gun;
            primaryGunText.text = _gun.name;
            PutGunOnHand(_gun, _holdingPoints);
            SetPrimaryGun(currentGun);
            currentGun = 1;
            Debug.Log("[Gun Added in Inventory 1]");
        }
        else if (_holdingPoints.childCount == 1)
        {
            SecondaryGun = _gun;
            secondaryGunText.text = _gun.name;
            PutGunOnHand(_gun, _holdingPoints);
            SetPrimaryGun(currentGun);
            Debug.Log("[Gun Added in Inventory 2]");
        }
    }
    private void PutGunOnHand(GameObject _gun, Transform _holdingPoints)
    {
        _gun.GetComponent<Rigidbody>().isKinematic = true;
        _gun.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _gun.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        _gun.GetComponent<CommonGunProperty>().PickGunByPlayer();
        _gun.transform.parent = _holdingPoints.transform;
        _gun.transform.position = Vector3.zero;
        _gun.transform.rotation = Quaternion.Euler(Vector3.zero);
        animationController.SetPlayerHandHasGun(true);
        _gun.transform.localPosition = Vector3.zero;
        _gun.transform.localEulerAngles = Vector3.zero;
        _holdingPoints.transform.SetParent(_gun.transform);
    }

    private void RemoveGunFromPlayerHand(int _currentGunIndex)
    {
        /*
        * This funciton is used to remove gun from player hand 
        */


        Debug.Log("[Remove Gun From Player Gun Index] " + _currentGunIndex);
        if (_currentGunIndex == 1 && primaryGun)
        {
            primaryGun.GetComponent<CommonGunProperty>().DropGunByPlayer();
            primaryGunText.text = "null";
            primaryGunBG.SetActive(false);
            primaryGun = null;
        }
        else if (_currentGunIndex == 2 && SecondaryGun)
        {
            SecondaryGun.GetComponent<CommonGunProperty>().DropGunByPlayer();
            secondaryGunText.text = "null";
            SecondaryGunBG.SetActive(false);
            SecondaryGun = null;
        }

    }



}
