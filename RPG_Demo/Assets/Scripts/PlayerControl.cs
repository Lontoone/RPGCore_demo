using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5;
    Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(xMove, yMove);
        transform.position = (Vector2)transform.position + move * speed * Time.deltaTime;

        //animation
        if (move.magnitude > 0.2f)
        {
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }

        //x flip?
        if (xMove > 0.1f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (xMove < -0.1f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

}
