using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlatform : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(waitFor(5.0f));
      
    }
   
    private IEnumerator waitFor(float sec)
    {
        yield return new WaitForSeconds(sec);
        anim.SetBool("Explode", true);
        StartCoroutine(waitForAnimation(0.5f));
      
    }
    private IEnumerator waitForAnimation(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);

    }
   
}
