using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public enum State
{
    FirstUI,
    SecondUI,
    DisableUI
}


public class InteractiveUI : MonoBehaviour
{
    #region UI Setup
    [Header("===== UI Setup =====")]
    [SerializeField] private float distanceUI = 5f;
    [SerializeField] private float distanceText = 2f;
    [SerializeField] private Transform interactionUIPos;

    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject textPrefab;

    [HideInInspector] public State currentState;
    private float startScale;
    private GameObject cam;
    private bool disable = false;

    #endregion

    #region Debug

    [Header("===== Debug =====")]
    [SerializeField] private bool visualDebugging = true;
    [SerializeField] private Color sphereColor = new Color(0.2f, 0.75f, 0.2f, 0.15f);

    #endregion

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        dotPrefab.SetActive(false);
        textPrefab.SetActive(false);
        currentState = State.DisableUI;
        startScale = dotPrefab.transform.localScale.x;
    }

    private void Update()
    {
        float dist;
        dist = Vector3.Distance(cam.transform.position, interactionUIPos.position);


        if (dist <= distanceText)
        {
            currentState = State.FirstUI;
        }
        else if (dist <= distanceUI)
        {
            currentState = State.SecondUI;
        }
        else if (dist > distanceUI)
        {
            currentState = State.DisableUI;
            if(disable)
            {
                Destroy(gameObject);
            }
        }
        

        switch (currentState) {
            case State.FirstUI:
                disable = true;
                dotPrefab.SetActive(false);
                textPrefab.SetActive(true);
                textPrefab.transform.LookAt(cam.transform.position);
                textPrefab.transform.Rotate(new Vector3(90, 0, 0));
                float interactionScale = startScale * dist * 0.5f;
                textPrefab.transform.localScale = new Vector3(interactionScale, interactionScale, interactionScale);
                break;

            case State.SecondUI:
                dotPrefab.SetActive(true);
                textPrefab.SetActive(false);
                dotPrefab.transform.LookAt(cam.transform.position);
                dotPrefab.transform.Rotate(new Vector3(90, 0, 0));
                float dotScale = startScale * dist * 0.5f;
                dotPrefab.transform.localScale = new Vector3(dotScale, dotScale, dotScale);
                break;

            case State.DisableUI:
                dotPrefab.SetActive(false);
                textPrefab.SetActive(false);
                break;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (!visualDebugging) return;
        Handles.color = sphereColor;
        Handles.RadiusHandle(Quaternion.identity, interactionUIPos.position, distanceUI);
        Handles.RadiusHandle(Quaternion.identity, interactionUIPos.position, distanceText);
    }
#endif

    private void OnDestroy()
    {
        Destroy(dotPrefab);
        Destroy(textPrefab);
    }
}