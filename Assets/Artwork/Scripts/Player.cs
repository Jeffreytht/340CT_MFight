using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public TMP_Text playerName;

    private PhotonView view;
    private BoxCollider2D boxCollider;
    private OnCoinDestroyed onCoinDestroyed;
    private OnPlayerHitByEnemy onPlayerHitByEnemy;
    private bool isFrozen = false;
    private bool isImmune = false;

    Vector2 movement;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        view = GetComponent<PhotonView>();
        playerName.text = view.Owner.NickName;
    }

    private void Update()
    {
        if (view.IsMine)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    public void FreezePlayer()
    {
        isFrozen = true;
    }

    public void UnfreezePlayer()
    {
        isFrozen = false;
    }

    public void ImmunePlayer()
    {
        isImmune = true;
    }

    public void UnimmunePlayer()
    {
        isImmune = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            Vector3 moveDelta = new Vector3(movement.x, movement.y, 0);

            if (!isFrozen)
            {
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
                if (hit.collider == null)
                {
                    transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
                }

                hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
                if (hit.collider == null)
                {
                    transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
                }
            }

            if (movement.x > 0)
            {
                transform.localScale = Vector3.one;
                playerName.transform.localScale = Vector3.one;
            }
            else if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                playerName.transform.localScale = new Vector3(-1, 1, 1);

            }
        }
    }

    public void SetOnCoinDestroyedListener(OnCoinDestroyed onCoinDestroyed)
    {
        this.onCoinDestroyed = onCoinDestroyed;
    }

    public void SetOnPlayerHitByEnemy(OnPlayerHitByEnemy onPlayerHitByEnemy)
    {
        this.onPlayerHitByEnemy = onPlayerHitByEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.CompareTag("Coin"))
        {
            Coin coin = gameObject.GetComponent<Coin>();

            if (onCoinDestroyed != null)
                onCoinDestroyed(coin);

            Debug.Log(coin.GetComponent<Coin>() == null);
            PhotonView photonView = PhotonView.Get(coin.GetComponent<Coin>());
            photonView.RPC("DestroyCoin", RpcTarget.All);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isImmune == false)
        {
            if (onPlayerHitByEnemy != null)
                onPlayerHitByEnemy(this);
        }
    }
}
