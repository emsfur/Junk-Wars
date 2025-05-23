using UnityEngine;
using Unity.Netcode;

public class RpcTest : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn() {
             if (!IsServer && IsOwner) { //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
                TestServerRpc(0, NetworkObjectId);
             }
    }

   [ClientRpc]
   void TestClientRpc(int value, ulong sourceNetworkObjectId)
   {
       Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
       if (IsOwner) //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
       {
           TestServerRpc(value + 1, sourceNetworkObjectId);
       }
   }

  [ServerRpc]
   void TestServerRpc(int value, ulong sourceNetworkObjectId)
   {
       Debug.Log($"Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
       TestClientRpc(value, sourceNetworkObjectId);
   }
}

