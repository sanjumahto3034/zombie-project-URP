using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public enum GunType { SINGLE, BURST, AUTO };
    public GunType gunType;

    public int GunDamage;
    public GameObject bullet;
    public float ReloadingTime = 2f;

    public Transform mullzlePoint;
    public float bulletVelocity = 100000;



    private float perBulletDelay = 1f;
    private bool isReloading = false;
    private bool isFiring = false;



    void Awake()
    {

    }



    public void FireGun(int _invoke)
    {
        if (gunType == GunType.AUTO)
        {
            Debug.Log(TAG_SHOOTING + " " + "gunType : auto");
            isFiring = (_invoke == constant.GUN_FIRE_AUTO) ? true : false;
            Debug.Log(TAG_SHOOTING + " " + "gunType : " + isFiring);

            if (isFiring) StartCoroutine(Scheduler());
            else StopCoroutine(Scheduler());
        }
        else if(gunType == GunType.SINGLE){

            if (_invoke == constant.GUN_FIRE_SINGLE)
            {
            bulletSpawn();
            }
            isFiring = false;
        }

    }


    IEnumerator Scheduler()
    {
        /*
        * @param int which show delay time
        */
        yield return new WaitForSeconds(perBulletDelay);
        if (isFiring) bulletSpawn();

    }
    void bulletSpawn()
    {
        GameObject spawnBullet = Instantiate(bullet, mullzlePoint, false);
        spawnBullet.transform.position = mullzlePoint.transform.position;
        spawnBullet.transform.parent = null;
        spawnBullet.AddComponent<Rigidbody>();
        spawnBullet.GetComponent<Rigidbody>().velocity = mullzlePoint.TransformDirection(Vector3.forward * bulletVelocity);
        spawnBullet.GetComponent<BulletBehaviour>().SetWeaponDamageRate(GunDamage);
        if (isFiring) StartCoroutine(Scheduler());

    }

    private const string TAG_SHOOTING = "[Bullet Behaviour]";
}
