using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    //Toutes les variables accessibles dans l'inspector
    #region Exposed
    [SerializeField] private GameObject _particlePrefab;

    [Header("Spawner Parameters")]
    [SerializeField] private float _spawnerRadius;
    [SerializeField] private float _spawnDelay;
    [Tooltip("Le Transform parent dans lequel sera stocké les particules.")]
    [SerializeField] private Transform _particleContainer;

    [Header("Particles Parameters")]
    [Tooltip("La vitesse initiale de la particule en m/s.")]
    [SerializeField] private float _particleSpeed;
    [Tooltip("Les frottesment à appliquer aux particules.")]
    [SerializeField] private float _particleDrag;

    [Header("Gizmozs")]
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private Color _gizmosColor;
    #endregion

    #region Unity Life Cycle
    void Awake()
    {
        _transform = transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawnTime)
        {
            //Spawn particle
            GameObject newParticle = SpawnParticle();
            //launch particle
            LaunchParticle(newParticle);
            _nextSpawnTime = Time.time + _spawnDelay;
        }
    }

    #endregion
    #region Main Methods
    private GameObject SpawnParticle()
    {
        Vector2 position = Random.insideUnitCircle * _spawnerRadius + (Vector2)_transform.position;
        GameObject particle = Instantiate(_particlePrefab, position, Quaternion.identity, _particleContainer);
        return particle;
    }

    private void LaunchParticle(GameObject particle)
    {
        Rigidbody2D rb2d = particle.GetComponent<Rigidbody2D>();
        rb2d.drag = _particleDrag;
        rb2d.velocity = _transform.right * _particleSpeed;
    }

    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        if(_drawGizmos)
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(transform.position, _spawnerRadius);
            Gizmos.DrawRay(transform.position, transform.right * _particleSpeed);
        }

    }
    #endregion
    //Les variables privées et protégées
    #region Private & Protected
    private float _nextSpawnTime;
    private Transform _transform;
    #endregion
}
