using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Faire en sorte que _volume ne passe jamais en dessous de zero, ni jamais au dessus de un.

public class MusicBox : MonoBehaviour
{
    //Toutes les variables accessibles dans l'inspector
    #region Exposed
    [SerializeField] private float _volumeRaisePerParticle;
    [SerializeField] private float _volumeDecayPerSecond;
    [SerializeField] private float _volumeDecayDelay;
    #endregion

    #region Unity Life Cycle
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _startDecayTime)
        {
           _volume = Mathf.Clamp01(_volume - _volumeDecayPerSecond * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colision with particle");
        _volume = Mathf.Clamp01(_volume + _volumeRaisePerParticle);
        // la mm chose _volume = Mathf.Clamp(_volume + _volumeRaisePerParticle, 0, 1);
        //on aurait pu mettre ça aussi _volume = Mathf.Min(_volume + _volumeRaisePerParticle, 1);

        _startDecayTime = Time.time + _volumeDecayDelay;

      
    }
    #endregion
    //Toutes les fonctions créées par l'équipe
    #region Main Methods

    #endregion

    //Les variables privées et protégées
    #region Private & Protected
    private float _volume;
    private float _startDecayTime;
    #endregion
}

    // Créer un script MusicBox, et le placer sur le prefab.
    // Implémenter les choses suivantes dans le script:
        // Une variable privée de type float appelée _volume;
        // Faire en sorte que lorsqu'une particule entre en collision avec la MusicBox, _volume augmente d'un certain montant, paramétrable;
        // Si on passe un certain temps, paramétrable, sans collision avec une particule, on baisse _volume d'un certain montant par seconde, paramétrable;
        // Faire en sorte que _volume ne passe jamais en dessous de zero, ni jamais au dessus de un.
