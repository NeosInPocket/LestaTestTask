using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float valueChangeSpeed;
    private int characterHealth;

    private async void Start()
    {
        healthSlider.maxValue = 100;
        healthSlider.value = 100;


        var character = GameContext.Instance.characterLoadingOperation.Character;

        if (character == null) await WaitForCharacterInitialize();

        character = GameContext.Instance.characterLoadingOperation.Character;
        character.State.OnDamageTake += OnCharacterHealthChanged;
    }

    private void OnCharacterHealthChanged(float value)
    {
        healthSlider.value = value;

        StopAllCoroutines();
        StartCoroutine(ChangeSliderValue(value));
    }

    private IEnumerator ChangeSliderValue(float value)
    {
        while (healthSlider.value < value)
        {
            healthSlider.value -= valueChangeSpeed * Time.deltaTime;
            yield return null;
        }

        healthSlider.value = value;
    }

    private async Task WaitForCharacterInitialize()
    {
        while (GameContext.Instance.characterLoadingOperation.Character == null) await Task.Yield();
    }
}