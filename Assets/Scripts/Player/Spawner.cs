using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject Ghost;

    public AudioClip spawnSound;

    public AudioSource spawnSoundSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the Ghost at the SpawnPoint's position and rotation
            Instantiate(Ghost, SpawnPoint.position, SpawnPoint.rotation);

            // Play the spawn sound
            spawnSoundSource.PlayOneShot(spawnSound);

            // Start the coroutine to wait for the sound to finish before destroying the object
            StartCoroutine(DestroyAfterSound());
        }
    }

    // Coroutine to delay the destruction of the object
    private IEnumerator DestroyAfterSound()
    {
        // Wait for the sound to finish playing
        yield return new WaitForSeconds(spawnSound.length);

        // Destroy the object this script is attached to
        Destroy(gameObject);
    }
}