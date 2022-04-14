using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum Mode
{
    MOVE,
    RESIZE,
    NULL
}*/
public class MouseManager : MonoBehaviour
{
    #region Exposed
    [SerializeField] private LayerMask _layerMask;
    [Header("Cursor Texture")]
    [SerializeField] private Texture2D _mouseMoveTexture;
    [SerializeField] private Texture2D _mouseResizeTexture;

    public float _limit = 2f;
    #endregion

    #region Unity Lifecycle

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _layerMask);
        hit = ChangeCursor(hit);
        ActiveEffector(hit);
        if (_activeEffector != null)
        {
            MoveEffector();
            ResizeEffector();
        }
        ResetCursor();
    }
    void FixedUpdate()
    {

    }
    #endregion

    #region Main Methods
    //changing the cursor according to the hover
    private RaycastHit2D ChangeCursor(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Move"))
            {
                Cursor.SetCursor(_mouseMoveTexture, new Vector2(16f, 16f), CursorMode.Auto);
            }
            else if (hit.collider.CompareTag("Resize"))
            {
                Cursor.SetCursor(_mouseResizeTexture, new Vector2(16f, 16f), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
        return hit;
    }
    //define the active effector Move or Resize
    private void ActiveEffector(RaycastHit2D hit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Move"))
                {
                    _activeEffector = hit.collider.transform.parent;
                }
                else
                {
                    _activeEffector = hit.collider.transform;
                }
                _mode = hit.collider.tag;
            }
        }
    }
    //si mon effector actif est tagué move alors je le déplace
    private void MoveEffector()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_mode == "Move")
        {
            _activeEffector.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, _activeEffector.transform.position.z);
        }
    }

    //si mon effector actif est tagué resize alors j'augmente le rayon de mon effector
    private void ResizeEffector()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_mode == "Resize")
        {
            _activeEffector.GetComponent<CircleShape>().Radius = Vector2.Distance(_activeEffector.position, worldMousePosition);
        }
    }
    private void ResetCursor()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _activeEffector = null;
            _mode = "Null";
        }
    }
    #endregion

    #region Privates & Protected
    private Transform _activeEffector;
    private string _mode = "Null"; //Move, Resize, Null
    //private Mode _mode = Mode.NULL;
    #endregion
}