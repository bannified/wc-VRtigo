using System.Collections.Generic;

namespace VRT_Constants
{
    public static class Tags
    {
        public static string PLAYER = "Player";
    }

    namespace ExperienceConstants
    {
        public static class PlayerInitiatedMovement
        {
            public static string TURN_ENABLED_KEY_BOOL = "PIM_TurnEnabled";
            public static string LATERAL_ENABLED_KEY_BOOL = "PIM_LateralMovementEnabled";
            public static string NONLINEAR_ENABLED_KEY_BOOL = "PIM_NonLinearMovementEnabled";
        }

        public static class ForcedHeadRotation
        {
            public static string CAMERA_ROTATION_LOCK_BOOL = "FHR_CameraRotationLock";
        }
    }

    namespace MainMenuConstants
    {
        public static class MainMenuConstants
        {
            public static string SPAWN_IN_CLASSROOM_BOOL = "MENU_SpawnInClassroom";
        }
    }
}
