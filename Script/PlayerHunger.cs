using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHunger : MonoBehaviour
{
    public bool dead;
    public float maxHunger = 100f;
    public float currentHunger;
    public float hungerDecreaseRate = 1f;
    public float hungerIncreaseAmount = 20f;

    public Slider hungerSlider;
    public TMP_Text gameOverText;

    public KeyCode eatKey = KeyCode.E;
    public KeyCode restartKey = KeyCode.R;

    void Start()
    {
        dead = false;
        currentHunger = maxHunger;
        InvokeRepeating("DecreaseHunger", 1f, 1f);
        UpdateGameOverText(false);
    }

    void Update()
    {
        if (!dead && currentHunger <= 0f)
        {
            Debug.Log("Game Over - Starved!");
            Time.timeScale = 0;
            dead = true;
            UpdateGameOverText(true);

            if (Input.GetKeyDown(restartKey))
            {
                RestartGame();
            }
        }

        UpdateHungerUI();

        if (!dead && Input.GetKeyDown(eatKey))
        {
            TryEat();
        }

        if (dead && Input.GetKeyDown(restartKey))
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        dead = false;
        currentHunger = maxHunger;
        Time.timeScale = 1;
        UpdateGameOverText(false);
    }

    void DecreaseHunger()
    {
        if (!dead)
        {
            currentHunger -= hungerDecreaseRate;
            currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
        }
    }

    void TryEat()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Food"))
            {
                EatObject(hit.collider.gameObject);
            }
        }
    }

    void EatObject(GameObject objectToEat)
    {
        Destroy(objectToEat);
        currentHunger += hungerIncreaseAmount;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
    }

    void UpdateHungerUI()
    {
        if (hungerSlider != null)
        {
            hungerSlider.value = currentHunger / maxHunger;
        }
    }

    void UpdateGameOverText(bool show)
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(show);
        }
    }
}
