// GENERATED AUTOMATICALLY FROM 'Assets/Player/Config/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Freemode"",
            ""id"": ""3bcd368d-a386-4464-ba84-8bdbb440b1f5"",
            ""actions"": [
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""c53f2231-134c-4607-be46-af3b63b2b473"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Button"",
                    ""id"": ""99fb3260-8b26-4f27-b8f1-84c71d5e6f6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotation"",
                    ""type"": ""Value"",
                    ""id"": ""eb06e22c-1800-4b7e-bcb8-6c3bc35b6e71"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotationLock"",
                    ""type"": ""Button"",
                    ""id"": ""637cffe8-8899-47fb-ba70-6d532aa00442"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Temp"",
                    ""type"": ""Button"",
                    ""id"": ""f426ff66-61ce-44b4-b8ff-3dcbf29337e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""451c0fe6-ac81-4d8d-8b2f-c8f1dd041ae0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8c07daf9-2582-44b5-b35a-735945013778"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""944b8952-8f07-4ad7-a3f6-96333a701021"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d06c4ce7-bd9c-4557-95b9-d4c37198385f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6d74d652-799b-4e7d-bfd7-31dcf66d218b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""99c6b91f-d12f-443d-a536-ca9a540e2466"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d7c046ee-b444-4193-883a-aecc9bd71d80"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ce798c2-ef9a-4e7a-aea2-3d9e9698a011"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotationLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""128a1158-8afb-4172-a756-c64196bb3c96"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Temp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combatmode"",
            ""id"": ""9a58ab6e-05a7-42c3-b598-5edf27c969b6"",
            ""actions"": [
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""bb54711d-8044-4bc3-aa30-ae9df273a1b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Button"",
                    ""id"": ""91368732-5ea5-4e65-9671-7b6c288d554f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotation"",
                    ""type"": ""Value"",
                    ""id"": ""9a069dd1-995e-45ab-b6c8-7ede55c0559e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotationLock"",
                    ""type"": ""Button"",
                    ""id"": ""f2d0e554-70a6-4b51-8f36-7ad0261f4f0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Temp"",
                    ""type"": ""Button"",
                    ""id"": ""abe8ff79-8769-4d04-8864-9f837cde6d27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ddb88a0-c5da-460f-9315-929a65e0deca"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""9281a10f-1e9f-4e77-b6c6-b471cb4a5ce6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9f3b3f7f-cdf3-4d4e-87fd-0a6ac6e3fe31"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""835b4e2f-dde5-4247-98e9-e0e6edcb41e5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""85fb3c1b-acd9-4397-8f32-8e661421794a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cd0ea9f4-cf6d-4092-b4d1-58922f40d8a6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b024293e-4da6-475f-8577-7e755392b064"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b6c3e9d-2ff2-444c-a784-d77c89d4e1c8"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotationLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a795407c-a173-4c3e-9766-de4f375bfcc9"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Temp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Freemode
        m_Freemode = asset.FindActionMap("Freemode", throwIfNotFound: true);
        m_Freemode_Interaction = m_Freemode.FindAction("Interaction", throwIfNotFound: true);
        m_Freemode_CameraMovement = m_Freemode.FindAction("CameraMovement", throwIfNotFound: true);
        m_Freemode_CameraRotation = m_Freemode.FindAction("CameraRotation", throwIfNotFound: true);
        m_Freemode_CameraRotationLock = m_Freemode.FindAction("CameraRotationLock", throwIfNotFound: true);
        m_Freemode_Temp = m_Freemode.FindAction("Temp", throwIfNotFound: true);
        // Combatmode
        m_Combatmode = asset.FindActionMap("Combatmode", throwIfNotFound: true);
        m_Combatmode_Interaction = m_Combatmode.FindAction("Interaction", throwIfNotFound: true);
        m_Combatmode_CameraMovement = m_Combatmode.FindAction("CameraMovement", throwIfNotFound: true);
        m_Combatmode_CameraRotation = m_Combatmode.FindAction("CameraRotation", throwIfNotFound: true);
        m_Combatmode_CameraRotationLock = m_Combatmode.FindAction("CameraRotationLock", throwIfNotFound: true);
        m_Combatmode_Temp = m_Combatmode.FindAction("Temp", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Freemode
    private readonly InputActionMap m_Freemode;
    private IFreemodeActions m_FreemodeActionsCallbackInterface;
    private readonly InputAction m_Freemode_Interaction;
    private readonly InputAction m_Freemode_CameraMovement;
    private readonly InputAction m_Freemode_CameraRotation;
    private readonly InputAction m_Freemode_CameraRotationLock;
    private readonly InputAction m_Freemode_Temp;
    public struct FreemodeActions
    {
        private @PlayerControls m_Wrapper;
        public FreemodeActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interaction => m_Wrapper.m_Freemode_Interaction;
        public InputAction @CameraMovement => m_Wrapper.m_Freemode_CameraMovement;
        public InputAction @CameraRotation => m_Wrapper.m_Freemode_CameraRotation;
        public InputAction @CameraRotationLock => m_Wrapper.m_Freemode_CameraRotationLock;
        public InputAction @Temp => m_Wrapper.m_Freemode_Temp;
        public InputActionMap Get() { return m_Wrapper.m_Freemode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FreemodeActions set) { return set.Get(); }
        public void SetCallbacks(IFreemodeActions instance)
        {
            if (m_Wrapper.m_FreemodeActionsCallbackInterface != null)
            {
                @Interaction.started -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnInteraction;
                @CameraMovement.started -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraMovement;
                @CameraRotation.started -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.performed -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.canceled -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraRotation;
                @CameraRotationLock.started -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraRotationLock;
                @CameraRotationLock.performed -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraRotationLock;
                @CameraRotationLock.canceled -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnCameraRotationLock;
                @Temp.started -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnTemp;
                @Temp.performed -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnTemp;
                @Temp.canceled -= m_Wrapper.m_FreemodeActionsCallbackInterface.OnTemp;
            }
            m_Wrapper.m_FreemodeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraRotation.started += instance.OnCameraRotation;
                @CameraRotation.performed += instance.OnCameraRotation;
                @CameraRotation.canceled += instance.OnCameraRotation;
                @CameraRotationLock.started += instance.OnCameraRotationLock;
                @CameraRotationLock.performed += instance.OnCameraRotationLock;
                @CameraRotationLock.canceled += instance.OnCameraRotationLock;
                @Temp.started += instance.OnTemp;
                @Temp.performed += instance.OnTemp;
                @Temp.canceled += instance.OnTemp;
            }
        }
    }
    public FreemodeActions @Freemode => new FreemodeActions(this);

    // Combatmode
    private readonly InputActionMap m_Combatmode;
    private ICombatmodeActions m_CombatmodeActionsCallbackInterface;
    private readonly InputAction m_Combatmode_Interaction;
    private readonly InputAction m_Combatmode_CameraMovement;
    private readonly InputAction m_Combatmode_CameraRotation;
    private readonly InputAction m_Combatmode_CameraRotationLock;
    private readonly InputAction m_Combatmode_Temp;
    public struct CombatmodeActions
    {
        private @PlayerControls m_Wrapper;
        public CombatmodeActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interaction => m_Wrapper.m_Combatmode_Interaction;
        public InputAction @CameraMovement => m_Wrapper.m_Combatmode_CameraMovement;
        public InputAction @CameraRotation => m_Wrapper.m_Combatmode_CameraRotation;
        public InputAction @CameraRotationLock => m_Wrapper.m_Combatmode_CameraRotationLock;
        public InputAction @Temp => m_Wrapper.m_Combatmode_Temp;
        public InputActionMap Get() { return m_Wrapper.m_Combatmode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatmodeActions set) { return set.Get(); }
        public void SetCallbacks(ICombatmodeActions instance)
        {
            if (m_Wrapper.m_CombatmodeActionsCallbackInterface != null)
            {
                @Interaction.started -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnInteraction;
                @CameraMovement.started -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraMovement;
                @CameraRotation.started -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.performed -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.canceled -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraRotation;
                @CameraRotationLock.started -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraRotationLock;
                @CameraRotationLock.performed -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraRotationLock;
                @CameraRotationLock.canceled -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnCameraRotationLock;
                @Temp.started -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnTemp;
                @Temp.performed -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnTemp;
                @Temp.canceled -= m_Wrapper.m_CombatmodeActionsCallbackInterface.OnTemp;
            }
            m_Wrapper.m_CombatmodeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraRotation.started += instance.OnCameraRotation;
                @CameraRotation.performed += instance.OnCameraRotation;
                @CameraRotation.canceled += instance.OnCameraRotation;
                @CameraRotationLock.started += instance.OnCameraRotationLock;
                @CameraRotationLock.performed += instance.OnCameraRotationLock;
                @CameraRotationLock.canceled += instance.OnCameraRotationLock;
                @Temp.started += instance.OnTemp;
                @Temp.performed += instance.OnTemp;
                @Temp.canceled += instance.OnTemp;
            }
        }
    }
    public CombatmodeActions @Combatmode => new CombatmodeActions(this);
    public interface IFreemodeActions
    {
        void OnInteraction(InputAction.CallbackContext context);
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraRotation(InputAction.CallbackContext context);
        void OnCameraRotationLock(InputAction.CallbackContext context);
        void OnTemp(InputAction.CallbackContext context);
    }
    public interface ICombatmodeActions
    {
        void OnInteraction(InputAction.CallbackContext context);
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraRotation(InputAction.CallbackContext context);
        void OnCameraRotationLock(InputAction.CallbackContext context);
        void OnTemp(InputAction.CallbackContext context);
    }
}
