using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class LevelUnlock : MonoBehaviour
{
    public Button n2, n3, a1, a2, a3;
    private string folderPath;
    private int slotNumber = 1;
    
    void Update()
    {
        folderPath = Path.Combine(Application.streamingAssetsPath, "SaveSlots");
        string filePath = Path.Combine(folderPath, $"Slot_{slotNumber}.txt");

        if (File.Exists(filePath) && slotNumber == 1)
        {
            n2.interactable = true;
            a1.interactable = true;
            slotNumber++;
        }
        else if (File.Exists(filePath) && slotNumber == 2)
        {
            n3.interactable = true;
            a2.interactable = true;
            slotNumber++;
        }
        else if (File.Exists(filePath) && slotNumber == 3)
        {
            a3.interactable = true;
        }
    }
}