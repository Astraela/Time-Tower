using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State{
        Walking
    }

    public State currentState = State.Walking;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    //customizable properties
    public float speed = 2; //loop snelheid
    public float jumpPower = 20; //kracht van springen
    public float maxJumpTime = .1f; //hoe lang je kan springen
    float currentJumpTime = 0;
    bool jumping = false;
    public float maxJumpCD = .1f; //hoe lang tot je weer kan springen (vooral nodig zodat je niet in de lucht kan springen)
    float currentJumpCD = 0;

    public bool holdingGem = false; //of ie wel of niet een gem heeft
    public Gem gem; //verwijzing naar de gem

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void SwitchState(State newState){
        currentState = newState;
    }

    void WalkUpdate(){
        var horizontal = Input.GetAxisRaw("Horizontal"); //horizontale snelheid dus, A&D; Links&Rechts; joystick x+ & x-
        var movement = new Vector2(horizontal,0) * speed; //vermenigvuldigt t met snelheid

        //kijken of je aan het springen bent, gaat springen of dat je bent opgehouden.
        if(Input.GetKeyDown(KeyCode.Space) && !jumping && Mathf.Abs(rb.velocity.y) <= 0.5f && currentJumpCD >= maxJumpCD){
            movement += new Vector2(0,jumpPower);
            jumping = true;
            currentJumpTime = 0;
        }else if(Input.GetKey(KeyCode.Space) && jumping && currentJumpTime < maxJumpTime){
            if(maxJumpTime - currentJumpTime <= 0.1f){
                movement += new Vector2(0,jumpPower*.5f);
            }else{
                movement += new Vector2(0,jumpPower);
            }
            currentJumpTime += Time.deltaTime;
        }else if(jumping){
            jumping = false;
            currentJumpCD = 0;
        }else{
            currentJumpCD += Time.deltaTime;
        }
        //zorgt voor animatie wisseling
            animator.SetBool("Walking", horizontal != 0);
        
        //welke kant de speler op kijkt
        if(horizontal < 0){
            sr.flipX = true;
        }else if(horizontal > 0){
            sr.flipX = false;
        }

        //snelheid assignen
        rb.velocity = movement;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState){
            case State.Walking:
                WalkUpdate();
            break;
        }
    }
}
