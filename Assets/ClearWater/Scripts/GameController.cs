using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI completionLabel;
    public TextMeshProUGUI purityLabel;
    public StageController stageController;

    [Header("Popups")]
    public GameObject popupComplete;
    public GameObject popupFailed;
    public GameObject popupSettings;

    [Space(10)]
    [Header("Popup Complete")]
    public TextMeshProUGUI popupCompletePurityPercent;
    public TextMeshProUGUI popupCompleteFinishText;
    public GameObject starOne;
    public GameObject starTwo;
    public GameObject starThree;

    [Header("Popup Failed")]
    public TextMeshProUGUI popupFailedPurityPercent;
    public TextMeshProUGUI popupFailedFinishText;

    [Space(10)]
    [Header("Game UI")]
    public GameObject gameUI;
    public GameObject panelStageSelect;

    void Start()
    {
    }

    public void UpdateScore(int finishedParticles, int coloredParticles)
    {
        int completion = Mathf.CeilToInt(finishedParticles / 600.0f * 100);
        int purity = Mathf.CeilToInt((1 - coloredParticles / 600.0f) * 100);

        completionLabel.text = completion + "%";
        purityLabel.text = purity + "%";

        if (completion > 90 && gameUI.activeSelf)
        {
            if (purity > 50)
            {
                LevelComplete(purity);
            }
            else
            {
                LevelFailed(purity);
            }
        }
    }

    private void LevelComplete(int purity)
    {
        gameUI.SetActive(false);

        popupCompletePurityPercent.text = purity + "%";

        int stars = 0;
        if (purity > 95)
        {
            popupCompleteFinishText.text = "Perfection!";
            starOne.SetActive(true);
            starTwo.SetActive(true);
            starThree.SetActive(true);
            stars = 3;
        }
        else if (purity > 75)
        {
            popupCompleteFinishText.text = "Well done";
            starOne.SetActive(true);
            starTwo.SetActive(true);
            starThree.SetActive(false);
            stars = 2;
        }
        else // purity > 50
        {
            popupCompleteFinishText.text = "Good job";
            starOne.SetActive(true);
            starTwo.SetActive(false);
            starThree.SetActive(false);
            stars = 1;
        }

        popupComplete.SetActive(true);

        stageController.CompleteCurrentStage(stars);
    }

    private void LevelFailed(int purity)
    {
        gameUI.SetActive(false);

        popupFailedPurityPercent.text = purity + "%";
        popupFailedFinishText.text = "Not enough purity";

        popupFailed.SetActive(true);
    }

    public void RestartStage()
    {
        popupComplete.SetActive(false);
        SceneManager.LoadScene(stageController.currentStage);
    }

    public void OpenStageSelect()
    {
        panelStageSelect.SetActive(true);
        popupComplete.SetActive(false);
        popupFailed.SetActive(false);

        stageController.LoadStages();
    }

    public void CloseStageSelect()
    {
        panelStageSelect.SetActive(false);
    }
}
