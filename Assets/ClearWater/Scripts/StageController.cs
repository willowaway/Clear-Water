using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public GameObject totalStarsText;
    public GameObject stageContainer;
    public List<GameObject> stages;
    public List<StageData> stagesData;

    public GameObject stagePrefab;
    public int currentStage = 1;
    public int numberOfStages = 18;
    
    string stageDataFileName
    {
        get { return "StageDataSaveFile.es3"; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ES3.DeleteKey("stages", stageDataFileName);
        //ES3.DeleteKey("currentStage");

        stages = new List<GameObject>();
        // Load the prefab
        for (int stageIndex = 0; stageIndex < numberOfStages; ++stageIndex)
        {
            GameObject stageGameObj = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
            stageGameObj.transform.parent = stageContainer.transform;
            stageGameObj.transform.localScale = Vector3.one;
            stages.Add(stageGameObj);
        }

        if (ES3.KeyExists("currentStage"))
        {
            currentStage = ES3.Load<int>("currentStage");
        }
        else
        {
            currentStage = 1;
        }

        LoadStages();
    }

    public void LoadStages()
    {
        if (ES3.KeyExists("stages", stageDataFileName))
        {
            stagesData = ES3.Load<List<StageData>>("stages", stageDataFileName);
            for (int stageIndex = 0; stageIndex < numberOfStages; ++stageIndex)
            {
                StageData stageData = stagesData[stageIndex];
                Stage stage = stages[stageIndex].GetComponent<Stage>();
                stage.stageData.stageNumber = stageData.stageNumber;
                stage.stageData.stars = stageData.stars;
                stage.stageData.isLocked = stageData.isLocked;
                stage.stageData.hasAttempted = stageData.hasAttempted;

                stage.UpdatePanel();
            }

            stagesData[currentStage - 1].isLocked = false;
            stages[currentStage - 1].GetComponent<Stage>().stageData.hasAttempted = stagesData[currentStage].hasAttempted;
            stages[currentStage - 1].GetComponent<Stage>().UpdatePanel();
        }
        else
        {
            stagesData = new List<StageData>();

            for (int stageIndex = 0; stageIndex < numberOfStages; ++stageIndex)
            {
                StageData stageData = new StageData();
                stageData.stageNumber = stageIndex + 1;
                stagesData.Add(stageData);

                Stage stage = stages[stageIndex].GetComponent<Stage>();
                stage.stageData.stageNumber = stageData.stageNumber;

                stage.UpdatePanel();
            }

            StageData firstStageData = stagesData[0];
            firstStageData.isLocked = false;
            stagesData.Add(firstStageData);

            Stage firstStage = stages[0].GetComponent<Stage>();
            firstStage.stageData.isLocked = stagesData[0].isLocked;
         
            firstStage.UpdatePanel();
        }

        SaveStages();
    }

    public void SaveStages()
    {
        ES3.Save("stages", stagesData, stageDataFileName);
    }

    public void CompleteCurrentStage(int stars)
    {
        int currentStageIndex = currentStage - 1;
        StageData stageData = stagesData[currentStageIndex];
        stageData.stars = stars;
        stageData.hasAttempted = true;

        Stage stage = stages[currentStageIndex].GetComponent<Stage>();
        stage.stageData.stars = stageData.stars;
        stage.stageData.hasAttempted = stageData.hasAttempted;

        stage.UpdatePanel();

        // Set the next stage to unlocked
        if (currentStageIndex + 1 < numberOfStages)
        {
            StageData nextStageData = stagesData[currentStageIndex + 1];
            nextStageData.isLocked = false;

            Stage nextStage = stages[currentStageIndex + 1].GetComponent<Stage>();
            nextStage.stageData.isLocked = nextStageData.isLocked;

            nextStage.UpdatePanel();
        }

        SaveStages();
    }

    public void NextStage()
    {
        if (!stagesData[currentStage + 1].isLocked)
        {
            currentStage++;
            ES3.Save("currentStage", currentStage);

            StageData stageData = stagesData[currentStage];
            stageData.hasAttempted = true;

            Stage stage = stages[currentStage].GetComponent<Stage>();
            stage.stageData.hasAttempted = stageData.hasAttempted;

            SaveStages();
        }
    }
}
