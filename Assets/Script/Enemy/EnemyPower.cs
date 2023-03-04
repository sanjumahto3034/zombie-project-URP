using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPower : MonoBehaviour
{
    [SerializeField] private int _health;
    private int _maxHealth;

    void Start()
    {
        _maxHealth = _health;
    }


    public void bulletHitToEnemy(int _hitPoint)
    {
        _health -= (_health >= 0) ? _hitPoint : 0;
        if (_health <= 0) KillEnemy();
    }

    public void HealEnemy(int heal)
    {
        _health += heal;
        _health = (_health > _maxHealth) ? _maxHealth : _health;
    }
    public void KillEnemy()
    {
        EnemyAI enemyAI = GetComponent<EnemyAI>();
        enemyAI.EnemyDead();
        // Destroy(gameObject);
    }

    public int GetEnemyHealth(){
        return _health;
    }
}
