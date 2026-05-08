using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private static Manager[] managers;

    [Header("User Interface")]
    public TextMeshProUGUI displayKeyText;
    public TextMeshProUGUI descriptionText;
    public Button CloseButton;
    public Button createProfileButton;
    public Button volumeUp;
    public Button volumeDown;
    public Button fontSizeUp;
    public Button fontSizeDown;
    public Button profile;
    public Button magnifyGlass;
    public Button SelectProfileButton;
    public TMP_InputField nameInputField;
    public List<GameObject> panels;
    public GameObject magnifyObject;
    public List<TextMeshProUGUI> allTextComps;
    public Transform profilesContainer;
    public GameObject profileItem;
    public TextMeshProUGUI profileName;
    public Button chooseProfileButton;
    public Button lessonCreatorButton;
    public Button saveLesson;
    public Button addStep;
    public GameObject[] visualKeyboard;
    [Space(10)]
    [Header("LessonManager")]
    public List<LessonData> lessons;
    public Button lessonButtonPrefab;
    [Space(10)]
    public Transform stepsContainer;
    public TypingStepUI stepPrefab;
    public TMPro.TMP_InputField lessonNameInput;
    [Space(10)]
    [Header("AudioManager")]
    public AudioSource voiceSource;
    public AudioSource feedbackSource;
    [Space(10)]
    [Header("Timers")]
    public List<Timer> timers = new List<Timer>();
    Timer autoSaveTimer;

    /// <summary>
    /// Constructor class to initalize managers and the instance.
    /// </summary>
    GameManager()
    {
        if (instance == null)
        {
            instance = this;
        }

        managers = new Manager[]
        {
         new VisualKeyboardManager(),
         new AudioManager(),
         new LessonManager(),
         new KeyboardInputManager(),
         new UIManager(),
         new SaveLoadManager(),
        };
    }

    /// <summary>
    /// Calling Awake for all managers
    /// </summary>
    public void Awake()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Awake();
        }
    }

    /// <summary>
    /// Getter method for getting a specific manager.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetManager<T>() where T : Manager
    {
        for (int i = 0; i < managers.Length; i++)
        {
            if (typeof(T) == managers[i].GetType())
            {
                return (T)managers[i];
            }
        }
        return default(T);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Start();
        }

        //Find a way to make this more clean
        createProfileButton.onClick.AddListener(GameManager.GetManager<UIManager>().CreateProfileButton);
        SelectProfileButton.onClick.AddListener(GameManager.GetManager<UIManager>().SelectLessonProfileButton);
        CloseButton.onClick.AddListener(GameManager.GetManager<UIManager>().CloseAllPanels);
        magnifyGlass.onClick.AddListener(GameManager.GetManager<UIManager>().MagnifyToggle);
        fontSizeUp.onClick.AddListener(GameManager.GetManager<UIManager>().FontSizeUp);
        fontSizeDown.onClick.AddListener(GameManager.GetManager<UIManager>().FontSizeDown);
        volumeDown.onClick.AddListener(GameManager.GetManager<AudioManager>().VolumeDown);
        volumeUp.onClick.AddListener(GameManager.GetManager<AudioManager>().VolumeUp);
        chooseProfileButton.onClick.AddListener(GameManager.GetManager<UIManager>().RefreshProfileUI);
        lessonCreatorButton.onClick.AddListener(GameManager.GetManager<UIManager>().StartCreatingLesson);
        saveLesson.onClick.AddListener(GameManager.GetManager<UIManager>().SaveLesson);
        addStep.onClick.AddListener(GameManager.GetManager<UIManager>().AddStep);
        profile.onClick.AddListener(GameManager.GetManager<UIManager>().OpenProfilePanel);

        autoSaveTimer = new Timer(0, "autosave");
        autoSaveTimer.SetTimer(2);


       

        GameManager.GetManager<AudioManager>().AddAudioSource(feedbackSource);
    }

    


    // Update is called once per frame
    public void Update()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Update();
        }
        if (autoSaveTimer != null)
        {
            if (autoSaveTimer.isActive && autoSaveTimer.TimerDone())
            {
                allTextComps = FindAllTextComponents();
                Debug.Log("Auto Saved");
                autoSaveTimer.RestartTimer();
            }
        }

    }

    //public List<IDataInterface> FindAllObjectsToSave()
    //{
    //    IEnumerable<IDataInterface> allSavedObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataInterface>();
    //    return new List<IDataInterface>(allSavedObjects);
    //}

    public List<TextMeshProUGUI> FindAllTextComponents() 
    {
        IEnumerable<TextMeshProUGUI> allText = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<TextMeshProUGUI>();
        return new List<TextMeshProUGUI>(allText);
    }


    public Timer GetTimerByName(string name)
    {
        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i].name == name)
            {
                return timers[i];
            }
        }
        Debug.LogError($"No Timer was found with name: {name}");
        return null;
    }

    /// <summary>
    /// Destroy for non monobehaviour classes
    /// </summary>
    /// <param name="gameObject"></param>
    public void CustomDestroyGameObject(GameObject gameObject) 
    {
        Destroy(gameObject);
    }
}
