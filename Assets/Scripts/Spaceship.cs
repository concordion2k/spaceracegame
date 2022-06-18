using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
    [Header("=== Ship Movement Settings ===")]
    [SerializeField]
    private bool inverted = true;
    [SerializeField]
    private float yawTorque = 500f;
    [SerializeField]
    private float pitchTorque = 1000f;
    [SerializeField]
    private float rollTorque = 1000f;

    [SerializeField]
    private float thrust = 100f;
    public float Thrust {
        get
        {
            return thrust;
        }
    }
    [SerializeField]
    private float upThrust = 50f;
    [SerializeField]
    private float strafeThrust = 50f;

    [Header("=== Boost Settings ===")]
    [SerializeField]
    private float maxBoostAmount = 2f;
    [SerializeField]
    private float boostDeprecationRate = 0.25f;
    [SerializeField]
    private float boostRechargeRate = 0.5f;
    [SerializeField]
    private float boostMultiplier = 5f;
    public bool boosting = false;
    public float currentBoostAmount = 0f;

    // The Range piece enabled a slider in the UI
    [SerializeField, Range(0.001f, 0.999f)]
    private float thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float leftRightGlideReduction = 0.111f;

    [Header("=== Ship Targeting Info ===")]
    public GameObject currTarget = null;
    public GameObject currWaypoint;
    public delegate void HandleChangedTarget(string name);
    public event HandleChangedTarget ChangedTarget;

    //private float glide = 0f;
    //private float verticalGlide = 0f;
    //private float horizontalGlide = 0f;

    Rigidbody rb;

    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;
    // Tracking both dimensions view up/down, left/right
    private Vector2 pitchYaw;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentBoostAmount = maxBoostAmount;
        GameObject.Find("Waypoints").GetComponent<WaypointPath>().WaypointChanged += OnChangedWaypoint;
    }


    void FixedUpdate()
    {
        HandleBoosting();
        HandleMovement();
    }

    void HandleBoosting()
    {
        // Boosting

        if (boosting && currentBoostAmount > 0f)
        {
            currentBoostAmount -= boostDeprecationRate;
            if (currentBoostAmount <= 0f)
            {
                boosting = false;
            }
        }
        else
        {
            if (currentBoostAmount < maxBoostAmount)
            {
                currentBoostAmount += boostRechargeRate;
            }
        }
    }

    void HandleMovement()
    {
        // Roll
        rb.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);
        // Pitch - some shenanigans going on here to ensure we are reversed?
        // Vector3.right - add torqu to the right side axis

        float inversion;
        if (inverted)
        {
            inversion = 1.0f;
        }
        else
        {
            inversion = -1.0f;
        }
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(inversion * pitchYaw.y, -1f, 1f) * pitchTorque * Time.deltaTime);

        // Yaw
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * yawTorque * Time.deltaTime);


        // Thrust
        // This IF is required for a controller stick for degrees of tilt
        if (thrust1D > 0.1f || thrust1D < -0.1f)
        {
            float currentThrust;
            if (boosting)
            {
                currentThrust = thrust * boostMultiplier;
            }
            else
            {
                currentThrust = thrust;
            }

            rb.AddRelativeForce(currentThrust * thrust1D * Time.deltaTime * Vector3.forward);
            //glide = thrust1D * thrust;
        }
        //else
        //{
        //    rb.AddRelativeForce(glide * Time.deltaTime * Vector3.forward);
        //    glide *= thrustGlideReduction;
        //}

        // UPDOWN
        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.deltaTime);
            //verticalGlide = upDown1D * upThrust;
        }
        //else
        //{
        //    rb.AddRelativeForce(Vector3.up * verticalGlide * Time.deltaTime);
        //    verticalGlide *= upDownGlideReduction;
        //}

        // STRAFE
        if (strafe1D > 0.1f || strafe1D < -0.1f)
        {
            // Pay attention to the Vector3.[direction] - these are short hands
            // and can be confusing
            rb.AddRelativeForce(Vector3.right * strafe1D * strafeThrust * Time.deltaTime);
            //horizontalGlide = strafe1D * strafeThrust;
        }
        //else
        //{
        //    rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.deltaTime);
        //    horizontalGlide *= leftRightGlideReduction;
        //}

    }

    private GameObject GetGOInFrontOfCamera()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        // the out keyword allows passing by ref, instead of by value
        // thus, `hit` should be inited and available in the if block
        if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.GetComponent<Asteroid>())
                return hit.transform.gameObject;

            return null;
        }
        else
        {
            return null;
        }
    }

    private void OnChangedWaypoint(string name)
    {
        currWaypoint = GameObject.Find(name);
    }

    // The below functions are selected in the unity editor in the
    // Player Input component. You first create your InputActions file to
    // denote your controls, then add the Player Input component to the
    // gameobject. then, create functions like those below. hook up these functions
    // by dragging the gameobject to the input and select the appropriate one.

    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchView(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        boosting = context.performed;
    }

    public void OnTargetObject(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pressed target button");
            currTarget = GetGOInFrontOfCamera();
            if (!currTarget && ChangedTarget != null)
            {
                ChangedTarget("None");
                return;
            }
            Debug.Log("Target GameObject:" + currTarget);
            if (ChangedTarget != null)
                ChangedTarget(currTarget.name);
        }
    }

}
