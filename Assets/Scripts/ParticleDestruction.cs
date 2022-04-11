using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestruction : MonoBehaviour
{
    //Toutes les variables accessibles dans l'inspector
    #region Exposed
    [SerializeField] private float _minSpeed;
    #endregion

    #region Unity Life Cycle
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_rigidbody.velocity.magnitude < _minSpeed)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion
    //Toutes les fonctions créées par l'équipe
    #region Main Methods

    #endregion

    //Les variables privées et protégées
    #region Private & Protected
    private Rigidbody2D _rigidbody;
    #endregion
}
