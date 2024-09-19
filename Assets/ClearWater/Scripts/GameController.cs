using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI completionLabel;
    public TextMeshProUGUI purityLabel;

    [Header("Popups")]
    public GameObject popupComplete;
    public GameObject popupFailed;
    public GameObject popupSettings;

    [Space(10)]
    [Header("Popup Complete")]
    public TextMeshProUGUI popupCompletePurityPercent;
    public TextMeshProUGUI popupCompleteFinishText;

    [Header("Popup Failed")]
    public TextMeshProUGUI popupFailedPurityPercent;
    public TextMeshProUGUI popupFailedFinishText;

    [Space(10)]
    [Header("Game UI")]
    public GameObject gameUI;

    public void UpdateScore(int finishedParticles, int coloredParticles)
    {
        int completion = Mathf.CeilToInt(finishedParticles / 600.0f * 100);
        int purity = Mathf.CeilToInt((1 - coloredParticles / 600.0f) * 100);

        completionLabel.text = completion + "%";
        purityLabel.text = purity + "%";

        if (completion > 90)
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

        if (purity > 95)
        {
            popupCompleteFinishText.text = "Perfection!";
        }
        else if (purity > 75)
        {
            popupCompleteFinishText.text = "Well done";
        }
        else // purity > 50
        {
            popupCompleteFinishText.text = "Good job";
        }

        popupComplete.SetActive(true);
    }

    private void LevelFailed(int purity)
    {
        gameUI.SetActive(false);

        popupFailedPurityPercent.text = purity + "%";
        popupFailedFinishText.text = "Not enough purity";

        popupFailed.SetActive(true);
    }
}
