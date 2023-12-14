using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{
    IEnumerator Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "name");
        form.AddField("password", "password");
        UnityWebRequest request = UnityWebRequest.Post("http://localhost/sqlconnect/webtest.php", form);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            string[] webresults = request.downloadHandler.text.Split('\t');
            Debug.Log(webresults[0]);
            foreach (string result in webresults)
            {
                Debug.Log("result is: " + result);
            }

            int webNumber = int.Parse(webresults[0]);
            webNumber *= 2;
            Debug.Log("numberdoubles is: " + webNumber);
        }
        
    }

}
