using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{
    #region Movement_variables
    public float movespeed;
    #endregion

    #region Physics Components
    Rigidbody2D EnemyRB;
    [SerializeField]
    [Tooltip("Healthpack")]
    private GameObject healthpack;
    #endregion

	#region Targeting_variables
    public Transform player;
    #endregion

    #region Attack_variables
    public bool Touched;
    public float Damage;
    public float attackRadius;
    public float timeToReload;
    float reloadTimer = 0;
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region Unity_functions
    //Runs once on creation
    private void Awake(){
    	EnemyRB = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }
    private void Update(){
    	if(player == null) {
    		return;
    	}
    	Move();
    }
    #endregion

    #region Movement_functions
    private void Move() {
    	Vector2 direction = player.position - transform.position;
    	EnemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region Attack_function

    private void OnCollisionStay2D(Collision2D coll){
    	Touched = true;
    	AttackPlayer();
    }

    public void AttackPlayer(){
    	if (reloadTimer > timeToReload){
    		reloadTimer = 0;
    		RaycastHit2D[] Hits = Physics2D.CircleCastAll(transform.position, attackRadius, Vector2.zero);
    		foreach(RaycastHit2D hit in Hits){
    			if (hit.transform.CompareTag("Player")){
                	hit.transform.GetComponent<PlayerController>().TakeDamage(Damage);
            	}
    		}
    	} else {
    		reloadTimer += Time.deltaTime;
    	}
    }

    public void interact(){
    	Touched = true;
    }

    #endregion

    #region Health_functions
    public void TakeDamage(float value) {
        FindObjectOfType<AudioManager>().Play("BatHurt");
        Touched = true;
        currHealth -= value;
        Debug.Log("Health is now " + currHealth.ToString());
        if (currHealth <= 0) {
            Die();
        }
    }

    private void Die(){
    	Instantiate(healthpack, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    #endregion
}
