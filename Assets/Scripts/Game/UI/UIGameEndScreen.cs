using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIGameEndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text reasonText;
    [SerializeField] private List<GameEndData> reasons;

    private async void Start()
    {
        var character = GameContext.Instance.characterLoadingOperation.Character;

        if (character == null) await WaitForCharacterInitialize();

        character = GameContext.Instance.characterLoadingOperation.Character;
        character.State.OnDeath += OnPlayerDeath;
        character.Movement.OnSlip += OnPlayerSlip;
        character.OnWin += OnPlayerWin;
        gameObject.SetActive(false);
    }

    public void ShowScreen(GameEndReason reason)
    {
        gameObject.SetActive(true);

        var endData = reasons.First(x => x.reason == reason);
        reasonText.text = endData.caption;
    }

    private void OnPlayerDeath()
    {
        ShowScreen(GameEndReason.Death);
    }

    private void OnPlayerSlip()
    {
        ShowScreen(GameEndReason.Slipped);
    }

    private void OnPlayerWin()
    {
        ShowScreen(GameEndReason.Win);
    }

    private async Task WaitForCharacterInitialize()
    {
        while (GameContext.Instance.characterLoadingOperation.Character == null) await Task.Yield();
    }
}