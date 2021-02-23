using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public static class VRT_Helpers
{
    public static void ResetHMDPosition(ETrackingUniverseOrigin posType = ETrackingUniverseOrigin.TrackingUniverseStanding)
    {
        Valve.VR.OpenVR.Chaperone.ResetZeroPose(posType);
    }
}
