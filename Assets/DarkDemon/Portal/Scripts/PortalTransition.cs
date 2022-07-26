using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkDemon
{
	public class PortalTransition : MonoBehaviour
	{
		[SerializeField] GameObject cam1;
		[SerializeField] GameObject cam2;

		private Transform Player;
		
		private Camera PlayerCamera;
		
		private Collider OtherCollider;
		
		private Transform PortalPart;
		
		private Transform OtherPortalPart;
		
		private Camera ThisCamera;

		private bool PlayerCrossedPortal = false;

		private Transform ObjectToTeleport;

		Portal Portal;

		Collider PlayerCollider;

		private void Start()
		{
			Portal = GetComponentInParent<Portal>();

			Initialization();
		}

		private void Initialization()
		{
			if (!Portal)
			{
				Debug.LogError("Portal script not found. Please attach portal script on PortalMain" +
				" gameobject");
			}

			if (!GetComponent<PortalParts>()) { Debug.LogError("Portal Parts script not attached"); return; }

			if (!GetComponent<Collider>()) { Debug.LogError("No collider attached"); return; }

			if (!transform.parent) { Debug.LogError("NO parent. Please attach parent to this collider"); }

			PortalParts.PortalPart thisPart = GetComponent<PortalParts>().GetPortalPart;

			Player = Portal.GetPlayer;

			PlayerCamera = Player.GetComponentInChildren<Camera>();

			PortalPart = transform.parent;



			if (thisPart == PortalParts.PortalPart.ColliderA)
			{
				OtherCollider = Portal.ColliderB;
			}
			else
			{
				OtherCollider = Portal.ColliderA;
			}



			if (PortalPart == Portal.PortalA)
			{
				OtherPortalPart = Portal.PortalB;
			}
			else
			{
				OtherPortalPart = Portal.PortalA;
			}

			ThisCamera = PortalPart.GetComponentInChildren<Camera>();

			PlayerCollider = Player.GetComponent<Collider>();

			if (PlayerCollider == null) { Debug.LogError("No collider attached to player"); }
		}

		void Update()
		{
			CameraControl();

			if (PlayerCrossedPortal)
			{
				if (ObjectToTeleport)
				{
					Teleport();
					//do camera skybox switch
				}

			}

		}


		private void Teleport()
		{
			Vector3 portalToObject = ObjectToTeleport.transform.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToObject);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{

				Vector3 vel = Vector3.zero;

				if (ObjectToTeleport.GetComponent<Rigidbody>())
				{
					vel = ObjectToTeleport.GetComponent<Rigidbody>().velocity;
				}
				float rotationDiff = -Quaternion.Angle(transform.rotation, OtherCollider.transform.rotation);
				rotationDiff += 180;
				ObjectToTeleport.transform.Rotate(Vector3.up, rotationDiff);
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToObject;

				ObjectToTeleport.transform.position = OtherCollider.transform.position + positionOffset;

				if (ObjectToTeleport.GetComponent<Rigidbody>())
				{
					ObjectToTeleport.GetComponent<Rigidbody>().velocity = vel;
				}


				PlayerCrossedPortal = false;
				ObjectToTeleport = null;

			}
		}


		private void CameraControl()
		{
			Vector3 offset = PlayerCamera.transform.position - OtherPortalPart.position;
			
			ThisCamera.transform.position = PortalPart.position + offset;
			
			float angle = Quaternion.Angle(PortalPart.rotation, OtherPortalPart.rotation);
			
			Quaternion toQuaternion = Quaternion.AngleAxis(angle, Vector3.up);
			
			Vector3 dir = toQuaternion * PlayerCamera.transform.forward;
			
			ThisCamera.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

		}

		void OnTriggerEnter(Collider other)
		{
			if ((other == PlayerCollider) || (other.GetComponent<Rigidbody>()))
			{
				PlayerCrossedPortal = true;
				ObjectToTeleport = other.transform;

			}
			
		}


		void OnTriggerExit(Collider other)
		{

			if ((other == PlayerCollider) || (other.GetComponent<Rigidbody>()))
			{
				PlayerCrossedPortal = false;
				ObjectToTeleport = null;
			}

		}

		
	}

}
