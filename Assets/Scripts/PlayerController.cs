using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed, jumpForce,dashSpeed, dashTime;
    public Transform groundPoint;
    private bool isOnGround, canDoubleJump;
    public LayerMask whatIsGround;

    public Animator anim;

    public BulletController shotToFire;
    public Transform shotPoint;

    private float dashCounter;

    public SpriteRenderer sr,afterImage;
    public float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;
    public float waitAfterDashing;
    private float dashRechargeCounter;

    public GameObject standing, ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    public Transform bombPoint;
    public GameObject bomb;
    public bool canMove;

    private PlayerAbilityTracker abilities;
    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();
        canMove = true;
    }

    void Update()
    {
        if (canMove && Time.timeScale != 0f)
        {
            if (dashRechargeCounter > 0) dashRechargeCounter -= Time.deltaTime;
            else
            {
                if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.canDash)
                {
                    dashCounter = dashTime;
                    ShowAfterImage();
                    AudioManager.instance.PlaySFX(7);
                }
            }
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);
                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0) ShowAfterImage();
                dashRechargeCounter = waitAfterDashing;
            }
            else
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
                if (rb.velocity.x < 0) transform.localScale = new Vector3(-1f, 1f, 1f);
                else if (rb.velocity.x > 0) transform.localScale = Vector3.one;
            }

            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
            {
                if (isOnGround)
                {
                    canDoubleJump = true;
                    AudioManager.instance.PlaySFXAdjusted(12);
                }
                else
                {
                    canDoubleJump = false;
                    anim.SetTrigger("doubleJump");
                    AudioManager.instance.PlaySFXAdjusted(9);
                }
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (standing.activeSelf)
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                    anim.SetTrigger("shotFired");
                    AudioManager.instance.PlaySFXAdjusted(14);
                }
                else if (ball.activeSelf && abilities.canDropBomb)
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                    AudioManager.instance.PlaySFX(13);
                }
            }
            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -.9f && abilities.canBecomeBall)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);
                        AudioManager.instance.PlaySFX(6);
                    }
                }
                else ballCounter = waitToBall;
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > .9f && CanBecomeStanding()) // add condition if there is no collision with ground
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);
                        AudioManager.instance.PlaySFX(10);
                    }
                }
                else ballCounter = waitToBall;
            }
        }
        else  rb.velocity = Vector2.zero; 
            if (standing.activeSelf)
            {
                anim.SetBool("isOnGround", isOnGround);
                anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            }
            if (ball.activeSelf)
            {
                ballAnim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
            }
        
    }
    private bool CanBecomeStanding()
    {
        CapsuleCollider2D checker = gameObject.AddComponent<CapsuleCollider2D>();
        checker.size = new Vector2(.5f, 2.5f);
        bool isColliding = Physics2D.OverlapCapsule(transform.position, checker.size,checker.direction,0, whatIsGround);
        Destroy(checker);
        return !isColliding;   
    }

    public void ShowAfterImage() 
    {
       SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = sr.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject,afterImageLifetime);

        afterImageCounter = timeBetweenAfterImages;
    }
}
