using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;


public class KeyCode : MonoBehaviour
{
    public PlayerInput input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject doorcode, numtext, incorrecttext, correcttext;
    public PlayerController playerscript;
    public TextMeshProUGUI numTex;
    public string codeString, correctCode;
    public int stringCharacters = 0;
    public bool interactable, codeDone;
    public Button but1, but2, but3, but4, but5, but6, but7, but8, but9, but0;
    int token = 0;
    public Rigidbody rb;
    public bool dooractive;

    
    //EMBRACE THE VOID
    

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (codeDone == false)
            {
                interactable = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactable = false;
        }
    }

    private void Start()
    {

    }
    void Update()
    {
        if (interactable == true)
        {
            /* if (Input.GetKeyDown(UnityEngine.KeyCode.F))
            {
                Debug.Log("beep"); 
                doorcode.SetActive(true);
  
                playerscript.enabled = false;
                dooractive = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                interactable = false;
          
            }
            */
        
        }
        if (dooractive == true)
        {
            if (Input.GetKeyDown(UnityEngine.KeyCode.Escape))
            {
                numtext.SetActive(true);
                correcttext.SetActive(false);
                incorrecttext.SetActive(false);
                stringCharacters = 0;
                codeString = "";
                but1.interactable = true;
                but2.interactable = true;
                but3.interactable = true;
                but4.interactable = true;
                dooractive = false;
                but5.interactable = true;
                but6.interactable = true;
                but7.interactable = true;
                token = 0;
                but8.interactable = true;
                but9.interactable = true;
                but0.interactable = true;
                //doorcode.SetActive(false);
                playerscript.enabled = true;
                interactable = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            numTex.text = codeString;
            if (stringCharacters == 4)
            {
                if (codeString == correctCode)
                {
                    numtext.SetActive(false);
                    correcttext.SetActive(true);
                    but1.interactable = false;
                    but2.interactable = false;
                    but3.interactable = false;
                    but4.interactable = false;
                    but5.interactable = false;
                    but6.interactable = false;
                    but7.interactable = false;
                    but8.interactable = false;
                    but9.interactable = false;
                    but0.interactable = false;
                    codeDone = true;
                    if (token == 0)
                    {
                        StartCoroutine(endSesh());
                        token = 1;
                    }
                }
                else
                {
                    numtext.SetActive(false);
                    incorrecttext.SetActive(true);
                    but1.interactable = false;
                    but2.interactable = false;
                    but3.interactable = false;
                    but4.interactable = false;
                    but5.interactable = false;
                    but6.interactable = false;
                    but7.interactable = false;
                    but8.interactable = false;
                    but9.interactable = false;
                    but0.interactable = false;
                    if (token == 0)
                    {
                        StartCoroutine(endSesh());
                        token = 1;
                    }
                }
            }
        }
    }
    IEnumerator endSesh()
    {
        yield return new WaitForSeconds(2.5f);
        numtext.SetActive(true);
        correcttext.SetActive(false);
        incorrecttext.SetActive(false);
        stringCharacters = 0;
        codeString = "";
        but1.interactable = true;
        but2.interactable = true;
        but3.interactable = true;
        but4.interactable = true;
        but5.interactable = true;
        but6.interactable = true;
        but7.interactable = true;
        token = 0;
        but8.interactable = true;
        but9.interactable = true;
        but0.interactable = true;
        doorcode.SetActive(false);
        playerscript.enabled = true;
        interactable = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void pressedButton(int newNum)
    {
        codeString = codeString + "" + newNum;
        stringCharacters = stringCharacters ++;
    }
    
    public void onInteract(InputAction.CallbackContext context)
    {
        

        /*if (context.performed)
        {
        */
         Debug.Log("beep");
         doorcode.SetActive(true);

         playerscript.enabled = false;
         dooractive = true;
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         interactable = false;
        //}
        

    }

}