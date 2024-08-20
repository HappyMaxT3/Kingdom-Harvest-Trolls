using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollController : MonoBehaviour
{
    public GameObject target;
    private MouseUIController controller;
    GameController gameController;

    public int max_health = 50;
    private int current_health;
    public int attack = 6;
    public float speed = 10;
    public bool is_attacing = false;

    private float zoom = 1f;

    private bool is_flipped = false;

    private void Start()
    {
        current_health = max_health;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        controller = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MouseUIController>();
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Knight");

        zoom = controller.zoom;

        if (is_attacing == false)
        {
            Move();
        }

        if (current_health <= 0)
        {
            DeathTroll();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISION");
        if (collision.gameObject.tag == "Knight")
        {
            is_attacing = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knight")
        {
            is_attacing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        is_attacing = false;
    }

    private void Move()
    {
        if (target == null)
            return;

        Vector3 direction = target.transform.position - transform.position;

        if ((direction.x < 0) && (is_flipped == false))
        {
            Flip();
        }
        if ((direction.x > 0) && (is_flipped == true))
        {
            Flip();
        }

        direction.Normalize();

        transform.position += direction * speed * zoom * Time.deltaTime;
    }

    public void Flip()
    {
        Vector3 scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        is_flipped = !is_flipped;
    }

    public void TakeDamage(int amount)
    {
        current_health -= amount;
    }

    public void DeathTroll()
    {
        gameController.knight_amount--;
        DestroyTroll();
    }

    private void DestroyTroll()
    {
        Destroy(gameObject, 0f);
    }
}
