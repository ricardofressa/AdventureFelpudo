using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharFelpudo : MonoBehaviour {

	public GameObject jogador;
	public Animation animacao;

	public GameObject particulaOvo;
	public GameObject particulaPena;
	public GameObject particulaEstrela;
	public GameObject objetoParticulaFogo;

	CharacterController objetoCharControler;
	float velocidade = 5.0f;
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

		moveCameraFrente = Vector3.Scale (transformCamera.forward, new Vector3 (1, 0, 1)).normalized;
		moveMove = Input.GetAxis ("Vertical") * moveCameraFrente + Input.GetAxis ("Horizontal") * transform.right;

		vetorDirecao.y -= 3.0f * Time.deltaTime;
		objetoCharControler.Move (vetorDirecao * Time.deltaTime);
		objetoCharControler.Move (moveMove * velocidade * Time.deltaTime);

		if (moveMove.magnitude > 1f)
			moveMove.Normalize ();
		moveMove = transform.InverseTransformDirection (moveMove);

		moveMove = Vector3.ProjectOnPlane (moveMove, normalZeroPiso);
		giro = Mathf.Atan2 (moveMove.x, moveMove.z);
		frente = moveMove.z;

		objetoCharControler.SimpleMove (Physics.gravity);
		aplicaRotacao ();



//		Vector3 forward = Input.GetAxis ("Vertical") * transform.TransformDirection (Vector3.forward) * velocidade;
//		transform.Rotate (new Vector3 (0, Input.GetAxis ("Horizontal") * giro * Time.deltaTime, 0));
//
//		objetoCharControler.Move (forward * Time.deltaTime);
//		objetoCharControler.SimpleMove (Physics.gravity);

		if (Input.GetButton ("Jump")) {
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation> ().Play ("Jump");
			}
		}
		else
		{
			if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) 
			{
				if (!animacao.IsPlaying ("Jump")) 
				{
					jogador.GetComponent<Animation> ().Play ("Walk");
				}
			} 
			else 
			{
				if (objetoCharControler.isGrounded == true) {
					jogador.GetComponent<Animation> ().Play ("Idle");
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
			Instantiate (particulaOvo, other.gameObject.transform.position, Quaternion.identity);
			other.gameObject.SetActive (false);
			numeroObjetos++; verificaPickObjetos();
		}

		if (other.gameObject.tag == "PENA") 
		{
			Instantiate (particulaPena, other.gameObject.transform.position, Quaternion.identity);
			other.gameObject.SetActive (false);
			numeroObjetos++; verificaPickObjetos();

		}

		if (other.gameObject.tag == "ESTRELA") 
		{
			if (podePegarStar) 
			{
				Instantiate (particulaPena, other.gameObject.transform.position, Quaternion.identity);
				other.gameObject.SetActive (false);
				Invoke ("carregaFase", 3);
			}


		}

		if (other.gameObject.tag == "FOGUEIRA") 
		{
			InvokeRepeating ("mudaEstadoFelpudo", 0, 0.1f);
			objetoCharControler.Move (transform.TransformDirection (Vector3.back) * 3);

		}

		if (other.gameObject.tag == "BURACO") 
		{


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
			Destroy(objetoParticulaFogo);
		}
	
	}

	void carregaFase()
	{
		SceneManager.LoadScene ("Game");
	}

}

