using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class database : MonoBehaviour
{
    public InputField Name;
    public InputField Level;

    public Text NameText;
    public Text LevelText;

    private string userID;
    private DatabaseReference dbReference;
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void CreateUser()
    {
        User newUser = new User(Name.text, int.Parse(Level.text));
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNameData = dbReference.Child("users").Child(userID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if(userNameData!=null)
        {
            DataSnapshot snapshot = userNameData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }
    public IEnumerator GetLevel(Action<int> onCallback)
    {
        var userLevelData = dbReference.Child("users").Child(userID).Child("Level").GetValueAsync();

        yield return new WaitUntil(predicate: () => userLevelData.IsCompleted);

        if (userLevelData != null)
        {
            DataSnapshot snapshot = userLevelData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }
    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name) =>
        {
            NameText.text = "Name: "+name;
        }));

        StartCoroutine(GetLevel((int Level) =>
        {
            LevelText.text = "Level: " + Level.ToString();
        }));
    }
}