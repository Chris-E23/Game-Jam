using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
   
    private Vector2 mouseInput;
    public float moveSpeed = 5f;
    private float activeMoveSpeed;
    private Vector3 moveDir;
    private Camera cam;
    public float jumpForce;
    public float jumpcooldown;
    public float airmultiplier;
    bool readyToJump;
    private bool isGrounded;
    public LayerMask groundLayers;
    float xRot;
    float yRot;
    public Transform orientation;
    public float playerHeight;
   
    float horizontalInput;
    float verticalInput;
    
    Rigidbody rb;
    public float groundDrag;
    public GameObject player;


    void Start()
    {

        rb.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cam = Camera.main;
        readyToJump = true;
      
    }


    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.4f, groundLayers);
        
       
        //openDoor();
        myInput();
        MovePlayer();

        SpeedControl();
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {

        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airmultiplier, ForceMode.Force);
    }
    public void openDoor()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject.tag == "door")
            {

                if (Input.GetKey(KeyCode.E))
                {

                    transform.position = hit.collider.transform.position + transform.forward * 5 + transform.up * 2;

                }




            }
            if (hit.collider.gameObject.tag == "door")
            {
                gameController.instance.doorText.gameObject.SetActive(true);



            }
            else
            {

                gameController.instance.doorText.gameObject.SetActive(false);
            }






        }
        else
        {

            gameController.instance.doorText.gameObject.SetActive(false);
        }







    }

    public void teleport(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;

    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space) && readyToJump && isGrounded)
        {

            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpcooldown);
        }


    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
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
        readyToJump = true;

    }







}



