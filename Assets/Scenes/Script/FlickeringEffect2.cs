using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickeringEffect2 : MonoBehaviour
{
    // 왼쪽: 8.57Hz
    private bool isFlickering = false;
    private Image image;
    private Color originalColor;

    public void OnEnable()
    {
        // UnityEngine.Debug.Log("on enable f2");

        image = GetComponent<Image>();
        originalColor = image.color;

        StartFlickering();
    }

    public void OnDisable()
    {
    // UnityEngine.Debug.Log("on disable f2");

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
            yield return new WaitForSeconds(0.11f);
            //오브젝트 활성화
            image.color = originalColor;
            yield return new WaitForSeconds(0.11f);
        }
    }
}
