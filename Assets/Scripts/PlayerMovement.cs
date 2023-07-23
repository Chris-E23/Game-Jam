using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
   
    private Vector3 moveDir;
    private Camera cam;
    public float jumpForce;
    public float jumpcooldown;
    public float airmultiplier;
    bool readytojump;
    private bool isGrounded;
    public LayerMask groundLayers;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    public float playerHeight;
    Rigidbody rb;
    public float groundDrag;
    public float moveSpeed;
    public KeyCode jumpKey = KeyCode.Space;
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readytojump = true;
        
    }

    // Update is called once per frame
    void Update()
    {


        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundLayers);

        MyInput();
        SpeedControl();

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;



    }
    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readytojump && isGrounded)
        {

            readytojump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpcooldown);
        }

    }

    private void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!isGrounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airmultiplier, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }

    }
    private void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


    }

    private void ResetJump()
    {

        readytojump = true;
    }

}
