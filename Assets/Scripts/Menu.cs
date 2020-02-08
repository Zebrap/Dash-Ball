using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button selectLevelButton;
    public GameObject levelContainer;

    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject instructionMenu;

    public GameObject ShopButtonPrefab;
    public GameObject ShopButtonContainer;

    public Text CashText;
    public Sprite fullStar;

    public Text BuySetSkinText;
    public Color32 SelectedSkinColor = new Color32(20, 250, 0, 100);
    public Color32 OwnedSkinColor = new Color32(255, 255, 255, 100);

    private float scaleItem = 1.125f;
    private int currentSelectedSkin;
    private int[] skinsCost = new int[] { 0, 5, 5, 10, 10, 10, 15, 15, 15};
    

    void Start()
    {
        InitLevels();
        InitSkins();
        InitCash();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void StartLevel(int scane)
    {
        GameManager.Instance.currentLevel = scane;
        GameManager.Instance.FadeToScane();
    }

    private void InitLevels()
    {
        int levelIndex = 0;
        foreach (string name in GameManager.Instance.scenesInBuild)
        {
            int index = levelIndex;
            Button button = Instantiate(selectLevelButton) as Button;
            button.GetComponentInChildren<Text>().GetComponentInChildren<Text>().text = (index+1).ToString();
       //     GameManager.Instance.state.score[index]
            button.onClick.AddListener(() => StartLevel(index));
            button.transform.SetParent(levelContainer.transform);
            // not enable
            if (GameManager.Instance.state.levelReached < index)
            {
                button.interactable = false;
            }
            else if(GameManager.Instance.state.score.Count > index)
            {
                // complate level, add stars
                for (int i = 0; i < GameManager.Instance.state.score[index]; i++)
                {
                    button.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = fullStar;
                }
            }
            levelIndex++;
        }
    }

    public void InitSkins()
    {
        int textureIndex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("Skins");
        foreach (Sprite thumbnail in textures)
        {
            GameObject container = Instantiate(ShopButtonPrefab) as GameObject;
            container.transform.GetChild(0).GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(ShopButtonContainer.transform, false);

            int index = textureIndex;
            container.GetComponent<Button>().onClick.AddListener(() => OnClickSelectSkin(index));
            
            if (IsSkinOwned(index))
            {
                if (GameManager.Instance.state.activeSkin == index)
                {
                    container.GetComponent<Image>().color = SelectedSkinColor;
                    container.GetComponent<RectTransform>().localScale = Vector3.one * scaleItem;
                }
                else
                {
                    container.GetComponent<Image>().color = OwnedSkinColor;
                }
            }
            /*
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));
            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = costs[index].ToString();
            // container.transform.GetComponentInChildren<Text>()
            if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
            {
                container.transform.GetChild(0).gameObject.SetActive(false);
            }*/
            textureIndex++;
        }
    }

    public void OnClickSelectSkin(int currentIndex)
    {

        if (currentSelectedSkin == currentIndex)
            return;

        // make it bigger
        ShopButtonContainer.transform.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * scaleItem;
        // normal scale
        ShopButtonContainer.transform.GetChild(currentSelectedSkin).GetComponent<RectTransform>().localScale = Vector3.one;
        
        currentSelectedSkin = currentIndex;

        // Change the content of the button
        if (IsSkinOwned(currentIndex))
        {
            // is onwed
            if (GameManager.Instance.state.activeSkin == currentIndex)
            {
                BuySetSkinText.text = "Current";
            }
            else
            {
                BuySetSkinText.text = "Select";
            }
        }
        else
        {
            // isn't owned
            BuySetSkinText.text = "Buy : "+ skinsCost[currentIndex].ToString();
        }
    }

    public bool IsSkinOwned(int index)
    {
        // check bit 
        return (GameManager.Instance.state.skinOwned & (1 << index)) != 0;
    }

    public void OnSkinBuySet()
    {
        //    Debug.Log("Buy/Set color");
        // Is the selected color owned
        if (IsSkinOwned(currentSelectedSkin) & GameManager.Instance.state.activeSkin != currentSelectedSkin)
        {
            SetSkinn();
        }
        else
        {
            // attempt to buy
            if (GameManager.Instance.BuySkin(currentSelectedSkin, skinsCost[currentSelectedSkin]))
            {
                // Success
                GameManager.Instance.UnlockSkin(currentSelectedSkin);
                SetSkinn();

                // Update gold text
                InitCash();
            }
            else
            {
                // do not have enough gold
                Debug.Log("Not enought gold");
            }
        }
    }
    public void SetSkinn()
    {
        ShopButtonContainer.transform.GetChild(GameManager.Instance.state.activeSkin).GetComponent<Image>().color = OwnedSkinColor;
        ShopButtonContainer.transform.GetChild(currentSelectedSkin).GetComponent<Image>().color = SelectedSkinColor;
        GameManager.Instance.ChangeActiveSkin(currentSelectedSkin);
        BuySetSkinText.text = "Current";
    }

    private void InitCash()
    {
        CashText.text = "Gold: " + GameManager.Instance.state.cash;
    }

    public void ShowMenuLevel()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void ShowInstructionMenu()
    {
        mainMenu.SetActive(false);
        instructionMenu.SetActive(true);
    }
    
    public void Back(GameObject menu)
    {
        menu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowThatMenu(GameObject menu)
    {
        mainMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void ResetSave()
    {
        GameManager.Instance.ResetSave();
    }
    
    public void SaveAndBack(GameObject menu)
    {
        Back(menu);
        gameObject.GetComponent<Options>().SaveOptions();
    }

    public void SaveProfileAndBack(GameObject menu)
    {
        Back(menu);
        GameManager.Instance.Save();
    }
}
