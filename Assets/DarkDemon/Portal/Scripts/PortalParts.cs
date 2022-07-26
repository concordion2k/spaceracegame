using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkDemon
{
    public class PortalParts : MonoBehaviour
    {
        public enum PortalPart
        {
            None, PortalPartA, PortalPartB, ColliderA, ColliderB, CameraA
        , CameraB, ScreenA, ScreenB
        }

        [SerializeField] PortalPart ThisPortal = PortalPart.None;

        public PortalPart GetPortalPart { get { return ThisPortal; } }


    }
}

