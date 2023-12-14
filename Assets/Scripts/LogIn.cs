using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;

    public Button submitButton;

    public void CallLogIn()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {

            if (www.downloadHandler.text[0] != '0')
            {
                Debug.Log("Error " + www.downloadHandler.text);
            }
            else
            {
                DBManager.username = nameField.text;
                DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[1]);

                SceneManager.LoadScene(0);
            }

        }

    }

    public void VerifyInputs()
    {
        //Submit button is also interactable if the namefield and passwordField have length of minimum 8 characters
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
