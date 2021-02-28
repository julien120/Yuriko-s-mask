using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CollisionWithEnemy : MonoBehaviour
{

    private Subject<Unit> hitSubject = new Subject<Unit>();
    public IObservable<Unit> OnCollision
    {
        get { return hitSubject; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            hitSubject.OnNext(Unit.Default);
        }
    }
}
