using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerInputConfig", menuName = "Gameplay/PlayerInput/PlayerInputConfig")]
public class PlayerInputConfig : ScriptableObject
{
    //Camera Settings
    [SerializeField]
    public float CameraSensitivity = 200;
    [SerializeField]
    public float CameraMovementSpeed = 60; 
}
