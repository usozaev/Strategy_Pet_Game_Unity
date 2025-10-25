using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance {get; set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;;
        }
    }

    public int credits;

    public event Action OnResourceChanged;
    public event Action OnBuildingsChanged;

    public TextMeshProUGUI creditsUI;

    public List<BuildingType> allExistingBuildings;
    public PlacementSystem placementSystem;


    public enum ResourcesType
    {
        Credits
    }

    public int getCredits()
    {
        return credits;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateBuildingChanged(BuildingType buildingType, bool isNew, Vector3 position)
    {
        if(isNew)
        {
            // For Building
            allExistingBuildings.Add(buildingType);

            SoundManager.Instance.PlayBuildingConstructionSound();
        }
        else
        {
            //For destruction and selling
            placementSystem.RemovePlacementData(position);
            allExistingBuildings.Remove(buildingType);
        }
    OnBuildingsChanged?.Invoke();
    }


    public void increaseResource(ResourcesType resource, int amountToIncrease)
    {
        switch(resource)
        {
            case ResourcesType.Credits:
                credits += amountToIncrease;
                break;
            default:
                break;
        }

        OnResourceChanged?.Invoke();
    }

    public void SellBuilding(BuildingType buildingType)
    {
        SoundManager.Instance.PlayBuildingSellingSound();

        var sellingPrice = 0;
        foreach(ObjectData obj in DatabaseManager.Instance.databseSO.objectsData)
        {
            if(obj.thisBuidingType ==  buildingType)
            {
                foreach(BuildRequirement req in obj.resourceRequirements)
                {
                    if(req.resource == ResourcesType.Credits)
                    {
                        sellingPrice = req.amount;
                    }
                }
            }
        }
        int amountToReturn = (int)(sellingPrice * 0.90f);

        increaseResource(ResourcesType.Credits, amountToReturn);
    }

    public void decreaseResource(ResourcesType resource, int amountToDecrease)
    {
        switch(resource)
        {
            case ResourcesType.Credits:
                credits -= amountToDecrease;
                break;
            default:
                break;
        }
        OnResourceChanged?.Invoke();
    }


    internal int GetResourceAmmount(ResourcesType resource)
    {
        switch (resource)
        {
            case ResourcesType.Credits:
                return credits;
            default: 
                break;
        }
        return 0;
    }

    internal void DecreaseResourcesBasedOnRequirment(ObjectData objectData)
    {
        foreach(BuildRequirement req in objectData.resourceRequirements)
        {
            decreaseResource(req.resource, req.amount);
        }
    }


// Credits UI Manager

    private void OnEnable()
    {
        OnResourceChanged += UpdateUI;
    }
        private void OnDisable()
    {
        OnResourceChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        creditsUI.text = $"{credits}";
    }

}
