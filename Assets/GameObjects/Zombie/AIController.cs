using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AIController : Character {

    [SerializeField]
    float searchRadius = 10.0f;
    float attackRadius = 2.0f;
    float attackSpeed = 1.0f;
    float elapsed = 0.0f;

    public GameObject head;
    public Transform headBone;

    public GameObject leftForeArm;
    public GameObject leftHand;
    public GameObject leftUpperArm;
    public Transform leftArmBone;

    public GameObject rightForeArm;
    public GameObject rightHand;
    public GameObject rightUpperArm;
    public Transform rightArmBone;

    internal void DisemBowel(string tag)
    {
        if(tag == "EnemyHead")
        {
            head.SetActive(false);
            headBone.GetComponentInChildren<ParticleSystem>().Emit(50);
        }

        if(tag == "EnemyLeftArm")
        {
            leftForeArm.SetActive(false);
            leftHand.SetActive(false);
            leftUpperArm.SetActive(false);
            leftArmBone.GetComponentInChildren<ParticleSystem>().Emit(25);
        }

        if(tag == "EnemyRightArm")
        {
            rightForeArm.SetActive(false);
            rightHand.SetActive(false);
            rightUpperArm.SetActive(false);
            rightArmBone.GetComponentInChildren<ParticleSystem>().Emit(25);
        }
    }

    float damageAmount = 20.0f;

    bool isDying = false;
    float dyingAnimElapsed = 0.0f;
    float dyingAnimTime = 1.0f;


    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;

    Character closestPlayerController;

	public override void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        destroyOnDeath = true;
	}
	
	void Update () {
        Vector3 target = FindClosestPlayer();

        if(Vector3.Distance(transform.position, target) < attackRadius)
        {
            animator.SetBool("isAttacking", true);
            elapsed += Time.deltaTime;
            if(elapsed > attackSpeed)
            {
                closestPlayerController.TakeDamage(damageAmount);
                elapsed = 0.0f;
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);            
        }

        if(isDying)
        {
            agent.Stop();
            dyingAnimElapsed += Time.deltaTime;
            if(dyingAnimElapsed > dyingAnimTime)
            {
                RpcDestroy();
            }
        }
        else
        {
            agent.SetDestination(target);
        }       
    }

    Vector3 FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float closestDist = searchRadius;
        // Debug.Log(players.Length);
        Vector3 closestPlayer = Vector3.zero;
        
        foreach(GameObject player in players)
        {
            float distFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distFromPlayer < searchRadius && distFromPlayer < closestDist)
            {
                closestDist = distFromPlayer;
                closestPlayer = player.transform.position;
                closestPlayerController = player.GetComponent<SoldierCharacter>();
            }
        }
        return closestPlayer;
    }

    public override void TakeDamage(float amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0.0f;
            isDying = true;
            animator.SetBool("isDead", isDying);
        }
    }
}
