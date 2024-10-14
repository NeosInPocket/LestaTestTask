using Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;

    private void Start()
    {
        vcam.Follow = GameContext.Instance.characterLoadingOperation.Character.transform;
        vcam.LookAt = GameContext.Instance.characterLoadingOperation.Character.transform;
    }
}