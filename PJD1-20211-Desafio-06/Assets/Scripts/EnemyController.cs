using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Rigidbody2DBase, IPoolableObject
{
    public int Hp { get; protected set; }


    private void Start()
    {
        Hp = Random.Range(100, 201);
    }

    public bool ApplyDamage(int damage)
    {
        Hp -= damage;
        /*
        if(Hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }*/
        if(Hp <= 0)
            Destroy(gameObject);
        return Hp <= 0;
    }

    public void Recycle()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        Start();
    }
}
