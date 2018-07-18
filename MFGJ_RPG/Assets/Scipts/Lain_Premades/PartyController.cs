using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//Simply Drag Script onto empty object used for holding player party
//Player units must be placed in this to layer them properly with their party as well as load the units between scene changes

namespace units{
	public class PartyController : MonoBehaviour {
	
		public GameObject[] playerParty;
		private SpriteRenderer sprite;
		private static bool partyExists;
	
		void Start(){
			if(!partyExists){
				DontDestroyOnLoad(this.gameObject);
				partyExists=true;
			}
			else{
				Destroy(this.gameObject);
			}
		}
		
		
		
		void Update(){
			OrderLayers();
		}
		
		void OrderLayers(){
			for(int i=0; i<playerParty.Length;i++){
				sprite=playerParty[i].GetComponent<SpriteRenderer>();
				for(int j=0; j<playerParty.Length;j++){
					if(i==j){
						continue;
					}
					else if(playerParty[i].transform.position.y<playerParty[j].transform.position.y){
						sprite.sortingOrder=i;
						if(sprite.sortingOrder==0){
							sprite.sortingOrder=playerParty.Length;
						}
						break;
					}
					else if(playerParty[i].transform.position.y>playerParty[j].transform.position.y){
						sprite.sortingOrder=playerParty.Length-i;
						if(sprite.sortingOrder==playerParty.Length){
							sprite.sortingOrder=0;
						}
						break;
					}
				}
				
			}
		}
		
		
	}
}