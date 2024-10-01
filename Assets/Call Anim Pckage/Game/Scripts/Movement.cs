using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    [HideInInspector] public bool Forward;

    [Space(10)]
    float MovementTimer;
    [Header("Movement Sprites")]
    public LeftMovement leftMovement;
    public RightMovement rightMovement;
    public Jump jump;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Transform groundCheck;
    bool isRight;
    float move;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        if (!GameManager.IsGameOver)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));

            rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
            if (move < 0)
            {
                isRight = true;
                Forward = false;
                if (isGrounded)
                {
                    MovementTimer += Time.deltaTime;
                    if(0.1f <= MovementTimer)
                    {
                        MovementTimer = 0;
                        if(GetComponent<Image>().sprite == rightMovement.RightWalk1)
                        {
                            GetComponent<Image>().sprite = rightMovement.RightWalk2;
                        }
                        else
                        {
                            GetComponent<Image>().sprite = rightMovement.RightWalk1;
                        }
                    }
                }
                else
                {
                    GetComponent<Image>().sprite = jump.RightJump;
                }
            }
            else if (move > 0)
            {
                isRight = false;
                Forward = true;
                if (isGrounded)
                {
                    MovementTimer += Time.deltaTime;
                    if (0.1f <= MovementTimer)
                    {
                        MovementTimer = 0;
                        if (GetComponent<Image>().sprite == leftMovement.LeftWalk1)
                        {
                            GetComponent<Image>().sprite = leftMovement.LeftWalk2;
                        }
                        else
                        {
                            GetComponent<Image>().sprite = leftMovement.LeftWalk1;
                        }
                    }
                }
                else
                {
                    GetComponent<Image>().sprite = jump.LeftJump;
                }
            }else if(move == 0)
            {
                if (isRight)
                {
                    GetComponent<Image>().sprite = rightMovement.RightIdle;
                }
                else
                {
                    GetComponent<Image>().sprite = leftMovement.LeftIdle;
                }
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
    public void Win()
    {
        GameManager.GameWin();
    }

    #region Mobile Controls
    public void Left()
    {
        move = -1;
    }

    public void Right()
    {
        move = 1;
    }

    public void OnclickEnd()
    {
        move = 0;
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    #endregion
}
[System.Serializable]
public class LeftMovement
{
    public Sprite LeftIdle;
    public Sprite LeftWalk1;
    public Sprite LeftWalk2;
}
[System.Serializable]
public class RightMovement
{
    public Sprite RightIdle;
    public Sprite RightWalk1;
    public Sprite RightWalk2;
}
[System.Serializable]
public class Jump
{
    public Sprite LeftJump;
    public Sprite RightJump;
}