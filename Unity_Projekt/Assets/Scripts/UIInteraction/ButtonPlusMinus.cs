using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPlusMinus : MonoBehaviour
{
    public Slider slider;
    public bool plus;
    // Start is called before the first frame update
    public void ChangeVal()
    {
        if (plus)
        {
            if(slider.value+1 <= slider.maxValue)
            {
                slider.value++;
            }
        }
        else
        {
            if (slider.value - 1 >= slider.minValue)
            {
                slider.value--;
            }
        }
    }
}
