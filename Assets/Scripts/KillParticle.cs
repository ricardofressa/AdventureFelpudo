using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour {

	void Start () {
		InvokeRepeating ("killParticle", 1, 3.0f);
	}
	

	void killParticle () {
		Destroy(this.gameObject);	
	}
}
