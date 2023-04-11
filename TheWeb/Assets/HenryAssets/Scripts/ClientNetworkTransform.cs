using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;

namespace Unity.Multiplayer.Samples.Utilities.ClientAuthority
{

    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        // Start is called before the first frame update
        protected override bool OnIsServerAuthoritative() {
            return false;
        }
    }
}
