using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class PointsHandler : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI p1Points;
    [SerializeField] private TextMeshProUGUI p2Points;

    private NetworkVariable<int> player1Points = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Server);
    private NetworkVariable<int> player2Points = new NetworkVariable<int>(writePerm: NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn() {
        if (IsServer)
        {
            player1Points.Value = 0;
            player2Points.Value = 0;
        }


        player1Points.OnValueChanged += OnPlayer1PointsChanged;
        player2Points.OnValueChanged += OnPlayer2PointsChanged;

    }

    // handles adding/subtracting points from player variables
    public void UpdatePoints(int player, int val) {
        if (player == 1) {
            ModPointsP1ServerRpc(val);
        } else if (player == 2) {
            ModPointsP2ServerRpc(val);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void ModPointsP1ServerRpc(int val) {
        player1Points.Value += val;
    }

    [ServerRpc(RequireOwnership = false)]
    void ModPointsP2ServerRpc(int val) {
        player2Points.Value += val;
    }

    private void OnPlayer1PointsChanged(int oldValue, int newValue)
    {
        if (p1Points == null)
        {
            Debug.LogWarning("PointsTMP reference is missing!");
            return;
        }

        p1Points.text = $"{newValue}";
    }

    private void OnPlayer2PointsChanged(int oldValue, int newValue)
    {
        if (p2Points == null)
        {
            Debug.LogWarning("PointsTMP reference is missing!");
            return;
        }

        p2Points.text = $"{newValue}";
    }


}
