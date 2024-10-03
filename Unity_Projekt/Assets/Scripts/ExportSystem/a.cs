using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PersonData", menuName = "ScriptableObjects/StudienTeilnehmer", order = 1)]
public class StudienTeilnehmer:ScriptableObject
{
    public string ID;
    public int Alter;
    public string Geschlecht;
    public bool VR_Erfahrung;
    public bool Gaming_Erfahrung;
    public string StudienTyp;
    public string StudienGruppe;
    public List<string> Szenarien;
    public List<string> Pfad;
    public List<string> Bewertungen;
    public List<float> Zögern_Zeiten;
    public List<float> Szenario_Zeiten;
    //Hier noch Herzfrequenz und Galvanischer Wert
}
