using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponsControl : MonoBehaviour
{
    internal float internalThing;
    [SerializeField]
    private float laserSpeed;
    [SerializeField]
    private float laserPower;
    //[SerializeField]
    //private float laserTTL;
    [SerializeField]
    private float laserFireRatePerSecond = 3;

    private bool firing = false;
    private bool readyToFire = false;

    private GameObject[] lasers;

    [SerializeField]
    private GameObject laserBullet;

    void Start()
    {
        lasers = GameObject.FindGameObjectsWithTag("Laser");
        StartCoroutine(FireRateCountdown());
    }

    void FixedUpdate()
    {
        ShootLaser();
    }

    
    /// <summary>
    /// A coroutine that sets <c>readyToFire</c> to <c>true</c> according to
    /// the <c>laserFireRatePerSecond</c> property on <c>WeaponsControl</c>.
    /// </summary>
    IEnumerator FireRateCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / laserFireRatePerSecond);
            readyToFire = true;
        }
    }

    /// <summary>
    /// Runs once per fixed frame. If <c>firing</c> and <c>readyToFire</c> are true,
    /// instantiates <c>LaserBullet</c> prefab at position and rotation of each
    /// laser hardpoint on <c>Spaceship</c>.
    /// </summary>
    void ShootLaser()
    {
        if (firing && readyToFire)
        {
            foreach (GameObject laser in lasers)
            {
                readyToFire = false;
                GameObject go = Instantiate(laserBullet, laser.transform.position, laser.transform.rotation);
                go.GetComponent<BulletMovement>().laserPower = laserPower;
                go.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * laserSpeed * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// Respond to FireLaser event. Sets <c>firing</c> to true.
    /// </summary>
    public void OnFireLaser(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }
}
