using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsController : MonoBehaviour
{
    private enum State
    {
        ShowingTitle,
        FadingTitle,
        ScrollingCredits,
        Finished
    }

    [Header("Título")]
    [SerializeField] private RawImage titleImage;        // Imagen del título
    [SerializeField] private float titleVisibleTime = 2f; // Tiempo que el título está fijo en pantalla
    [SerializeField] private float titleFadeTime = 1f;    // Tiempo que tarda en desvanecerse

    [Header("Créditos")]
    [SerializeField] private RectTransform creditsPanel; // Panel que contiene el texto de créditos
    [SerializeField] private float scrollSpeed = 100f;   // Velocidad del scroll (configurable)
    [SerializeField] private float startY = -800f;       // Posición Y inicial del panel
    [SerializeField] private float endY = 1200f;         // Posición Y a la que, al llegar, cambia de escena

    [Header("Escena siguiente")]
    [SerializeField] private string nextSceneName;       // Nombre de la escena a cargar al finalizar
    [SerializeField] private float waitBeforeExit = 2f; // segundos antes de cambiar de escena

    private State currentState = State.ShowingTitle;
    private float titleTimer = 0f;
    private float fadeTimer = 0f;
    private Vector2 creditsPos;

    private void Start()
    {
        // Posicionar el panel de créditos al inicio
        if (creditsPanel != null)
        {
            creditsPos = creditsPanel.anchoredPosition;
            creditsPos.y = startY;
            creditsPanel.anchoredPosition = creditsPos;
        }

        // Asegurar que el título arranque visible
        if (titleImage != null)
        {
            Color c = titleImage.color;
            c.a = 1f;
            titleImage.color = c;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.ShowingTitle:
                UpdateShowingTitle();
                break;

            case State.FadingTitle:
                UpdateFadingTitle();
                break;

            case State.ScrollingCredits:
                UpdateScrollingCredits();
                break;

            case State.Finished:
                // No hacemos nada más acá
                break;
        }
    }

    private void UpdateShowingTitle()
    {
        // Contamos el tiempo que el título está quieto
        titleTimer += Time.deltaTime;

        if (titleTimer >= titleVisibleTime)
        {
            // Pasamos al estado de desvanecimiento
            currentState = State.FadingTitle;
            fadeTimer = 0f;
        }
    }

    private void UpdateFadingTitle()
    {
        if (titleImage == null)
        {
            // Si no hay título asignado, saltamos directo a los créditos
            currentState = State.ScrollingCredits;
            return;
        }

        fadeTimer += Time.deltaTime;
        float t = Mathf.Clamp01(fadeTimer / titleFadeTime);

        Color c = titleImage.color;
        c.a = 1f - t;      // De 1 a 0
        titleImage.color = c;

        if (t >= 1f)
        {
            // Ocultamos el objeto del título y arrancamos el scroll
            titleImage.gameObject.SetActive(false);
            currentState = State.ScrollingCredits;
        }
    }

    private void UpdateScrollingCredits()
    {
        if (creditsPanel == null) return;

        creditsPos = creditsPanel.anchoredPosition;
        creditsPos.y += scrollSpeed * Time.deltaTime;
        creditsPanel.anchoredPosition = creditsPos;

        // Cuando llega a la Y final, cargamos la escena
        if (creditsPos.y >= endY)
        {
            currentState = State.Finished;
            StartCoroutine(LoadNextSceneDelayed());
        }
    }

    private IEnumerator LoadNextSceneDelayed()
    {
        // Espera la cantidad de segundos configurada
        yield return new WaitForSeconds(waitBeforeExit);

        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Los créditos terminaron pero no se configuró 'nextSceneName' en el inspector.");
        }
    }
}
