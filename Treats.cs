using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treats : MonoBehaviour
{
    public int pointsPerClick = 50;
    public GameManager gameManager;

    private Vector3 startScale;

    private float scaleDuration = 2f;
    private float currentTime = 0;
    private float tValue;

    private void Start()
    {
        startScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(startScale, Vector3.zero, currentTime);

        currentTime += Time.deltaTime / scaleDuration; // duration will now go from 0 to 1

        if (currentTime > scaleDuration)
        {
            Destroy(gameObject);
        }
    }

    public void OnTreatClicked()
    {
        gameManager.TreatClicked(pointsPerClick);
        Destroy(gameObject);
    }

}
