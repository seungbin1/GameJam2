using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Obstacle
{
    [SerializeField] private float speed = 2f;

    void OnEnable()
    {
        Spawn();
    }

    void Update()
    {
        Move(speed+GameManager.Instance.SpeedUP);
        Return();
        print(speed);
    }

    protected override void Move(float speed)
    {
        transform.position -= Vector3.right * Time.deltaTime * (speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnDamage();
            collision.transform.GetComponent<Player_Die>().Die();
        }
    }
}
