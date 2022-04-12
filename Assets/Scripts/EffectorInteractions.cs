using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorInteractions : MonoBehaviour
{
    //Toutes les variables accessibles dans l'inspector
    #region Exposed
    [SerializeField] private Texture2D _iconMove;
    [SerializeField] private Texture2D _iconResize;
    [SerializeField] private GameObject _activeEffector;
    #endregion

    #region Unity Life Cycle
    void Awake()
    {
        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hits2d = Physics2D.GetRayIntersection(ray);
            if (hits2d.collider.gameObject.tag == "Effector" && hits2d.collider != null)
            {
                _activeEffector = hits2d.transform.gameObject;
                offset = _activeEffector.transform.position - mousePosition;
            }
        }

        if(Input.GetMouseButtonUp(0) && _activeEffector)
        {
            _activeEffector = null;
        }
    }
    private void FixedUpdate()
    {
        if (_activeEffector)
        {
            _activeEffector.transform.position = mousePosition + offset;
        }
    }
    #endregion
    //Toutes les fonctions créées par l'équipe
    #region Main Methods
    void DetectObject()
    {

    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(_iconMove, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(_iconResize, Vector2.zero, cursorMode);
    }
    #endregion

    //Les variables privées et protégées
    #region Private & Protected
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private Camera mainCamera;
    private Vector3 offset;
    private Vector3 mousePosition;
    #endregion
}
