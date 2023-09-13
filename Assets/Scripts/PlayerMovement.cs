using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed, runSpeed;
    public bool canMove;

    private Vector3 vectorMovement;
    private float speed;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        speed = 0f;
        vectorMovement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Walk();
            Run();
            Gravity();
        }
    }

    void Walk()
    {
        vectorMovement.x = Input.GetAxis("Horizontal");
        vectorMovement.z = Input.GetAxis("Vertical");

        vectorMovement = vectorMovement.normalized;

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

    void Gravity()
    {
        characterController.Move(new Vector3(0f, -4f * Time.deltaTime, 0f));
    }
}
