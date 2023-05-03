// GENERATED AUTOMATICALLY FROM 'Assets/Input Manager/inputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""inputManager"",
    ""maps"": [
        {
            ""name"": ""Giocatore"",
            ""id"": ""b21a0be6-3c7c-4fde-92ba-d951174c29dc"",
            ""actions"": [
                {
                    ""name"": ""Movimento"",
                    ""type"": ""Value"",
                    ""id"": ""e1bc9d7d-8d45-4e0b-ba35-71b1c685151a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotazioneVista"",
                    ""type"": ""Value"",
                    ""id"": ""48b4a186-eefa-4850-af5b-6ce767905507"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Salto"",
                    ""type"": ""Button"",
                    ""id"": ""da53dde3-9ec9-45a7-86c1-66fca2ac177d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sparo"",
                    ""type"": ""Button"",
                    ""id"": ""f26f3809-c089-4931-ab45-7752eea8acba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Uso Rum"",
                    ""type"": ""Button"",
                    ""id"": ""82dc36c7-d7da-484a-a419-acdaa5f40eac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Tastiera"",
                    ""id"": ""4dcc89e8-1e17-40d5-8b8b-021e323ed295"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimento"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9ba46b25-ed3e-4578-b79b-b26a8e0c028b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""08a0f9be-3fe9-4422-8e0f-41f765a603b6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7670eb44-0675-4e6d-8475-4ddb554a2825"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9e5bc89b-3f9f-4042-8ac7-7a2708d248a7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movimento"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d37ec524-57ce-4209-b5ea-58f88a4f0bad"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotazioneVista"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5e522b8-617c-4a43-9c99-2173b5402f1b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Salto"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a162d99-8993-427c-8779-ff2177f33f07"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sparo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c441abc-3fd1-40f6-bf68-67da88cc9289"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Uso Rum"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a70e5e89-0225-4691-976e-779c372dda3e"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Uso Rum"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Generali"",
            ""id"": ""37d1c5cc-688c-4b22-9f63-71206622120b"",
            ""actions"": [
                {
                    ""name"": ""Pausa"",
                    ""type"": ""Button"",
                    ""id"": ""71dc6642-f5ed-4631-aaab-3d6e7ecf47ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9343ebad-568d-4dda-a089-0300db0535d9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pausa"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77430a8b-a29a-43a7-aa96-7654c8c5a5f4"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pausa"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Giocatore
        m_Giocatore = asset.FindActionMap("Giocatore", throwIfNotFound: true);
        m_Giocatore_Movimento = m_Giocatore.FindAction("Movimento", throwIfNotFound: true);
        m_Giocatore_RotazioneVista = m_Giocatore.FindAction("RotazioneVista", throwIfNotFound: true);
        m_Giocatore_Salto = m_Giocatore.FindAction("Salto", throwIfNotFound: true);
        m_Giocatore_Sparo = m_Giocatore.FindAction("Sparo", throwIfNotFound: true);
        m_Giocatore_UsoRum = m_Giocatore.FindAction("Uso Rum", throwIfNotFound: true);
        // Generali
        m_Generali = asset.FindActionMap("Generali", throwIfNotFound: true);
        m_Generali_Pausa = m_Generali.FindAction("Pausa", throwIfNotFound: true);
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

    // Giocatore
    private readonly InputActionMap m_Giocatore;
    private IGiocatoreActions m_GiocatoreActionsCallbackInterface;
    private readonly InputAction m_Giocatore_Movimento;
    private readonly InputAction m_Giocatore_RotazioneVista;
    private readonly InputAction m_Giocatore_Salto;
    private readonly InputAction m_Giocatore_Sparo;
    private readonly InputAction m_Giocatore_UsoRum;
    public struct GiocatoreActions
    {
        private @InputManager m_Wrapper;
        public GiocatoreActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movimento => m_Wrapper.m_Giocatore_Movimento;
        public InputAction @RotazioneVista => m_Wrapper.m_Giocatore_RotazioneVista;
        public InputAction @Salto => m_Wrapper.m_Giocatore_Salto;
        public InputAction @Sparo => m_Wrapper.m_Giocatore_Sparo;
        public InputAction @UsoRum => m_Wrapper.m_Giocatore_UsoRum;
        public InputActionMap Get() { return m_Wrapper.m_Giocatore; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GiocatoreActions set) { return set.Get(); }
        public void SetCallbacks(IGiocatoreActions instance)
        {
            if (m_Wrapper.m_GiocatoreActionsCallbackInterface != null)
            {
                @Movimento.started -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnMovimento;
                @Movimento.performed -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnMovimento;
                @Movimento.canceled -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnMovimento;
                @RotazioneVista.started -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnRotazioneVista;
                @RotazioneVista.performed -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnRotazioneVista;
                @RotazioneVista.canceled -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnRotazioneVista;
                @Salto.started -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnSalto;
                @Salto.performed -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnSalto;
                @Salto.canceled -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnSalto;
                @Sparo.started -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnSparo;
                @Sparo.performed -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnSparo;
                @Sparo.canceled -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnSparo;
                @UsoRum.started -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnUsoRum;
                @UsoRum.performed -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnUsoRum;
                @UsoRum.canceled -= m_Wrapper.m_GiocatoreActionsCallbackInterface.OnUsoRum;
            }
            m_Wrapper.m_GiocatoreActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movimento.started += instance.OnMovimento;
                @Movimento.performed += instance.OnMovimento;
                @Movimento.canceled += instance.OnMovimento;
                @RotazioneVista.started += instance.OnRotazioneVista;
                @RotazioneVista.performed += instance.OnRotazioneVista;
                @RotazioneVista.canceled += instance.OnRotazioneVista;
                @Salto.started += instance.OnSalto;
                @Salto.performed += instance.OnSalto;
                @Salto.canceled += instance.OnSalto;
                @Sparo.started += instance.OnSparo;
                @Sparo.performed += instance.OnSparo;
                @Sparo.canceled += instance.OnSparo;
                @UsoRum.started += instance.OnUsoRum;
                @UsoRum.performed += instance.OnUsoRum;
                @UsoRum.canceled += instance.OnUsoRum;
            }
        }
    }
    public GiocatoreActions @Giocatore => new GiocatoreActions(this);

    // Generali
    private readonly InputActionMap m_Generali;
    private IGeneraliActions m_GeneraliActionsCallbackInterface;
    private readonly InputAction m_Generali_Pausa;
    public struct GeneraliActions
    {
        private @InputManager m_Wrapper;
        public GeneraliActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pausa => m_Wrapper.m_Generali_Pausa;
        public InputActionMap Get() { return m_Wrapper.m_Generali; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneraliActions set) { return set.Get(); }
        public void SetCallbacks(IGeneraliActions instance)
        {
            if (m_Wrapper.m_GeneraliActionsCallbackInterface != null)
            {
                @Pausa.started -= m_Wrapper.m_GeneraliActionsCallbackInterface.OnPausa;
                @Pausa.performed -= m_Wrapper.m_GeneraliActionsCallbackInterface.OnPausa;
                @Pausa.canceled -= m_Wrapper.m_GeneraliActionsCallbackInterface.OnPausa;
            }
            m_Wrapper.m_GeneraliActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pausa.started += instance.OnPausa;
                @Pausa.performed += instance.OnPausa;
                @Pausa.canceled += instance.OnPausa;
            }
        }
    }
    public GeneraliActions @Generali => new GeneraliActions(this);
    public interface IGiocatoreActions
    {
        void OnMovimento(InputAction.CallbackContext context);
        void OnRotazioneVista(InputAction.CallbackContext context);
        void OnSalto(InputAction.CallbackContext context);
        void OnSparo(InputAction.CallbackContext context);
        void OnUsoRum(InputAction.CallbackContext context);
    }
    public interface IGeneraliActions
    {
        void OnPausa(InputAction.CallbackContext context);
    }
}
