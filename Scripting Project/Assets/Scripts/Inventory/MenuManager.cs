using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.U2D;
using Newtonsoft.Json;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private FullMenu fullMenu;
    public Color SelectedColor, DefaultColor;

    private void Awake()
    {
        fullMenu = new FullMenu(this, SelectedColor, DefaultColor);
    }
}

#region MASTER MENU
public class FullMenu
{
    private MenuManager _manager;
    public Transform MenuTransform;
    private Button _menuButton, _optionsButton, _itemsButton;
    private Image _menuButtonImage, _optionsButtonImage, _itemsButtonImage;
    private MainMenu _menu;
    private OptionsMenu _options;
    private InventoryMenu _inventory;
    private Color _selectedColor, _defaultColor;

    public FullMenu(MenuManager manager, Color selectedColor, Color defaultColor)
    {
        Debug.Log("Full Menu Setup");
        _manager = manager;
        MenuTransform = _manager.transform.Find("Menu");
        _menuButton = MenuTransform.Find("Main Background/Left Section/Buttons/System Button").GetComponent<Button>();
        _menuButtonImage = _menuButton.GetComponent<Image>();
        _menu = new MainMenu(this);
        _menuButton.onClick.AddListener(OpenMenu);
        _optionsButton = MenuTransform.Find("Main Background/Left Section/Buttons/Options Button").GetComponent<Button>();
        _optionsButtonImage = _optionsButton.GetComponent<Image>();
        _options = new OptionsMenu(this);
        _optionsButton.onClick.AddListener(OpenOptions);
        _itemsButton = MenuTransform.Find("Main Background/Left Section/Buttons/Inventory Button").GetComponent<Button>();
        _itemsButtonImage = _itemsButton.GetComponent<Image>();
        _inventory = new InventoryMenu(this);
        _itemsButton.onClick.AddListener(OpenInventory);
        _selectedColor = selectedColor;
        _defaultColor = defaultColor;
    }
    public void Open()
    {
        MenuTransform.gameObject.SetActive(true);
        OpenMenu();
    }
    public void OpenMenu()
    {
        Debug.Log("Open Menu");
        _options.Close();
        _inventory.Close();
        _menu.Open();
        MoveSelection("system");
    }
    public void OpenOptions()
    {
        Debug.Log("Open Options");
        _menu.Close();
        _inventory.Close();
        _options.Open();
        MoveSelection("options");
    }
    public void OpenInventory()
    {
        Debug.Log("Open Inventory");
        _menu.Close();
        _options.Close();
        _inventory.Open();
        MoveSelection("inventory");
    }
    public void Close()
    {
        MenuTransform.gameObject.SetActive(false);
        _menu.Close();
        _options.Close();
        _inventory.Close();
    }

    public void MoveSelection(string menuName)
    {
        switch (menuName)
        {
            case "system":
                _menuButtonImage.color = _selectedColor;
                _optionsButtonImage.color = _defaultColor;
                _itemsButtonImage.color = _defaultColor;
                break;
            case "options":
                _menuButtonImage.color = _defaultColor;
                _optionsButtonImage.color = _selectedColor;
                _itemsButtonImage.color = _defaultColor;
                break;
            case "inventory":
                _menuButtonImage.color = _defaultColor;
                _optionsButtonImage.color = _defaultColor;
                _itemsButtonImage.color = _selectedColor;
                break;
        }
    }
}
#endregion
#region SYSTEM MENU
public class MainMenu
{
    private Button _saveButton, _quitButton, _loadButton;
    private FullMenu _manager;
    private Transform _menuTransform;

    public MainMenu(FullMenu manager)
    {
        Debug.Log("Main Menu Setup");
        _manager = manager;
        _menuTransform = _manager.MenuTransform.Find("Main Background/Right Section/Menu Setup");
        _saveButton = _menuTransform.Find("System Layout/Save Button").GetComponent<Button>();
        _saveButton.onClick.AddListener(OnSave);
        _quitButton = _menuTransform.Find("System Layout/Quit Button").GetComponent<Button>();
        _quitButton.onClick.AddListener(OnQuit);
        _loadButton = _menuTransform.Find("System Layout/Load Button").GetComponent<Button>();
        _loadButton.onClick.AddListener(OnLoad);
    }

    public void OnSave()
    {
        //Save Dialogue
        Debug.Log("Save Button Clicked");
    }
    public void OnQuit()
    {
        //Quit Dialogue
        Debug.Log("Quit Button Clicked");
    }
    public void OnLoad()
    {
        //Load Dialogue
        Debug.Log("Load Button Clicked");
    }
    public void Open()
    {
        //Open Menu
        Debug.Log("Open Menu");
        _menuTransform.gameObject.SetActive(true);
    }
    public void Close()
    {
        //Close Menu
        Debug.Log("Close Menu");
        _menuTransform.gameObject.SetActive(false);
    }
    
}
#endregion
#region OPTIONS MENU
public class OptionsMenu
{
    private FullMenu _manager;
    private Transform _optionsTransform;
    private Slider _volumeSlider, _musicSlider, _sfxSlider, _dialogueSlider;
    private Slider _dialogueSpeedSlider;
    private Slider _cameraSensitivitySlider;

    public OptionsMenu(FullMenu manager)
    {
        Debug.Log("Options Menu Setup");
        _manager = manager;
        _optionsTransform = _manager.MenuTransform.Find("Main Background/Right Section/Options Setup");

        _volumeSlider = _optionsTransform.Find("Settings Layout/Main Volume/Volume Slider").GetComponent<Slider>();
        _volumeSlider.onValueChanged.AddListener(OnVolumeSliderChange);
        _volumeSlider.value = SceneVariables.MainVolume;
        _musicSlider = _optionsTransform.Find("Settings Layout/Music Volume/Music Slider").GetComponent<Slider>();
        _musicSlider.onValueChanged.AddListener(OnMusicSliderChange);
        _musicSlider.value = SceneVariables.MusicVolume;
        _sfxSlider = _optionsTransform.Find("Settings Layout/SFX Volume/SFX Slider").GetComponent<Slider>();
        _sfxSlider.onValueChanged.AddListener(OnSFXSliderChange);
        _sfxSlider.value = SceneVariables.SFXVolume;
        _dialogueSlider = _optionsTransform.Find("Settings Layout/Dialogue Volume/Dialogue Slider").GetComponent<Slider>();
        _dialogueSlider.onValueChanged.AddListener(OnDialogueSliderChange);
        _dialogueSlider.value = SceneVariables.DialogueVolume;

        _dialogueSpeedSlider = _optionsTransform.Find("Settings Layout/Dialogue Speed/Dialogue Speed Slider").GetComponent<Slider>();
        _dialogueSpeedSlider.onValueChanged.AddListener(OnDialogueSpeedChange);
        _dialogueSpeedSlider.value = SceneVariables.DialogueSpeed;
        _cameraSensitivitySlider = _optionsTransform.Find("Settings Layout/Camera Sensitivity/Camera Sensitivity Slider").GetComponent<Slider>();
        _cameraSensitivitySlider.onValueChanged.AddListener(OnCameraSensitivityChange);
        _cameraSensitivitySlider.value = SceneVariables.CameraSensitivity;

    }

    public void OnVolumeSliderChange(float val)
    {
        SceneVariables.MainVolume = val;
    }
    public void OnMusicSliderChange(float val)
    {
        SceneVariables.MusicVolume = val;
    }
    public void OnSFXSliderChange(float val)
    {
        SceneVariables.SFXVolume = val;
    }
    public void OnDialogueSliderChange(float val)
    {
        SceneVariables.DialogueVolume = val;
    }
    public void OnDialogueSpeedChange(float val)
    {
        SceneVariables.DialogueSpeed = val;
    }
    public void OnCameraSensitivityChange(float val)
    {
        SceneVariables.CameraSensitivity = val;
    }
    public void Open()
    {
        _optionsTransform.gameObject.SetActive(true);
    }
    public void Close()
    {
        _optionsTransform.gameObject.SetActive(false);
    }
}
#endregion
#region INVENTORY MENU
public class InventoryMenu
{
    private FullMenu _manager;
    private Transform _inventoryTransform;
    private List<InventoryItem> items;
    private Transform _descriptionTransform;
    private RectTransform _itemsTransform;
    private TextMeshProUGUI _descriptionText;
    private TextMeshProUGUI _itemName;
    private Image _itemImage;
    private float _right = 235;
    private Transform _contentHolder;
    private Transform _buttonPrefab;

    public InventoryMenu(FullMenu manager)
    {
        _manager = manager;
        _inventoryTransform = _manager.MenuTransform.Find("Main Background/Right Section/Inventory Setup");
        _descriptionTransform = _inventoryTransform.Find("Inventory Layout/Description");
        _descriptionText = _descriptionTransform.Find("Item Description").GetComponent<TextMeshProUGUI>();
        _itemName = _descriptionTransform.Find("Item Name").GetComponent<TextMeshProUGUI>();
        _itemImage = _descriptionTransform.Find("Item Image").GetComponent<Image>();
        _itemsTransform = _inventoryTransform.Find("Inventory Layout/Items").GetComponent<RectTransform>();
        _contentHolder = _itemsTransform.Find("Scroll View/Viewport/Content");
        _buttonPrefab = _contentHolder.Find("Inventory Button");
    }
    public void Open()
    {
        _itemsTransform.offsetMax = new Vector2(10, 10);
        _descriptionTransform.gameObject.SetActive(false);

        items = GetItems();
        foreach(Transform c in _contentHolder)
        {
            GameObject.Destroy(c.gameObject);
        }
        for(int i = 0; i<items.Count; i++)
        {
            var temp = GameObject.Instantiate(_buttonPrefab, _contentHolder);
            var buttonObj = temp.GetComponent<Button>();
            var image = temp.Find("Image").GetComponent<Image>();
            image.sprite = items[i].ItemSprite;
            buttonObj.onClick.AddListener(() => { ClickButton(items[i]); });
            temp.gameObject.SetActive(true);
        }
        _inventoryTransform.gameObject.SetActive(true);
    }
    public void Close()
    {
        _inventoryTransform.gameObject.SetActive(false);
    }

    public List<InventoryItem> GetItems()
    {
        string json = Resources.Load<TextAsset>("JSON/ItemsInventory").text;
        var items = JsonConvert.DeserializeObject<List<InventoryItem>>(json);
        return items;
    }

    public void ClickButton(InventoryItem item)
    {
        if(_descriptionTransform.gameObject.activeSelf && _itemName.text == item.name)
        {
            CloseDescription();
        }
        else
        {
            OpenDescription(item);
        }
    }

    public void OpenDescription(InventoryItem item)
    {
        _itemsTransform.offsetMax = new Vector2(10, _right);
        _itemImage.sprite = item.ItemSprite;
        _descriptionText.text = item.description;
        _itemName.text = item.name;
        _descriptionTransform.gameObject.SetActive(true);
    }

    public void CloseDescription()
    {
        _itemsTransform.offsetMax = new Vector2(10, 10);
        _descriptionTransform.gameObject.SetActive(false);
    }
}

public class InventoryItem
{
    public string name;
    public int quantity;
    public string description;
    public string itemType;
    string spriteAtlasName;
    string spriteName;
    [NonSerialized]
    private Sprite _itemSprite;
    public Sprite ItemSprite
    {
        get
        {
            if(_itemSprite == null)
            {
                Debug.Log("Sprite Atlas: " + spriteAtlasName + "  Sprite: " + spriteName);
                SpriteAtlas sa = Resources.Load<SpriteAtlas>("Sprites/Inventory/" + spriteAtlasName);
                _itemSprite = sa.GetSprite(spriteName);
            }
            return _itemSprite;
        }
    }

    public InventoryItem(string name, int quantity, string description, string itemType, string spriteAtlasName, string spriteName)
    {
        this.name = name;
        this.quantity = quantity;
        this.description = description;
        this.itemType = itemType;
        this.spriteAtlasName = spriteAtlasName;
        this.spriteName = spriteName;
    }
}



#endregion

