using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float speed;
    //public float lerpValue;
    //public float tValue;
    public float lifetime = 3;

    private TMP_Text floatingText;
    private Color initialColor;
    private float age = 0;

    void Start()
    {
        floatingText = GetComponent<TMP_Text>();
        initialColor = floatingText.color;
    }

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);

        // Time.delaTime is the time that passes between each frame so everytime the 
        // Update() function runs, Time.deltaTime has the amount of time that has
        // passed since we were last here (like a stopwatch)
        age += Time.deltaTime;

        floatingText.color = Color.Lerp(initialColor, Color.clear, age / lifetime);

        if (age > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
