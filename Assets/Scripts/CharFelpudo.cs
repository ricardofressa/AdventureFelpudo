using UnityEngine;
using System.Collections;
//using UnityStandardAssets.CrossPlatformInput;

public class CharFelpudo : MonoBehaviour {

		float velocidade = 5.0f;
		CharacterController objetoCharControler;
//		Transform transformCamera;
//		Vector3 moveCameraFrente;
//		Vector3 moveMove;
//		Vector3 normalZeroPiso = new Vector3(0,0,0);
		float giro = 3.0f;
		float frente = 3.0f;
		Vector3 vetorDirecao = new Vector3(0,0,0);





		public GameObject jogador;
		public Animation animacao; 
//		public GameObject particulaOvo;
//		public GameObject particulaPena;
//		public GameObject particulaEstrela;
//		public GameObject objetoParticulaFogo;

//		public AudioClip somOvo;
//		public AudioClip somPena;
//		public AudioClip somEstrela; 
//		public AudioClip somHit;
//		public AudioClip somWin;
//		public AudioClip somLose;
//		public AudioClip somApareceStar;
//		public AudioClip somFelpudoVoa;

		//	private float velocidade = 3.5f;
		//	private float giro = 230.0f;
		//	private float gravidade = 4f;
		private float pulo = 5.0f;
		//	private CharacterController objetoCharControler;
		//	private Vector3 vetorDirecao = new Vector3(0,0,0); 


		private int numeroObjetos;
		private int contaPisca;
		private bool podePegarStar;


		void Start () { 
			objetoCharControler = GetComponent<CharacterController>(); 
			animacao = jogador.GetComponent<Animation>(); 
//			transformCamera = Camera.main.transform;
		}

		void Update (){ 

//			moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1, 0, 1)).normalized;
//			moveMove = CrossPlatformInputManager.GetAxis("Vertical")*moveCameraFrente + CrossPlatformInputManager.GetAxis("Horizontal")*transformCamera.right;

			//		if(CrossPlatformInputManager.GetButton("Jump"))
			//		{
			//			if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
			//		} 

//			vetorDirecao.y -= 3.0f * Time.deltaTime;	
//			objetoCharControler.Move(vetorDirecao * Time.deltaTime);
//			objetoCharControler.Move(moveMove * velocidade * Time.deltaTime);
//
//			if (moveMove.magnitude > 1f) moveMove.Normalize();
//			moveMove = transform.InverseTransformDirection(moveMove);
//
//			moveMove = Vector3.ProjectOnPlane(moveMove, normalZeroPiso);
//			giro = Mathf.Atan2(moveMove.x, moveMove.z);
//			frente = moveMove.z;

			objetoCharControler.SimpleMove(Physics.gravity);
//			aplicaRotacao();


					Vector3 forward = CrossPlatformInputManager.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;
			 		transform.Rotate(new Vector3(0,CrossPlatformInputManager.GetAxis("Horizontal") * giro *Time.deltaTime,0));
			
					objetoCharControler.Move(forward * Time.deltaTime);
					objetoCharControler.SimpleMove(Physics.gravity);

//					if(CrossPlatformInputManager.GetButton("Jump"))
//					{
//						if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
//					} 

			if(CrossPlatformInputManager.GetButton("Jump"))
			{
				if (objetoCharControler.isGrounded == true) {
					vetorDirecao.y = pulo;
					jogador.GetComponent<Animation>().Play("JUMP");
					GetComponent<AudioSource>().PlayOneShot(somFelpudoVoa, 0.7F);
				}
			}else
			{
				//if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")  )
				if((CrossPlatformInputManager.GetAxis("Horizontal") != 0.0f) || (CrossPlatformInputManager.GetAxis("Vertical") != 0.0f) )
				{
					if (!animacao.IsPlaying("JUMP"))
					{	 
						jogador.GetComponent<Animation>().Play("WALK");
					}



				}else
				{
					if (objetoCharControler.isGrounded == true) 
					{	
						jogador.GetComponent<Animation>().Play("IDLE");
					}
				}
			}




			//		vetorDirecao.y -= gravidade * Time.deltaTime;	
			//	    objetoCharControler.Move(vetorDirecao * Time.deltaTime);
		}




//		void OnTriggerEnter(Collider other) 
//		{
//			if (other.gameObject.tag == "pickOvo")
//			{
//
//				GetComponent<AudioSource>().PlayOneShot(somOvo, 0.7F);
//
//				Instantiate(particulaOvo, other.gameObject.transform.position, Quaternion.identity);
//				other.gameObject.SetActive (false); 
//
//				numeroObjetos++; verificaPickObjetos();
//			}
//
//
//			if (other.gameObject.tag == "pickPena")
//			{
//
//				GetComponent<AudioSource>().PlayOneShot(somPena, 0.7F);
//
//				Instantiate(particulaPena, other.gameObject.transform.position, Quaternion.identity);
//				other.gameObject.SetActive (false); 
//				numeroObjetos++; verificaPickObjetos();
//			}
//
//
//			if (other.gameObject.tag == "pickStar")
//			{
//
//
//
//				if (podePegarStar) {
//
//					GetComponent<AudioSource>().PlayOneShot(somEstrela, 0.7F);
//
//					Instantiate(particulaEstrela, other.gameObject.transform.position, Quaternion.identity);
//					other.gameObject.SetActive (false);
//
//					GetComponent<AudioSource>().PlayOneShot(somWin, 0.7F);
//					fimDeJogo();
//				}
//
//			}
//			if (other.gameObject.tag == "Fire")
//			{
//
//				InvokeRepeating("mudaEstadoFelpdudo",0,0.1f);
//				GetComponent<AudioSource>().PlayOneShot(somHit, 0.7F); 
//				objetoCharControler.Move(transform.TransformDirection(Vector3.back)*3);
//
//
//			}
//			if (other.gameObject.tag == "Finish")
//			{
//				GetComponent<AudioSource>().PlayOneShot(somLose, 0.7F);
//				fimDeJogo();
//
//			}
//		}



//		void fimDeJogo()
//		{ 
//			Invoke("carregaFase",3);
//		}
//
//		void carregaFase()
//		{
//			Application.LoadLevel("CenaAdventure");
//
//		}
//
//
//		void mudaEstadoFelpdudo()
//		{
//			contaPisca++;
//			jogador.SetActive(!jogador.activeInHierarchy);
//
//
//			if (contaPisca>7) {contaPisca=0;jogador.SetActive(true); CancelInvoke();}
//
//		}

//		void verificaPickObjetos()
//		{
//			if (numeroObjetos>15)
//			{
//				podePegarStar = true;
//				Destroy(objetoParticulaFogo);
//				GetComponent<AudioSource>().PlayOneShot(somApareceStar, 0.7F);
//			}
//
//		}
//
//		void aplicaRotacao()
//		{
//			float turnSpeed = Mathf.Lerp(180, 360, frente);
//			transform.Rotate(0, giro * turnSpeed * Time.deltaTime, 0);
//		}

	}

}
