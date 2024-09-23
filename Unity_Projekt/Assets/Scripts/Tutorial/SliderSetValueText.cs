using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetValueText : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Slider slider;
    // Start is called before the first frame update
    public void ChangeText(){
        timerText.SetText(slider.value.ToString());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
