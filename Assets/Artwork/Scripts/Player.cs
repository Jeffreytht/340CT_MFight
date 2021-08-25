using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    private Transform name;
    private bool check = false;//when countdown finish only able to move
    private int countdownTime = 3;
    Vector2 movement;

    void Start()
    {
        name = transform.GetChild(0).GetChild(0);
        StartCoroutine(Delay());
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        if(check)
        {
            rb.MovePosition(newPos);
        }

        if (movement.x > 0)
        {
            transform.localScale = Vector3.one;
            name.localScale = Vector3.one;
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            name.localScale = new Vector3(-1, 1, 1);
        }
    }
    IEnumerator Delay()
    {
        while(countdownTime>0)
        {
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        yield return new WaitForSeconds(1f);
        check = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}
