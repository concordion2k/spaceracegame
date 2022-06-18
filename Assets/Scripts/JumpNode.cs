using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpNode : MonoBehaviour
{
    // For use in a future iteration
    private string currScene;
    private string[] terrains;

    // Start is called before the first frame update
    void Start()
    {
        currScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Terrain1");
        }
        Debug.Log($"Sent the following thru jump node: {other.name}");
    }
}
