using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//Drag onto gameobject with BoxCollider2D on it, set Box Colider to Trigger

namespace Scenes
{

	public class LoadNewArea : MonoBehaviour{
		
		public string loadTarget;
		public bool isTriggered;

		
		void OnTriggerEnter2D(Collider2D other){
			isTriggered=true;
			if(other.gameObject.tag == "Player"){
				 SceneManager.LoadScene(loadTarget);
			}
		}
	}
}
