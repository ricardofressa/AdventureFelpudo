using UnityEngine;
using System.Collections;

public class rotateObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 80, 0) * Time.deltaTime);
	}
}
