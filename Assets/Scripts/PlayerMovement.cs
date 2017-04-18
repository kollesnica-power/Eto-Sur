using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField][Range(1f, 10f)] private float speed = 5f;
    [SerializeField][Range(1f, 10f)] private float jumpForce = 5f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private PlayerHealth playerHealth;

    public int playerNumber{set; get;}

    private Rigidbody myRigidbody;
    private Animator myAnimator;
    private bool isGrounded;
    private float currentSpeed;
    private float currentJumpForce;

    // Use this for initialization
    void Start () {

        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        currentSpeed = speed;
        currentJumpForce = jumpForce;

    }

    void Update() {

        

    }

    // Update is called once per frame
    void FixedUpdate () {

        if (!playerHealth.IsDead) {
            HandleMovement(); 
        }


    }

    void HandleMovement() {

        float moveHorizontal = Input.GetAxisRaw("Horizontal" + playerNumber);
        float moveVertical = Input.GetAxisRaw("Vertical" + playerNumber);

        // move
        Vector3 velocity = new Vector3(moveHorizontal * currentSpeed, myRigidbody.velocity.y, moveVertical * currentSpeed);
        myRigidbody.velocity = velocity;
        myAnimator.SetFloat("speed", Mathf.Max(Mathf.Abs(moveHorizontal), Mathf.Abs(moveVertical)));

        // rotate
        if (moveVertical != 0 && moveHorizontal == 0) {
            myRigidbody.MoveRotation(Quaternion.Euler(0f, (moveVertical > 0 ? 0f : 180f), 0f));
        }else if (moveHorizontal != 0 && moveVertical == 0) {
            myRigidbody.MoveRotation(Quaternion.Euler(0f, (moveHorizontal > 0 ? 90f : -90f), 0f));
        } else if (moveVertical > 0 && moveHorizontal > 0) {
            myRigidbody.MoveRotation(Quaternion.Euler(0f, 45f, 0f));
        } else if (moveVertical < 0 && moveHorizontal > 0) {
            myRigidbody.MoveRotation(Quaternion.Euler(0f, 135f, 0f));
        } else if (moveVertical < 0 && moveHorizontal < 0) {
            myRigidbody.MoveRotation(Quaternion.Euler(0f, 225f, 0f));
        } else if (moveVertical > 0 && moveHorizontal < 0) {
            myRigidbody.MoveRotation(Quaternion.Euler(0f, 315f, 0f));
        }

        isGrounded = isGround();
        myAnimator.SetBool("isGrounded", isGrounded);

        // jump
        if (Input.GetButtonDown("Jump" + playerNumber) && !myAnimator.IsInTransition(0) && isGrounded) {
            myRigidbody.AddForce(Vector3.up * currentJumpForce, ForceMode.VelocityChange);
        }

    }

    bool isGround() {

        Vector3 origin = transform.position;
        origin.y += 0.1f;

        if (Physics.Raycast(origin, Vector3.down, 0.2f, ground)) {
            return true;
        } else {
            return false;
        }
 
    }

    public void StopPlayer() {

        if (myAnimator && myRigidbody) {
            myAnimator.SetFloat("speed", 0f);
            myRigidbody.velocity = Vector3.zero; 
        }

    }

   


    public void IncreaseSpeed(float multiplier) {

        currentSpeed += speed * multiplier;

    }

    public void DecreaseSpeed(float multiplier) {

        currentSpeed -= speed * multiplier;

    }

    public void IncreaseJumpForce(float multiplier) {

        currentJumpForce += jumpForce * multiplier;

    }

    public void DecreaseJumpForce(float multiplier) {

        currentJumpForce -= jumpForce * multiplier;

    }

}
