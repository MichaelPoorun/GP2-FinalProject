using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaircaseManager : MonoBehaviour
{
    [SerializeField] GameObject staircase;

    Vector3 teleportSpot;

    private void Start()
    {
        teleportSpot = transform.GetChild(0).position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = staircase.transform.GetComponent<StaircaseManager>().teleportSpot;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy); // Destroy each enemy in the scene
            }
        }
    }
}
