using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class CharFelpudo : MonoBehaviour {

	public GameObject jogador;
	public Animation animacao;

	public GameObject particulaOvo;
	public GameObject particulaPena;
	public GameObject particulaEstrela;
	public GameObject objetoParticulaFogo;

	public AudioClip somOvo;
	public AudioClip somPena;
	public AudioClip somEstrela; 
	public AudioClip somHit;
	public AudioClip somWin;
	public AudioClip somLose;
	public AudioClip somApareceStar;
	public AudioClip somFelpudoVoa;

	CharacterController objetoCharControler;
	float velocidade = 2.0f;
	float giro = 300.0f;
	float frente = 3.0f;
	private float pulo = 5.0f;
	Vector3 vetorDirecao = new Vector3(0,0,0);
	Vector3 moveCameraFrente;
	Vector3 moveMove;
	Vector3 normalZeroPiso = new Vector3 (0, 0, 0);
	Transform transformCamera;

	float contaPisca = 0;
	bool podePegarStar = false;
	int numeroObjetos;

	void Start () { 
		objetoCharControler = GetComponent<CharacterController>(); 
		animacao = jogador.GetComponent<Animation>(); 
		transformCamera = Camera.main.transform;

	}

	void Update (){ 

		moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1, 0, 1)).normalized;
		moveMove = CrossPlatformInputManager.GetAxis("Vertical")*moveCameraFrente + CrossPlatformInputManager.GetAxis("Horizontal")*transformCamera.right;

		//		if(CrossPlatformInputManager.GetButton("Jump"))
		//		{
		//			if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
		//		} 

		vetorDirecao.y -= 3.0f * Time.deltaTime;	
		objetoCharControler.Move(vetorDirecao * Time.deltaTime);
		objetoCharControler.Move(moveMove * velocidade * Time.deltaTime);

		if (moveMove.magnitude > 1f) moveMove.Normalize();
		moveMove = transform.InverseTransformDirection(moveMove);

		moveMove = Vector3.ProjectOnPlane(moveMove, normalZeroPiso);
		giro = Mathf.Atan2(moveMove.x, moveMove.z);
		frente = moveMove.z;

		objetoCharControler.SimpleMove(Physics.gravity);
		aplicaRotacao();


		//		Vector3 forward = CrossPlatformInputManager.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;
		// 		transform.Rotate(new Vector3(0,CrossPlatformInputManager.GetAxis("Horizontal") * giro *Time.deltaTime,0));
		//
		//		objetoCharControler.Move(forward * Time.deltaTime);
		//		objetoCharControler.SimpleMove(Physics.gravity);

		//		if(CrossPlatformInputManager.GetButton("Jump"))
		//		{
		//			if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
		//		} 

		if(CrossPlatformInputManager.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("Jump");
				GetComponent<AudioSource>().PlayOneShot(somFelpudoVoa, 0.7F);
			}
		}else
		{
			//if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")  )
			if((CrossPlatformInputManager.GetAxis("Horizontal") != 0.0f) || (CrossPlatformInputManager.GetAxis("Vertical") != 0.0f) )
			{
				if (!animacao.IsPlaying("Jump"))
				{	 
					jogador.GetComponent<Animation>().Play("Walk");
				}



			}else
			{
				if (objetoCharControler.isGrounded == true) 
				{	
					jogador.GetComponent<Animation>().Play("Idle");
				}
			}
		}

	}

	void aplicaRotacao()
	{
		float turnSpeed = Mathf.Lerp (180, 360, frente);
		transform.Rotate (0, giro * turnSpeed * Time.deltaTime, 0);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "OVO") 
		{
			GetComponent<AudioSource> ().PlayOneShot (somOvo, 0.7F);
			Instantiate (particulaOvo, other.gameObject.transform.position, Quaternion.identity);
			other.gameObject.SetActive (false);
			numeroObjetos++; verificaPickObjetos();
		}

		if (other.gameObject.tag == "PENA") 
		{
			GetComponent<AudioSource> ().PlayOneShot (somPena, 0.7F);
			Instantiate (particulaPena, other.gameObject.transform.position, Quaternion.identity);
			other.gameObject.SetActive (false);
			numeroObjetos++; verificaPickObjetos();

		}

		if (other.gameObject.tag == "ESTRELA") 
		{
			if (podePegarStar) 
			{
				GetComponent<AudioSource> ().PlayOneShot (somEstrela, 0.7F);
				Instantiate (particulaPena, other.gameObject.transform.position, Quaternion.identity);
				other.gameObject.SetActive (false);
				GetComponent<AudioSource> ().PlayOneShot (somWin, 0.7F);
				Invoke ("carregaFase", 3);
			}


		}

		if (other.gameObject.tag == "FOGUEIRA") 
		{
			InvokeRepeating ("mudaEstadoFelpudo", 0, 0.1f);
			GetComponent<AudioSource> ().PlayOneShot (somHit, 0.7F);
			objetoCharControler.Move (transform.TransformDirection (Vector3.back) * 3);

		}

		if (other.gameObject.tag == "BURACO") 
		{
			GetComponent<AudioSource> ().PlayOneShot (somLose, 0.7F);
			Invoke ("carregaFase", 0.5f);

		}
	}

	void mudaEstadoFelpudo()
	{
		contaPisca++;
		jogador.SetActive (!jogador.activeInHierarchy);
		if (contaPisca > 7) 
		{
			contaPisca = 0;
			jogador.SetActive (true);
			CancelInvoke ();
		}
	}

	void verificaPickObjetos()
	{
		if (numeroObjetos >= 19) 
		{
			podePegarStar = true;
			GetComponent<AudioSource> ().PlayOneShot (somApareceStar, 0.7F);
			Destroy(objetoParticulaFogo);
		}
	
	}

	void carregaFase()
	{
		SceneManager.LoadScene ("Game");
	}

}

