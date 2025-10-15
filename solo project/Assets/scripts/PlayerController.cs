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

    Ray interactRay;
    RaycastHit interactHit;
    GameObject pickupObj;

    public PlayerInput input;
    public Transform weaponSlot;
    public Weapon currentWeapon; 

    float inputX;
    float inputY;
    

    public float Xsensitivity = .1f;
    public float Ysensitivity = .1f;

    public float speed = 5f;
    public float jumpHeight = 5f;
    public float jumpRayDistance = 1.1f;
    public float calculationLimit = 90;
    public float interactDistance = 1f; 
    
    public int health = 5;
    public int maxHealth = 5;

    public bool attacking = false;
    public bool climbing = false;

    public GameObject resetBlock;
    public GameObject moveBlock;

    public int key = 0;

    public AudioSource speakers;

    public AudioClip[] SFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        interactRay = new Ray(transform.position, transform.forward);
        jumpRay = new Ray(transform.position, -transform.up);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        lookAxis = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        weaponSlot = playerCam.transform.GetChild(0); 

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

        interactRay.origin = playerCam.transform.position;
        interactRay.direction = playerCam.transform.forward;

        if (Physics.Raycast(interactRay, out interactHit, interactDistance))
        {
            if (interactHit.collider.tag == "weapon")
            {
                pickupObj = interactHit.collider.gameObject;
            }
        }
        else
            pickupObj = null;

        if (currentWeapon)
            if (currentWeapon.holdToAttack && attacking)
                currentWeapon.fire();

       

        //Movement System

        Vector3 tempMove = rb.linearVelocity;

        if (climbing)
            tempMove.y = inputY * speed;
        else
            tempMove.x = inputY * speed;

        tempMove.z = inputX * speed;

        rb.linearVelocity = (tempMove.x * transform.forward) + (tempMove.y * transform.up) + (tempMove.z * transform.right);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                    attacking = true;
                else
                    attacking = false;
            }

            else if (context.ReadValueAsButton())
                currentWeapon.fire(); 
        }
    }

    public void Reload()
    {
        if (currentWeapon)
            currentWeapon.reload();
    }

    public void Interact()
    {
        if (pickupObj)
        {
            if (pickupObj.tag == "weapon")
                pickupObj.GetComponent<Weapon>().equip(this);

        }
        else
            Reload();
    }

    public void DropWeapon()
    {
        if (currentWeapon)
        {
            currentWeapon.GetComponent<Weapon>().unequip(); 
        }
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

        speakers.resource = SFX[0];
        speakers.Play();
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
        if (collision.gameObject.tag == "ladder")
        {
            climbing = true;
        }
        if (collision.gameObject.tag == "reset")
        {
            moveBlock.transform.position = resetBlock.transform.position; 
        }

        if (collision.gameObject.tag == "key")
        {
            key += 1;
            Destroy(collision.gameObject);
        }
        if ((key ==1) && collision.gameObject.tag == "door")
        {
            Destroy (collision.gameObject);
            key = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "ladder")
        {
            climbing = false;
        }
    }
}