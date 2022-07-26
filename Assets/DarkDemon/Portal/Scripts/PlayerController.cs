using UnityEngine;

namespace DarkDemon
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5;
        Vector2 velocity;
        public float jumpStrength = 2;
        Rigidbody Rigidbody;

        public float maxGroundDistance = 1;
        public bool isGrounded;


        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }


        void FixedUpdate()
        {
            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

            transform.Translate(velocity.x, 0, velocity.y);


        }

        void LateUpdate()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, maxGroundDistance);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Rigidbody.AddForce(Vector3.up * 100 * jumpStrength);

            }
        }
    }
}

