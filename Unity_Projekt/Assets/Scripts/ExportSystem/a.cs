using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonData", menuName = "ScriptableObjects/StudienTeilnehmer", order = 1)]
public class StudienTeilnehmer : ScriptableObject
{
    public string ID;
    public int Alter;
    public string Geschlecht;
    public int VR_Erfahrung; // Changed to int
    public int Gaming_Erfahrung; // Changed to int
    public string StudienTyp;
    public string StudienGruppe;
    public List<string> Szenarien = new List<string>();
    public List<string> Pfad = new List<string>();
    public List<string> Bewertungen = new List<string>();
    public List<float> Zögern_Zeiten = new List<float>();
    public List<float> Szenario_Zeiten = new List<float>();
}
