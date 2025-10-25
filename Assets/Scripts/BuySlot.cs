using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public Sprite availableSprite;
    public Sprite unavailableSprite;

    private bool isAvailable;

    public BuySystem buySystem;

    public int databaseItemID;

    private void Start()
    {
        // Listen to Event
        ResourceManager.Instance.OnResourceChanged += HandleResourceChanged;
        HandleResourceChanged();
        // Listen to Event
        ResourceManager.Instance.OnBuildingsChanged += HandleBuildingsChanged;
        HandleBuildingsChanged();
    }

    public void ClickedOnSlot()
    {
        if(isAvailable)
        {
            buySystem.placementSystem.StartPlacement(databaseItemID);
        }
    }

    private void UpdateAvailabilityUI()
    {
        if(isAvailable)
        {
            GetComponent<Image>().sprite = availableSprite;
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Image>().sprite = unavailableSprite;
            GetComponent<Button>().interactable = false;            
        }
    }

    private void HandleResourceChanged()
    {
    ObjectData objectData = DatabaseManager.Instance.databseSO.objectsData[databaseItemID];
    bool requirmentMet = true;

    foreach (BuildRequirement req in objectData.resourceRequirements)
    {
        if(ResourceManager.Instance.GetResourceAmmount(req.resource) < req.amount)
        {
            requirmentMet = false;
            break;
        }
    }

    isAvailable = requirmentMet;

    UpdateAvailabilityUI();
    }

    private void HandleBuildingsChanged()
    {
        ObjectData objectData = DatabaseManager.Instance.databseSO.objectsData[databaseItemID];
        
        foreach(BuildingType dependency in objectData.buildDependency)
        {
            // If building does not have dependencies
            if (dependency == BuildingType.None)
            {
                gameObject.SetActive(true);
                return;
            }

            // Check if dependency exists
            if (ResourceManager.Instance.allExistingBuildings.Contains(dependency) == false)
            {
                gameObject.SetActive(false);
                return;
            }


        }

        //If all requirments met
        gameObject.SetActive(true);
    }
}
