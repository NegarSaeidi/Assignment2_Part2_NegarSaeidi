using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[RequireComponent(typeof(PolygonCollider2D))]
public class LOS : MonoBehaviour
{
    public Collider2D collidesWith;
    public ContactFilter2D contactFilter;
    public List<Collider2D> colliderList;

    public PolygonCollider2D LOSCollider;
    // Start is called before the first frame update
    void Start()
    {
        LOSCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics2D.GetContacts(LOSCollider, contactFilter, colliderList);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        collidesWith = other;
    }
}