using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public float jumpHeight = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private bool doubleJump = true;

    private Rigidbody rb;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void SetCountText(){

        countText.text = "Count: " + count.ToString();

        if (count >= 12){

            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate(){

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 jumpMovement = new Vector3(0.0f, jumpHeight, 0.0f);

        rb.AddForce(movement * speed);

        if (rb.position.y <= 0.51){
            
            doubleJump = true;

            if(Input.GetButtonDown("Jump")){
                rb.AddForce(jumpMovement);
        }
        } else { 
            if (Input.GetButtonDown("Jump") && doubleJump){
                rb.AddForce(jumpMovement);
                doubleJump = false;
                    }

            }
        }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }



}