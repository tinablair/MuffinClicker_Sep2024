using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum UpgradeType
{
    MuffinsPerClick = 0,
    MuffinsPerSecond = 1
}

public class UpgradeButton : MonoBehaviour
{

    public TMP_Text levelText;
    public TMP_Text priceText;

    [SerializeField] private int pricePerLevel = 10;

    public int level = 0;
    private int price;

    [SerializeField] private GameManager gameManager;

    public UpgradeType upgradeType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Update the level text
        levelText.text = level.ToString();

        // Calculate the current price of the upgrade
        price = (level + 1) * pricePerLevel;

        // Update the price text
        priceText.text = price.ToString();

        // Color the price text accordingly to whether the player can afford it or not
        priceText.color = gameManager.totalClicks >= price ? Color.green : Color.red;
    }
    //public void CheckPrice()
    //{
    //    if (gameManager.CanBuy(price, upgradeType))
    //    {
    //        priceText.color = Color.green;
    //    }
    //    else
    //    {
    //        priceText.color = Color.red;
    //    }

    //    // Ternary: Thing we want to set = < Boolean condition > ? < value if True > : < value if False >;

    //}

    public void OnUpgradeClicked()
    {
        //Debug.Log("Hey you clicked me!");
        if (gameManager.TryToPurchase(price, upgradeType, true)) // 'True' to update UI
        {
            level++;
        }
        else
        {
            // Play failed sound

            // Play shake animation on button

        }
    }

}
