using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingShow : MonoBehaviour
{
    public Text InputFieldText;
    //[SerializeField]
    //InputField inputField; //string
    //[SerializeField]
    //Slider slider; //float

    public void Save()
    {
        PlayerPrefs.SetString("StringA",InputFieldText.text);
        //PlayerPrefs.SetFloat("SliderA",slider.value);
    }

    public void Load()
    {
        InputFieldText.text = PlayerPrefs.GetString("StringA");
        //slider.value = PlayerPrefs.GetFloat("SliderA");
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
