using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public enum WeaponType { None, Pistol, Shotgun, MachineGun, Sniper, RocketLauncher }

public class Weapon : MonoBehaviour
{
    public string Name { get; protected set; }
    public int Ammo { get; protected set; }
    public int AmmoMax { get; protected set; }
    public int Damage { get; protected set; }
    public float FireRate { get; protected set; }
    public float ReloadSpeed { get; protected set; }
    public float BulletSpeed { get; protected set; }
    public float Distance { get; protected set; }
    public WeaponType Type { get; protected set; }

    public WeaponDTO weaponDTO;

    protected bool isFiring;
    protected bool isReloading;
    protected bool CanFire 
    { 
        get {
            return !isFiring && !isReloading && Ammo > 0;
        } 
    }

    [SerializeField]
    protected Transform bulletRespawn;

    protected virtual void Awake()
    {
        bulletRespawn = transform.Find("BulletRespawn");

        if(weaponDTO != null)
        {
            Init(weaponDTO);
        }
    }

    public virtual void Init(WeaponDTO wdto)
    {
        Name = wdto.Name;
        Ammo = wdto.Ammo;
        AmmoMax = wdto.AmmoMax;
        Damage = wdto.Damage;
        FireRate = wdto.FireRate;
        ReloadSpeed = wdto.ReloadSpeed;
        BulletSpeed = wdto.BulletSpeed;
        Distance = wdto.Distance;

        weaponDTO = wdto;
    }

    public void Fire()
    {
        if(CanFire)
        {
            CreateProjectile();
            StartCoroutine(FireCooldown());
        }
    }

    protected virtual void CreateProjectile()
    {
        GameObject go = Factory.GetObject("bullet", bulletRespawn.position, bulletRespawn.rotation);
        BulletController bullet = go.GetComponent<BulletController>();
        bullet.Init(weaponDTO);
    }

    private IEnumerator FireCooldown()
    {
        isFiring = true;
        Ammo--;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax);
        yield return new WaitForSeconds(FireRate);
        isFiring = false;
    }

    public void Reload()
    {
        if(!isReloading && Ammo < AmmoMax)
        {
            StartCoroutine(ReloadCooldown());
        }
    }

    private IEnumerator ReloadCooldown()
    {
        Debug.Log("Begin Reload");
        isReloading = true;
        GameEvents.WeaponReloadEvent.Invoke(ReloadSpeed);
        yield return new WaitForSeconds(ReloadSpeed);
        Ammo = AmmoMax;
        isReloading = false;
        GameEvents.WeaponFireEvent.Invoke(Ammo, AmmoMax);
        Debug.Log("End Reload");
    }

}
