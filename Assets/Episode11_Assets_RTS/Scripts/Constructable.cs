using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Constructable : MonoBehaviour, iDamagable
{

    private float constHealth;
    public float constMaxHealth;

    public HealthTracker healthTracker;
    public bool isEnemy = false;

    NavMeshObstacle obstacle;

    public BuildingType buildingType;
    
    public Vector3 buildPosition;

    private void Start()
    {
       constHealth = constMaxHealth;
       UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(constHealth, constMaxHealth);
        if(constHealth <= 0)
        {
            ResourceManager.Instance.UpdateBuildingChanged(buildingType, false, buildPosition);
            SoundManager.Instance.PlayBuildingDestructionSound();

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        constHealth -= damage;
        UpdateHealthUI();
    }

    public void ConstructableWasPlaced()
    {
        // Make Healthbar Visible

        healthTracker.gameObject.SetActive(true);

         ActivateObstacle();

        if(isEnemy)
        {
            gameObject.tag = "Enemy";
        }
    }

    private void OnDestroy()
    {
        if(constHealth > 0 && buildPosition != Vector3.zero)
        {
        ResourceManager.Instance.SellBuilding(buildingType);
        }
    }

    private void ActivateObstacle()
    {
        obstacle = GetComponentInChildren<NavMeshObstacle>();
        obstacle.enabled = true;
    }
}
