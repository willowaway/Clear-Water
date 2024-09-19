using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Stage : MonoBehaviour
{
    public int stageNumber;
    public int stars;
    public bool isLocked;
    public bool hasAttempted;
    public GameObject stageNumberText;
    public Sprite stageAttemptedBackgroundSprite;
    public Sprite stageDefaultBackgroundSprite;
    public Sprite stageLockBackgroundSprite;
    public Sprite filledInStarSprite;
    public Sprite outlineStarSprite;
    public GameObject stageLock;
    public List<GameObject> starGameObjects;
    
    public Stage()
    {
        stageNumber = 0;
        stars = 0;
        isLocked = true;
        hasAttempted = false;
    }

    void Start()
    {
        TextMeshProUGUI stageText = stageNumberText.GetComponent<TextMeshProUGUI>();
        stageText.text = stageNumber.ToString();

        UpdateBackgroundSprite();

        // Set Star Sprites to Filled for each star
        for (int starIndex = 0; starIndex < stars; starIndex++)
        {
            starGameObjects[starIndex].GetComponent<Image>().sprite = filledInStarSprite;
        }
    }

    public void LoadStage()
    {
        SceneManager.LoadScene(stageNumber);
    }

    public void SetDefault()
    {
        isLocked = false;
        UpdateBackgroundSprite();
    }

    private void UpdateBackgroundSprite()
    {
        // Set Background Sprite
        if (isLocked)
        {
            gameObject.GetComponent<Image>().sprite = stageLockBackgroundSprite;
            stageLock.SetActive(true);
            stageNumberText.SetActive(false);

            foreach (GameObject starGameObject in starGameObjects)
            {
                starGameObject.SetActive(false);
            }
        }
        else if (!hasAttempted)
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
