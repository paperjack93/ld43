using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class GnomeScript : MonoBehaviour
{
	public NavMeshAgent agent;
	public Animator animator;
	public SpriteRenderer sprite;
	public AnimationCurve jumpCurve;
	public GameObject explosionPrefab;
	public GameObject splatPrefab;
	public Transform shadow;
	public LayerMask groundLayer;
	public bool sacrificed = false;

	RaycastHit _hit;

	void Update() {
		if(sacrificed) return;
		animator.SetBool("Walking", agent.desiredVelocity.sqrMagnitude > 0f);
		sprite.flipX = agent.desiredVelocity.x > 0f;
    }

    void LateUpdate(){
		if (Physics.Raycast(transform.position, Vector3.down, out _hit, 100, groundLayer)) {
			shadow.position = _hit.point+(Vector3.up*0.1f);
		}
    }

    public void Sacrifice(Vector3 position){
    	sacrificed = true;
    	agent.enabled = false;
    	GetComponentInChildren<CameraFacing>().IsSpinning = true;
		animator.SetBool("Walking", false);
		animator.SetBool("Jumping", true);
		float distance = Vector3.Distance(transform.position, position);
		SoundManager.PlaySFXAt("jump", transform.position);
		SoundManager.PlaySFXAt("gnomeAttack"+Random.Range(1,13), transform.position);
		transform.DOJump(position, distance/2, 1, distance/20f, false).SetEase(jumpCurve).OnComplete(
			()=>{
				sacrificed = false;
				Instantiate(explosionPrefab, transform.position, Quaternion.identity);
				Death();
			}
		);
    }

    public void Death(){
    	if(sacrificed) return;
		Instantiate(splatPrefab, transform.position, Quaternion.identity);
		GnomeController.instance.RemoveGnome(this);
		SoundManager.PlaySFXAt("splat"+Random.Range(2,8), transform.position);
		Destroy(gameObject);
    }
}
