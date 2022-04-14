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
        //changing the cursor according to the hover
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
        //moving the active effector on click
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

        if (_activeEffector != null)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_mode == "Move")
            {
                _activeEffector.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, _activeEffector.transform.position.z);
            }
            else if (_mode == "Resize")
            {
                _activeEffector.GetComponent<CircleShape>().Radius = Vector2.Distance(_activeEffector.position, worldMousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _activeEffector = null;
            _mode = "Null";
        }
    }

    void FixedUpdate()
    {

    }

    #endregion

    #region Main Methods
    #endregion

    #region Privates & Protected

    private Transform _activeEffector;
    private string _mode = "Null"; //Move, Resize, Null
    //private Mode _mode = Mode.NULL;

    #endregion

}