using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour
{
    public Button shieldButton;
    private Color originalColor;
    private Renderer playerRenderer;
    private bool isShieldActive = false;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;

        shieldButton.onClick.AddListener(() => StartCoroutine(ActivateShield()));
    }

    IEnumerator ActivateShield()
    {
        isShieldActive = true;
        playerRenderer.material.color = new Color(173f / 255f, 255f / 255f, 47f / 255f); // #ADFF2F - player's color while shield is activated
        yield return new WaitForSeconds(2);
        playerRenderer.material.color = originalColor;
        isShieldActive = false;
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }
}
