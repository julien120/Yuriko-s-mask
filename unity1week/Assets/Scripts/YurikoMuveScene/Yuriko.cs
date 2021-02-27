using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Yuriko : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Camera camera;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private IDisposable moveDisposable;

    private bool isJumping = false;

    public void Initialize()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnGameStart()
    {
        moveDisposable = this.UpdateAsObservable()
            .Subscribe(_ => Move());
    }

    void OnGameOver()
    {

    }

    private void Move()
    {
        Vector3 newPosition = transform.position;

        if (Input.GetKey("right"))
        {
            newPosition += new Vector3(1 * moveSpeed, 0, 0);
            animator.SetBool("IsMove", true);

        }
        else if (Input.GetKey("left"))
        {
            newPosition += new Vector3(-1 * moveSpeed, 0, 0);
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }

        transform.position = Clamp(newPosition);


        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
            rigidbody2D.AddForce(new Vector2(0, 300));
        }
    }

    /// <summary>
    /// 移動制限
    /// </summary>
    private Vector3 Clamp(Vector3 position)
    {
        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);

        return position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //マスクや敵との接触処理とか
        if (collision.transform.tag == "Ground")
        {
            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }
        }
    }
}
