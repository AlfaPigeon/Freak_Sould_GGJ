using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OnMouseScript : MonoBehaviour
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
        SceneManager.LoadScene("SampleScene");
    }
}
