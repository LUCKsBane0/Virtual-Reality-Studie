using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
   [HideInInspector] 
    public UnityEvent OnTargetMet;  // Event that is invoked when the trigger condition is met

    // This method can be called by child classes when their condition is met
    protected void TriggerTargetMet()
    {
        OnTargetMet?.Invoke();
    }
    public void TriggerTargetMet_DO_USE_WITH_CARE()
    {
        OnTargetMet?.Invoke();
        Debug.LogWarning("Used TriggerTargetMet without a ChildClass of Trigger on Object: " + gameObject.name);
    }
}
