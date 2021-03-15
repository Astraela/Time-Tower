using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Vector3 Offset;
    public float speed;

    bool following;
    public Transform obj;

    Rigidbody2D rb;

    public float followEnd = .9f;
    public float followStop = 1.2f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    //wanneer de player het aanraakt
    void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<Player>()){
            var plr = other.GetComponent<Player>();
            if(plr.holdingGem == true) return;
            plr.holdingGem = true;
            plr.gem = this;
            following = true;
            obj = other.transform;
        }
    }

    //zorgt ervoor dat de gem naar de de player gaat
    //wanneer dichtbij genoeg sloomt het eerst een stukje af tot het helemaal stopt
    //IE bij een followEnd van 0 stopt het in het midden
    //bij een followStop van 1, gaat de snelheid omlaag vanaf 1 afstand
    //followEnd = 0 & followStop = 1 betekend dus dat bij een afstand van 0.5 het voor 50% is versloomt
    //followEnd = 0.8 & followStop = 0.9 betekend dus dat bij een afstand van 0.85 het voor 50% is versloomt
    void Update(){
        if(following){
            Vector3 angle = (obj.position + Offset)- transform.position;
            if(angle.magnitude < followStop)
                angle *= Mathf.Clamp((angle.magnitude-followEnd)/(followStop-followEnd),0,1);
            rb.velocity = angle * speed;
        }
    }
}
