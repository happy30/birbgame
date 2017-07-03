using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int moveSpeed;
    public EnemyState enemyState;
    public Animator _an;

    public virtual void Movement()
    {
        switch(enemyState)
        {
            case EnemyState.Patrol:

                //walk left and right till edges or walls

                break;

            case EnemyState.Attack:

                //Get in range of the player to attack

                break;

            case EnemyState.Die:

                //Stop moving

                break;
        }
    }

    public virtual void Attack()
    {

    }

}
