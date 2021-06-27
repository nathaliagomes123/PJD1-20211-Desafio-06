using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class MachineGun : Weapon
{
    public float Accuracy { get; protected set; }
    public float DeltaDistance { get; protected set; }

    protected override void Awake()
    {
        Type = WeaponType.MachineGun;
        base.Awake();
    }

    public override void Init(WeaponDTO wdto)
    {
        base.Init(wdto);

        if(wdto is MachineGunDTO)
        {
            MachineGunDTO mdto = wdto as MachineGunDTO;
            Accuracy = mdto.Accuracy;
            DeltaDistance = mdto.DeltaDistance;
        }
    }

    protected override void CreateProjectile()
    {
        Vector3 rotationEuler = bulletRespawn.rotation.eulerAngles;
        float rotationZ = Random.Range(rotationEuler.z - Accuracy, rotationEuler.z + Accuracy);
        Quaternion rotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, rotationZ);
        GameObject go = Factory.GetObject("bullet", bulletRespawn.position, rotation);
        BulletController bullet = go.GetComponent<BulletController>();
        MachineGunDTO mdto = (MachineGunDTO)weaponDTO;
        bullet.Init(mdto);
    }
}
