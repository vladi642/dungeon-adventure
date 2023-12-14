using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;

public class FirebaseController : MonoBehaviour
{

    public GameObject loginPanel, signupPanel, profilePanel, forgetPasswordPanel, notificationPanel;

    public InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, signupUserName, forgetPassEmail;

    public Text notif_Title_Text, notif_Message_Text, profileUserName_Text, profileUserEmail_Text;

    public Toggle rememberMe;

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    bool isSignIn = false;

    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenProfileUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }
    public void OpenForgetPassPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }

    public void LogininUser()
    {
        if (string.IsNullOrEmpty(loginEmail.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            showNotificationMessage("Error", "Fields empty! Please Input Details In All Fields");
            return;
        }

        // Debug log to check the password value
        Debug.Log("Password: " + loginPassword.text);

        SignInUser(loginEmail.text, loginPassword.text);
    }


    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signupEmail.text) && string.IsNullOrEmpty(signupPassword.text) && string.IsNullOrEmpty(signupCPassword.text) && string.IsNullOrEmpty(signupUserName.text))
        {
            showNotificationMessage("Error", "Fields empty! Please Input Details In All Fields");
            return;
        }
        CreateUser(signupEmail.text, signupPassword.text, signupUserName.text);
    }

    public void forgetPass()
    {
        if (string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotificationMessage("Error", "Fields empty! Please Input Details In All Fields");
            return;
        }
    }

    private void showNotificationMessage(string title, string message)
    {
        notif_Title_Text.text = "" + title;
        notif_Message_Text.text = "" + message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotif_Panel()
    {
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";

        notificationPanel.SetActive(false);
    }

    public void LogOut()
    {
        auth.SignOut();
        profileUserEmail_Text.text = "";
        profileUserName_Text.text = "";
        OpenLoginPanel();
    }

    public void CreateUser(string email, string password, string username)
    {
        // Check if the email is properly formatted
        if (!IsValidEmail(email))
        {
            Debug.LogError("Invalid email format.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                // Log detailed error information
                foreach (Exception innerException in task.Exception.InnerExceptions)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + innerException.Message);
                }
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);

            UpdateUserProfile(username);
        });
    }

    // Function to check email format validity
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }


    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
            profileUserName_Text.text = "" + user.DisplayName;
            profileUserEmail_Text.text = "" + user.Email;
            OpenProfileUpPanel();
        });
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    void DebugLog(string message)
    {
        // Define your own DebugLog method if needed
        Debug.Log(message);
    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                DebugLog("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                DebugLog("Signed in " + user.UserId);
                isSignIn = true;
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    void UpdateUserProfile(string UserName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = UserName,
                PhotoUrl = new System.Uri("https://via.placeholder.com/150"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");

                showNotificationMessage("Alert", "Account Successfully Created");
            });
        }
        bool isSigned = false;

        void Update()
        {
            if (isSignIn)
            {
                if (!isSigned)
                {
                    isSigned = true;
                    profileUserName_Text.text = "" + user.DisplayName;
                    profileUserEmail_Text.text = "" + user.Email;
                    OpenProfileUpPanel();
                }
            }
        }
    }
}