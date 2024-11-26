using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

public class TriggerCube : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> hints;
    [SerializeField] int maxAlpha = 45;
    bool fadeIn;
    float alphaLevel;

    private void Update()
    {
        if (fadeIn)
        {
            foreach(SpriteRenderer hint in hints)
            {
                if(alphaLevel < maxAlpha)
                {
                    alphaLevel += 0.1f;
                    hint.color = new Color(1, 1, 1, alphaLevel * Time.deltaTime);
                }
            }
        }
        else
        {
            foreach (SpriteRenderer hint in hints)
            {
                if(alphaLevel > 0)
                {
                    alphaLevel -= 0.1f;
                    hint.color = new Color(1, 1, 1, alphaLevel * Time.deltaTime);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            fadeIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadeIn = false;
        }
    }
}
