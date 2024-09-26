using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTrigger : Trigger
{
    public int SliderValue = 0;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null && slider.value == SliderValue)
        {
            this.TriggerTargetMet();
        }
    }
}
