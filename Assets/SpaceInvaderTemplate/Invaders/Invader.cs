using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab = null;
    [SerializeField] private Transform shootAt = null;
    [SerializeField] private string collideWithTag = "Player";

    [SerializeField] private GameObject bloodParticlePrefab = null;

    internal Action<Invader> onDestroy;

    public Vector2Int GridIndex { get; private set; }

    public void Initialize(Vector2Int gridIndex)
    {
        this.GridIndex = gridIndex;
    }

    public void OnDestroy()
    {
        onDestroy?.Invoke(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != collideWithTag) { return; }
        AudioSource audio = AudioManager.Instance.PlayPlayerSound(2);
        Destroy(audio, 0.5f);
        Instantiate(bloodParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, shootAt.position, Quaternion.identity);
        AudioSource audio = AudioManager.Instance.PlayPlayerSound(0);
        Destroy(audio, 0.5f);
    }
}
