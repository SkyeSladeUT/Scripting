using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private FullMenu fullMenu;
    public Color SelectedColor, DefaultColor;

    private void Awake()
    {
        fullMenu = new FullMenu(this, SelectedColor, DefaultColor);
    }
}

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
        _manager = manager;
        MenuTransform = _manager.transform.Find("Menu");
        _menuButton = MenuTransform.Find("System Button").GetComponent<Button>();
        _menuButtonImage = _menuButton.GetComponent<Image>();
        _menu = new MainMenu(this);
        _menuButton.onClick.AddListener(OpenMenu);
        _optionsButton = MenuTransform.Find("Options Button").GetComponent<Button>();
        _optionsButtonImage = _optionsButton.GetComponent<Image>();
        _options = new OptionsMenu(this);
        _optionsButton.onClick.AddListener(OpenOptions);
        _itemsButton = MenuTransform.Find("Items Button").GetComponent<Button>();
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
        _options.Close();
        _inventory.Close();
        _menu.Open();
        MoveSelection("system");
    }
    public void OpenOptions()
    {
        _menu.Close();
        _inventory.Close();
        _options.Open();
        MoveSelection("options");
    }
    public void OpenInventory()
    {
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

public class MainMenu
{
    private Button _saveButton, _quitButton, _loadButton;
    private FullMenu _manager;
    private Transform _menuTransform;

    public MainMenu(FullMenu manager)
    {
        _manager = manager;
        _menuTransform = _manager.MenuTransform.Find("Menu Setup");
        _saveButton = _menuTransform.Find("Save Button").GetComponent<Button>();
        _saveButton.onClick.AddListener(OnSave);
        _quitButton = _menuTransform.Find("Quit Button").GetComponent<Button>();
        _quitButton.onClick.AddListener(OnQuit);
        _loadButton = _menuTransform.Find("Load Button").GetComponent<Button>();
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

public class OptionsMenu
{
    private FullMenu _manager;
    private Transform _optionsTransform;
    private Slider _volumeSlider, _musicSlider, _sfxSlider, _dialogueSlider;
    private Slider _dialogueSpeedSlider;
    private Slider _cameraSensitivitySlider;

    public OptionsMenu(FullMenu manager)
    {
        _manager = manager;
        _optionsTransform = _manager.MenuTransform.Find("Options Setup");

        _volumeSlider = _optionsTransform.Find("Volume Slider").GetComponent<Slider>();
        _volumeSlider.onValueChanged.AddListener(OnVolumeSliderChange);
        _musicSlider = _optionsTransform.Find("Music Slider").GetComponent<Slider>();
        _musicSlider.onValueChanged.AddListener(OnMusicSliderChange);
        _sfxSlider = _optionsTransform.Find("SFX Slider").GetComponent<Slider>();
        _sfxSlider.onValueChanged.AddListener(OnSFXSliderChange);
        _dialogueSlider = _optionsTransform.Find("Dialogue Slider").GetComponent<Slider>();
        _dialogueSlider.onValueChanged.AddListener(OnDialogueSliderChange);

        _dialogueSpeedSlider = _optionsTransform.Find("Dialogue Speed Slider").GetComponent<Slider>();
        _dialogueSpeedSlider.onValueChanged.AddListener(OnDialogueSpeedChange);
        _cameraSensitivitySlider = _optionsTransform.Find("Camera Sensitivity Slider").GetComponent<Slider>();
        _cameraSensitivitySlider.onValueChanged.AddListener(OnCameraSensitivityChange);

    }

    public void OnVolumeSliderChange(float val)
    {
        Debug.Log("Main Volume Change");
        SceneVariables.MainVolume = val;
    }
    public void OnMusicSliderChange(float val)
    {
        Debug.Log("Music Volume Change");
        SceneVariables.MusicVolume = val;
    }
    public void OnSFXSliderChange(float val)
    {
        Debug.Log("SFX Volume Change");
        SceneVariables.SFXVolume = val;
    }
    public void OnDialogueSliderChange(float val)
    {
        Debug.Log("Dialogue Volume Change");
        SceneVariables.DialogueVolume = val;
    }
    public void OnDialogueSpeedChange(float val)
    {
        Debug.Log("Dialogue Speed Change");
        SceneVariables.DialogueSpeed = val;
    }
    public void OnCameraSensitivityChange(float val)
    {
        Debug.Log("Camera Sensitivity Change");
        SceneVariables.CameraSensitivity = val;
    }
    public void Open()
    {
        Debug.Log("Open Options Menu");
        _optionsTransform.gameObject.SetActive(true);
    }
    public void Close()
    {
        Debug.Log("Close Options Menu");
        _optionsTransform.gameObject.SetActive(false);
    }
}

public class InventoryMenu
{
    private FullMenu _manager;
    private Transform _inventoryTransform;

    public InventoryMenu(FullMenu manager)
    {
        _manager = manager;
        _inventoryTransform = _manager.MenuTransform.Find("Inventory Setup");
    }
    public void Open()
    {
        Debug.Log("Open Inventory Menu");
        _inventoryTransform.gameObject.SetActive(true);
    }
    public void Close()
    {
        Debug.Log("Close Inventory Menu");
        _inventoryTransform.gameObject.SetActive(false);
    }
}
