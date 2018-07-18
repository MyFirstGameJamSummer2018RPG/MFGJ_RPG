using System.Collections;
using System;
using UnityEngine;

//Universal Unit Controller
//----------------------------
//Requires unit to have Sprite Renderer, Animator, Rigidbody2D, and BoxCollider2D
//Set Gravity Scale on Rigidbody2D for it to work properly
//For followers, set BoxCOllider2D to Is Trigger to prevent collsion issues
//Use Move Speed to set Move Speed
//Use Follow Speed to set an additional speed offset for followers
//Use Is Player to set unit to player controlled unit
//Drag Game Object that contains this script to set follower target.

namespace units{
	public class UnitController : MonoBehaviour {
	
	
	public float moveSpeed=1f;
	public float followSpeed=2f;
	private Animator animator;
	private Rigidbody2D unitBody;
	private bool playerMoving;
	public bool isMoving;
	private Vector2 lastMove;
	public Vector2 pubLastMove;
	private Vector2 curMove;
	public Vector2 pubCurMove;
	public bool isPlayer;
	public GameObject target;
	private UnitController followerController;
	private Vector2 movement;
	public static bool inCombat;
	public bool isColliding;
	
	void Start(){
		animator = GetComponent<Animator>();
		unitBody = GetComponent<Rigidbody2D>();
		lastMove=new Vector2(0,-1);
		pubLastMove=lastMove;
		inCombat=false;
		if(!isPlayer){
			followerController=target.GetComponent<UnitController>();
			moveSpeed=followSpeed + followerController.moveSpeed;
		}
		else{
			movement=new Vector2(0,0);
		}
		
	}
	
	void Update(){
		overworldMove(inCombat);
	}
	
	void overworldMove(bool combat){
		if(combat){
			return;
		}
		else if(isPlayer){
			curMove=new Vector2((float)Input.GetAxisRaw("Horizontal"), (float)Input.GetAxisRaw("Vertical"));
			pubCurMove=curMove;
			if(Mathf.Abs(curMove.x)>float.Epsilon && Mathf.Abs(curMove.y)>float.Epsilon){
				unitBody.velocity=new Vector2(moveSpeed*0.707f*curMove.x, moveSpeed*0.707f*curMove.y);
				//transform.Translate(new Vector3(moveSpeed*0.707f*curMove.x*Time.deltaTime,moveSpeed*0.707f*curMove.y*Time.deltaTime,0));
				playerMoving=true;
				lastMove=new Vector2(curMove.x,curMove.y);
				pubLastMove=lastMove;
		   	}
			else if(Mathf.Abs(curMove.x)>float.Epsilon||Mathf.Abs(curMove.y)>float.Epsilon){
				unitBody.velocity=new Vector2(moveSpeed*curMove.x, moveSpeed*curMove.y);
				//transform.Translate(new Vector3(moveSpeed*curMove.x*Time.deltaTime,moveSpeed*curMove.y*Time.deltaTime,0));
				playerMoving=true;
				lastMove=new Vector2(curMove.x,curMove.y);
				pubLastMove=lastMove;
		   	}
			else{
				playerMoving=false;
				unitBody.velocity=new Vector2(0f,0f);
			}
			isMoving=playerMoving;
			updateAnimator(curMove.x, curMove.y, isMoving, lastMove.x, lastMove.y);
		}
		else if(followerController.isMoving && !followerController.isColliding){
			curMove=followerController.pubCurMove;
			pubCurMove=curMove;
			movement=new Vector2((target.transform.position.x - transform.position.x),(target.transform.position.y - transform.position.y));
			if(Mathf.Abs(movement.x)>float.Epsilon && Mathf.Abs(movement.y)>float.Epsilon){
				unitBody.velocity= new Vector2(moveSpeed*0.707f*movement.x,moveSpeed*0.707f*movement.y);
				//transform.Translate(new Vector3(moveSpeed*0.707f*movement.x*Time.deltaTime,moveSpeed*0.707f*movement.y*Time.deltaTime,0));
				playerMoving=true&&followerController.isMoving;
				lastMove=new Vector2(followerController.pubLastMove.x,followerController.pubLastMove.y);
			}
			else if(Mathf.Abs(movement.x)>float.Epsilon||Mathf.Abs(movement.y)>float.Epsilon){
				unitBody.velocity=new Vector2(moveSpeed*movement.x,moveSpeed*movement.y);
				//transform.Translate(new Vector3(moveSpeed*movement.x*Time.deltaTime,moveSpeed*movement.y*Time.deltaTime,0));
				playerMoving=true&&followerController.isMoving;
				lastMove=new Vector2(followerController.pubLastMove.x,followerController.pubLastMove.y);
			}
			else{
				playerMoving=false;
				unitBody.velocity=new Vector2(0f,0f);
			}
			isMoving=playerMoving;
			pubLastMove=lastMove;
			updateAnimator(curMove.x, curMove.y, isMoving, lastMove.x, lastMove.y);
		}
		else{
			isMoving=false;
			unitBody.velocity=new Vector2(0f,0f);
			pubLastMove=lastMove;
			updateAnimator(curMove.x, curMove.y, isMoving, lastMove.x, lastMove.y);
		}
	}
	
	void updateAnimator(float x, float y, bool move, float lx, float ly){
		animator.SetFloat("MoveX", x);
		animator.SetFloat("MoveY", y);
		animator.SetBool("PlayerMoving", move);
		animator.SetFloat("LastMoveX", lx);
		animator.SetFloat("LastMoveY", ly);
	}
	void OnCollisionEnter2D(Collision2D other){
			isColliding=true;
		}
	void OnCollisionExit2D(Collision2D other){
			isColliding=false;
		}
}
}