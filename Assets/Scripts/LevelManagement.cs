using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    //Material mlevelMat = 
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject cam4;

    public GameObject[] scene1GameObjects;
    public GameObject[] scene2GameObjects;
    public GameObject[] scene3GameObjects;
    public GameObject[] scene4GameObjects;

    private MeshCollider targetBox;


    //Load
    public void LoadScene1()
    {
        Debug.Log("Load Scene 1");
        SceneManager.LoadScene("Scenes/scene1", LoadSceneMode.Additive);
        //create a reference to all scene1 objects that can be used in the hidden call below
        //Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
        //GameObject[] sceneGameObjects = scene.GetAllGameObjects();

        //Scene scene1 = SceneManager.GetSceneByName("scene1");
        scene1GameObjects = SceneManager.GetSceneByName("scene1").GetRootGameObjects();
    }
    public void LoadScene2()
    {
        Debug.Log("Load Scene 2");
        SceneManager.LoadScene("Scenes/scene2", LoadSceneMode.Additive);
        //Scene scene2 = SceneManager.GetSceneByName("scene2");
        scene2GameObjects = SceneManager.GetSceneByName("scene2").GetRootGameObjects();
    }
    public void LoadScene3()
    {
        Debug.Log("Load Scene 3");
        SceneManager.LoadScene("Scenes/scene3", LoadSceneMode.Additive);
        Scene scene3 = SceneManager.GetSceneByName("scene3");
        scene3GameObjects = scene3.GetRootGameObjects();
    }
    public void LoadScene4()
    {
        Debug.Log("Load Scene 4");
        SceneManager.LoadScene("Scenes/scene4", LoadSceneMode.Additive);
        Scene scene4 = SceneManager.GetSceneByName("scene4");
        scene4GameObjects = scene4.GetRootGameObjects();
    }

    //Unload
    public void UnloadScene1()
    {
        Debug.Log("Unload Scene 1");
        SceneManager.UnloadScene(SceneManager.GetSceneByName("scene1"));
    }
    public void UnloadScene2()
    {
        Debug.Log("Unload Scene 2");
        SceneManager.UnloadScene(SceneManager.GetSceneByName("scene2"));
    }
    public void UnloadScene3()
    {
        Debug.Log("Unload Scene 3");
        SceneManager.UnloadScene(SceneManager.GetSceneByName("scene3"));
    }
    public void UnloadScene4()
    {
        Debug.Log("Unload Scene 4");
        SceneManager.UnloadScene(SceneManager.GetSceneByName("scene4"));
    }

    //Hide
    public void HideScene1()
    {


        //set correct player camera
        cam1.SetActive(false);
        cam2.SetActive(true);
        Debug.Log("Hide Scene 1");
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene2"));s
        scene1GameObjects = SceneManager.GetSceneByName("scene1").GetRootGameObjects();
        foreach (GameObject gameobjects in scene1GameObjects)
        {
            
            if (this.GetComponent<MeshCollider>() != null)
            {
                targetBox = this.GetComponent<MeshCollider>();
                targetBox.enabled = false;
            }


            //count++;
            //gameobjects.SetActive(false);
            //Debug.Log("Hide object: "+gameobjects.name);
        }
        //Debug.Log("Objects Hidden from Scene 1: " + count.ToString());
    }
    public void HideScene2()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        //int count = 0;
        Debug.Log("Hide Scene 2");
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene2"));
        scene2GameObjects = SceneManager.GetSceneByName("scene2").GetRootGameObjects();
        foreach (GameObject gameobjects in scene2GameObjects)
        {
            if (this.GetComponent<MeshCollider>() != null)
            {
                targetBox = this.GetComponent<MeshCollider>();
                targetBox.enabled = false;
            }
            //Debug.Log("Hide object: " + gameobjects.name);
        }
       // Debug.Log("Objects Hidden from Scene 2: " + count.ToString());
    }
    public void HideScene3()
    {
        Debug.Log("Hide Scene 3");
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene3"));
        foreach (GameObject gameobjects in scene3GameObjects)
        {
            gameobjects.SetActive(false);
            Debug.Log("Hide object: " + gameobjects.name);
        }
    }
    public void HideScene4()
    {
        Debug.Log("Hide Scene 4");
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene4"));
        foreach (GameObject gameobjects in scene4GameObjects)
        {
            gameobjects.SetActive(false);
            Debug.Log("Hide object: " + gameobjects.name);
        }
    }

    //Unhide
    public void UnHideScene1()
    {
        Debug.Log("Unhide Scene 1");
        scene1GameObjects = SceneManager.GetSceneByName("scene1").GetRootGameObjects();
        foreach (GameObject gameobjects in scene1GameObjects)
        {
            if (this.GetComponent<MeshCollider>() != null)
            {
                targetBox = this.GetComponent<MeshCollider>();
                targetBox.enabled = true;
            }
        }
    }
    public void UnHideScene2()
    {
        Debug.Log("Unhide Scene 2");
        scene2GameObjects = SceneManager.GetSceneByName("scene2").GetRootGameObjects();
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene2"));
        foreach (GameObject gameobjects in scene2GameObjects)
        {
            if (this.GetComponent<MeshCollider>() != null)
            {
                targetBox = this.GetComponent<MeshCollider>();
                targetBox.enabled = true;
            }
        }
    }
    public void UnHideScene3()
    {
        Debug.Log("Unhide Scene 3");
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene3"));
        foreach (GameObject gameobjects in scene3GameObjects)
        {
            gameobjects.SetActive(true);
        }
    }
    public void UnHideScene4()
    {
        Debug.Log("Unhide Scene 4");
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("scene4"));
        foreach (GameObject gameobjects in scene4GameObjects)
        {
            gameobjects.SetActive(true);
        }
    }

    //Camera Active
    public void camera1()
    {
        Debug.Log("cam1");
        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);
        cam4.SetActive(false);
    }
    public void camera2()
    {
        Debug.Log("cam2");
        cam2.SetActive(true);
        cam1.SetActive(false);
        cam3.SetActive(false);
        cam4.SetActive(false);
    }
    public void camera3()
    {
        Debug.Log("cam3");
        cam3.SetActive(true);
        cam2.SetActive(false);
        cam1.SetActive(false);
        cam4.SetActive(false);
    }
    public void camera4()
    {
        Debug.Log("cam4");
        cam4.SetActive(true);
        cam3.SetActive(false);
        cam2.SetActive(false);
        cam1.SetActive(false);
    }

// Start is called before the first frame update
void Start()
    {
        LoadScene1();
        LoadScene2();
        //LoadScene3();
        //LoadScene3();
        //HideScene2();
        //HideScene3();
        //HideScene4();
    }

    // Update is called once per frame
    void Update()
    {
        //HideScene2();
        //HideScene3();
        //HideScene4();
    }
}
