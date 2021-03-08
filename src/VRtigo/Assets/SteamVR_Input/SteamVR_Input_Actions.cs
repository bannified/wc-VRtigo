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
        
        private static SteamVR_Action_Boolean p_default_InteractUI;
        
        private static SteamVR_Action_Boolean p_default_Teleport;
        
        private static SteamVR_Action_Boolean p_default_Grab;
        
        private static SteamVR_Action_Boolean p_default_GrabGrip;
        
        private static SteamVR_Action_Pose p_default_Pose;
        
        private static SteamVR_Action_Skeleton p_default_SkeletonLeftHand;
        
        private static SteamVR_Action_Skeleton p_default_SkeletonRightHand;
        
        private static SteamVR_Action_Single p_default_Squeeze;
        
        private static SteamVR_Action_Boolean p_default_HeadsetOnHead;
        
        private static SteamVR_Action_Boolean p_default_SnapTurnLeft;
        
        private static SteamVR_Action_Boolean p_default_SnapTurnRight;
        
        private static SteamVR_Action_Vibration p_default_Haptic;
        
        private static SteamVR_Action_Vector2 p_platformer_Move;
        
        private static SteamVR_Action_Boolean p_platformer_Jump;
        
        private static SteamVR_Action_Vector2 p_buggy_Steering;
        
        private static SteamVR_Action_Single p_buggy_Throttle;
        
        private static SteamVR_Action_Boolean p_buggy_Brake;
        
        private static SteamVR_Action_Boolean p_buggy_Reset;
        
        private static SteamVR_Action_Pose p_mixedreality_ExternalCamera;
        
        private static SteamVR_Action_Single p_eXPPlayerMovement_Move;
        
        private static SteamVR_Action_Vector2 p_eXPPlayerMovement_MoveDirection;
        
        private static SteamVR_Action_Vector2 p_eXPPlayerMovement_TurnDirection;
        
        private static SteamVR_Action_Boolean p_eXPPlayerMovement_Menu;
        
        private static SteamVR_Action_Pose p_eXPPlayerMovement_Pose;
        
        private static SteamVR_Action_Vibration p_eXPPlayerMovement_Haptic;
        
        private static SteamVR_Action_Vector2 p_forcedCameraRotation_Walking;
        
        private static SteamVR_Action_Single p_forcedCameraRotation_CameraRotateLeft;
        
        private static SteamVR_Action_Single p_forcedCameraRotation_CameraRotateRight;
        
        private static SteamVR_Action_Single p_forcedCameraRotation_CameraLookUp;
        
        private static SteamVR_Action_Single p_forcedCameraRotation_CameraLookDown;
        
        private static SteamVR_Action_Pose p_classroom_Pose;
        
        public static SteamVR_Action_Boolean default_InteractUI
        {
            get
            {
                return SteamVR_Actions.p_default_InteractUI.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_Teleport
        {
            get
            {
                return SteamVR_Actions.p_default_Teleport.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_Grab
        {
            get
            {
                return SteamVR_Actions.p_default_Grab.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_GrabGrip
        {
            get
            {
                return SteamVR_Actions.p_default_GrabGrip.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Pose default_Pose
        {
            get
            {
                return SteamVR_Actions.p_default_Pose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Skeleton default_SkeletonLeftHand
        {
            get
            {
                return SteamVR_Actions.p_default_SkeletonLeftHand.GetCopy<SteamVR_Action_Skeleton>();
            }
        }
        
        public static SteamVR_Action_Skeleton default_SkeletonRightHand
        {
            get
            {
                return SteamVR_Actions.p_default_SkeletonRightHand.GetCopy<SteamVR_Action_Skeleton>();
            }
        }
        
        public static SteamVR_Action_Single default_Squeeze
        {
            get
            {
                return SteamVR_Actions.p_default_Squeeze.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Boolean default_HeadsetOnHead
        {
            get
            {
                return SteamVR_Actions.p_default_HeadsetOnHead.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_SnapTurnLeft
        {
            get
            {
                return SteamVR_Actions.p_default_SnapTurnLeft.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_SnapTurnRight
        {
            get
            {
                return SteamVR_Actions.p_default_SnapTurnRight.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Vibration default_Haptic
        {
            get
            {
                return SteamVR_Actions.p_default_Haptic.GetCopy<SteamVR_Action_Vibration>();
            }
        }
        
        public static SteamVR_Action_Vector2 platformer_Move
        {
            get
            {
                return SteamVR_Actions.p_platformer_Move.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Boolean platformer_Jump
        {
            get
            {
                return SteamVR_Actions.p_platformer_Jump.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Vector2 buggy_Steering
        {
            get
            {
                return SteamVR_Actions.p_buggy_Steering.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Single buggy_Throttle
        {
            get
            {
                return SteamVR_Actions.p_buggy_Throttle.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Boolean buggy_Brake
        {
            get
            {
                return SteamVR_Actions.p_buggy_Brake.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean buggy_Reset
        {
            get
            {
                return SteamVR_Actions.p_buggy_Reset.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Pose mixedreality_ExternalCamera
        {
            get
            {
                return SteamVR_Actions.p_mixedreality_ExternalCamera.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Single eXPPlayerMovement_Move
        {
            get
            {
                return SteamVR_Actions.p_eXPPlayerMovement_Move.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Vector2 eXPPlayerMovement_MoveDirection
        {
            get
            {
                return SteamVR_Actions.p_eXPPlayerMovement_MoveDirection.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Vector2 eXPPlayerMovement_TurnDirection
        {
            get
            {
                return SteamVR_Actions.p_eXPPlayerMovement_TurnDirection.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Boolean eXPPlayerMovement_Menu
        {
            get
            {
                return SteamVR_Actions.p_eXPPlayerMovement_Menu.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Pose eXPPlayerMovement_Pose
        {
            get
            {
                return SteamVR_Actions.p_eXPPlayerMovement_Pose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Vibration eXPPlayerMovement_Haptic
        {
            get
            {
                return SteamVR_Actions.p_eXPPlayerMovement_Haptic.GetCopy<SteamVR_Action_Vibration>();
            }
        }
        
        public static SteamVR_Action_Vector2 forcedCameraRotation_Walking
        {
            get
            {
                return SteamVR_Actions.p_forcedCameraRotation_Walking.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Single forcedCameraRotation_CameraRotateLeft
        {
            get
            {
                return SteamVR_Actions.p_forcedCameraRotation_CameraRotateLeft.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Single forcedCameraRotation_CameraRotateRight
        {
            get
            {
                return SteamVR_Actions.p_forcedCameraRotation_CameraRotateRight.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Single forcedCameraRotation_CameraLookUp
        {
            get
            {
                return SteamVR_Actions.p_forcedCameraRotation_CameraLookUp.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Single forcedCameraRotation_CameraLookDown
        {
            get
            {
                return SteamVR_Actions.p_forcedCameraRotation_CameraLookDown.GetCopy<SteamVR_Action_Single>();
            }
        }
        
        public static SteamVR_Action_Pose classroom_Pose
        {
            get
            {
                return SteamVR_Actions.p_classroom_Pose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        private static void InitializeActionArrays()
        {
            Valve.VR.SteamVR_Input.actions = new Valve.VR.SteamVR_Action[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_GrabGrip,
                    SteamVR_Actions.default_Pose,
                    SteamVR_Actions.default_SkeletonLeftHand,
                    SteamVR_Actions.default_SkeletonRightHand,
                    SteamVR_Actions.default_Squeeze,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_SnapTurnLeft,
                    SteamVR_Actions.default_SnapTurnRight,
                    SteamVR_Actions.default_Haptic,
                    SteamVR_Actions.platformer_Move,
                    SteamVR_Actions.platformer_Jump,
                    SteamVR_Actions.buggy_Steering,
                    SteamVR_Actions.buggy_Throttle,
                    SteamVR_Actions.buggy_Brake,
                    SteamVR_Actions.buggy_Reset,
                    SteamVR_Actions.mixedreality_ExternalCamera,
                    SteamVR_Actions.eXPPlayerMovement_Move,
                    SteamVR_Actions.eXPPlayerMovement_MoveDirection,
                    SteamVR_Actions.eXPPlayerMovement_TurnDirection,
                    SteamVR_Actions.eXPPlayerMovement_Menu,
                    SteamVR_Actions.eXPPlayerMovement_Pose,
                    SteamVR_Actions.eXPPlayerMovement_Haptic,
                    SteamVR_Actions.forcedCameraRotation_Walking,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateLeft,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateRight,
                    SteamVR_Actions.forcedCameraRotation_CameraLookUp,
                    SteamVR_Actions.forcedCameraRotation_CameraLookDown,
                    SteamVR_Actions.classroom_Pose};
            Valve.VR.SteamVR_Input.actionsIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_GrabGrip,
                    SteamVR_Actions.default_Pose,
                    SteamVR_Actions.default_SkeletonLeftHand,
                    SteamVR_Actions.default_SkeletonRightHand,
                    SteamVR_Actions.default_Squeeze,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_SnapTurnLeft,
                    SteamVR_Actions.default_SnapTurnRight,
                    SteamVR_Actions.platformer_Move,
                    SteamVR_Actions.platformer_Jump,
                    SteamVR_Actions.buggy_Steering,
                    SteamVR_Actions.buggy_Throttle,
                    SteamVR_Actions.buggy_Brake,
                    SteamVR_Actions.buggy_Reset,
                    SteamVR_Actions.mixedreality_ExternalCamera,
                    SteamVR_Actions.eXPPlayerMovement_Move,
                    SteamVR_Actions.eXPPlayerMovement_MoveDirection,
                    SteamVR_Actions.eXPPlayerMovement_TurnDirection,
                    SteamVR_Actions.eXPPlayerMovement_Menu,
                    SteamVR_Actions.eXPPlayerMovement_Pose,
                    SteamVR_Actions.forcedCameraRotation_Walking,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateLeft,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateRight,
                    SteamVR_Actions.forcedCameraRotation_CameraLookUp,
                    SteamVR_Actions.forcedCameraRotation_CameraLookDown,
                    SteamVR_Actions.classroom_Pose};
            Valve.VR.SteamVR_Input.actionsOut = new Valve.VR.ISteamVR_Action_Out[] {
                    SteamVR_Actions.default_Haptic,
                    SteamVR_Actions.eXPPlayerMovement_Haptic};
            Valve.VR.SteamVR_Input.actionsVibration = new Valve.VR.SteamVR_Action_Vibration[] {
                    SteamVR_Actions.default_Haptic,
                    SteamVR_Actions.eXPPlayerMovement_Haptic};
            Valve.VR.SteamVR_Input.actionsPose = new Valve.VR.SteamVR_Action_Pose[] {
                    SteamVR_Actions.default_Pose,
                    SteamVR_Actions.mixedreality_ExternalCamera,
                    SteamVR_Actions.eXPPlayerMovement_Pose,
                    SteamVR_Actions.classroom_Pose};
            Valve.VR.SteamVR_Input.actionsBoolean = new Valve.VR.SteamVR_Action_Boolean[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_GrabGrip,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_SnapTurnLeft,
                    SteamVR_Actions.default_SnapTurnRight,
                    SteamVR_Actions.platformer_Jump,
                    SteamVR_Actions.buggy_Brake,
                    SteamVR_Actions.buggy_Reset,
                    SteamVR_Actions.eXPPlayerMovement_Menu};
            Valve.VR.SteamVR_Input.actionsSingle = new Valve.VR.SteamVR_Action_Single[] {
                    SteamVR_Actions.default_Squeeze,
                    SteamVR_Actions.buggy_Throttle,
                    SteamVR_Actions.eXPPlayerMovement_Move,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateLeft,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateRight,
                    SteamVR_Actions.forcedCameraRotation_CameraLookUp,
                    SteamVR_Actions.forcedCameraRotation_CameraLookDown};
            Valve.VR.SteamVR_Input.actionsVector2 = new Valve.VR.SteamVR_Action_Vector2[] {
                    SteamVR_Actions.platformer_Move,
                    SteamVR_Actions.buggy_Steering,
                    SteamVR_Actions.eXPPlayerMovement_MoveDirection,
                    SteamVR_Actions.eXPPlayerMovement_TurnDirection,
                    SteamVR_Actions.forcedCameraRotation_Walking};
            Valve.VR.SteamVR_Input.actionsVector3 = new Valve.VR.SteamVR_Action_Vector3[0];
            Valve.VR.SteamVR_Input.actionsSkeleton = new Valve.VR.SteamVR_Action_Skeleton[] {
                    SteamVR_Actions.default_SkeletonLeftHand,
                    SteamVR_Actions.default_SkeletonRightHand};
            Valve.VR.SteamVR_Input.actionsNonPoseNonSkeletonIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_InteractUI,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_GrabGrip,
                    SteamVR_Actions.default_Squeeze,
                    SteamVR_Actions.default_HeadsetOnHead,
                    SteamVR_Actions.default_SnapTurnLeft,
                    SteamVR_Actions.default_SnapTurnRight,
                    SteamVR_Actions.platformer_Move,
                    SteamVR_Actions.platformer_Jump,
                    SteamVR_Actions.buggy_Steering,
                    SteamVR_Actions.buggy_Throttle,
                    SteamVR_Actions.buggy_Brake,
                    SteamVR_Actions.buggy_Reset,
                    SteamVR_Actions.eXPPlayerMovement_Move,
                    SteamVR_Actions.eXPPlayerMovement_MoveDirection,
                    SteamVR_Actions.eXPPlayerMovement_TurnDirection,
                    SteamVR_Actions.eXPPlayerMovement_Menu,
                    SteamVR_Actions.forcedCameraRotation_Walking,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateLeft,
                    SteamVR_Actions.forcedCameraRotation_CameraRotateRight,
                    SteamVR_Actions.forcedCameraRotation_CameraLookUp,
                    SteamVR_Actions.forcedCameraRotation_CameraLookDown};
        }
        
        private static void PreInitActions()
        {
            SteamVR_Actions.p_default_InteractUI = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/InteractUI")));
            SteamVR_Actions.p_default_Teleport = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Teleport")));
            SteamVR_Actions.p_default_Grab = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Grab")));
            SteamVR_Actions.p_default_GrabGrip = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/GrabGrip")));
            SteamVR_Actions.p_default_Pose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/default/in/Pose")));
            SteamVR_Actions.p_default_SkeletonLeftHand = ((SteamVR_Action_Skeleton)(SteamVR_Action.Create<SteamVR_Action_Skeleton>("/actions/default/in/SkeletonLeftHand")));
            SteamVR_Actions.p_default_SkeletonRightHand = ((SteamVR_Action_Skeleton)(SteamVR_Action.Create<SteamVR_Action_Skeleton>("/actions/default/in/SkeletonRightHand")));
            SteamVR_Actions.p_default_Squeeze = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/default/in/Squeeze")));
            SteamVR_Actions.p_default_HeadsetOnHead = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/HeadsetOnHead")));
            SteamVR_Actions.p_default_SnapTurnLeft = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/SnapTurnLeft")));
            SteamVR_Actions.p_default_SnapTurnRight = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/SnapTurnRight")));
            SteamVR_Actions.p_default_Haptic = ((SteamVR_Action_Vibration)(SteamVR_Action.Create<SteamVR_Action_Vibration>("/actions/default/out/Haptic")));
            SteamVR_Actions.p_platformer_Move = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/platformer/in/Move")));
            SteamVR_Actions.p_platformer_Jump = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/platformer/in/Jump")));
            SteamVR_Actions.p_buggy_Steering = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/buggy/in/Steering")));
            SteamVR_Actions.p_buggy_Throttle = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/buggy/in/Throttle")));
            SteamVR_Actions.p_buggy_Brake = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/buggy/in/Brake")));
            SteamVR_Actions.p_buggy_Reset = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/buggy/in/Reset")));
            SteamVR_Actions.p_mixedreality_ExternalCamera = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/mixedreality/in/ExternalCamera")));
            SteamVR_Actions.p_eXPPlayerMovement_Move = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/EXPPlayerMovement/in/Move")));
            SteamVR_Actions.p_eXPPlayerMovement_MoveDirection = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/EXPPlayerMovement/in/MoveDirection")));
            SteamVR_Actions.p_eXPPlayerMovement_TurnDirection = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/EXPPlayerMovement/in/TurnDirection")));
            SteamVR_Actions.p_eXPPlayerMovement_Menu = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/EXPPlayerMovement/in/Menu")));
            SteamVR_Actions.p_eXPPlayerMovement_Pose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/EXPPlayerMovement/in/Pose")));
            SteamVR_Actions.p_eXPPlayerMovement_Haptic = ((SteamVR_Action_Vibration)(SteamVR_Action.Create<SteamVR_Action_Vibration>("/actions/EXPPlayerMovement/out/Haptic")));
            SteamVR_Actions.p_forcedCameraRotation_Walking = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/ForcedCameraRotation/in/Walking")));
            SteamVR_Actions.p_forcedCameraRotation_CameraRotateLeft = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/ForcedCameraRotation/in/CameraRotateLeft")));
            SteamVR_Actions.p_forcedCameraRotation_CameraRotateRight = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/ForcedCameraRotation/in/CameraRotateRight")));
            SteamVR_Actions.p_forcedCameraRotation_CameraLookUp = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/ForcedCameraRotation/in/CameraLookUp")));
            SteamVR_Actions.p_forcedCameraRotation_CameraLookDown = ((SteamVR_Action_Single)(SteamVR_Action.Create<SteamVR_Action_Single>("/actions/ForcedCameraRotation/in/CameraLookDown")));
            SteamVR_Actions.p_classroom_Pose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/Classroom/in/Pose")));
        }
    }
}
