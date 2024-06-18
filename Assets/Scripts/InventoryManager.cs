using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //인벤토리 데이터 파일 이름
    private const string inventoryDataFileName = "inventoryData.json";

    //인벤토리 아이템 리스트
    public List<Item> inventoryItems = new List<Item>();
    public List<GameObject> inventoryItemsObject = new List<GameObject>();
    
    public Transform inventoryContent = null;
    public GameObject objItem = null;
    // Start is called before the first frame update
   
    void Start()
    {
        LoadInventoryData(); // 인벤토리 데이터 로드
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    // 인벤토리에 아이템 추가
    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
        SaveInventoryData(); // 인벤토리 데이터 저장
    }

    // 인벤토리 데이터 저장
    private void SaveInventoryData()
    {
        string jsonData = JsonUtility.ToJson(new InventoryData(inventoryItems));
        PlayerPrefs.SetString(inventoryDataFileName, jsonData);
        PlayerPrefs.Save();
    }

    // 인벤토리 데이터 로드
        private void LoadInventoryData()
    {
        if (PlayerPrefs.HasKey(inventoryDataFileName))
        {
            string jsonData = PlayerPrefs.GetString(inventoryDataFileName);
            InventoryData data = JsonUtility.FromJson<InventoryData>(jsonData);
            inventoryItems = data.items;
            Debug.Log("" + jsonData);
            SetInventory();
        }
    }

    public void ClearInvetoryUI () {

        foreach (var item in inventoryItemsObject) {
            Destroy(item);
        }
        inventoryItems.Clear();
        inventoryItemsObject.Clear();
    }
    public void SetInventory() {
        foreach (var item in inventoryItems) {
            AddInventoryItem(item);
        }
    }

    public void AddInventoryItem(Item item) {
        GameObject newItem = Instantiate(objItem, inventoryContent) as GameObject;
        inventoryItemsObject.Add(newItem);
        newItem.GetComponent<ItemSlot>().SetInit(item);
    }
    public void OnTempDeleteKey() {
        PlayerPrefs.DeleteKey(inventoryDataFileName);
        PlayerPrefs.Save();
        ClearInvetoryUI () ;
    }

}


[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string icon;
}

// 인벤토리 데이터 클래스
[System.Serializable]
public class InventoryData
{
    public List<Item> items;

    public InventoryData(List<Item> items)
    {
        this.items = items;
    }
}