using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemHolder : MonoBehaviour
{
    bool hasGem;
    public Transform gemSpot;

    //activeerd time in reverse :)
    IEnumerator Activate(){
        Transform Fixed = GameObject.FindObjectOfType<SpriteMask>().transform;
        float max = 3000;
        float goal = 155;
        //zorgt ervoor dat de masker vergroot naar goal over de tijd van max
        //1 = 1 frame dus een max van 1000 is 1000 frames
        //bij 60 fps is dat dus 16.666 secondes tot het bij de goal komt
        for(float i = 0; i < max; i++){
            Fixed.localScale = new Vector3(i/max*goal,i/max*goal,i/max*goal);
            yield return new WaitForEndOfFrame();
        }
    }

    //wanneer de speler GemInput aanraakt
    public void Touched(Collider2D other){
        if(!hasGem && other.GetComponent<Player>()){
            var plr = other.GetComponent<Player>();
            if(plr.holdingGem){ //zodat het niet 2 keer wordt afgevuurd
                Gem gem = plr.gem;
                gem.followEnd = 0;
                gem.followStop = .3f;
                gem.obj = gemSpot;
                //^^^ zorgen ervoor dat de gem nu naar de pedestal gaat
                hasGem = true;
                StartCoroutine(Activate());
            }
        }
    }
}
