using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float deadzone;
    [SerializeField] private float maxSpeed;

    [SerializeField] private Bullet bulletPrefab = null;
    [SerializeField] private Transform shootAt = null;
    [SerializeField] private float shootCooldown;
    [SerializeField] private string collideWithTag = "Untagged";

    private float lastShootTimestamp = Mathf.NegativeInfinity;

    void Update()
    {
        UpdateMovement();
        UpdateActions();
    }

    void UpdateMovement() 
    {
        float move = Input.GetAxis("Horizontal");
        if (Mathf.Abs(move) < deadzone) { return; }

        move = Mathf.Sign(move);
        float delta = move * maxSpeed * Time.deltaTime;

        transform.position = GameManager.Instance.KeepInBounds(transform.position + Vector3.right * delta);
    }

    void UpdateActions()
    {
        if (Input.GetKey(KeyCode.Space) &&  Time.time > lastShootTimestamp + shootCooldown )
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, shootAt.position, Quaternion.identity);
        lastShootTimestamp = Time.time;
        AudioManager.Instance.PlayPlayerSound(1);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != collideWithTag) { return; }

        GameManager.Instance.PlayGameOver();
    }
}
