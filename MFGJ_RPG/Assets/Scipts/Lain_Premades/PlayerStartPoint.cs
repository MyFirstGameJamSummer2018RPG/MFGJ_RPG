using UnityEngine;
using System.Collections;

//Simply drag onto camera object

namespace units{

	public class PlayerStartPoint : MonoBehaviour{
		
		private PartyController party;
		private CameraController partyCamera;

		
		void Start(){
			party=FindObjectOfType<PartyController>();
			partyCamera=FindObjectOfType<CameraController>();
			party.transform.position=transform.position;
			partyCamera.transform.position=new Vector3(transform.position.x,transform.position.y, partyCamera.transform.position.z);
		}
		
		void Update(){
			
		}
	}
}
