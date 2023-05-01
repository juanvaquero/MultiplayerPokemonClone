using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Vector2 _spawnAreaSize;

    private void Start()
    {
        PhotonNetwork.Instantiate(_playerPrefab.name, GetRandomPosition(), Quaternion.identity);
    }

    private Vector2 GetRandomPosition()
    {
        float halfSizeX = _spawnAreaSize.x / 2f;
        float halfSizeY = _spawnAreaSize.y / 2f;
        float randomX = Random.Range(-halfSizeX, halfSizeX);
        float randomY = Random.Range(-halfSizeY, halfSizeY);
        return new Vector2(randomX, randomY);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _spawnAreaSize);
    }
}
