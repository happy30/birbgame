using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public override void Movement()
    {
        switch (enemyState)
        {
            case EnemyState.Patrol:

                //Fly across the level

                break;

            case EnemyState.Attack:

                //Fly to the player with pathfinding

                break;

            case EnemyState.Die:

                //Stop moving

                break;
        }
    }
}
