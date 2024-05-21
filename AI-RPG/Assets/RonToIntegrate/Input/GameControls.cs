//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.2
//     from Assets/RonToIntegrate/Input/GameControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

public partial class @GameControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3ad2e3db-a696-4a78-8790-66ea5964a280"",
            ""actions"": [
                {
                    ""name"": ""Walk"",
                    ""type"": ""Value"",
                    ""id"": ""09ea3e08-c336-41a1-97db-3fad9a8f0ed7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseMovement"",
                    ""type"": ""Value"",
                    ""id"": ""d5ba8052-3c17-42d3-a29f-72a67a60bfe8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftShift"",
                    ""type"": ""Value"",
                    ""id"": ""f4a039f4-f6f1-455a-8d55-60ddc10127fa"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ActionButton1"",
                    ""type"": ""Value"",
                    ""id"": ""2ba0314e-a237-4b9a-83c7-1c228fa3a779"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ActionButton2"",
                    ""type"": ""Value"",
                    ""id"": ""35c29ed3-fee2-4ede-b5d0-03801347644f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ActionButton3"",
                    ""type"": ""Value"",
                    ""id"": ""88906022-d008-45a9-adc8-7b94ba384043"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FlightMode"",
                    ""type"": ""Button"",
                    ""id"": ""b1da9e55-ef35-4a65-a227-c21e33ff6a94"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""12a776b6-a6e3-4953-8e42-4ab3d4ef309e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interract"",
                    ""type"": ""Button"",
                    ""id"": ""8807b532-28a9-4248-97c8-f7ec967869d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""dd536b12-1627-4c73-8d6a-a3fe666f1d95"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Q"",
                    ""type"": ""Button"",
                    ""id"": ""d7eafbc0-b28b-43ec-b30d-e679036ace7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""E"",
                    ""type"": ""Button"",
                    ""id"": ""e40d32e9-f45a-421e-8206-66bda2ee8a9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""98e54712-92d8-4bbb-9290-651f765af8ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crafting"",
                    ""type"": ""Button"",
                    ""id"": ""e5a3d51e-07f5-40c5-be21-6488a2adbed9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""ffbbecb2-67cf-4194-8211-85d79f576a49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Stats"",
                    ""type"": ""Button"",
                    ""id"": ""9722af05-bd5b-4fb8-b6da-c05f95a0b532"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""e5329b62-cb55-44b8-b129-1420e20e40d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c21880a6-0994-40cf-8740-212579536b6d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""543c77b7-66e0-46ac-b3f8-069ba7d6f4ad"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""adf374ed-4d47-4a21-8de6-328f3036d4c5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""081c1a2c-b2f1-4ce7-8007-77c6dbaecf4b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3df77e2a-232c-4033-8786-9db7c4e78f28"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""575fd9e9-381b-400b-a45f-6b91dc1d578b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f4b4116-4c15-4006-9289-ce36778ce73e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c92e1ce2-289b-4aae-9f83-5625d63de79d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89bd2210-d9c1-4d2f-a1ae-203be0b41b46"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff8564fe-c978-48b8-8de5-5c08f304b81e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftShift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eecca096-209c-4968-8528-6e641104ba00"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlightMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2da2230-049c-41df-ac57-816e79004c7e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""959f096d-26e6-42e6-8dd1-d3dc9813c537"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43b4ff8c-f2b9-4402-865f-bbbfc5716597"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85f7f723-7055-4e44-9ce7-aea4028ae41c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Q"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aca012e8-f8f3-4f07-9029-1ff0566f78a1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8767894-07f6-4e14-aa47-99089554b49b"",
                    ""path"": ""<Keyboard>/#(I)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63440930-a85f-4334-9b8f-c4b395038fc3"",
                    ""path"": ""<Keyboard>/#(C)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crafting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cabc7be3-e2b7-4141-b5bb-fd8b73e2726f"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e2b58f5-00c7-4cda-a6cb-bf5dfc7e1892"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""805436c9-dd3d-49df-b0fa-f18e7972daa3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Walk = m_Player.FindAction("Walk", throwIfNotFound: true);
        m_Player_MouseMovement = m_Player.FindAction("MouseMovement", throwIfNotFound: true);
        m_Player_LeftShift = m_Player.FindAction("LeftShift", throwIfNotFound: true);
        m_Player_ActionButton1 = m_Player.FindAction("ActionButton1", throwIfNotFound: true);
        m_Player_ActionButton2 = m_Player.FindAction("ActionButton2", throwIfNotFound: true);
        m_Player_ActionButton3 = m_Player.FindAction("ActionButton3", throwIfNotFound: true);
        m_Player_FlightMode = m_Player.FindAction("FlightMode", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Interract = m_Player.FindAction("Interract", throwIfNotFound: true);
        m_Player_ScrollWheel = m_Player.FindAction("ScrollWheel", throwIfNotFound: true);
        m_Player_Q = m_Player.FindAction("Q", throwIfNotFound: true);
        m_Player_E = m_Player.FindAction("E", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        m_Player_Crafting = m_Player.FindAction("Crafting", throwIfNotFound: true);
        m_Player_Map = m_Player.FindAction("Map", throwIfNotFound: true);
        m_Player_Stats = m_Player.FindAction("Stats", throwIfNotFound: true);
        m_Player_Escape = m_Player.FindAction("Escape", throwIfNotFound: true);
    }

    ~@GameControls()
    {
        Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, GameControls.Player.Disable() has not been called.");
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Walk;
    private readonly InputAction m_Player_MouseMovement;
    private readonly InputAction m_Player_LeftShift;
    private readonly InputAction m_Player_ActionButton1;
    private readonly InputAction m_Player_ActionButton2;
    private readonly InputAction m_Player_ActionButton3;
    private readonly InputAction m_Player_FlightMode;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Interract;
    private readonly InputAction m_Player_ScrollWheel;
    private readonly InputAction m_Player_Q;
    private readonly InputAction m_Player_E;
    private readonly InputAction m_Player_Inventory;
    private readonly InputAction m_Player_Crafting;
    private readonly InputAction m_Player_Map;
    private readonly InputAction m_Player_Stats;
    private readonly InputAction m_Player_Escape;
    public struct PlayerActions
    {
        private @GameControls m_Wrapper;
        public PlayerActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Walk => m_Wrapper.m_Player_Walk;
        public InputAction @MouseMovement => m_Wrapper.m_Player_MouseMovement;
        public InputAction @LeftShift => m_Wrapper.m_Player_LeftShift;
        public InputAction @ActionButton1 => m_Wrapper.m_Player_ActionButton1;
        public InputAction @ActionButton2 => m_Wrapper.m_Player_ActionButton2;
        public InputAction @ActionButton3 => m_Wrapper.m_Player_ActionButton3;
        public InputAction @FlightMode => m_Wrapper.m_Player_FlightMode;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Interract => m_Wrapper.m_Player_Interract;
        public InputAction @ScrollWheel => m_Wrapper.m_Player_ScrollWheel;
        public InputAction @Q => m_Wrapper.m_Player_Q;
        public InputAction @E => m_Wrapper.m_Player_E;
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputAction @Crafting => m_Wrapper.m_Player_Crafting;
        public InputAction @Map => m_Wrapper.m_Player_Map;
        public InputAction @Stats => m_Wrapper.m_Player_Stats;
        public InputAction @Escape => m_Wrapper.m_Player_Escape;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Walk.started += instance.OnWalk;
            @Walk.performed += instance.OnWalk;
            @Walk.canceled += instance.OnWalk;
            @MouseMovement.started += instance.OnMouseMovement;
            @MouseMovement.performed += instance.OnMouseMovement;
            @MouseMovement.canceled += instance.OnMouseMovement;
            @LeftShift.started += instance.OnLeftShift;
            @LeftShift.performed += instance.OnLeftShift;
            @LeftShift.canceled += instance.OnLeftShift;
            @ActionButton1.started += instance.OnActionButton1;
            @ActionButton1.performed += instance.OnActionButton1;
            @ActionButton1.canceled += instance.OnActionButton1;
            @ActionButton2.started += instance.OnActionButton2;
            @ActionButton2.performed += instance.OnActionButton2;
            @ActionButton2.canceled += instance.OnActionButton2;
            @ActionButton3.started += instance.OnActionButton3;
            @ActionButton3.performed += instance.OnActionButton3;
            @ActionButton3.canceled += instance.OnActionButton3;
            @FlightMode.started += instance.OnFlightMode;
            @FlightMode.performed += instance.OnFlightMode;
            @FlightMode.canceled += instance.OnFlightMode;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Interract.started += instance.OnInterract;
            @Interract.performed += instance.OnInterract;
            @Interract.canceled += instance.OnInterract;
            @ScrollWheel.started += instance.OnScrollWheel;
            @ScrollWheel.performed += instance.OnScrollWheel;
            @ScrollWheel.canceled += instance.OnScrollWheel;
            @Q.started += instance.OnQ;
            @Q.performed += instance.OnQ;
            @Q.canceled += instance.OnQ;
            @E.started += instance.OnE;
            @E.performed += instance.OnE;
            @E.canceled += instance.OnE;
            @Inventory.started += instance.OnInventory;
            @Inventory.performed += instance.OnInventory;
            @Inventory.canceled += instance.OnInventory;
            @Crafting.started += instance.OnCrafting;
            @Crafting.performed += instance.OnCrafting;
            @Crafting.canceled += instance.OnCrafting;
            @Map.started += instance.OnMap;
            @Map.performed += instance.OnMap;
            @Map.canceled += instance.OnMap;
            @Stats.started += instance.OnStats;
            @Stats.performed += instance.OnStats;
            @Stats.canceled += instance.OnStats;
            @Escape.started += instance.OnEscape;
            @Escape.performed += instance.OnEscape;
            @Escape.canceled += instance.OnEscape;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Walk.started -= instance.OnWalk;
            @Walk.performed -= instance.OnWalk;
            @Walk.canceled -= instance.OnWalk;
            @MouseMovement.started -= instance.OnMouseMovement;
            @MouseMovement.performed -= instance.OnMouseMovement;
            @MouseMovement.canceled -= instance.OnMouseMovement;
            @LeftShift.started -= instance.OnLeftShift;
            @LeftShift.performed -= instance.OnLeftShift;
            @LeftShift.canceled -= instance.OnLeftShift;
            @ActionButton1.started -= instance.OnActionButton1;
            @ActionButton1.performed -= instance.OnActionButton1;
            @ActionButton1.canceled -= instance.OnActionButton1;
            @ActionButton2.started -= instance.OnActionButton2;
            @ActionButton2.performed -= instance.OnActionButton2;
            @ActionButton2.canceled -= instance.OnActionButton2;
            @ActionButton3.started -= instance.OnActionButton3;
            @ActionButton3.performed -= instance.OnActionButton3;
            @ActionButton3.canceled -= instance.OnActionButton3;
            @FlightMode.started -= instance.OnFlightMode;
            @FlightMode.performed -= instance.OnFlightMode;
            @FlightMode.canceled -= instance.OnFlightMode;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Interract.started -= instance.OnInterract;
            @Interract.performed -= instance.OnInterract;
            @Interract.canceled -= instance.OnInterract;
            @ScrollWheel.started -= instance.OnScrollWheel;
            @ScrollWheel.performed -= instance.OnScrollWheel;
            @ScrollWheel.canceled -= instance.OnScrollWheel;
            @Q.started -= instance.OnQ;
            @Q.performed -= instance.OnQ;
            @Q.canceled -= instance.OnQ;
            @E.started -= instance.OnE;
            @E.performed -= instance.OnE;
            @E.canceled -= instance.OnE;
            @Inventory.started -= instance.OnInventory;
            @Inventory.performed -= instance.OnInventory;
            @Inventory.canceled -= instance.OnInventory;
            @Crafting.started -= instance.OnCrafting;
            @Crafting.performed -= instance.OnCrafting;
            @Crafting.canceled -= instance.OnCrafting;
            @Map.started -= instance.OnMap;
            @Map.performed -= instance.OnMap;
            @Map.canceled -= instance.OnMap;
            @Stats.started -= instance.OnStats;
            @Stats.performed -= instance.OnStats;
            @Stats.canceled -= instance.OnStats;
            @Escape.started -= instance.OnEscape;
            @Escape.performed -= instance.OnEscape;
            @Escape.canceled -= instance.OnEscape;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnWalk(InputAction.CallbackContext context);
        void OnMouseMovement(InputAction.CallbackContext context);
        void OnLeftShift(InputAction.CallbackContext context);
        void OnActionButton1(InputAction.CallbackContext context);
        void OnActionButton2(InputAction.CallbackContext context);
        void OnActionButton3(InputAction.CallbackContext context);
        void OnFlightMode(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInterract(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnQ(InputAction.CallbackContext context);
        void OnE(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnCrafting(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnStats(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
    }
}
