using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    private GameObject target;
    private MouseUIController controller;
    GameController gameController;

    public int max_health = 50;
    private int current_health;
    public int attack = 5;
    public float speed = 4;
    public bool is_attacing = false;

    private float zoom = 1f;

    private void Start()
    {
        current_health = max_health;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        controller = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MouseUIController>();

        target = GameObject.FindGameObjectWithTag("Troll");
        if (target == null)
        {
            DestroyKnight();
        }
    }

    private void Update()
    {
        zoom = controller.zoom;

        if (is_attacing == false)
        {
            Move();
        }

        if (current_health <= 0)
        {
            DeathKnight();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Troll")
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

        direction.Normalize();

        transform.position += direction * speed * zoom * Time.deltaTime;
    }

    public void DeathKnight()
    {
        gameController.knight_amount--;
        DestroyKnight();
    }

    private void DestroyKnight()
    {
        Destroy(gameObject, 0f);
    }
}
