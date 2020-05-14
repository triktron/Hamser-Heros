using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockeble : MonoBehaviour
{
    public string Name;
    public bool Unlocked;
    public bool Selecteble;
    public string Need;
    public int Cost;
    public bool UnderConstruction;

    public void UpdateValues()
    {
        Unlocked = Unlocked || PlayerPrefs.GetInt("Level" + Name + "Unlocked", 0) == 1;
        Selecteble = Selecteble || PlayerPrefs.GetInt("Level" + Need + "Unlocked", 0) == 1;
    }
}
