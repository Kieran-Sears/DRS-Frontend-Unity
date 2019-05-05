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

    IEnumerator SendConfigurationRequest(Configurations confs) {
        var jsonString = JsonUtility.ToJson(confs) ?? "";
        UnityWebRequest request = UnityWebRequest.Post(TRAIN_URL, jsonString);
        request.SetRequestHeader("Content-Type", "application/json");

        if (request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        } else {
            Debug.Log("Form upload complete!");
        }

        yield return request.Send();
    }
}

public class Customer {

}