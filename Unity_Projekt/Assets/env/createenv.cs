using UnityEngine;

[CreateAssetMenu(fileName = "EnvSettings", menuName = "ScriptableObjects/Env", order = 1)]
public class Env : ScriptableObject
{
    [Header("General Settings")]
    public int scenario_count = 3;
    [Header("Export Settings")]
    public string exportIPAddress = "http://localhost:3000";
    public string exportFileName = "personData.json";
    
    public string study_group = "NON_REALISTIC";
    public string study_type = "PILOT";
}