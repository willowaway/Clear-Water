using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Stage : MonoBehaviour
{
    public StageData stageData;
    public GameObject stageNumberText;
    public Sprite stageAttemptedBackgroundSprite;
    public Sprite stageDefaultBackgroundSprite;
    public Sprite stageLockBackgroundSprite;
    public Sprite filledInStarSprite;
    public Sprite outlineStarSprite;
    public GameObject stageLock;
    public List<GameObject> starGameObjects;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadStage);

        UpdatePanel();
    }

    public void LoadStage()
    {
        if (!stageData.isLocked)
        {
            ES3.Save("currentStage", stageData.stageNumber);
            SceneManager.LoadScene(stageData.stageNumber);
        }
    }

    public void SetDefault()
    {
        stageData.isLocked = false;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        // Update the stage number
        TextMeshProUGUI stageText = stageNumberText.GetComponent<TextMeshProUGUI>();
        stageText.text = stageData.stageNumber.ToString();

        // Set Star Sprites to Filled for each star
        for (int starIndex = 0; starIndex < stageData.stars; starIndex++)
        {
            starGameObjects[starIndex].GetComponent<Image>().sprite = filledInStarSprite;
        }

        // Set Background Sprite
        if (stageData.isLocked)
        {
            gameObject.GetComponent<Image>().sprite = stageLockBackgroundSprite;
            stageLock.SetActive(true);
            stageNumberText.SetActive(false);

            foreach (GameObject starGameObject in starGameObjects)
            {
                starGameObject.SetActive(false);
            }
        }
        else if (stageData.hasAttempted)
        {
            gameObject.GetComponent<Image>().sprite = stageAttemptedBackgroundSprite; ;
            stageLock.SetActive(false);
            stageNumberText.SetActive(true);

            foreach (GameObject starGameObject in starGameObjects)
            {
                starGameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = stageDefaultBackgroundSprite;
            stageLock.SetActive(false);
            stageNumberText.SetActive(true);

            foreach (GameObject starGameObject in starGameObjects)
            {
                starGameObject.SetActive(true);
            }
        }
     }
}
