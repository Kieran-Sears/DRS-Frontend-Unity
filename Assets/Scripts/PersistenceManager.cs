using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class PersistenceManager : MonoBehaviour
{

    public static PersistenceManager instance { get; private set; }

    TextAsset _xml;
    XmlSerializer serializer;
    StringReader reader;
    Configurations configuration;

    private void Awake() {
        if (instance) {
            DestroyImmediate(this);
        }
        else {
            instance = this;
        }
    }

    public System.Object Load(string path) {

        switch (path) {
            case "configurations":
                _xml = Resources.Load<TextAsset>(path);
                serializer = new XmlSerializer(typeof(Configurations));
                reader = new StringReader(_xml.text);
                configuration = serializer.Deserialize(reader) as Configurations;
                reader.Close();
                return configuration;
            default:
                return null;
        }

    }

    public void AddCustomerConfig(Configurations conf) {


    }

    public void AddActionConfig(Configurations conf) {


    }
}
