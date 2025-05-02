using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRB : MonoBehaviour
{
    //for physics based interactions, rb is better (fallguys)

    //but for precise platforming movement, character controller is better-smoother and easier to fine tune (celeste,mario)
    
    //execute so dash
    public Dash _ability;
    public FlailAttack _attack;

    private Rigidbody rb;
    public float moveSpeed = 5f;
    //public float jumpForce = 7f;
    public bool isGrounded;
    public Camera cam;

    //layer for ground detection
    public LayerMask groundLayer;
    //empty gameobject at feet
    public Transform groundCheck;
    //raycast distance for ground detection
    private float groundDistance = .3f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //dash function in inspector
        _ability = ScriptableObject.CreateInstance<Dash>();
        _ability.name = "Dash";

        _attack = ScriptableObject.CreateInstance<FlailAttack>();
        _attack.name = "Attack";

        animator = GetComponent<Animator>();
        //shows whether or not she can run
        animator.SetBool("isRunning", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //more reliable for collision than collision enter exit
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.green);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //apply movement
        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        if (moveX != 0 || moveZ != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void Update()
    {
        // Converting the mouse position to a point in 3D-space
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        // Using some math to calculate the point of intersection between the line going through the camera and the mouse position with the XZ-Plane
        float t = cam.transform.position.y / (cam.transform.position.y - point.y);
        Vector3 finalPoint = new Vector3(t * (point.x - cam.transform.position.x) + cam.transform.position.x, 1, t * (point.z - cam.transform.position.z) + cam.transform.position.z);
        //Rotating the object to that point
        transform.LookAt(finalPoint, Vector3.up);

        if(Input.GetKeyDown(KeyCode.Space) && !_ability.isDashed)
        {
            Debug.Log("Dashed");
            StartCoroutine(_ability.Run(rb));
            Debug.Log("ran coroutine");
        }


        /*if(Input.GetMouseButtonDown(0) && !_attack.attackedOnce)
        {
            StartCoroutine(_attack.Attack());
        }*/
    }

    private void ClearForces()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void AmpSpeed(float speed)
    {
        speed = 1.5f;
        moveSpeed = speed * moveSpeed;
    }
  
}
