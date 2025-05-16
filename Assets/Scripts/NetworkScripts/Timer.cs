using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Timer : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startingTime = 300f;
    [SerializeField] private AudioClip timerBeepSound;
    [SerializeField] private AudioSource timerAudioSource;

    private int lastSecondPlayed = -1;
    private float beepThreshold = 30f;

    private NetworkVariable<float> remainingTime = new NetworkVariable<float>(
        writePerm: NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        Debug.Log("The StartingTime:" + startingTime);
        if (IsServer)
        {
            remainingTime.Value = startingTime;
            Debug.Log("[Server] Starting Time Set: " + remainingTime.Value);
        }
    }

    private void Update()
    {
        Debug.Log("Current Remaining Time: " + remainingTime.Value);

        if (IsServer)
        {
            if (remainingTime.Value > 0f)
            {
                remainingTime.Value -= Time.deltaTime;

                int currentSecond = Mathf.CeilToInt(remainingTime.Value);

                if (currentSecond <= beepThreshold && currentSecond != lastSecondPlayed && currentSecond > 0)
                {
                    lastSecondPlayed = currentSecond;
                    PlayTimerBeepClientRpc();
                }
            }
            else
            {
                remainingTime.Value = 0f;
                Debug.Log("[Server] Timer ended.");
            }
        }
        Debug.Log("Update Here" + remainingTime.Value);
        UpdateTimerDisplay(remainingTime.Value);
    }

    private void UpdateTimerDisplay(float time)
    {
        if (timerText == null)
        {
            Debug.LogWarning("TimerText reference is missing!");
            return;
        }

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"Time Remaining\n{minutes:00}:{seconds:00}";
    }

    [ClientRpc]
    private void PlayTimerBeepClientRpc()
    {
        if (timerBeepSound != null && timerAudioSource != null)
        {
            timerAudioSource.PlayOneShot(timerBeepSound);
        }
    }
}
