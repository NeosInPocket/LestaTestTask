using System.Collections;
using UnityEngine;

public class DelayedDamageTileBehaviour : TileBehaviour
{
    [SerializeField] private DelayedDamageTileBehaviourConfig config;
    [SerializeField] private DelayedDamageTileView view;
    private CharacterState characterState;
    private float damageRatio;
    private bool isReloading;

    protected override void HandleCollisionExit(Collision other)
    {
        characterState = null;

        StopCoroutine(EnableDamageIncreasing());
        StartCoroutine(DisableDamageIncreasing());
    }

    protected override void HandleCollisionEnter(Collision other)
    {
        if (isReloading) return;
        StopAllCoroutines();

        if (other.gameObject.TryGetComponent(out CharacterContainer character))
        {
            characterState = character.State;
            StartCoroutine(EnableDamageIncreasing());
        }
    }

    private IEnumerator EnableDamageIncreasing()
    {
        while (damageRatio < 1f)
        {
            damageRatio += Time.deltaTime * config.DamageTime;
            view.UpdateView(damageRatio);
            yield return null;
        }

        damageRatio = 1f;
        characterState.TakeDamage(config.DamageAmount);
        StartCoroutine(Reload());
        view.UpdateView(damageRatio);
    }

    private IEnumerator DisableDamageIncreasing()
    {
        while (damageRatio > 0)
        {
            damageRatio -= Time.deltaTime * config.DamageTime;
            view.UpdateView(damageRatio);
            yield return null;
        }

        damageRatio = 0;
        view.UpdateView(damageRatio);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(config.ReloadTime);
        isReloading = false;
        ResetTile();
    }

    private void ResetTile()
    {
        view.UpdateView(0);
    }
}