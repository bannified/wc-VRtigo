//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Input_ActionSet_default p__default;
        
        private static SteamVR_Input_ActionSet_EXPPlayerMovement p_EXPPlayerMovement;
        
        private static SteamVR_Input_ActionSet_ForcedCameraRotation p_ForcedCameraRotation;
        
        private static SteamVR_Input_ActionSet_Classroom p_Classroom;
        
        private static SteamVR_Input_ActionSet_PlayerForcedMovement p_PlayerForcedMovement;
        
        public static SteamVR_Input_ActionSet_default _default
        {
            get
            {
                return SteamVR_Actions.p__default.GetCopy<SteamVR_Input_ActionSet_default>();
            }
        }
        
        public static SteamVR_Input_ActionSet_EXPPlayerMovement EXPPlayerMovement
        {
            get
            {
                return SteamVR_Actions.p_EXPPlayerMovement.GetCopy<SteamVR_Input_ActionSet_EXPPlayerMovement>();
            }
        }
        
        public static SteamVR_Input_ActionSet_ForcedCameraRotation ForcedCameraRotation
        {
            get
            {
                return SteamVR_Actions.p_ForcedCameraRotation.GetCopy<SteamVR_Input_ActionSet_ForcedCameraRotation>();
            }
        }
        
        public static SteamVR_Input_ActionSet_Classroom Classroom
        {
            get
            {
                return SteamVR_Actions.p_Classroom.GetCopy<SteamVR_Input_ActionSet_Classroom>();
            }
        }
        
        public static SteamVR_Input_ActionSet_PlayerForcedMovement PlayerForcedMovement
        {
            get
            {
                return SteamVR_Actions.p_PlayerForcedMovement.GetCopy<SteamVR_Input_ActionSet_PlayerForcedMovement>();
            }
        }
        
        private static void StartPreInitActionSets()
        {
            SteamVR_Actions.p__default = ((SteamVR_Input_ActionSet_default)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_default>("/actions/default")));
            SteamVR_Actions.p_EXPPlayerMovement = ((SteamVR_Input_ActionSet_EXPPlayerMovement)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_EXPPlayerMovement>("/actions/EXPPlayerMovement")));
            SteamVR_Actions.p_ForcedCameraRotation = ((SteamVR_Input_ActionSet_ForcedCameraRotation)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_ForcedCameraRotation>("/actions/ForcedCameraRotation")));
            SteamVR_Actions.p_Classroom = ((SteamVR_Input_ActionSet_Classroom)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_Classroom>("/actions/Classroom")));
            SteamVR_Actions.p_PlayerForcedMovement = ((SteamVR_Input_ActionSet_PlayerForcedMovement)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_PlayerForcedMovement>("/actions/PlayerForcedMovement")));
            Valve.VR.SteamVR_Input.actionSets = new Valve.VR.SteamVR_ActionSet[] {
                    SteamVR_Actions._default,
                    SteamVR_Actions.EXPPlayerMovement,
                    SteamVR_Actions.ForcedCameraRotation,
                    SteamVR_Actions.Classroom,
                    SteamVR_Actions.PlayerForcedMovement};
        }
    }
}
