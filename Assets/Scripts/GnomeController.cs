using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GnomeController : MonoBehaviour
{

	public static GnomeController instance;
	public GameObject gnomePrefab;
	public List<GnomeScript> gnomes;
	public List<Vector3> offsets;
	public LayerMask groundLayer;
	public Transform crown;

	Vector3 _lastPos;

	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
			return;
		}

		offsets.Add(Vector3.zero);
		int i = 0;
		for(i = 0; i < 8; i ++){
			offsets.Add(FormationCircle(Vector3.zero, 2f, i, 360/8));
		}
		for(i = 0; i < 15; i ++){
			offsets.Add(FormationCircle(Vector3.zero, 3.5f, i, 360/15));
		}
		for(i = 0; i < 24; i ++){
			offsets.Add(FormationCircle(Vector3.zero, 5f, i, 360/24));
		}
		for(i = 0; i < 36; i ++){
			offsets.Add(FormationCircle(Vector3.zero, 6.5f, i, 360/34));
		}

		for(i = 0; i < offsets.Count; i ++){
			gnomes.Add(Instantiate(gnomePrefab, transform.position + offsets[i], Quaternion.identity, transform.parent).GetComponent<GnomeScript>());
		}

		_lastPos = transform.position;
	}

	public void RemoveGnome(GnomeScript gnome){
		gnomes.Remove(gnome);
		if(gnomes.Count == 0) GameManager.instance.Lose();
	}

	void Update() {
		if(gnomes.Count == 0) return;
		Vector3 targetPos = gnomes[0].transform.position + new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		gnomes[0].agent.destination = targetPos;
		for(int i = 1; i < gnomes.Count; i ++){
			gnomes[i].agent.destination = gnomes[0].transform.position + offsets[i];
		}

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
				
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500f, groundLayer)) {
				gnomes[gnomes.Count-1].Sacrifice(hit.point);
				gnomes.RemoveAt(gnomes.Count-1);
				if(gnomes.Count == 0) GameManager.instance.Lose();
			}
		}
	}

    void LateUpdate(){
		if(gnomes.Count == 0) return;
    	Vector3 medianPos = Vector3.zero;
    	foreach(GnomeScript gnome in gnomes){
    		medianPos += gnome.transform.position;
    	}
    	medianPos /= gnomes.Count;
    	Vector3 targetPos = Vector3.Slerp(transform.position, medianPos, Time.deltaTime * 5f);
    	Vector3 velocity = targetPos - _lastPos;
    	transform.position = Vector3.Slerp(transform.position, targetPos + (velocity * 15f), Time.deltaTime * 5f);
    	_lastPos = transform.position;

		crown.position = gnomes[0].transform.position + Vector3.up;
    }

	Vector3 FormationCircle(Vector3 center, float radius, int index, float angleIncrement)
	{
		float ang = index * angleIncrement;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		return pos;
	}
}
