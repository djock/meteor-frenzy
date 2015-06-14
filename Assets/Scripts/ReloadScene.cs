using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour
{

    void OnClick()
    {
        StartCoroutine(GoToMenu());
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Application reloaded");
        Application.LoadLevel("game");
    }

}
