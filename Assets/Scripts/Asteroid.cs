using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float asteroidHealth = 100;
    private string LASER_BULLET_NAME = "LaserBullet";

    public delegate void HandleChangeHealth(string health);
    public event HandleChangeHealth HealthChanged;

    private void Start()
    {
        asteroidHealth = gameObject.transform.localScale.x * 10;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(LASER_BULLET_NAME))
        {
            asteroidHealth -= collision.gameObject.GetComponent<BulletMovement>().laserPower;
            if (HealthChanged != null)
                HealthChanged($"{asteroidHealth}");
        }
        //if (asteroidHealth <= 0)
        //{
            Debug.Log($"Asteroid {gameObject.name} destroyed!");
            HealthChanged("Destroyed!");
            this.GetComponent<Fracture>().FractureObject();
            //Destroy(gameObject);
        //}
    }
}
