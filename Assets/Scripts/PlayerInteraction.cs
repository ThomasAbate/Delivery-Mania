using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum InteractionType
{
    None,
    Pickup,
    OpenDoor
}

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;
    private InteractionType _possibleInteraction = InteractionType.None;
    private Interactive _possibleInteractive;

    [HideInInspector] public bool isHolding;
    public Transform holdPosition;

    public GameObject Box;

    private Transform cameraPos;
    private bool doVisibleTest;
    private bool isObjectVisible;
    [SerializeField] private LayerMask layerMask = ~0;


    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        cameraPos = Camera.main.transform;
    }
    public void SetInteraction(InteractionType interaction)
    {
        _possibleInteraction = interaction;
        //montrer ici ou créer un script InteractionHelper qu'il est possible d'intéragir dans le jeu
        //highlight les contours de l'objet, afficher un icone de touche, etc...
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (_possibleInteraction != InteractionType.None && ctx.started)
        {
            if (doVisibleTest && !isObjectVisible) return;
            Interact();
        }
    }

    private void Interact()
    {
        _possibleInteractive.OnInteraction();
        if (_possibleInteractive && _possibleInteractive.onlyOnce)
        {
            DisableInteractive();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Interactive"))
        {
            doVisibleTest = true;
            if(!isHolding)
            {
                Interactive interactive = other.GetComponent<Interactive>();
                if (interactive == null) return;
                _possibleInteractive = interactive;
                SetInteraction(_possibleInteractive.interactionType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Interactive"))
        {
            doVisibleTest = false;
            if(!isHolding) StopInteractive();
        }
    }

    public void StopInteractive()
    {
        SetInteraction(InteractionType.None);
        _possibleInteractive = null;
    }

    private void DisableInteractive()
    {
        _possibleInteractive.GetComponent<SphereCollider>().enabled = false;
        Destroy(_possibleInteractive);
        SetInteraction(InteractionType.None);
    }

    bool InteractionVisible()
    {
        return (Physics.Raycast(cameraPos.position, cameraPos.transform.forward, 25f, layerMask));
    }

    private void Update()
    {
        if (doVisibleTest)
        {
            isObjectVisible = InteractionVisible();
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) 
        {
            if(hit.transform.CompareTag("Interactive"))
            {
                Box = hit.transform.gameObject;
                Box.GetComponent<Outline>().enabled = true;
            }
            else
            {
                Box.GetComponent<Outline>().enabled = false;
                Box = null; 
            }
        }
    }
}
