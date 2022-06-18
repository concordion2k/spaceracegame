using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTargetInfo : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject shipTarget;

    GameObject nameText;
    GameObject healthText;
    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.Find("Ship");
        shipTarget = playerShip.GetComponent<Spaceship>().currTarget;

        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform t in children)
        {
            if (t.name == "Name")
                nameText = t.gameObject;
            if (t.name == "Health")
                healthText = t.gameObject;
        }

        playerShip.GetComponent<Spaceship>().ChangedTarget += UpdateTargetName;
        shipTarget.GetComponent<Asteroid>().HealthChanged += UpdateTargetHealth;
    }

    public void UpdateTargetName(string name)
    {
        shipTarget = playerShip.GetComponent<Spaceship>().currTarget;
        if (!shipTarget)
        {
            nameText.GetComponent<TextMeshProUGUI>().text = "Target: None";
            healthText.GetComponent<TextMeshProUGUI>().text = "Health: 0";
            return;
        }
        shipTarget.GetComponent<Asteroid>().HealthChanged += UpdateTargetHealth;
        nameText.GetComponent<TextMeshProUGUI>().text = $"Target: {name}";
        healthText.GetComponent<TextMeshProUGUI>().text = $"Health: {shipTarget.GetComponent<Asteroid>().asteroidHealth}";
    }

    public void UpdateTargetHealth(string health)
    {
        healthText.GetComponent<TextMeshProUGUI>().text = $"Health: {health}";
    }
}
