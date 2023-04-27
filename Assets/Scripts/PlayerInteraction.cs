using System.Net;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum InteractionType
{
    None,
    Interactive,
    Pickable
}


public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    #region Interaction

    private InteractionType interactionType;
    private GameObject possibleInteraction;
    private Interactive interactive;
    public bool lockInteractions;

    #endregion

    #region Pickup

    [HideInInspector] public GameObject heldObject;
    [HideInInspector] public Rigidbody heldObjectRb;
    [SerializeField] private float pickupForce = 150.0f;
    public Transform holdArea;
    private Pickup pickup;

    #endregion

    #region Raycast

    [SerializeField] private Transform cameraPos;
    private RaycastHit hit;
    private RaycastHit hitObstacle;
    [Range(1f, 50f)][SerializeField] private float distanceInteraction = 5f;
    [SerializeField] private LayerMask interactiveMask = ~0;
    [SerializeField] private LayerMask obstacleMask = ~0;

    #endregion

    #region UI

    private Outline gameObjectOutline;

    #endregion

    #region Debug
    [Space]
    [Header("Debug")]
    [Space]
    [SerializeField] private bool visualDebugging = true;
    [SerializeField] private Color color = new Color(0.75f, 0.2f, 0.2f, 0.75f);
    [Range(1f, 10f)][SerializeField] private float lineWidth = 6f;

    #endregion


    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        cameraPos = Camera.main.transform;
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if(!lockInteractions)
            {
                Interact();
            }
        }
    }

    private void Update()
    {
        if (heldObject == null)
        {
            if (Physics.Raycast(cameraPos.position, cameraPos.transform.forward, out hit, distanceInteraction, interactiveMask, QueryTriggerInteraction.Ignore))
            {
                float distObject = Vector3.Distance(hit.transform.position, cameraPos.transform.position);
                if (!Physics.Raycast(cameraPos.position, cameraPos.transform.forward, out hitObstacle, distObject, obstacleMask, QueryTriggerInteraction.Ignore))
                {
                    if (possibleInteraction == null)
                    {
                        SetInteractive();
                        SetInteractiveUI();
                    }
                }
                else
                {
                    possibleInteraction = null;
                    interactionType = InteractionType.None;
                    DisableInteractiveUI();
                }
            }
            else
            {
                possibleInteraction = null;
                interactionType = InteractionType.None;
                DisableInteractiveUI();
            }
        }
        else if (heldObject != null)
        {
            MoveObject();
        }
    }

    private void Interact()
    {
        if (interactionType == InteractionType.None) return;
        if (interactionType == InteractionType.Interactive)
        {
            interactive.OnInteraction();
            if (interactive.onlyOnce)
            {
                possibleInteraction.GetComponent<Interactive>().enabled = false;
            }
        }
        if (interactionType == InteractionType.Pickable)
        {
            gameObjectOutline.enabled = false;
            pickup.OnInteraction();
        }
    }

    public void SetInteractive()
    {
        possibleInteraction = hit.transform.gameObject;
        if (possibleInteraction.CompareTag("Interactive"))
        {
            interactive = possibleInteraction.GetComponent<Interactive>();
            interactionType = InteractionType.Interactive;
        }
        if (possibleInteraction.CompareTag("Pickup"))
        {
            pickup = possibleInteraction.GetComponent<Pickup>();
            interactionType = InteractionType.Pickable;
        }
    }

    private void SetInteractiveUI()
    {
        gameObjectOutline = possibleInteraction.GetComponent<Outline>();
        gameObjectOutline.enabled = true;
    }

    private void DisableInteractiveUI()
    {
        if(gameObjectOutline != null)
        {
            gameObjectOutline.enabled = false;
            gameObjectOutline = null;
        }
    }

    private void MoveObject()
    {
        if(Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObject.transform.position).normalized;
            heldObjectRb.AddForce(moveDirection * pickupForce);
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (!visualDebugging)
            return;

        Handles.color = color;
        Handles.DrawAAPolyLine(lineWidth,cameraPos.position, cameraPos.position + cameraPos.transform.forward * distanceInteraction);

    }
#endif


}
