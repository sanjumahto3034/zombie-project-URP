using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private int bulletDamage;
    void Start()
    {
        gameObject.transform.parent = null;
        StartCoroutine(DestroyBulletAfterDelay(3));
    }
     IEnumerator DestroyBulletAfterDelay(float _time)
    {
        /*
        * @param int which show delay time
        * This function will destroy gameObject after "@param _time" delay
        */
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }

    public void SetWeaponDamageRate(int _weaponDamage){
        /*
        * This function will take the damage of per bullet from weapon
        * @param int bullet damage 
        */
        bulletDamage = _weaponDamage;
    }

    
    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.tag == constant.TAG_ENEMY){
            GameObject _enemy = other.gameObject;
            _enemy.GetComponent<EnemyPower>().bulletHitToEnemy(bulletDamage);
            Destroy(gameObject);
       }
       else if(other.gameObject.tag == constant.WALL || other.gameObject.tag == constant.FLOOR){
            Destroy(gameObject);
       }
       
    }






    private void print(string _print){
    }



    
    private const string TAG_BULLET_BEHAVIOUR ="[Bullet Behaviour]";
   

}
