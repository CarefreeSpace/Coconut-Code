using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using System.IO;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool timerOn, timeTrial;
    public int maxTime;
    public TMP_Text timerText;

    private float timeLeft;
    private string folderPath;

    void Awake()
    {
        folderPath = Path.Combine(Application.streamingAssetsPath, "SaveSlots");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    void Start()
    {
        if (timeTrial)
        {
            timeLeft = maxTime;
        }
    }

    void Update()
    {
        if (timerOn)
        {
            if (timeTrial)
            {
                if (timeLeft >= 0)
                {
                    timeLeft -= Time.deltaTime;
                    timerText.text = RealTime();
                }
                else
                {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
            }
            else
            {
                timeLeft += Time.deltaTime;
                timerText.text = RealTime();
            }
        }
    }

    string RealTime()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        string realTimeLeft = string.Format("{0:00}:{1:00}", minutes, seconds);
        return realTimeLeft;
    }

    public void SaveTimeToSlot(int slotNumber)
    {
        string filePath = Path.Combine(folderPath, $"Slot_{slotNumber}.txt");
        int secondsToSave = Mathf.FloorToInt(timeLeft);
        File.WriteAllText(filePath, secondsToSave.ToString());
        Debug.Log($"Saved time in seconds: {secondsToSave} to {filePath}");
    }

    public int LoadTimeFromSlot(int slotNumber)
    {
        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogError("Folder path is not initialized.");
            return 0;
        }

        string filePath = Path.Combine(folderPath, $"Slot_{slotNumber}.txt");
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath).Trim();
            Debug.Log($"Loaded file content: {content}");

            if (int.TryParse(content, out int loadedSeconds))
            {
                Debug.Log($"Loaded time in seconds: {loadedSeconds}");
                return loadedSeconds;
            }
            else
            {
                Debug.LogWarning($"Invalid number format in slot {slotNumber}: {content}");
                return 0;
            }
        }
        else
        {
            Debug.LogWarning($"Slot {slotNumber} does not exist at path: {filePath}");
            return 0;
        }
    }
}