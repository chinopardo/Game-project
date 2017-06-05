using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	[SerializeField]
	private float movementSpeed;
	private bool attack;
	public Collider2D attackTrigger;

	private bool slide;
	private bool facingRight;
	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask whatIsGround;
	private bool isGrounded;
	private bool jump;
	[SerializeField]
	private bool airControl;
	[SerializeField]
	private float jumpForce;
	private bool jumpAttack;
	public static int curHealth = 5;
	public int maxHealth = 5;
	private gameMaster gm;

	// Use this for initialization
	void Start () {
		facingRight = true;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();

		curHealth = maxHealth;
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
	}
	void Update(){
		HandleInput ();

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curHealth <= 0) {
			Die();
		}



	}


	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis("Horizontal");

		isGrounded = IsGrounded();

		HandleMovement(horizontal);

		flip(horizontal);

		HandleAttacks ();

		HangleLayers ();

		ResetValues ();
	}
	private void HandleMovement(float horizontal){
		if (myRigidbody.velocity.y < 0) {
			myAnimator.SetBool("land", true);
		}
		if(!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl)){
			myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); // x=1, y=0;
		}
		if(isGrounded && jump){
			isGrounded = false;
			myRigidbody.AddForce(new Vector2(0, jumpForce));
			myAnimator.SetTrigger("jump");
		}
		if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) {
			myAnimator.SetBool ("slide", true);
		} else if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Slide")) {
			myAnimator.SetBool("slide", false);
		}

		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));
	}
	private void HandleAttacks(){
		if (attack && isGrounded &&!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
			myAnimator.SetTrigger("AttackMelee");
			myRigidbody.velocity = Vector2.zero;
		}
		if (jumpAttack && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack")){
			myAnimator.SetBool("jumpattack", true);
		}
		if(!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack")){
			myAnimator.SetBool("jumpattack", false);
		}
	}
	private void HandleInput(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			jump = true;
		}
		if(Input.GetKeyDown(KeyCode.W)){
			attack = true;
			jumpAttack = true;
			attackTrigger.enabled = true;
		}
		if(Input.GetKeyDown(KeyCode.LeftControl)){
			slide = true;
		}
	}

	private void flip(float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;

			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}
	private void ResetValues(){
		attack = false;
		slide = false;
		jump = false;
		jumpAttack = false;
	}

	private bool IsGrounded(){
		if (myRigidbody.velocity.y <= 0) {
			foreach(Transform point in groundPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++){
					if(colliders[i].gameObject != gameObject){
						myAnimator.ResetTrigger("jump");
						myAnimator.SetBool("land", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	private void HangleLayers(){
		if (!isGrounded) {
			myAnimator.SetLayerWeight(1, 1);
		} else {
			myAnimator.SetLayerWeight(1, 0);
		}
	}

	void Die(){
		if (PlayerPrefs.HasKey ("HighScore")) {
			if (PlayerPrefs.GetInt ("HighScore") < gm.points) {
				PlayerPrefs.SetInt ("HighScore", gm.points);
			}
		} else {
			PlayerPrefs.SetInt("HighScore", gm.points);
		}
		Application.LoadLevel (Application.loadedLevel);
	}

	public static void Damage(int dmg){
		curHealth -= dmg;
	}

	//public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir){
		//float timer = 0;
		//while (knockDur > timer) {
			//timer +=Time.deltaTime;
			//rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
		//}
		//yield return 0;
	//}

	void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Coin")){
			Destroy(col.gameObject);
			gm.points += 1;
		}
	}
}
