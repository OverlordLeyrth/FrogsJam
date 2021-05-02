using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleController : MonoBehaviour
{
public Transform leftPoint;

public Transform rightPoint;
public float moveSpeed;
private Rigidbody2D myRigidbody;
public bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     if(movingRight && transform.position.x > rightPoint.position.x)
     {
          movingRight = false;
          transform.localScale = new Vector3(-1f, 1f, 1f);
     }
        if(!movingRight && transform.position.x < leftPoint.position.x)
     {
          movingRight = true;
          transform.localScale = new Vector3(1f, 1f, 1f);
     }
        if(movingRight)
     {
          myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
     }
     else
     { 
     myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
     }
    }
}
