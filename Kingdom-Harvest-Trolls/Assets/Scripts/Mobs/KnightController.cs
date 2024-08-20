using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    private GameObject target;

    public int max_health = 50;
    private int current_health;
    public int attack = 5;
    public float speed = 20;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Troll");
        if (target == null)
        {
            DestroyKnight();
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (target == null)
            return;

        // ��������� ����������� ��������
        Vector3 direction = target.transform.position - transform.position;

        // ����������� ������ �����������, ����� �������� ��������� ������
        direction.Normalize();

        // ���������� ����
        transform.position += direction * speed * Time.deltaTime;
    }

    private void DestroyKnight()
    {
        Destroy(gameObject, 0f);
    }
}
