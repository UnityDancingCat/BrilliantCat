using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static TutorialText;
using static TutorialStim;

public class SquareImageWithOutlineForTutorial : MonoBehaviour
{
    public Color outlineColor = Color.red;
    public Vector2 outlineDistance = new Vector2(5f, 5f);
    public float moveInterval = 3f;

    private Image image;
    private Outline outline;
    private RectTransform rectTransform;

    public Vector3[] positions;
    public static int currentPosition;

    public List<int> currentPositionIndex;
    public List<int> t;
    public List<int> tutorial1Positon;
    public List<int> tutorial2Positon;
    public List<int> tutorial3Positon;
    public GameObject outlineObj;

    public int tutorialDialogNum;
    public static bool stimEnd;
    public bool startDialog;
    public bool end;

    private void Start()
    {
        /* image = GetComponent<Image>();

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
        tutorial2Positon = new List<int>() {1, 2, 3, 99};
        tutorial3Positon = new List<int>() {3, 3, 99};

        positions = new Vector3[]
        {
            new Vector3(0f, -384f, 0f),
            new Vector3(-753f, 0f, 0f),
            new Vector3(0f, 384f, 0f),
            new Vector3(753f, 0f, 0f),
            new Vector3(1000f, 1000f, 0f)
        };

        tutorialDialogNum = TutorialText.dialogNum;
        startDialog = TutorialText.startStim;
 */
        // StartCoroutine(MoveOutline());

        // SelectScene();
        // SelectTutorial();
        // tutorialDialogNum = TutorialText.dialogNum;
        // SelectTutorial();
    }

    public void OnEnable()
    {
        /* tutorial1Positon = new List<int>() {1, 1, 99};
        tutorial2Positon = new List<int>() {1, 2, 3, 99};
        tutorial3Positon = new List<int>() {3, 3, 99}; */

        // UnityEngine.Debug.Log("on enable");
        // tutorialDialogNum = TutorialText.dialogNum;

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
        tutorial1Positon = new List<int>() {1, 1, 4, 99};
        tutorial2Positon = new List<int>() {4, 1, 2, 3, 4, 99};
        tutorial3Positon = new List<int>() {4, 3, 3, 4, 99};

        positions = new Vector3[]
        {
            new Vector3(0f, -33f, 0f),
            new Vector3(-55f, 0f, 0f),
            new Vector3(0f, 33f, 0f),
            new Vector3(55f, 0f, 0f),
            new Vector3(100f, 100f, 0f)
        };

        SelectTutorial();
    }

    public void OnDisable()
    {
        // UnityEngine.Debug.Log("on disable");
    }

    public void stimEndFunc()
    {
        // UnityEngine.Debug.Log("stimEnd: " + stimEnd);

        if (currentPosition == 99)
        {
            stimEnd = true;
            // outlineObj.SetActive(false);

            // end = TutorialText.dialEnd;
            end = TutorialStim.dialBox;

            if (end == true)
            {
                stimEnd = false;
            }
        }
        else
        {
            stimEnd = false;
            // outlineObj.SetActive(true);
        }
    }

    public void Update()
    {
        // tutorialDialogNum = TutorialText.dialogNum;
        stimEndFunc();
    }

    public void SelectScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        UnityEngine.Debug.Log("Scene Name: " + sceneName);

        /* if (sceneName == "TutorialScene") //Scene -> dial num으로 해보자
        {
            StartCoroutine(MoveOutlineTutorial());
        } */

        if (sceneName == "Lev1Scene")
        {
        }

        else
        {
            SelectTutorial();
        }
    }

    public void SelectTutorial()
    {
        tutorialDialogNum = TutorialText.dialogNum;
        // UnityEngine.Debug.Log("tutorialDialogNum: " + tutorialDialogNum);

        if (tutorialDialogNum == 1)
        {
            // UnityEngine.Debug.Log("tutorial 1 start");
            t = tutorial1Positon;
            StartCoroutine(MoveOutlineTutorial1());
        }
        else if (tutorialDialogNum == 2)
        {
            t = tutorial2Positon;
            StartCoroutine(MoveOutlineTutorial2());
        }
        else if (tutorialDialogNum == 3)
        {
            t = tutorial3Positon;
            StartCoroutine(MoveOutlineTutorial3());
        }
        
        // UnityEngine.Debug.Log("tutorialDialogNum: " + tutorialDialogNum + ", " + t[0]);
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

    private IEnumerator MoveOutlineTutorial1()
    {
        // UnityEngine.Debug.Log(t);

        for(int i=0; i < t.Count; i++)
        {
            if (t[i] != 99)
            {
                yield return new WaitForSeconds(moveInterval);
                currentPosition = t[i];            
                
                Vector3 targetPosition = positions[currentPosition];

                // Move the outline to the target position
                rectTransform.anchoredPosition = targetPosition;

                yield return new WaitForSeconds(moveInterval);

                currentPosition = 4;
                rectTransform.anchoredPosition = positions[currentPosition];   
            }    
            else
            {
                UnityEngine.Debug.Log("Positon: " + t[i]);
                currentPosition = t[i];
            }  
        };
    }

    private IEnumerator MoveOutlineTutorial2()
    {
        for(int i=0; i < t.Count; i++)
        {
            if (t[i] != 99)
            {
                yield return new WaitForSeconds(moveInterval);
                currentPosition = t[i];            
                
                Vector3 targetPosition = positions[currentPosition];

                // Move the outline to the target position
                rectTransform.anchoredPosition = targetPosition;

                yield return new WaitForSeconds(moveInterval);

                currentPosition = 4;
                rectTransform.anchoredPosition = positions[currentPosition];   
            }    
            else
            {
                UnityEngine.Debug.Log("Positon: " + t[i]);
                currentPosition = t[i];
            }  
        };
    }

    private IEnumerator MoveOutlineTutorial3()
    {

        for(int i=0; i < t.Count; i++)
        {
            if (t[i] != 99)
            {
                yield return new WaitForSeconds(moveInterval);
                currentPosition = t[i];            
                
                Vector3 targetPosition = positions[currentPosition];

                // Move the outline to the target position
                rectTransform.anchoredPosition = targetPosition;

                yield return new WaitForSeconds(moveInterval);

                currentPosition = 4;
                rectTransform.anchoredPosition = positions[currentPosition];   
            }    
            else
            {
                UnityEngine.Debug.Log("Positon: " + t[i]);
                currentPosition = t[i];
            }  
        };
    }

}