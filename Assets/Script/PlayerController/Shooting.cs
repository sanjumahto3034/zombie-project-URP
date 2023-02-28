using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public enum GunType {SINGLE, BURST, AUTO};
    public GunType gunType;

    public int GunDamage;
    public GameObject bullet;

    public Transform mullzlePoint;
    public float bulletVelocity = 100000;



    public void FireGun(){
                GameObject spawnBullet = Instantiate(bullet,mullzlePoint,false);
                spawnBullet.transform.position = mullzlePoint.transform.position;
                spawnBullet.transform.parent = null;
                spawnBullet.AddComponent<Rigidbody>();
                spawnBullet.GetComponent<Rigidbody>().velocity = mullzlePoint.TransformDirection(Vector3.forward * bulletVelocity);
                spawnBullet.GetComponent<BulletBehaviour>().SetWeaponDamageRate(GunDamage);
    }
}
