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
    
    
    public class SteamVR_Input_ActionSet_EXPPlayerMovement : Valve.VR.SteamVR_ActionSet
    {
        
        public virtual SteamVR_Action_Single Move
        {
            get
            {
                return SteamVR_Actions.eXPPlayerMovement_Move;
            }
        }
        
        public virtual SteamVR_Action_Vector2 MoveDirection
        {
            get
            {
                return SteamVR_Actions.eXPPlayerMovement_MoveDirection;
            }
        }
        
        public virtual SteamVR_Action_Vector2 TurnDirection
        {
            get
            {
                return SteamVR_Actions.eXPPlayerMovement_TurnDirection;
            }
        }
        
        public virtual SteamVR_Action_Boolean Menu
        {
            get
            {
                return SteamVR_Actions.eXPPlayerMovement_Menu;
            }
        }
        
        public virtual SteamVR_Action_Vibration Haptic
        {
            get
            {
                return SteamVR_Actions.eXPPlayerMovement_Haptic;
            }
        }
    }
}