using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] float health = 3f;

    [SerializeField] AudioClip hurtSound;
    [Range(0f, 100f)]
    [SerializeField] float volume = 0.5f;

    private void Start()
    {
        
    }

    public void TakeDamage(float damage)
    { 
        
    }
    
}
