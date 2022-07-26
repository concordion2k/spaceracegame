using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkDemon
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Shader ScreenCut;
        
        [SerializeField] Transform Player;

        bool IsMaterialsCreated = false;

        string LayerName = "PlayerVisible";

        private void Awake()
        {
            PlayerSetUp();
            InitialSetUp();

        }

        


        private void Update()
        {
            if (IsMaterialsCreated) return;

            RenderTexture(CameraA, ScreenB);
            RenderTexture(CameraB, ScreenA);
            
            IsMaterialsCreated = true;
        }


        void RenderTexture(Camera cam, Transform screen)
        {

            cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

            Material material = new Material(ScreenCut);

            MeshRenderer renderer = screen.GetComponent<MeshRenderer>();

            material.mainTexture = cam.targetTexture;

            renderer.sharedMaterial = material;

        }

        private void PlayerSetUp()
        {
            if (Player == null)
            {
                if (Camera.main != null)
                {
                    if (Camera.main.transform.root != Camera.main.transform)
                    {
                        Player = Camera.main.transform.root;

                        Debug.Log("Setting Main camera's root as the player as no Player attached in the provided field");

                    }
                    else
                    {
                        Player = Camera.main.transform;
                        Debug.Log("Setting Main camera as the player as no Player attached in the provided field");
                    }
                }
                else
                {
                    Debug.LogError("The main camera is not present. Please attach a Player Transform" +
                        " in the provided field or add a Main camera as the player in the hierarchy");

                }


            }

        }


        public void InitialSetUp()
        {
            PortalParts[] parts = GetComponentsInChildren<PortalParts>();

            for (int i = 0; i < parts.Length; i++)
            {
                switch (parts[i].GetPortalPart)
                {
                    case PortalParts.PortalPart.CameraA:
                        CameraA = parts[i].GetComponent<Camera>();
                        LayerSetup(CameraA);

                        break;

                    case PortalParts.PortalPart.CameraB:
                        CameraB = parts[i].GetComponent<Camera>();

                        LayerSetup(CameraB);
                        
                        break;

                    case PortalParts.PortalPart.ScreenA:
                        ScreenA = parts[i].transform; break;

                    case PortalParts.PortalPart.ScreenB:

                        ScreenB = parts[i].transform; break;

                    case PortalParts.PortalPart.ColliderA:

                        ColliderA = parts[i].GetComponent<Collider>(); break;

                    case PortalParts.PortalPart.ColliderB:

                        ColliderB = parts[i].GetComponent<Collider>(); break;

                    case PortalParts.PortalPart.PortalPartA:

                        PortalA = parts[i].transform; break;

                    case PortalParts.PortalPart.PortalPartB:

                        PortalB = parts[i].transform; break;


                }
            }
        }

        private void LayerSetup(Camera cam)
        {
            int layerindex = LayerMask.NameToLayer(LayerName);

            if (layerindex == -1)
            {
                Debug.Log("Layer PlayerVisible does not exist. Please create a layer called" +
                    " PlayerVisible");
            }
            else
            {
                cam.cullingMask = ~(1 << layerindex);

            }
        }

        public Camera CameraA { get; private set; }
        public Camera CameraB { get; private set; }
        public Transform ScreenA { get; private set; }
        public Transform ScreenB { get; private set; }
        public Collider ColliderA { get; private set; }
        public Collider ColliderB { get; private set; }
        public Transform PortalA { get; private set; }
        public Transform PortalB { get; private set; }
        public Transform GetPlayer { get { return Player; } }
        public Transform SetPlayer { set { Player = value; } }

        

    }



    
}

