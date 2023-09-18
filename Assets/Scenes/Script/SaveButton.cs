using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public GameObject NameInput;

    public void Start()
    {
        NameInput.SetActive(false);
    }

    public void SaveBtnSetActive()
    {
        NameInput.SetActive(!NameInput.active);
    }
}
