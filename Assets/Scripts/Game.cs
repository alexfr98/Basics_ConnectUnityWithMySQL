using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerDisplay;
    [SerializeField]
    private TextMeshProUGUI scoreDisplay;

    private void Awake()
    {
        if(DBManager.username == null)
        {
            SceneManager.LoadScene(0);
        }

        playerDisplay.text = "Player " + DBManager.username;
        scoreDisplay.text = "Score: " + DBManager.score;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form);
        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.downloadHandler.text[0] == '0')
            {
                Debug.Log("Game Saved");
            }
            else
            {
                Debug.Log("Save failed. Error #" + www.downloadHandler.text);
            }

            DBManager.LogOut();
            SceneManager.LoadScene(0);
        }
    }

    public void IncreaseScore()
    {
        DBManager.score++;
        scoreDisplay.text = "Score: " + DBManager.score;
    }
}
