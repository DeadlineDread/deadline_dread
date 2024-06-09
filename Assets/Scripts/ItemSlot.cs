using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    public int ID;
    public TMP_Text textItemName;
    public Image imageIcon;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    public void SetInit (Item item) {
        ID = item.id;
        textItemName.text = item.name;

        Sprite sprite = Resources.Load<Sprite> ("Inventory/" + item.icon);

        // Check if the sprite is loaded successfully
        if (sprite != null) {
            // Assign the sprite to the UI Image
            imageIcon.sprite = sprite;
        }
    }
}