using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodDecalPrefab; 
    private ParticleSystem  _bloodparticles;
    private ParticleSystem.Particle[] _particles;
    
    [SerializeField]
    [Tooltip ("A splatter will spawn every X particles dies")]
    private int _numSplatters = 0;

    private void Start()
    {
        _bloodparticles = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[_bloodparticles.main.maxParticles];
    }

    private void LateUpdate()
    {
        int num_particlesAlive = _bloodparticles.GetParticles(_particles);
        GameObject bloodDecalParent = GameObject.Find("BloodDecals");
        if (bloodDecalParent == null)
        {
            bloodDecalParent = new GameObject("BloodDecals");
        }

        for (int i = 0; i < num_particlesAlive; i += _numSplatters)
        {
            if (_particles[i].remainingLifetime <= 0.1f) 
            {
            Vector3 spawnPosition = _particles[i].position;
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            spawnPosition.z = 1;
            Instantiate(_bloodDecalPrefab, spawnPosition, randomRotation, bloodDecalParent.transform);
            }
        }
    }
}
