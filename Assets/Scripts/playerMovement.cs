using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int Jars;
    public GameObject[] hearts;
    public int min, sec, hour;
    private float timer;
    private float startTime;
    private int index = 0;
    public Transform playerSpawnPoint;
    public Transform checkForHitEnemy;
    public LayerMask EnemyMask;
    private int DamageToEnemy;
    private bool kill,attacking;
    private AudioSource sword;
    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        DamageToEnemy = 0;
        Jars = 0;
        min = sec = hour = 0;
        timer = 0;
        kill = false;
        enemyCount = 0;
        attacking = false;
        startTime = Time.time;
        sword = GetComponent<AudioSource>();
        animController = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
        setTimer();
       
        if (transform.position.y < -5.62f)
            loseHeart();

        
    }
    public void loseHeart()
    {
         if (index < 2)
          {
                transform.position = playerSpawnPoint.position;
                Destroy(hearts[index].gameObject);
               index++;
          }
         else
        {
            PlayerPrefs.SetInt("jars", Jars);
            PlayerPrefs.SetString("time", timeLabel.text);
            PlayerPrefs.SetInt("enemy", enemyCount);
           animController.SetTrigger("dead");
            StartCoroutine(delayBeforLoading(1.5f));
           
        }
        }
    private IEnumerator delayBeforLoading(float sec)
    {
       
        yield return new WaitForSeconds(sec);
    
        SceneManager.LoadScene("Result");

    }
    private void checkForHit()
    {
      
        Vector3 playerTransform = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        var hit = Physics2D.Linecast(playerTransform, checkForHitEnemy.position, EnemyMask);
        if (hit)
        {
         
                if (DamageToEnemy < 2)
                {
                    DamageToEnemy++;
                }
                else
                {

                    kill = true;

                }
            
        }

    }
    private void Move()
    {
        float x, y, jump;
        bool attack;
        x = Input.GetAxisRaw("Horizontal");
        if (isGrounded)
        {
            //float deltaTime = Time.deltaTime;

           
            // Keyboard Input

            y = Input.GetAxisRaw("Vertical");
            jump = Input.GetAxisRaw("Jump");
            attack = Input.GetKeyDown(KeyCode.X);
        
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
            if(attack)
            {
                sword.Play();
                animController.SetTrigger("Attack");
                checkForHit();
            }
      
            //else
            //{
            //    attacking = false;
            //}
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
        Vector3 playerTransform = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
        Gizmos.DrawLine(playerTransform, checkForHitEnemy.position);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            transform.SetParent(other.gameObject.transform.parent);
        }
        else if(other.gameObject.CompareTag("potion"))
        {
            Jars += 10;
            jarCount.text = Jars.ToString();
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("hazard"))
        {
            loseHeart();
           
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (kill)
            {
                enemyCount++;
                other.gameObject.GetComponent<Animator>().SetBool("Dead", true);
                StartCoroutine(causeDelay(other.gameObject));
            }
        }
    }

    private IEnumerator causeDelay(GameObject other)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(other.gameObject);
        kill = false;

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
