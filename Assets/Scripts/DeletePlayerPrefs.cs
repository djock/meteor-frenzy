using UnityEngine;
using System.Collections;

public class DeletePlayerPrefs : MonoBehaviour {

    void OnClick()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted all player prefs.");
    }
}
