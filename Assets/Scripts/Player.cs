using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public AudioSource hitSound;

    public Camera playerCamera;
    private CameraShaker cameraShaker;

    [HideInInspector]
    public ArmController armController;
    public Animator playerDeath;
    [HideInInspector]
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpController;


    private void Awake()
    {
        instance = this;

        gunType = GunType.HANDGUN;

        armController = GetComponent<ArmController>();
        armController.armAnim.SetLayerWeight(1, 1);
        armController.armAnim.SetLayerWeight(2, 0);

        fpController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        cameraShaker = playerCamera.GetComponent<CameraShaker>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
                    armController.SwapWeapon(0, true);
                else
                {
                    if(gunType == GunType.HANDGUN)
                        armController.SwapWeapon(0, false);
                    else
                        armController.SwapWeapon(1, false);
                }
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

        if(armController.isReload)
            armController.armAnim.SetBool("isAiming", false);
        else
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

            hitSound.Play();
            health -= 25;
            if (health <= 0)
            {
                SceneManager.LoadScene("Death");
            }

            GameMng.GetInstance.AttackDamege();

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