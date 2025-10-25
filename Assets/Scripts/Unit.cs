using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, iDamagable
{

    private float unitHealth;
    public float unitMaxHealth;

    public HealthTracker healthTracker;

    Animator animator;
    NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);

        unitHealth = unitMaxHealth;
        UpdateHealthUI();

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }



    public void OnDestroy()
    {
       UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            //Dying Logic

            //Dying Animation

            //Dying Sound Effect
            
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            //attackController.targetToAttack = null;
            animator.SetBool("isMoving", true);
        }
        else
        {
           animator.SetBool("isMoving", false); 
        }
    }
}
