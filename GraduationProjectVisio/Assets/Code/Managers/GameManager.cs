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
    public Button SelectProfileButton;
    public TMP_InputField nameInputField;
    public List<GameObject> panels;
    [Space(10)]
    [Header("LessonManager")]
    public List<LessonData> lessons;
    public Button lessonButtonPrefab;
    [Space(10)]
    [Header("AudioManager")]
    public AudioSource voiceSource;
    public AudioSource feedbackSource;
    [Space(10)]
    [Header("Save Load Manager")]
    public List<ProfileData> profiles;
    [SerializeField] private string fileName;
    public string profileName;

    [Space(10)]
    [Header("Timers")]
    public List<Timer> timers = new List<Timer>();
    Timer autoSaveTimer;

    GameManager()
    {
        if (instance == null)
        {
            instance = this;
        }

        managers = new Manager[]
        {
         new AudioManager(),
         new LessonManager(),
         new KeyboardInputManager(),
         new UIManager(),
         new SaveLoadManager(),
        };
    }


    public void Awake()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Awake();
        }
    }

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

    public void DisablePanel(GameObject panel) 
    {
        panel.SetActive(false);     
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Start();
        }

        createProfileButton.onClick.AddListener(GameManager.GetManager<UIManager>().CreateProfileButton);
        SelectProfileButton.onClick.AddListener(GameManager.GetManager<UIManager>().SelectLessonProfileButton);
        CloseButton.onClick.AddListener(GameManager.GetManager<UIManager>().CloseAllPanels);


        GameManager.GetManager<SaveLoadManager>().LoadSave();

        autoSaveTimer = new Timer(0, "autosave");
        autoSaveTimer.SetTimer(2);

       
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
                // GetManager<SaveLoadManager>().SaveData();
                Debug.Log("Auto Saved");
                autoSaveTimer.RestartTimer();
            }
        }

    }

    public List<IDataInterface> FindAllObjectsToSave()
    {
        IEnumerable<IDataInterface> allSavedObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataInterface>();
        return new List<IDataInterface>(allSavedObjects);
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
}
