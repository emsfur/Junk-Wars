using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Timer : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startingTime = 300f;

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
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
