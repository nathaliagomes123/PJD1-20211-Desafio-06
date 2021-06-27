using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class BulletController : Rigidbody2DBase, IPoolableObject
{
    private float speed = 5f;
    private float distance = 2f;
    private int damage;
    private Vector2 startPosition;

    public void Recycle()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        Start();
    }

    private void Start()
    {
        startPosition = tf.position;
        rb.velocity = tf.up * speed;
    }

    public void Init(WeaponDTO wdto)
    {
        damage = wdto.Damage;
        distance = wdto.Distance;
        speed = wdto.BulletSpeed;
        Start();
    }

    public void Init(MachineGunDTO mdto)
    {
        damage = mdto.Damage;
        speed = mdto.BulletSpeed;
        distance = Random.Range(mdto.Distance - mdto.DeltaDistance, mdto.Distance + mdto.DeltaDistance);
        Start();
    }

    private void Update()
    {
        if(Vector2.Distance(startPosition,tf.position) >= distance)
        {
            Debug.Log(gameObject.name);
            Factory.Recycle("bullet", gameObject);
        }
    }

}
