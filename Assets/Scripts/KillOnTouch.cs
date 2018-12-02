using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
    	if(collider.transform.tag == "Player") collider.transform.SendMessage("Death", SendMessageOptions.DontRequireReceiver);			
    }
    void OnCollisionEnter(Collision collision) {
    	if(collision.transform.tag == "Player") collision.transform.SendMessage("Death", SendMessageOptions.DontRequireReceiver);			
    }

}
