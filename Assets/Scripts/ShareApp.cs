using UnityEngine;
using System.Collections;

public class ShareApp : MonoBehaviour
{

    string subject = "Meteor Frenzy";
    string body = "PLAY THIS AWESOME GAME.";

    public void callShareApp()
    {
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("shareText", subject, body);
        Debug.LogError("Share");
    }
}