using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public enum GunType { SINGLE, BURST, AUTO };
    public GunType gunType;


    public GameObject bulletHole;
    public int GunDamage;
    public GameObject bullet;
    public float recoilAmountX;
    public float recoilAmountY;

    public GameObject muzzlePoint;
    public float bulletVelocity = 100000;

    public Transform _camera;
    public float range;
    public float perBulletDelay = 0.3f;



    public int TotalMagazineCapacity;
    public int maxBulletPlayerHas;
    public float reloadTime;
    private int remainingMagazine;



    private bool isReloading = false;
    private bool isFiring = false;
    private Transform muzzlePointTransform;
    private AudioSource fireSound;

    private PlayerMovementManager player;

    public WeaponController weaponController;
    private float counter;

    void Start()
    {
        muzzlePointTransform = muzzlePoint.GetComponent<Transform>();
        fireSound = muzzlePoint.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag(constant.PLAYER).GetComponent<PlayerMovementManager>();
        counter = perBulletDelay;
        remainingMagazine = TotalMagazineCapacity;

    }

    void FixedUpdate()
    {

        if (isFiring)
        {
            counter -= 0.015f;
            if (counter <= 0)
            {
                counter = perBulletDelay;
                bulletSpawn();
            }
        }
    }


    public void FireGun(int _invoke)
    {
        /*
        * This function will invoke from weapon controller class 
        * @param int _invoke
        * 1 for shoot
        * 0 for not shoot
        */

        if (gunType == GunType.AUTO && !isReloading)
        {
            isFiring = (_invoke == constant.GUN_FIRE_AUTO) ? true : false;



            if (!isFiring) StartCoroutine(bulletDelayFunction());


        }
        else if (gunType == GunType.SINGLE && !isReloading)
        {

            if (_invoke == constant.GUN_FIRE_SINGLE)
            {
                bulletSpawn();
                isFiring = false;
            }
        }

    }


    IEnumerator bulletDelayFunction()
    {
        /*
        * @param int which show delay time
        */
        yield return new WaitForSeconds(perBulletDelay);
        player.resetRecoil();

    }
    IEnumerator waitWhileReload()
    {
        /*
        * @param int which show delay time
        * after delay reload completed
        */
        Debug.Log(TAG_SHOOTING + "Player Reloading");
        isReloading = true;
        isFiring = false;
        yield return new WaitForSeconds(reloadTime);
        if (TotalMagazineCapacity <= maxBulletPlayerHas)
        {
            remainingMagazine = TotalMagazineCapacity;
            maxBulletPlayerHas -= TotalMagazineCapacity;
            isReloading = false;
            Debug.Log(TAG_SHOOTING + "Reload Successfull " + maxBulletPlayerHas);
        }
        else if (TotalMagazineCapacity >= maxBulletPlayerHas)
        {
            if (maxBulletPlayerHas > 0)
            {
                remainingMagazine = maxBulletPlayerHas;
                maxBulletPlayerHas = 0;
                isReloading = false;
                Debug.Log(TAG_SHOOTING + "Reload Successfull " + maxBulletPlayerHas);
            }
        }
        weaponController.setBulletRemainingText(getGunBulletTextInString());


    }
    void bulletSpawn()
    {
        /*
        * This function will spawn bullet from gun from gun muzzle
        * Spawn the raycast from camera to the end
        * IF hit enemy then give damage as per weapon damage
        */
        if (remainingMagazine <= 0 && !isReloading)
        {

            if (maxBulletPlayerHas > 0) StartCoroutine(waitWhileReload());
            else Debug.Log(TAG_SHOOTING + "Player Is Out Of Bullets");

            return;
        }
        if (remainingMagazine > 0 && !isReloading)
        {

            fireSound.Play();
            remainingMagazine--;
            float recoilX = Random.Range(-recoilAmountX, recoilAmountX);
            float recoilY = Random.Range(0, recoilAmountY);
            Vector3 recoilPattern = _camera.transform.forward + new Vector3(recoilX, 0, 0);
            player.addRecoilOnPlayerCamera(new Vector2(0, recoilY));

            if (Physics.Raycast(_camera.transform.position, recoilPattern, out RaycastHit hit, range))
            {
                if (hit.transform.CompareTag(constant.TAG_ENEMY))
                {
                    if (hit.transform.TryGetComponent(out EnemyPower enemy))
                    {
                        enemy.bulletHitToEnemy(GunDamage);
                    }
                }
                else if (hit.transform.CompareTag(constant.WALL))
                {
                    Instantiate(bulletHole, hit.point, Quaternion.identity);
                }
            }

            GameObject spawnBullet = Instantiate(bullet, muzzlePointTransform.position, Quaternion.identity);
            spawnBullet.transform.SetParent(null);
            spawnBullet.AddComponent<Rigidbody>().velocity = muzzlePointTransform.forward * bulletVelocity;
        }
        weaponController.setBulletRemainingText(getGunBulletTextInString());
    }

    public string getGunBulletTextInString()
    {
        return remainingMagazine + " | " + maxBulletPlayerHas;
    }



    private const string TAG_SHOOTING = "[SHOOTING] ";
}
