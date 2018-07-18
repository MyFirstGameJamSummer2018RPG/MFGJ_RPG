using UnityEngine;
using System.Collections;

//Simply drag onto camera object

namespace units{

	public class CameraController : MonoBehaviour{
		
		public GameObject followTarget;
		private Vector3 targetPosition;
		public float chaseSpeed;
		private static bool cameraExists;
		
		void Start(){
			if(!cameraExists){
				DontDestroyOnLoad(this.gameObject);
				cameraExists=true;
			}
			else{
				Destroy(this.gameObject);
			}
		}
		
		void Update(){
			targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosition, chaseSpeed*Time.deltaTime);
		}
	}
}
