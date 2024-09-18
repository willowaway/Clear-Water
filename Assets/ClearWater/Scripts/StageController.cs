using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public GameObject popupStage;
    public GameObject popupStageReady;
    public GameObject popupStageLocked;

    public GameObject stageContainer;
    public List<GameObject> stages;
    public GameObject stageComplete;
    public GameObject stageDefault;
    public GameObject stageLock;

    private int currentStage;
    private List<int> stageProgress;

    // Start is called before the first frame update
    void Start()
    {
        stageProgress = new List<int>(3);
    }

    public void OpenStageSelect()
    {
        popupStage.SetActive(true);
    }

    /// <summary>
    /// Loads a stage based on the list of scene numbers
    /// </summary>
    /// <param name="stage"></param>
    public void LoadStage(int stage)
    {
        SceneManager.LoadScene(stage);
    }

    public void NextStage()
    {

    }
}
