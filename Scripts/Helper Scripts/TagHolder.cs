using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis {
    
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
}

public class MouseAxis {
    
    public const string MOUSE_X = "Mouse X";
    public const string MOUSE_Y = "Mouse Y";
}

public class AnimationTags {

    public const string ZOOM_IN_ANIM = "Zoom_In"; // name of Animator parameters
    public const string ZOOM_OUT_ANIM = "Zoom_Out"; // name of Animator parameters

    public const string ATTACK_SHOOT_TRIGGER = "Shoot"; // name of Animator parameters
    public const string AIM_PARAMETER = "Aim"; // name of Animator parameters

    public const string WALK_PARAMETER = "Walk"; // name of Animator parameters
    public const string RUN_PARAMETER = "Run"; // name of Animator parameters
    public const string ATTACK_TRIGGER = "Attack"; // name of Animator parameters
    public const string DEAD_TRIGGER = "Death"; // name of Animator parameters
    
}

public class Tags {

    public const string LOOK_ROOT = "Look_Root"; 
    public const string ZOOM_CAMERA = "FP_Camera";
    public const string CROSSHAIR = "Crosshair";

    public const string ARROW_TAG = "Arrow";
    public const string AXE_TAG = "Axe";
    public const string SPEAR_TAG = "Spear";

    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";

}
