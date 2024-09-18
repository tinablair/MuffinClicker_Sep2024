using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public struct SaveData
{
    public int totalClicks;
    public int pointsPerClick;
    public int pointsPerSecond;
    public int upgradeButton1Level;
    public int upgradeButton2Level;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Score
    public int totalClicks = 0;
    [SerializeField] private int pointsPerClick = 1;
    [SerializeField] private int pointsPerSecond = 1;

    // Floating Text
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private RectTransform floatingTextParent;

    [SerializeField] private HeaderUI headerUI = null;

    [SerializeField] private UpgradeButton upgradeButton1 = null;
    [SerializeField] private UpgradeButton upgradeButton2 = null;

    private float passiveMuffinCooldown = 1f;

    private void Awake()
    {
        //instance = this;
        LoadGame();
    }

    void Start()
    {
        headerUI.UpdateUI(totalClicks, pointsPerSecond);

        InvokeRepeating(nameof(CollectPassiveMuffin), 5f, 1f);
    }

    private void CollectPassiveMuffin()
    {
        //Debug.Log("Hello There!");
        totalClicks += pointsPerSecond;
        headerUI.UpdateUI(totalClicks, pointsPerSecond);
        passiveMuffinCooldown = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetGame();

            headerUI.UpdateUI(totalClicks, pointsPerSecond);
        }

        // Update our passive muffin cooldown
        passiveMuffinCooldown -= Time.deltaTime;

        if (passiveMuffinCooldown <= 0)
        {
            CollectPassiveMuffin();
        }

    }

    private void ResetGame()
    {
        totalClicks = 0;
        pointsPerClick = 1;
        pointsPerSecond = 1;
        upgradeButton1.level = 0;
        upgradeButton2.level = 0;
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void OnMuffinButtonClicked()
    {
        // Increasing the score
        totalClicks = totalClicks + pointsPerClick;

        // Update UI
        headerUI.UpdateUI(totalClicks, pointsPerSecond);

        // Create the floating text notification
        CreateFloatingText("+" + pointsPerClick);
    }

    private void CreateFloatingText(string message)
    {
        // Spawn a new floating text
        GameObject newFloatingText = Instantiate(floatingTextPrefab, floatingTextParent);

        //Generate a random position around the muffin
        Vector3 randomPosition = GetRandomPosAroundMuffin();

        // Position the new floating text at the random position
        newFloatingText.transform.localPosition = randomPosition;

        // Set the floating text's actual text
        newFloatingText.GetComponent<TMP_Text>().text = message;
    }

    private Vector3 GetRandomPosAroundMuffin() => new Vector3(Random.Range(-200f, 200f), Random.Range(75f, 225f));
    //{
    // Line 115 above replaces the following. However, use the following for better readability if desired.
    //float x = Random.Range(-200f, 200f);
    //float y = Random.Range(75f, 225f);
    //float z = 0;

    //return new Vector3(x, y);
    //}

    private void SaveGame()
    {
        Debug.Log("Saving Game...");

        // Create the save data object
        SaveData saveData = new SaveData();

        // Populate the save data object with the game's current state
        saveData.totalClicks = totalClicks;
        saveData.pointsPerClick = pointsPerClick;
        saveData.pointsPerSecond = pointsPerSecond;
        saveData.upgradeButton1Level = upgradeButton1.level;
        saveData.upgradeButton2Level = upgradeButton2.level;

        // Convert the save data object into JSON (Serialize it!)
        string saveJSON = JsonUtility.ToJson(saveData);
        Debug.Log("Save JSON: " + saveJSON);

        // Save the JSON to PlayerPrefs (computer's hard drive)
        PlayerPrefs.SetString("savegame", saveJSON);
    }

    private void LoadGame()
    {
        Debug.Log("Loading Game...");

        // Load the JSON to PlayerPrefs (computer's hard drive)
        string saveJSON = PlayerPrefs.GetString("savegame", "{}");

        // Convert the JSON into save data object (Deserialize it!)
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveJSON);

        // Populate the game's current state with the save data object
        totalClicks = saveData.totalClicks;

        if (saveData.pointsPerClick == 0)
        {
            pointsPerClick = 1;
        }
        else
        {
            pointsPerClick = saveData.pointsPerClick;
        }

        pointsPerSecond = saveData.pointsPerSecond;
        upgradeButton1.level = saveData.upgradeButton1Level;
        upgradeButton2.level = saveData.upgradeButton2Level;

    }

    //public bool TryToPurchase(int price, UpgradeType upgradeType)
    //{
    //    return TryToPurchase(price, upgradeType, headerUI.UpdateUI(totalClicks, pointsPerSecond));
    //}

    public bool TryToPurchase(int price, UpgradeType upgradeType, bool updateUI)
    {
        if (totalClicks < price)
        {
            return false; // Purchase fail
        }

        totalClicks -= price;

        switch (upgradeType)
        {
            case UpgradeType.MuffinsPerClick:
                pointsPerClick++;
                break;
            case UpgradeType.MuffinsPerSecond:
                pointsPerSecond++;
                break;
        }

        return true; // Purchase success
    }

    public void TreatClicked(int points)
    {
        // Logic for when a treat is clicked
        totalClicks += points;
        Debug.Log("Treat was clicked! Points added: " + points);

        // Update the UI
        headerUI.UpdateUI(totalClicks, pointsPerSecond);
    }

}