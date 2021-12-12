using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public Text jarCount,timeLabel;
    private Animator animController;
    private Rigidbody2D rigidbody;
    private int Jars;
    private int min, sec, hour;
    private float timer;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        Jars = 0;
        min = sec = hour = 0;
        timer = 0;
        startTime = Time.time;
        animController = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
        setTimer();
        
    }
    private void Move()
    {
        float x, y, jump;
        x = Input.GetAxisRaw("Horizontal");
        if (isGrounded)
        {
            //float deltaTime = Time.deltaTime;

           
            // Keyboard Input

            y = Input.GetAxisRaw("Vertical");
            jump = Input.GetAxisRaw("Jump");

            // Check for Flip
            if (x != 0)
            {
                x = FlipAnimation(x);
                animController.SetInteger("AnimState", 1);
            }
            else
            {
                animController.SetInteger("AnimState", 0);
            }
      

            // Touch Input
            Vector2 worldTouch = new Vector2();
            foreach (var touch in Input.touches)
            {
                worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            }

            float horizontalMoveForce = x * horizontalForce;// * deltaTime;
            float jumpMoveForce = jump * verticalForce; // * deltaTime;

            float mass = rigidbody.mass * rigidbody.gravityScale;



            rigidbody.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            rigidbody.velocity *= 0.99f; // scaling / stopping hack
         

        }
        else
        {
            animController.SetInteger("AnimState", 2);
            if (x != 0.0f)
            {
                x = FlipAnimation(x);
                float horizontalMoveForce = x * horizontalForce * 0.5f;// air controll;
                float mass = rigidbody.mass * rigidbody.gravityScale;



                rigidbody.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);

            }
        }

    }
    private float FlipAnimation(float x)
    {
        // depending on direction scale across the x-axis either 1 or -1
        x = (x > 0) ? 0.5f : -0.5f;

        transform.localScale = new Vector3(x, 0.5f);
        return x;
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }




    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            transform.SetParent(other.transform);
        }
        else if(other.gameObject.CompareTag("potion"))
        {
            Jars += 10;
            jarCount.text = Jars.ToString();
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            transform.SetParent(null);
        }
    }
    private void setTimer()
    {
        timer = Time.time - startTime;
        min =(int) timer / 60;
        sec = (int)timer % 60;
        
        timeLabel.text=min+":" + sec;

    }
}
