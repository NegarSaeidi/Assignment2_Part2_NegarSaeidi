using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float runForce;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask,playerMask;
    public bool isGroundAhead;
    public Transform checkForHitPlayer;
    [Header("PlayerDetection")]
    public LOS enemyLOS;
    private Rigidbody2D rigidbody;
    private Animator anim;
    public playerMovement player;
    private int damageToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        damageToPlayer = 0;
        enemyLOS = GetComponent<LOS>();
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAhead();
        LookInFront();
        checkForHit();
        if (!HasLOS())
        {
            MoveEnemy();
            anim.SetBool("Attack", false);
        }
        else
            anim.SetBool("Attack", true);

    }
    private void checkForHit()
    {
        Vector3 enemyTransform = new Vector3(transform.position.x, transform.position.y -0.5f, transform.position.z);
        var hit = Physics2D.Linecast(enemyTransform, checkForHitPlayer.position, playerMask);
        if(hit)
        {
           
            if(damageToPlayer<160)
            {
                damageToPlayer++;
            }
            else
            {
               
                player.loseHeart();
                damageToPlayer = 0;
                
            }
        }

    }
    private void LookAhead()
    {
        var hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, groundLayerMask);
        isGroundAhead = (hit) ? true : false;
    }

    private void LookInFront()
    {
        var hit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, wallLayerMask);
        if (hit)
        {
            Flip();
        }
    }

    private void MoveEnemy()
    {
        if (isGroundAhead)
        {
            rigidbody.AddForce(Vector2.left * runForce * transform.localScale.x);
            rigidbody.velocity *= 0.90f;
        }
        else
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }

    // EVENTS

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            transform.SetParent(other.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            transform.SetParent(null);
        }
    }
    private bool HasLOS()
    {
        if (enemyLOS.colliderList.Count > 0)
        {
            if ((enemyLOS.collidesWith.gameObject.name == "Player") && (enemyLOS.colliderList[0].gameObject.name == "Player"))

                return true;
        }

        return false;
    }

    // UTILITIES

    private void OnDrawGizmos()
    {
        Vector3 enemyTransform = new Vector3(transform.position.x, transform.position.y -0.5f, transform.position.z);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, lookAheadPoint.position);
        Gizmos.DrawLine(transform.position, lookInFrontPoint.position);
        Gizmos.DrawLine(enemyTransform, checkForHitPlayer.position);
    }
}