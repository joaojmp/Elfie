using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elfie : MonoBehaviour
{
	private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;
    private Vector3 localScale;
    private float moveSpeed, dirX;
    private bool isGrounded;
    private bool invincible = false;

    public int lifes, amo;
    // public Text textAmo;
    // public Image[] hearts;
    public Sprite fullHeart;
    public Sprite heartState2;
    public Sprite heartState3;
    public Sprite heartState4;
    public Sprite emptyHeart;

    private void Start()
    {
        rb              = GetComponent<Rigidbody2D>();
        anim            = GetComponent<Animator>();
        sp              = GetComponent<SpriteRenderer>();
        localScale      = transform.localScale;
        moveSpeed       = 3f;
        lifes           = 12;
        amo             = 20;
        isGrounded      = true;
        // textAmo.text    = amo.ToString();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (isGrounded && Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector2.up * 200f);
        }

        if (dirX < 0) {
            sp.flipX = true;
        } else if (dirX > 0) {
            sp.flipX = false;
        }

        if (Mathf.Abs(dirX) > 0 || Mathf.Abs(dirX) < 0) {
            anim.SetBool("Running", true);
        } else {
            anim.SetBool("Running", false);
        }

        if (rb.velocity.y > 1) {
            anim.SetBool("Jumping", true);
            anim.SetBool("Falling", false);
        } else if (rb.velocity.y < -1) {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        } else {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
        }

        if (Input.GetMouseButtonDown(0)) {
            anim.SetBool("Shooting", true);
        }

        // switch (lifes) {
        //     case 0:
        //         hearts[0].sprite = emptyHeart;
        //         hearts[1].sprite = emptyHeart;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 1:
        //         hearts[0].sprite = heartState4;
        //         hearts[1].sprite = emptyHeart;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 2:
        //         hearts[0].sprite = heartState3;
        //         hearts[1].sprite = emptyHeart;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 3:
        //         hearts[0].sprite = heartState2;
        //         hearts[1].sprite = emptyHeart;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 4:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = emptyHeart;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 5:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = heartState4;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 6:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = heartState3;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 7:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = heartState2;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 8:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = fullHeart;
        //         hearts[2].sprite = emptyHeart;
        //     break;
        //     case 9:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = fullHeart;
        //         hearts[2].sprite = heartState4;
        //     break;
        //     case 10:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = fullHeart;
        //         hearts[2].sprite = heartState3;
        //     break;
        //     case 11:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = fullHeart;
        //         hearts[2].sprite = heartState2;
        //     break;
        //     case 12:
        //         hearts[0].sprite = fullHeart;
        //         hearts[1].sprite = fullHeart;
        //         hearts[2].sprite = fullHeart;
        //     break;
        // }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Scenario")) {
            isGrounded = true;
        }

        if(!invincible) {
            if (coll.gameObject.CompareTag("Monster")) {
                lifes--;
                if (!sp.flipX) {
                    rb.AddForce(new Vector2(-500f, 10f));
                } else {
                    rb.AddForce(new Vector2(500f, 10f));
                }
                StartCoroutine(Invulnerability());
            }
        }

        IEnumerator Invulnerability() {
            moveSpeed = 0;
            Physics2D.IgnoreLayerCollision(8, 9, true);
            anim.SetBool("Damage", true);
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreLayerCollision(8, 9, false);
            anim.SetBool("Damage", false);
            moveSpeed = 3f;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Scenario")) {
            isGrounded = false;
        }
    }
}
