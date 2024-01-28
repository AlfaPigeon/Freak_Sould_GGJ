using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIExitButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hovered;
    [SerializeField] private Sprite normal;

    private void OnMouseEnter()
    {
        spriteRenderer.sprite = hovered;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = normal;
    }

    private void OnMouseDown()
    {
        
        Application.Quit();
    }
}
