using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapController : MonoBehaviour {

    public RectTransform prefab;
    public Text countText;
    public ScrollRect scrollView;
    public RectTransform content;

    public List<TrainingItemView> itemList = new List<TrainingItemView>();

    public void UpdateItems(List<TrainingItem> data) {
        int count = 0;
        int.TryParse(countText.text, out count);
        foreach (GameObject item in content) {
            Destroy(item);
        }

        foreach (TrainingItem trainingData in data) {
            GameObject instance = Instantiate(prefab.gameObject, content);
            trainingData.InitItemView(instance, count);
            itemList.Add(trainingData.view);
            count++;
        }   
    }

    //void InitItemView(Customer customerData, CustomerTrainItem item, int index) {
    //    item.name.text = customerData.id;
    //    item.arrears.text = customerData.arrears.ToString();
    //    item.satisfaction.text = customerData.satisfaction.ToString();
    //}
}
