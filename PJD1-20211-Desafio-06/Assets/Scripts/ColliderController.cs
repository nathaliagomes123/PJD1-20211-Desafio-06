using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public PlayerController playerController;
    public EnemyController enemyController;

    void Start() 
    {
        playerController = GetComponent<PlayerController>();
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.name);
        
        if (other.tag == "bullet")
        {

            if (playerController != null)
                playerController.Damage();
            if(enemyController != null)
                enemyController.ApplyDamage(50);
        }
    }
}
