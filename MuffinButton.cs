using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MuffinButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void OnMuffinClicked()
    {
        gameManager.OnMuffinButtonClicked();

        // Can be added here:
        // Animate the button or
        // Add sound to the button

    }

}