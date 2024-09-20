using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public GameObject totalStarsText;
    public GameObject stageContainer;

    public GameObject stagePrefab;
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
    }

    public void SaveStage(int currentStage, int stars)
    {
        Stage stage = stages[currentStage].GetComponent<Stage>();
        stage.stars = stars;
        stage.isLocked = false;

        if (currentStage + 1 < numberOfStages)
        {
            Stage nextStage = stages[currentStage + 1].GetComponent<Stage>();
            nextStage.SetDefault();
        }

        ES3.Save<List<GameObject>>("stages", stages);
    }

}
