using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerShield : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float shieldDuration = 2f;
    private Color originalColor;
    private Renderer playerRenderer;
    private bool isShieldActive = false;
    private Coroutine shieldCoroutine;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color; // Saving player's original color
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Activate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Deactivate();
    }

    public void Activate()
    {
        if (shieldCoroutine == null) // If shield is not activated
        {
            Debug.Log("Shield activated");
            shieldCoroutine = StartCoroutine(ActivateShield());
        }
    }

    public void Deactivate()
    {
        if (shieldCoroutine != null) // Deactivating shield
        {
            Debug.Log("Shield deactivated");
            StopCoroutine(shieldCoroutine);
            shieldCoroutine = null;
            playerRenderer.material.color = originalColor; // Returning original color
            isShieldActive = false;
        }
    }

    private IEnumerator ActivateShield()
    {
        isShieldActive = true;
        playerRenderer.material.color = new Color(173f / 255f, 255f / 255f, 47f / 255f); // #ADFF2F - player's color with activated shield
        yield return new WaitForSeconds(shieldDuration); // Activating shield for 2 seconds
        playerRenderer.material.color = originalColor; // Returning original color
        isShieldActive = false;
        shieldCoroutine = null;
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }
}
