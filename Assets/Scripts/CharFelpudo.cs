using UnityEngine;
using System.Collections;

public class CharFelpudo : MonoBehaviour {

		float velocidade = 5.0f;
		CharacterController objetoCharControler;
		float giro = 3.0f;
		float frente = 3.0f;
		Vector3 vetorDirecao = new Vector3(0,0,0);





		public GameObject jogador;
		public Animation animacao; 

		private float pulo = 5.0f;




		void Start () { 
			objetoCharControler = GetComponent<CharacterController>(); 
			animacao = jogador.GetComponent<Animation>(); 

		}

		void Update (){ 



		}
}

