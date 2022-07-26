using UnityEngine;

namespace DarkDemon
{
    public class MouseMovement : MonoBehaviour
    {

        Vector2 mouseLook;
        Vector2 changeinDelta;
        public float sensitivity = 1;
        public float smoothing = 2;

        PlayerController Character;


        void Start()
        {
            Character = GetComponentInParent<PlayerController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {

            Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);

            changeinDelta = Vector2.Lerp(changeinDelta, smoothMouseDelta, 1 / smoothing);

            mouseLook += changeinDelta;

            mouseLook.y = Mathf.Clamp(mouseLook.y, -90, 90);


            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

            Character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
        }
    }
}

