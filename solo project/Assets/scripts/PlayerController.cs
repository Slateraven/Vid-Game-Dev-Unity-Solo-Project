using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Vector3 cameraOffset = new Vector3(0, .5f, 2f);
    Vector2 cameraRotation = Vector2.zero;
    Camera playerCam;
    InputAction lookAxis;
    public Rigidbody rb;

    Ray jumpRay;

    float inputX;
    float inputY;

    public float Xsensitivity = .1f;
    public float Ysensitivity = .1f;

    public float speed = 5f;
    public float jumpHeight = 5f;
    public float jumpRayDistance = 1.1f;
    public float calculationLimit = 90;

    public int health = 5;
    public int maxHealth = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        jumpRay = new Ray(transform.position, -transform.up);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        lookAxis = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    private void Update()
    {
        //Camera Handle
        //playerCam.transform.position = transform.position + cameraOffset;
        // cameraRotation.x += lookAxis.ReadValue<Vector2>().x * Xsensitivity;
        // cameraRotation.y += lookAxis.ReadValue<Vector2>().y * Ysensitivity;
        //cameraRotation.y = Mathf.Clamp(cameraRotation.y, -calculationLimit, calculationLimit);
        //playerCam.transform.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        Quaternion playerRotation = Quaternion.identity;
        playerRotation.y = playerCam.transform.rotation.y;
        playerRotation.w = playerCam.transform.rotation.w;
        transform.rotation = playerRotation;

        jumpRay.origin = transform.position;
        jumpRay.direction = -transform.up;

        //Movement System

        Vector3 tempMove = rb.linearVelocity;

        tempMove.x = inputY * speed;
        tempMove.z = inputX * speed;

        rb.linearVelocity = (tempMove.x * transform.forward) + (tempMove.y * transform.up) + (tempMove.z * transform.right);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();

        inputX = InputAxis.x;
        inputY = InputAxis.y;
    }
    public void Jump()
    {
        if (Physics.Raycast(jumpRay, jumpRayDistance))
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killzone")
        {
            health = 0;
        }

        if ((other.tag == "health") && (health < maxHealth))
        {
            health++;
            Destroy(other.gameObject);

            //other.gameObject.SetActive(false);
            //Above is if I want to do temporary object, then it comes back 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print (collision.gameObject.name);
        if (collision.gameObject.tag == "enemy")
        {
            health--;
        }
        if (collision.gameObject.tag == "murber")
        {
            health--;
        }
    }



}