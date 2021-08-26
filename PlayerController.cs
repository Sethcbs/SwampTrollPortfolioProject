using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public CharacterController player;
    public Transform cam;
    public Animator animator;

    [SerializeField] float speed = 9f;
    [SerializeField] float secondsToWait;

    float smoothVelocity;
    float smoothTime = 0.1f;
    bool isGrounded;
    Vector3 velocity;

    public float gravity = -9f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    public float jumpHeight = 2f;


    private void Start()
    {
        //keeps mouse locked in the center of the screen when playing.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void Update()
    {
        //get access to input axis
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;
        //create gravity
        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);
        //if moving run movement method and walking animation
        if (move.magnitude >= 0.1)
        {
            movement();
            WalkAnimation();
        }
        //if not moving run stopping animation
        else if (move.magnitude <= 0.1)
        {
            StoppingAnimation();
        }

        void movement()
        { 
            //get smoothed angles for rotating and moving the player with the camera.
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, smoothTime);

            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            player.Move(moveDirection * speed * Time.deltaTime);
        }
        //checks if player is on the ground.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        //if the player is on the ground and not currently falling then keep velocity low. 
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        //run the jump animation, wait for animation to be ready, then jump player up.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartCoroutine(JumpDelay());
        }
        IEnumerator JumpDelay()
        {
            JumpAnimation();
            yield return new WaitForSeconds(secondsToWait);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        void WalkAnimation()
        {
            animator.SetTrigger("Walk");
        }
        void StoppingAnimation()
        {
            animator.SetTrigger("Stopping");
        }
        void JumpAnimation()
        {
            animator.SetTrigger("JumpAnimate");
        }
    }
}
