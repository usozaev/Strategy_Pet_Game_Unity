using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{



    public static SoundManager Instance {get; set;}

    [Header ("Infantry Attacks")]
    private AudioSource infantyAttackChannel;
    public AudioClip infantyAttackClip;


    [Header("Unit")]
    private AudioSource unitCommandChannel;
    private AudioSource unitSelectionChannel;

    public AudioClip unitCommandSound;
    public AudioClip unitSelectionSound;


    [Header("Buildings")]
    private AudioSource extraBuildingChannel;
    private AudioSource destructionBuildingChannel;
    private AudioSource constructionBuildingChannel;

    public AudioClip sellingSound;
    public AudioClip buildingConstructionSound;
    public AudioClip buildingDestructionSound;






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

        infantyAttackChannel = gameObject.AddComponent<AudioSource>();
        infantyAttackChannel.volume = 0.1f;
        infantyAttackChannel.playOnAwake = false;

        unitSelectionChannel = gameObject.AddComponent<AudioSource>();
        unitSelectionChannel.volume = 0.1f;
        unitSelectionChannel.playOnAwake = false;

        unitCommandChannel = gameObject.AddComponent<AudioSource>();
        unitCommandChannel.volume = 0.1f;
        unitCommandChannel.playOnAwake = false;

        destructionBuildingChannel = gameObject.AddComponent<AudioSource>();
        destructionBuildingChannel.volume = 0.1f;
        destructionBuildingChannel.playOnAwake = false;

        extraBuildingChannel = gameObject.AddComponent<AudioSource>();
        extraBuildingChannel.volume = 0.1f;
        extraBuildingChannel.playOnAwake = false;


        constructionBuildingChannel = gameObject.AddComponent<AudioSource>();
        constructionBuildingChannel.volume = 0.1f;
        constructionBuildingChannel.playOnAwake = false;

    }

    public void PlayInfantryAttackSound()
    {
        if(infantyAttackChannel.isPlaying == false)
        {
            infantyAttackChannel.PlayOneShot(infantyAttackClip);
        }
    }

        public void PlayBuildingSellingSound()
    {
        if(extraBuildingChannel.isPlaying == false)
        {
            extraBuildingChannel.PlayOneShot(sellingSound);
        }
    }

        public void PlayBuildingConstructionSound()
    {
        if(constructionBuildingChannel.isPlaying == false)
        {
            constructionBuildingChannel.PlayOneShot(buildingConstructionSound);
        }
    }

        public void PlayBuildingDestructionSound()
    {

        if(destructionBuildingChannel.isPlaying == false)
        {
            destructionBuildingChannel.PlayOneShot(buildingDestructionSound);
        }
    }

    public void PlayUnitSelectionSound()
    {

        if(unitSelectionChannel.isPlaying == false)
        {
            unitSelectionChannel.PlayOneShot(unitSelectionSound);
        }
    }
    public void PlayUnitCommandSound()
    {

        if(unitCommandChannel.isPlaying == false)
        {
            unitCommandChannel.PlayOneShot(unitCommandSound);
        }
    }
}
