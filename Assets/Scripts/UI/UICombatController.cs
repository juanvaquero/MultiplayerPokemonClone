using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICombatController : MonoBehaviour
{

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

    public void Initialize(Pokemon playerPokemon, CombatManager combatManager)
    {
        _combatManager = combatManager;

        LoadPokemonActions(playerPokemon, combatManager);

        SetTextGeneralInfoPanel("What will " + playerPokemon.Name + " do?");
    }

    public void LoadPokemonActions(Pokemon playerPokemon, CombatManager combatManager)
    {
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