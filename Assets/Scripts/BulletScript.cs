using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public Vector3 direction;
	public float speed = 2f;
	public float lifeTimer = 30f;
	public LayerMask attackLayer;
	RaycastHit _hit;

    void Start(){
        Invoke("Death", lifeTimer);
    }

    void Update() {
    	if (Physics.SphereCast(transform.position, 1f, direction, out _hit, speed*Time.deltaTime, attackLayer)) {
			if(_hit.transform.tag != "Player") { 
				Death();
			} else {
				_hit.transform.SendMessage("Death");
				Death();
			}
		} else transform.position += direction*speed*Time.deltaTime;
    }

    public void Death(){
		Destroy(gameObject);
    }
}
