using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    //Toutes les variables accessibles dans l'inspector
    #region Exposed
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private int _amountOfParticle;

    #endregion

    #region Unity Life Cycle
    void Awake()
    {
        // On initialise le tableau de particules à la bonne taille.
        _particles = new GameObject[_amountOfParticle];
        // On effectue une boucle afin d'instancier toutes les particules.
        // On les désactive immédiatement.
        for (int i = 0; i < _amountOfParticle; i++)
        {
            _particles[i] = Instantiate(_particlePrefab, transform);
            _particles[i].SetActive(false);
        }
    }
    #endregion
    //Toutes les fonctions créées par l'équipe
    #region Main Methods
    public GameObject GetParticle()
    {
        for (int i = 0; i < _amountOfParticle; i++)
        {
            // Si on trouve une particule désactivée, c'est qu'on peut l'utiliser, on retourne donc sa référence.
            if (!_particles[i].activeInHierarchy)
            {
                return _particles[i];
            }
        }
        // Si notre code s'execute jusqu'ici, c'est que toutes les particules sont deja utilisées.
        // Soit notre pool est trop petite, soit on a un soucis ailleurs dans le projet et on ne désactive pas les particules qui ne sont plus utilisées.
        // On retourne donc une référence null puisqu'on est en rupture de stock de particules.
        return null;
    }
    #endregion

    //Les variables privées et protégées
    #region Private & Protected
    private GameObject[] _particles;
    #endregion
}
