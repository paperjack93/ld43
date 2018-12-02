using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenchScript : MonoBehaviour
{
	public Animator animator;
	public LayerMask sightLayer;
	public GameObject bulletPrefab;
	public Transform target;

	RaycastHit _hit;
	bool _isAttacking = false;

	void Start(){
		target = GnomeController.instance.transform;
		InvokeRepeating("RunAI", 1f, 3f + Random.Range(0f, 3f));
	}

	void RunAI(){
		if(_isAttacking) return;
		if(CanSeeTarget()) StartAttack();
	}

	bool CanSeeTarget(){
		if (Physics.Linecast(transform.position, target.position, out _hit, sightLayer)) {
			if(_hit.transform.tag != "Player") return false;
			return true;
		}
		return false;
	}

	public void StartAttack(){
		_isAttacking = true;
		SoundManager.PlaySFXAt("snort", transform.position, 0.2f);
		animator.SetTrigger("Attack");
	}

    public void Spit(){
    	_isAttacking = false;
		SoundManager.PlaySFXAt("spit", transform.position, 0.2f);
    	GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    	bullet.GetComponent<BulletScript>().direction = (target.position-transform.position).normalized;
    }

    public void Death(){
    	GameManager.instance.GreenchKilled();
    	Destroy(gameObject);
    }

}
