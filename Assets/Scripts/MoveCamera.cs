using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {


	Vector3 offSet;
	public GameObject player;

	// Use this for initialization
	void Start () {
		offSet = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offSet;
	}
}
