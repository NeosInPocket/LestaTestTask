using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingScreen : MonoBehaviour
{
    [SerializeField] private Canvas rootCanvas;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TMP_Text progressValueText;
    [SerializeField] private TMP_Text currentOperationText;
    [SerializeField] private float sliderFillSpeed;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeOutTime;

    private float currentProgress;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public async Task Load(Queue<ILoadingOperation> loadingOperations)
    {
        rootCanvas.enabled = true;
        canvasGroup.alpha = 1f;
        StartCoroutine(UpdateProgressBar());

        foreach (var operation in loadingOperations)
        {
            ResetSlider();
            currentOperationText.text = operation.Description;

            await operation.Load(OnProgress);
            await WaitForBarFill();
        }

        StartCoroutine(FadeOut());
    }

    public async Task Load(Queue<ILoadingOperation> loadingOperations, List<Action> onLoadingEndActions)
    {
        await Load(loadingOperations);

        foreach (var endAction in onLoadingEndActions) endAction();
    }

    private async Task WaitForBarFill()
    {
        while (progressSlider.value < currentProgress) await Task.Delay(1);

        await Task.Delay(TimeSpan.FromSeconds(0.15f));
    }

    private void OnProgress(float progress)
    {
        currentProgress = progress;
    }

    private void ResetSlider()
    {
        progressSlider.value = 0;
        currentProgress = 0;
        progressValueText.text = (int)(currentProgress * 100f) + "%";
    }

    private IEnumerator UpdateProgressBar()
    {
        while (rootCanvas.enabled)
        {
            if (progressSlider.value < currentProgress)
            {
                progressSlider.value += Time.deltaTime * sliderFillSpeed;
                progressValueText.text = (int)(currentProgress * 100f) + "%";
            }

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float timeElapsed = 0;

        while (timeElapsed < fadeOutTime)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = 1f - timeElapsed / fadeOutTime;

            yield return null;
        }

        rootCanvas.enabled = false;
    }
}