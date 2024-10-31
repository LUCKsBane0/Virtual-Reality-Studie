using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnvSetting
{
    public string envVariableName; // Name of the Env variable (e.g., scenario_count)
    public UnityEvent<int> intSetterEvent;    // UnityEvent to set int variables
    public UnityEvent<string> stringSetterEvent; // UnityEvent to set string variables
}

public class EnvController : MonoBehaviour
{
    [Header("Environment Settings")]
    public Env env; // Reference to the Env ScriptableObject
    public List<EnvSetting> envSettings = new List<EnvSetting>();

    private void Start()
    {
        ApplyEnvSettings();
    }

    private void ApplyEnvSettings()
    {
        foreach (var setting in envSettings)
        {
            // Check if the setting exists in the Env ScriptableObject and apply it
            if (setting.envVariableName == nameof(env.scenario_count))
            {
                InvokeIntEvent(setting, env.scenario_count);
            }
            else if (setting.envVariableName == nameof(env.exportIPAddress))
            {
                InvokeStringEvent(setting, env.exportIPAddress);
            }
            else if (setting.envVariableName == nameof(env.exportFileName))
            {
                InvokeStringEvent(setting, env.exportFileName);
            }
            else if (setting.envVariableName == nameof(env.study_group))
            {
                InvokeStringEvent(setting, env.study_group);
            }
            else if (setting.envVariableName == nameof(env.study_type))
            {
                InvokeStringEvent(setting, env.study_type);
            }
        }
    }

    private void InvokeIntEvent(EnvSetting setting, int value)
    {
        for (int i = 0; i < setting.intSetterEvent.GetPersistentEventCount(); i++)
        {
            var targetObject = setting.intSetterEvent.GetPersistentTarget(i);
            var methodName = setting.intSetterEvent.GetPersistentMethodName(i);

            // Create a new UnityEvent<int> and add the method dynamically
            UnityEvent<int> tempEvent = new UnityEvent<int>();
            tempEvent.AddListener((int val) =>
            {
                targetObject.GetType().GetMethod(methodName)?.Invoke(targetObject, new object[] { val });
            });

            // Invoke the dynamically created event
            tempEvent.Invoke(value);

            // Log the event
            Debug.Log($"[EnvController] Set {setting.envVariableName} with value '{value}' on object '{targetObject}' using function '{methodName}'");
        }
    }

    private void InvokeStringEvent(EnvSetting setting, string value)
    {
        for (int i = 0; i < setting.stringSetterEvent.GetPersistentEventCount(); i++)
        {
            var targetObject = setting.stringSetterEvent.GetPersistentTarget(i);
            var methodName = setting.stringSetterEvent.GetPersistentMethodName(i);

            // Create a new UnityEvent<string> and add the method dynamically
            UnityEvent<string> tempEvent = new UnityEvent<string>();
            tempEvent.AddListener((string val) =>
            {
                targetObject.GetType().GetMethod(methodName)?.Invoke(targetObject, new object[] { val });
            });

            // Invoke the dynamically created event
            tempEvent.Invoke(value);

            // Log the event
            Debug.Log($"[EnvController] Set {setting.envVariableName} with value '{value}' on object '{targetObject}' using function '{methodName}'");
        }
    }
}
