using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    List<IStatusEffect> effects = new List<IStatusEffect>();
    Rigidbody2D _rb;

    public int health;
    public PlayerStats stats;

    public LayerMask ground;
    

    //Name
    //Sprites

    

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }    

    void Update()
    {
        DebugThings();
    }


    //Basic moving script
    void Move()
    {
        float hSpeed = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(hSpeed * stats.moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(movement);
    }

    void Jump()
    {
        
        if(Input.GetButtonDown("Jump") && Grounded())
        {
            Debug.Log("Jump");
            _rb.velocity = new Vector2(0, stats.maxJumpHeight);
        }

        if(Input.GetButtonUp("Jump"))
        {
            StartCoroutine(ReduceYVelocity());
        }
    }

    //Check two raycast to see if we're standing on ground
    bool Grounded()
    {
        float rayLength = stats.modelHeigth;

        Vector2 rayOriginLeft = new Vector2(transform.position.x - (stats.modelWidth / 2), transform.position.y);
        Vector2 rayOriginRight = new Vector2(transform.position.x + (stats.modelWidth / 2), transform.position.y);

        bool hitLeft = Physics2D.Raycast(rayOriginLeft, -transform.up, rayLength, ground);
        bool hitRight = Physics2D.Raycast(rayOriginRight, -transform.up, rayLength, ground);

        //Return true if one of them hits
        return (hitLeft || hitRight);
    }


    //Get a status effect and start coroutine to apply
    void GetStatusEffect(IStatusEffect effect)
    {
        effects.Add(effect);
        StartCoroutine(ApplyStatusEffect(effect));
    }	

    //Apply the status effect for the remaining ticks.
    IEnumerator ApplyStatusEffect(IStatusEffect effect)
    {
        effect.OnAdd(this);
        while(effect.GetRemainingTicks() > 0)
        {
            effect.UseTick(this);
            yield return new WaitForSeconds(1f);
        }

        effect.OnRemove(this);
        effects.Remove(effect);

        yield break;
    }

    IEnumerator ReduceYVelocity()
    {
        while (_rb.velocity.y > 0.3f)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y * 0.99f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    void DebugThings()
    {
        Debug.DrawRay(new Vector2(transform.position.x - (stats.modelWidth / 2), transform.position.y), -transform.up);
        Debug.DrawRay(new Vector2(transform.position.x + (stats.modelWidth / 2), transform.position.y), -transform.up);
    }
}


