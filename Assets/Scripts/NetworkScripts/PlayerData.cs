// using UnityEngine;
// using Unity.Netcode;
// using Unity.Collections;
// using JetBrains.Annotations;

// public class PlayerData : NetworkBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//   NetworkVariable<int> Score = new NetworkVariable<int>();
//   NetworkVariable<FixedString128Bytes> Name = new NetworkVariable<FixedString128Bytes>();

//   public override void OnNetworkSpawn() {
//     base.OnNetworkSpawn();
//     if(!IsServer) return;
//     Score.Value=0;
//     GetNameClientRpc(
//       new ClientRpcParams
//       {
//         Send = new ClientRpcSendParams
//         {
//           TargetClientIds = new ulong[] { GetComponent<NetworkObject>() OwnerClientId }
//         } 
    
//         }
//     );
//     [ClientRpc]
    
//           }
