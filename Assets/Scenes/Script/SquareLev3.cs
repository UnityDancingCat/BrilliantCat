using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static TutorialText;

public class SquareLev3 : MonoBehaviour
{
    public Color outlineColor = Color.blue;
    public Vector2 outlineDistance = new Vector2(5f, 5f);
    public float moveInterval = 7f;

    private Image image;
    private Outline outline;
    private RectTransform rectTransform;

    public Vector3[] positions;
    public static int currentPosition;

    private List<int> currentPositionIndex;
    public List<int> t;
    private List<int> lev3Position2;

    public int tutorialDialogNum;

    private void Start()
    {
        image = GetComponent<Image>();

        // Disable the image component's default outline effect
        image.material = new Material(Shader.Find("UI/Default"));

        outline = gameObject.AddComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.effectDistance = outlineDistance;

        rectTransform = GetComponent<RectTransform>();

        SetImageAlpha(1f);

        // '99' means end of the sitmluation
        lev3Position2 = new List<int>() {0, 2, 3, 1, 3, 1, 0, 2, 99};

        positions = new Vector3[]
        {
            new Vector3(0f, -70f, 0f),
            new Vector3(-140f, 0f, 0f),
            new Vector3(0f, 70f, 0f),
            new Vector3(140f, 0f, 0f),
            new Vector3(1000f, 1000f, 0f)
        };

        tutorialDialogNum = TutorialText.dialogNum;

        // StartCoroutine(MoveOutline());
        SelectScene();
    }

    public void SelectScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        UnityEngine.Debug.Log("Scene Name: " + sceneName);

        if (sceneName == "Lev3Scene")
        {
            StartCoroutine(MoveOutlineLev3());
        }
    }

    public void SetImageAlpha(float alpha)
    {
        // Set the alpha value of the image's material color
        Color materialColor = image.material.color;
        materialColor.a = alpha;
        image.material.color = materialColor;

        // Set the alpha value of the outline's effect color
        Color effectColor = outline.effectColor;
        effectColor.a = 1f; // Ensure the outline color remains fully opaque
        outline.effectColor = effectColor;
    }

    public IEnumerator MoveOutline()
    {
        for(int i=0; i < currentPositionIndex.Count; i++)
        {
            yield return new WaitForSeconds(moveInterval);
            currentPosition = currentPositionIndex[i];            
            Vector3 targetPosition = positions[currentPosition];

            // Move the outline to the target position
            rectTransform.anchoredPosition = targetPosition;
            // UnityEngine.Debug.Log("currentPositionIndex: " + currentPositionIndex[i]);

            yield return new WaitForSeconds(moveInterval);

            currentPosition = 4;
            rectTransform.anchoredPosition = positions[currentPosition];
            // UnityEngine.Debug.Log("currentPositionIndex: " + rectTransform.anchoredPosition);
            
        };
    }

    private IEnumerator MoveOutlineLev3()
    {
        for(int i=0; i < lev3Position2.Count; i++)
        {
            yield return new WaitForSeconds(moveInterval);
            currentPosition = lev3Position2[i];            
            Vector3 targetPosition = positions[currentPosition];

            // Move the outline to the target position
            rectTransform.anchoredPosition = targetPosition;

            yield return new WaitForSeconds(moveInterval);

            currentPosition = 4;
            rectTransform.anchoredPosition = positions[currentPosition];
            
        };
    }
}
