using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static TutorialText;

public class SquareImageWithOutline : MonoBehaviour
{
    public Color outlineColor = Color.red;
    public Vector2 outlineDistance = new Vector2(5f, 5f);
    public float moveInterval = 3f;

    private Image image;
    private Outline outline;
    private RectTransform rectTransform;

    public Vector3[] positions;
    public static int currentPosition;

    public static string sceneName;

    private List<int> currentPositionIndex;
    public List<int> t;
    public List<int> tutorial1Positon;
    public List<int> tutorial2Positon;
    public List<int> lev1Position;
    public List<int> lev2Position;
    private List<int> lev3Position;
    public List<int> lev4Position;

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
        currentPositionIndex = new List<int>() {0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 99};
        // currentPositionIndex = new List<int>() {4, 0, 4, 1, 4, 1, 4, 1, 4, 2, 4, 3, 4, 0, 4, 3, 4, 1, 4, 2, 4, 0, 4, 3, 4};
        tutorial1Positon = new List<int>() {1, 1, 99};
        tutorial2Positon = new List<int>() {1, 3, 2, 99};
        // lev1Position = new List<int>() {1, 99}; //1, 2, 2, 3, 0, 1, 3, 0, 1, 0, 1, 2, 3, 0, 1, 2, 2, 3, 3, 0, 1, 2,
        lev1Position = new List<int>() {0, 2, 2, 1, 1, 3, 3, 99}; //1, 2, 2, 3, 0, 1, 3, 0, 1, 0, 1, 2, 3, 0, 1, 2, 2, 3, 3, 0, 1, 2,
        lev2Position = new List<int>() {1, 2, 3, 0, 3, 0, 2, 3, 99};
        //lev3Position = new List<int>() {1, 2, 3, 4, 2, 3, 4, 1, 2, 3, 1, 2, 1, 2, 3, 4, 3, 4, 2, 3, 4, 1, 2, 3, 4, 99};
        lev3Position = new List<int>() {3, 1, 2, 0, 2, 0, 3, 1, 99};

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
        sceneName = scene.name;
        UnityEngine.Debug.Log("Scene Name: " + sceneName);

        if (sceneName == "TutorialScene")
        {
            StartCoroutine(MoveOutlineTutorial());
        }
        else if (sceneName == "Lev1Scene")
        {
            StartCoroutine(MoveOutlineLev1());
        }
        else if (sceneName == "Lev2Scene")
        {
            StartCoroutine(MoveOutlineLev2());
        }
        else if (sceneName == "Lev3Scene")
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

    private IEnumerator MoveOutlineTutorial()
    {
        if (tutorialDialogNum == 1) {t = tutorial1Positon;}
        else if (tutorialDialogNum == 2) {t = tutorial2Positon;}

        //UnityEngine.Debug.Log(t[0]);

        for(int i=0; i < t.Count; i++)
        {
            UnityEngine.Debug.Log(t.Count);
            yield return new WaitForSeconds(moveInterval);
            currentPosition = t[i];            
            Vector3 targetPosition = positions[currentPosition];

            // Move the outline to the target position
            rectTransform.anchoredPosition = targetPosition;

            yield return new WaitForSeconds(moveInterval);

            currentPosition = 4;
            rectTransform.anchoredPosition = positions[currentPosition];
            
        };
    }

    private IEnumerator MoveOutlineLev1()
    {
        for(int i=0; i < lev1Position.Count; i++)
        {
            if (lev1Position[i] == 99)
            {
                UnityEngine.Debug.Log("Lev1Positon: " + lev1Position[i]);
                currentPosition = lev1Position[i];
                break;
            }     
            yield return new WaitForSeconds(moveInterval);
            currentPosition = lev1Position[i];            
            
            Vector3 targetPosition = positions[currentPosition];

            // Move the outline to the target position
            rectTransform.anchoredPosition = targetPosition;

            yield return new WaitForSeconds(moveInterval);

            currentPosition = 4;
            rectTransform.anchoredPosition = positions[currentPosition];       
        };
    }

    private IEnumerator MoveOutlineLev2()
    {
        for(int i=0; i < lev2Position.Count; i++)
        {
            if (lev2Position[i] == 99)
            {
                UnityEngine.Debug.Log("Lev2Positon: " + lev2Position[i]);
                currentPosition = lev2Position[i];
                break;
            } 
            yield return new WaitForSeconds(moveInterval);
            currentPosition = lev2Position[i];            
            Vector3 targetPosition = positions[currentPosition];

            // Move the outline to the target position
            rectTransform.anchoredPosition = targetPosition;

            yield return new WaitForSeconds(moveInterval);

            currentPosition = 4;
            rectTransform.anchoredPosition = positions[currentPosition];
            
        };
    }

    private IEnumerator MoveOutlineLev3()
    {
        for(int i=0; i < lev3Position.Count; i++)
        {
            if (lev3Position[i] == 99)
            {
                UnityEngine.Debug.Log("Lev2Positon: " + lev3Position[i]);
                currentPosition = lev3Position[i];
                break;
            } 
            yield return new WaitForSeconds(moveInterval);
            currentPosition = lev3Position[i];            
            Vector3 targetPosition = positions[currentPosition];

            // Move the outline to the target position
            rectTransform.anchoredPosition = targetPosition;

            yield return new WaitForSeconds(moveInterval);

            currentPosition = 4;
            rectTransform.anchoredPosition = positions[currentPosition];
            
        };
    }

}