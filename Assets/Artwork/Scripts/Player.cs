using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    PhotonView view;

    Vector2 movement;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);

            if (movement.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}
