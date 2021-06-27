using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class Shotgun : Weapon
{
    public int Pellet { get; protected set; }
    public float Spread { get; protected set; }

    protected override void Awake()
    {
        Type = WeaponType.Shotgun;
        base.Awake();
    }

    public override void Init(WeaponDTO wdto)
    {
        base.Init(wdto);

        if(wdto is ShotgunDTO)
        {
            ShotgunDTO sdto = wdto as ShotgunDTO;
            Pellet = sdto.Pellet;
            Spread = sdto.Spread;
        }
    }

    protected override void CreateProjectile()
    {
        //List<BulletController> bullets = new List<BulletController>();
        float stepAngle = Spread / (float)(Pellet - 1);
        float firstAngle = -(float)Spread / 2f;
        for (int i = 0; i < Pellet; i++)
        {
            Vector3 rotationEuler = bulletRespawn.rotation.eulerAngles;
            float rotationZ = rotationEuler.z + firstAngle + stepAngle * i;
            Quaternion rotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, rotationZ);
            GameObject go = Factory.GetObject("bullet", bulletRespawn.position, rotation);
            BulletController bullet = go.GetComponent<BulletController>();
            bullet.Init(weaponDTO);
        }
    }
}
