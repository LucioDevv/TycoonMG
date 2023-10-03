using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform cameraAim;
    public float walkSpeed, runSpeed, jumpForce, rotationSpeed;
    public bool canMove;
    public GroundDetector groundDetector;

    private Vector3 vectorMovement, verticalForce;
    private float speed;
    private bool isGrounded;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        speed = 0f;
        vectorMovement = Vector3.zero;
        verticalForce = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Walk();
            Run();
            AlignPlayer();
            Jump();
        }
        Gravity();
        CheckGround();
    }

    void Walk()
    {
        vectorMovement.x = Input.GetAxis("Horizontal");
        vectorMovement.z = Input.GetAxis("Vertical");

        vectorMovement = vectorMovement.normalized;

        vectorMovement = cameraAim.TransformDirection(vectorMovement);

        characterController.Move(vectorMovement * speed * Time.deltaTime);
    }

    void Run()
    {
        if(Input.GetAxis("Run") > 0f)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }

    void Jump() 
    { 
    
        if(isGrounded && Input.GetAxis("Jump") > 0f)
        {
            verticalForce = new Vector3(0f, jumpForce, 0f);
            isGrounded = false;
        }

    }


    void Gravity()
    {
        if (!isGrounded)
        {
            verticalForce += Physics.gravity * Time.deltaTime;
        }
        else
        {
            verticalForce = new Vector3(0f, -2f, 0f);
        }

        characterController.Move(verticalForce * Time.deltaTime);
    }

    void AlignPlayer()
    {
        if (characterController.velocity.magnitude > 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vectorMovement), rotationSpeed * Time.deltaTime);
        }
    }

    void CheckGround()
    {
        isGrounded = groundDetector.GetIsGrounded();
    }
}
