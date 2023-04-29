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


    private void Start()
    {
        _fightButton.onClick.AddListener(Fight);
        _runButton.onClick.AddListener(Run);
    }

    public void Initialize(Pokemon playerPokemon, Pokemon opponentPokemon)
    {
        _playerInfoPanel.Initialize(playerPokemon.Name, playerPokemon.MaxHealth, playerPokemon.CurrentHealth);
        _opponentInfoPanel.Initialize(opponentPokemon.Name, opponentPokemon.MaxHealth, opponentPokemon.CurrentHealth);

        SetTextGeneralInfoPanel("What will " + playerPokemon.Name + " do?");

        //TODO fill movement panels
        _movementsPanel.Initialize(playerPokemon.Movements, playerPokemon.Abilities);
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
        CombatManager combatManager = GameManager.Instance.CombatManager;
        combatManager.EndCombat();
        //TODO review if it is necessary unload something more
    }

}