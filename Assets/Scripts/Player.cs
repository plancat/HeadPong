using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum GunType
    {
        HANDGUN,
        RIFLE,
        GRENADE,
        NONE
    }

    private static Player instance = null;
    public static Player GetInstance
    {
        get
        {
            return instance;
        }
    }

    public int health = 100;
    public int cash = 100;

    public GunType gunType;

    private bool isHit = false;
    private bool isAiming = false;

    public Camera playerCamera;
    private CameraShaker cameraShaker;

    [HideInInspector]
    public ArmController armController;
    public Animator playerDeath;


    private void Awake()
    {
        instance = this;

        gunType = GunType.HANDGUN;

        armController = GetComponent<ArmController>();

        cameraShaker = playerCamera.GetComponent<CameraShaker>();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Update()
    {
        AimingUpdate();
        ShootUpdate();
        ReloadUpdate();
        JumpUpdate();
        WeaponSwap();
    }

    void WeaponSwap()
    {
        GunType nextGunType = GunType.NONE;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextGunType = GunType.HANDGUN;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nextGunType = GunType.RIFLE;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nextGunType = GunType.GRENADE;
        }

        if (nextGunType != GunType.NONE)
        {
            if (nextGunType != gunType)
            {
                gunType = nextGunType;

                if (gunType == GunType.GRENADE)
                    armController.SwapWeapon(true);
                else
                    armController.SwapWeapon(false);
            }
        }
    }

    void ShootUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (gunType == GunType.GRENADE)
            {
                armController.Grenade();
            }
            else
            {
                armController.Shoot(isAiming);
            }
        }
    }

    void AimingUpdate()
    {
        if (Input.GetMouseButton(1))
            isAiming = true;
        else
            isAiming = false;

        armController.armAnim.SetBool("isAiming", isAiming);
    }

    void ReloadUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            armController.Reload();
        }
    }

    void JumpUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Play jump animation
            armController.armAnim.Play("Jump");
        }
    }

    public void Hit()
    {
        if (!isHit && health > 0)
        {
            isHit = true;

            health -= 25;
            if (health <= 0)
            {
                playerDeath.Play("Death");
            }

            StartCoroutine(HitDelay());

            cameraShaker.ShakeCamera(0.4f, 0.03f);
        }
    }

    private IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }
}