using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class AutoSave : MonoBehaviour
{
    public ScoreManager scoreManager;

    Toggle toggleComponent;

    void Start()
    {
        toggleComponent = GetComponent<Toggle>();
        toggleComponent.isOn = scoreManager.autoSave;
    }

    public void OnToggle()
    {
        scoreManager.autoSave = toggleComponent.isOn;
    }
}
