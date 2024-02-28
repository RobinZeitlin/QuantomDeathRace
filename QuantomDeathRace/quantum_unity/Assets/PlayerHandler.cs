using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;
using Cinemachine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] EntityView entityView;

    public void SetPlayerCamera()
    {
        QuantumGame game = QuantumRunner.Default.Game;

        Frame frame = game.Frames.Verified;

        if(frame.TryGet(entityView.EntityRef, out PlayerLink playerLink))
        {
            if (game.PlayerIsLocal(playerLink.Player))
            {
                CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>();
                vcam.Follow = entityView.transform;
                vcam.LookAt = entityView.transform;
            }
        }
    }
}
