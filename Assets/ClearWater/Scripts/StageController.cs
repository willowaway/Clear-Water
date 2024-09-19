using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public GameObject panelStageSelect;
    public GameObject popupStageReady;
    public GameObject popupStageLocked;

    public GameObject totalStarsText;
    public GameObject stageContainer;

    public GameObject stagePrefab;
    public int currentStage = 1;
    public int numberOfStages = 18;
    public List<GameObject> stages;

    // Start is called before the first frame update
    void Start()
    {
        if (ES3.KeyExists("stages"))
        {
            stages = ES3.Load<List<GameObject>>("stages");
        }
        else
        {
            // Delete the placeholder stage panels
            while (stageContainer.transform.childCount > 0)
            {
                DestroyImmediate(stageContainer.transform.GetChild(0).gameObject);
            }

            stages = new List<GameObject>(numberOfStages);

            for (int stageIndex = 0;  stageIndex < numberOfStages; ++stageIndex)
            {
                GameObject stage = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
                stage.GetComponent<Stage>().stageNumber = stageIndex + 1;
                stages.Add(stage);
                stage.transform.parent = stageContainer.transform;
                stage.transform.localScale = Vector3.one;
            }
            Stage firstStage = stages[0].GetComponent<Stage>();
            firstStage.SetDefault();
        }

        if (ES3.KeyExists("currentStage"))
        {
            currentStage = ES3.Load<int>("currentStage");
        }
        else
        {
            currentStage = 1;
        }
    }

    public void OpenStageSelect()
    {
        panelStageSelect.SetActive(true);
    }

    public void CloseStageSelect()
    {
        panelStageSelect.SetActive(false);
    }
}
