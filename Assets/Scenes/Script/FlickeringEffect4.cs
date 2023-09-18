using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickeringEffect4 : MonoBehaviour
{
    // 위: 12Hz
    private bool isFlickering = false;
    private Image image;
    private Color originalColor;

    public void OnEnable()
    {
        // UnityEngine.Debug.Log("on enable f4");

        image = GetComponent<Image>();
        originalColor = image.color;

        StartFlickering();
    }

    public void OnDisable()
    {
        // UnityEngine.Debug.Log("on disable f4");

        StopFlickering();
    }

    private void Start()
    {

    }

    private void StartFlickering()
    {
        if (isFlickering)
            return;

        isFlickering = true;
        StartCoroutine(Flicker());
    }

    public void StopFlickering()
    {
        if (!isFlickering)
            return;

        isFlickering = false;
        StopCoroutine(Flicker());
        image.color = originalColor;
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            //오브젝트 비활성화
            image.color = Color.clear;
            yield return new WaitForSeconds(0.08f);
            //오브젝트 활성화
            image.color = originalColor;
            yield return new WaitForSeconds(0.08f);
        }
    }
}

