using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FirebaseController : MonoBehaviour
{
    public GameObject loginPanel, signupPanel, profilePanel;
    public InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, signupUserName;

    private FirebaseAuth auth;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
        });
    }

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
    }

    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilePanel.SetActive(false);
    }

    public void OpenProfileUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signupEmail.text) || string.IsNullOrEmpty(signupPassword.text) || string.IsNullOrEmpty(signupCPassword.text) || string.IsNullOrEmpty(signupUserName.text))
        {
            Debug.LogError("Please fill in all the required fields.");
            return;
        }

        if (signupPassword.text != signupCPassword.text)
        {
            Debug.LogError("Passwords do not match.");
            return;
        }

        // Call the function to sign up the user
        SignUpWithEmailAndPassword(signupEmail.text, signupPassword.text);
    }

    private async void SignUpWithEmailAndPassword(string email, string password)
    {
        try
        {
            AuthResult authResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            FirebaseUser newUser = authResult.User;

            // User creation is successful
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        }
        catch (FirebaseException e)
        {
            Debug.LogError("Error creating user: " + e.Message);
        }
    }
}
