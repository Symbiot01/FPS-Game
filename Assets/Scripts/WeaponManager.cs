// using UnityEngine;
// using System.Collections;
//
// public class PlayerShoot : MonoBehaviour
// {
//     public float damage = 10f;
//     public float range = 100f;
//     public float fireRate = 0.5f;  // Time between shots
//     public int maxAmmo = 10;       // Max ammo per clip
//     public float reloadTime = 2f;  // Reload time
//     public float recoilForce = 100f; // Recoil force
//
//     public Camera fpsCam;
//     public Rigidbody camRigidbody; // Assign the camera's rigidbody
//
//     private float nextTimeToFire = 0f;
//     private int currentAmmo;
//     private bool isReloading = false;
//
//     void Start()
//     {
//         currentAmmo = maxAmmo;
//     }
//
//     void Update()
//     {
//         if (isReloading)
//             return;
//
//         if (currentAmmo <= 0)
//         {
//             StartCoroutine(Reload());
//             return;
//         }
//
//         if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
//         {
//             nextTimeToFire = Time.time + 1f / fireRate;
//             Shoot();
//         }
//     }
//
//     IEnumerator Reload()
//     {
//         isReloading = true;
//         Debug.Log("Reloading...");
//         yield return new WaitForSeconds(reloadTime);
//         currentAmmo = maxAmmo;
//         isReloading = false;
//         Debug.Log("Reloaded");
//     }
//
//     void Shoot()
//     {
//         currentAmmo--;
//
//         RaycastHit hit;
//         if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
//         {
//             Debug.Log(hit.transform.name);
//
//             Target target = hit.transform.GetComponent<Target>();
//             if (target != null)
//             {
//                 target.TakeDamage(damage);
//             }
//         }
//
//         ApplyRecoil();
//     }
//
//     void ApplyRecoil()
//     {
//         Debug.Log("Recoil");
//         if (camRigidbody != null)
//         {
//             Vector3 recoil = new Vector3(-recoilForce, 0, 0);
//             camRigidbody.AddTorque(recoil);
//         }
//     }
// }


using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    public int selectedWeapon = 0;

    public Camera fpsCam;
    public Rigidbody camRigidbody;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        EquipWeapon(selectedWeapon);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / weapons[selectedWeapon].fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0); // Equip first weapon
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1); // Equip second weapon
        }
    }

    void EquipWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            selectedWeapon = index;
            currentAmmo = weapons[selectedWeapon].maxAmmo;
            Debug.Log("Equipped: " + weapons[selectedWeapon].name);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(weapons[selectedWeapon].reloadTime);
        currentAmmo = weapons[selectedWeapon].maxAmmo;
        isReloading = false;
        Debug.Log("Reloaded");
    }

    void Shoot()
    {
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, weapons[selectedWeapon].range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(weapons[selectedWeapon].damage);
            }
        }

        ApplyRecoil();
    }

    void ApplyRecoil()
    {
        if (camRigidbody != null)
        {
            Vector3 recoil = new Vector3(-weapons[selectedWeapon].recoilForce, 0, 0);
            camRigidbody.AddTorque(recoil);
        }
    }
}
