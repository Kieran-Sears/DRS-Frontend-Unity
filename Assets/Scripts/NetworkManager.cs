using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {

    private static NetworkManager _instance;
    private const string TRAIN_URL = "http://localhost:8080/train";

    public static NetworkManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<NetworkManager>();
                if (_instance == null) {
                    GameObject go = new GameObject();
                    go.name = typeof(NetworkManager).Name;
                    _instance = go.AddComponent<NetworkManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public IEnumerator SendConfigurationRequest(Configurations confs) {
        //var jsonString = JsonUtility.ToJson(confs, true);
        string jsonString = JsonConvert.SerializeObject(confs, new StringEnumConverter());
        Debug.Log(confs.ToString());
        Debug.Log(jsonString);
        UnityWebRequest request = UnityWebRequest.Put(TRAIN_URL, jsonString);
        request.SetRequestHeader("Content-Type", "application/json");
       // request.SetRequestHeader("Accept", "text/json");

        yield return request.SendWebRequest();

        Debug.Log("Response Code: " + request.responseCode);
        if (request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        } else {
            string results = request.downloadHandler.text;
            Debug.Log("Printing results...");
            Debug.Log(request.downloadHandler.text);
           // yield return request.downloadHandler.text;
        }

       
    }

 

}
