using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreditsButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hovered;
    [SerializeField] private Sprite normal;
    [SerializeField] private SpriteRenderer creditNames;

    private void Start()
    {
        creditNames.enabled = false;
    }

    private void OnMouseEnter()
    {
        creditNames.enabled = true;
        spriteRenderer.sprite = hovered;
    }

    private void OnMouseExit()
    {
        creditNames.enabled = false;
        spriteRenderer.sprite = normal;
    }
}
