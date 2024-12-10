using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D RB;

    public SpriteRenderer SR;

    private float speed = 3f;

    private float mx;

    private float my;

    private Vector2 mousePos;

    public AudioClip TakingDamage;
    public AudioSource AS;

    [SerializeField] float health, maxHealth = 3f;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        //Make a variable of the type
        Vector2 vel = new Vector2(0, 0);

        //Figure out what value you want
        if (Input.GetKey(KeyCode.D))
        {
            vel.x = 2;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -2;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vel.y = 2;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vel.y = -2;
        }

        //Plug it into the component
        RB.velocity = vel;
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(mx, my).normalized * speed;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Defeat");

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            AS.PlayOneShot(TakingDamage);
            Debug.Log("TookDamage");
        }
    }
}