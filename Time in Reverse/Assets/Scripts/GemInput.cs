using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInput : MonoBehaviour
{
    //zorgt ervoor dat de GemHolder de collission ziet
    void OnTriggerEnter2D(Collider2D other){
        GetComponentInParent<GemHolder>().Touched(other);
    }
}
