using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICombatController : MonoBehaviour
{
    [Header("Pokemon info panels")]
    [SerializeField]
    private UIPokemonInfoPanel _playerInfoPanel;
    [SerializeField]
    private UIPokemonInfoPanel _opponentInfoPanel;

    [Header("General info panel")]
    [SerializeField]
    private GameObject _infoPanel;
    [SerializeField]
    private TextMeshProUGUI _infoPanelText;

    [Header("Pokemon movements panel")]
    [SerializeField]
    private UIMovementsPanel _movementsPanel;

    [Header("General Buttons")]
    [SerializeField]
    private Button _fightButton;
    [SerializeField]
    private Button _runButton;

    private CombatManager _combatManager;

    private void Start()
    {
        _fightButton.onClick.AddListener(Fight);
        _runButton.onClick.AddListener(Run);
    }

    public void Initialize(Pokemon playerPokemon, Pokemon opponentPokemon, CombatManager combatManager)
    {
        _combatManager = combatManager;

        _playerInfoPanel.Initialize(playerPokemon.Name, playerPokemon.MaxHealth, playerPokemon.CurrentHealth, combatManager.PlayerUnit);
        _opponentInfoPanel.Initialize(opponentPokemon.Name, opponentPokemon.MaxHealth, opponentPokemon.CurrentHealth, combatManager.OpponentUnit);

        SetTextGeneralInfoPanel("What will " + playerPokemon.Name + " do?");

        _movementsPanel.Initialize(playerPokemon.Movements, playerPokemon.Abilities, combatManager);
    }


    private void SetTextGeneralInfoPanel(string text)
    {
        _infoPanel.SetActive(true);
        _infoPanelText.text = text;
    }

    private void Fight()
    {
        _infoPanel.SetActive(false);
    }

    private void Run()
    {
        _combatManager.EndCombat();
        //TODO review if it is necessary unload something more
    }

}