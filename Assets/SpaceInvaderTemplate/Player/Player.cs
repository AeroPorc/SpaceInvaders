using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float deadzone;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private Transform shootAt = null;
    [SerializeField] private float shootCooldown;
    [SerializeField] private string collideWithTag = "Untagged";

    [SerializeField] private int _health = 3;

    private GameObject _camera;

    private float lastShootTimestamp = Mathf.NegativeInfinity;
    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Update()
    {
        UpdateMovement();
        UpdateActions();
    }

    void UpdateMovement() 
    {
        float move = Input.GetAxis("Horizontal");
        if (Mathf.Abs(move) < deadzone) { return; }

        float targetSpeed = move * maxSpeed; 
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        float delta = currentSpeed * Time.deltaTime;
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
        
        Destroy(collision.gameObject);
        _camera.GetComponent<CameraShake>().shakecamera();

        _health--;
        if (_health <= 0)
        {
            GameManager.Instance.PlayGameOver();
        }
    }
}
