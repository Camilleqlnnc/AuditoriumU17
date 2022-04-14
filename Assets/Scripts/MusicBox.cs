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
    [SerializeField] private SpriteRenderer[] _volumeBars;
    [SerializeField] private Color _enabledColor;
    [SerializeField] private Color _disabledColor;
    [SerializeField] private float _timeToWin;
    #endregion

    #region Unity Life Cycle
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Si l'heure actuelle dépasse l'heure à partir de laquelle on doit baisser le volume.
        if (Time.time > _startDecayTime)
        {
            // On baisse le volume et on limite le résultat entre zero et un.
            _volume = Mathf.Clamp01(_volume - _volumeDecayPerSecond * Time.deltaTime);
        }
        // On change le volume de l'audioSource par la valeur contenue dans _volume.
        _audioSource.volume = _volume;
        // On met à jour l'affichage des barres de volume.
        UpdateRenderers();
        Win();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // On augmente le volume et on limite le résultat entre zero et un.
        _volume = Mathf.Clamp01(_volume + _volumeRaisePerParticle);
        // la mm chose _volume = Mathf.Clamp(_volume + _volumeRaisePerParticle, 0, 1);
        //on aurait pu mettre ça aussi _volume = Mathf.Min(_volume + _volumeRaisePerParticle, 1);

        // On calcule l'heure à partir de laquelle on devra baisser le volume.
        _startDecayTime = Time.time + _volumeDecayDelay;
    }
    #endregion
    //Toutes les fonctions créées par l'équipe
    #region Main Methods
    private void UpdateRenderers()
    {
        // On compte le nombre de barres à activer et on arrondi à l'entier inférieur.
        barsToEnable = Mathf.FloorToInt(_volumeBars.Length * _volume);

        // On boucle sur le tableau contenant les spriteRenderers des barres.
        for (int i = 0; i < _volumeBars.Length; i++)
        {
            // Si l'index actuel est inférieur au nombre de barre à allumer.
            if (i<barsToEnable)
            {
                // On active la barre.
                _volumeBars[i].color = _enabledColor;
            }
            else
            {
                // On éteint la barre.
                _volumeBars[i].color = _disabledColor;
            }

        }
    }
    private void Win()
    {
        if(barsToEnable == Mathf.FloorToInt(_volumeBars.Length) && _volume == 1 )
        {
            if (timerIsRunning)
            {
                if(_timeToWin > 0)
                {
                    _timeToWin -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("You win");
                    _timeToWin = 0;
                    timerIsRunning = false;
                }
            }
        }
    }
    #endregion

    //Les variables privées et protégées
    #region Private & Protected
    private float _volume;
    private float _startDecayTime;
    private AudioSource _audioSource;
    private int barsToEnable;
    private bool timerIsRunning = false;
    #endregion
}