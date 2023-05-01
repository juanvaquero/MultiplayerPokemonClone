using UnityEngine;
using Photon.Pun;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private const string PLAYER_TRIGER_TAG = "PlayerTrigger";
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string SPEED = "Speed";

    [SerializeField]
    private float _speed = 5f; // Player movement speed

    private bool _blockMovement = false;
    private Vector2 _moveDirection; // Current movement direction of the player
    private Rigidbody2D _rigidBody; // Player's Rigidbody2D component

    private Animator _animator; // Player's Animator component
    private PokemonInventory _pokemonInventory; // Pokemon inventory component

    private PhotonView _photonView; // Photon view

    private CombatManager _combatManager;
    private UIController _uicontroller;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _pokemonInventory = GetComponent<PokemonInventory>();
        _photonView = GetComponent<PhotonView>();
        _moveDirection = Vector2.zero; // Set the initial movement direction to (0, 0)

        _combatManager = GameManager.Instance.CombatManager;
        _combatManager.OnCombatStart = BlockPlayerMovement;
        _combatManager.OnCombatEnd = UnBlockPlayerMovement;

        _uicontroller = GameManager.Instance.UiController;
    }

    void Update()
    {
        if (_photonView.IsMine)
        {
            if (_blockMovement)
                return;

            // Get horizontal and vertical input axis values
            float horizontalInput = Input.GetAxisRaw(HORIZONTAL);
            float verticalInput = Input.GetAxisRaw(VERTICAL);

            // Set the movement direction based on input axis values
            _moveDirection = new Vector2(horizontalInput, verticalInput);

            //Set the axis values into the animator for animete the player
            _animator.SetFloat(HORIZONTAL, _moveDirection.x);
            _animator.SetFloat(VERTICAL, _moveDirection.y);

            //For detect if the player is moving calculate te sqrMagnitude of the movement direction
            //Use sqrMagnitude instead of magnitude because it is more efficient
            float playerMoving = _moveDirection.sqrMagnitude;
            _animator.SetFloat(SPEED, playerMoving);

            CheckPokemonEncounter(playerMoving != 0);
        }
    }

    void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            if (_blockMovement)
                return;

            // Move the player based on the current movement direction and speed
            _rigidBody.MovePosition(_rigidBody.position + _moveDirection * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PLAYER_TRIGER_TAG) && !_uicontroller.IsPopupDisplayed() && !_blockMovement)
        {
            _blockMovement = true;

            //TODO Get opponent name
            //Get opponent pokemons
            PokemonInventory opponentInventory = other.GetComponentInParent<PokemonInventory>();
            _uicontroller.ShowConfirmPopup("Do you want to battle with the player?", _combatManager.StartPlayerEncounter(_pokemonInventory, opponentInventory));
        }
    }

    private void CheckPokemonEncounter(bool playerIsMoving)
    {
        if (playerIsMoving)
        {
            //Move the position of the player to down direction for check the bottom of the player sprite.
            Vector3 playerPosition = transform.position + Vector3.down * 0.5f;
            GameManager.Instance.PokemonSpawner.TryEncounterPokemon(playerPosition, _pokemonInventory);
        }
    }

    public PokemonInventory GetPokemonInventory()
    {
        return _pokemonInventory;
    }

    public void UnBlockPlayerMovement()
    {
        _blockMovement = false;
    }

    public void BlockPlayerMovement()
    {
        _blockMovement = true;
        //To reset animation player to idle state
        _animator.SetFloat(SPEED, 0f);
    }
}
