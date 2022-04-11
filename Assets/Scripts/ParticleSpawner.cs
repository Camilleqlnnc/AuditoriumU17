using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    //Toutes les variables accessibles dans l'inspector
    #region Exposed
    [Tooltip("Le pool de particules dans lequel aller chercher les particules.")]
    [SerializeField] private ParticlePool _pool;

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
        //calcul a rand position in a cercle with _spawnerRadius radius, centered on the spawer
        Vector2 position = Random.insideUnitCircle * _spawnerRadius + (Vector2)_transform.position;
        //get a particle in the pool
        GameObject particle = _pool.GetParticle();

        if(particle != null)
        {
            particle.SetActive(true);
            particle.transform.position = position;
            particle.GetComponent<TrailRenderer>().Clear();           
        }
        return particle;
    }

    private void LaunchParticle(GameObject particle)
    {
        if(particle != null)
        {
            Rigidbody2D rb2d = particle.GetComponent<Rigidbody2D>();
            rb2d.drag = _particleDrag;
            rb2d.velocity = _transform.right * _particleSpeed;
        }

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
