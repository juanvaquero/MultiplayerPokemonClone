using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using Photon.Pun;

public class UICombatController : MonoBehaviour
{

    [Header("Combat info panel")]
    [SerializeField]
    UIDialogSystem _uiCombatDialog;

    [Header("Pokemon movements panel")]
    [SerializeField]
    private UIMovementsPanel _movementsPanel;

    [Header("General Buttons")]
    [SerializeField]
    private GameObject _generalButtons;
    [SerializeField]
    private Button _fightButton;
    [SerializeField]
    private Button _runButton;

    private CombatManager _combatManager;
    private PhotonView _photonView;


    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _fightButton.onClick.AddListener(Fight);
        _runButton.onClick.AddListener(RunPun);
    }

    public void LoadPokemonMovements(Pokemon playerPokemon, CombatManager combatManager)
    {
        _combatManager = combatManager;
        _movementsPanel.Initialize(playerPokemon.Movements, playerPokemon.Abilities, combatManager);
    }

    public IEnumerator TypeCombatDialog(string text)
    {
        yield return _uiCombatDialog.TypeDialog(text);
    }

    public void SetEnableGeneralButtons(bool enable)
    {
        _generalButtons.SetActive(enable);
    }

    private void Fight()
    {
        _uiCombatDialog.SetEnableDialog(false);
    }
[PunRPC]
    private void Run()
    {
        StartCoroutine(_combatManager.EndCombat());
    }

    private void RunPun()
    {
        _photonView.RPC("Run", RpcTarget.All);
    }

    public void SetInteractableRunButton(bool enable)
    {
        _runButton.interactable = enable;
    }

}