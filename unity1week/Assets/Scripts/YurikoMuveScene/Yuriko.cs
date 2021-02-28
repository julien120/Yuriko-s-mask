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
    private int jumpPower;

    [SerializeField]
    private Camera camera; // このカメラ内に移動制限
    
    [SerializeField]
    private CollisionWithEnemy collisionWithEnemy;

    private IntReactiveProperty score;
    public IObservable<int> OnScoreChanged
    {
        get { return score; }
    }

    private IntReactiveProperty immunity; // 免疫力(体力)
    public IObservable<int> OnImmunityChanged
    {
        get { return immunity; }
    }

    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private IDisposable moveDisposable;
    private IDisposable collisionEnemyDisposable;

    private bool isJumping = false;

    public void Initialize()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        score = new IntReactiveProperty(0);
        immunity = new IntReactiveProperty(0);
    }

    public void OnGameStart()
    {
        moveDisposable = this.UpdateAsObservable()
            .Subscribe(_ => Move());

        collisionEnemyDisposable = collisionWithEnemy.OnCollision
            .Subscribe(_ => OnCollisionEnemy());

        // デバッグ用
        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.S))
            .Subscribe(_ => score.Value++);
    }

    /// <summary>
    /// ゲームオーバー時の処理
    /// </summary>
    void OnGameOver()
    {
        // 移動と敵との接触時の購読を解除する
        moveDisposable.Dispose();
        collisionEnemyDisposable.Dispose();
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
            rigidbody2D.AddForce(new Vector2(0, jumpPower));
        }
    }

    /// <summary>
    /// 移動制限
    /// </summary>
    private Vector3 Clamp(Vector3 position)
    {
        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = camera.ViewportToWorldPoint(new Vector2(0, 0));

        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = camera.ViewportToWorldPoint(new Vector2(1, 1));

        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);

        return position;
    }

    private void OnCollisionEnemy()
    {

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
