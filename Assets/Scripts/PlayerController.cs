using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Transform viewPoint;
    public float mouseSens = 1f;
    private float vertRotStore;
    private Vector2 mouseInput;
    public float moveSpeed = 5f, runSpeed = 8f;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    public CharacterController charCon;
    private Camera cam;
    public float jumpForce = 7.5f, gravityMod = 2.5f;
    public Transform groundCheckPoint;
    private bool isGrounded;
    public LayerMask groundLayers;
  
    public Transform frontdoor;
    public PlayerController instance;


    void Start()
    {
        
       
        cam = Camera.main;
        instance = this; 
    }

   
    void Update()
    {
        
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSens;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        vertRotStore += mouseInput.y;

        vertRotStore = Mathf.Clamp(vertRotStore, -60f, 60f);

        viewPoint.rotation = Quaternion.Euler(-vertRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
       

        }
        else
        {
            activeMoveSpeed = moveSpeed;
          
        }
        
        float yVel = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
        movement.y = yVel;
        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);


        if (charCon.isGrounded)
        {

            movement.y = 0f;
        }
        
        
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpForce;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;

        }
       
        else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }


        }
        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
        charCon.Move(movement * Time.deltaTime);


        openDoor();


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







}


