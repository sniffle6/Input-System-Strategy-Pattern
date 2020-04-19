using System.Collections;
using _InputTest.WorldObjects.Scripts;
using UnityEngine;

namespace _InputTest.WorldObjects.Chest.Scripts.Monobehaviours
{
    public class TreasureChest : MonoBehaviour, IOpenable, IInteractable
    {
        [SerializeField] private float force;
        [SerializeField] private GameObject lootPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnLootAfterSeconds;
        [SerializeField] private bool hasBeenLooted;

        private WaitForSeconds _wait;
        private Animation _animator;
        private static readonly int OpenId = Animator.StringToHash("Open");
        private static readonly int CloseId = Animator.StringToHash("Close");

        public bool Opened { get; set; } = false;


        private void Awake()
        {
            _wait = new WaitForSeconds(spawnLootAfterSeconds);
            _animator = GetComponent<Animation>();
        }


        public void Open()
        {
            if (Opened == true) return;
            Opened = true;
            if (_animator != null)
                _animator.Play();
            StopAllCoroutines();
            if (!hasBeenLooted)
                StartCoroutine(SpawnLooterator());
        }

        public void Close()
        {
            /*if (Opened == false) return;
            Opened = false;
            if (_animator != null)
                _animator.SetTrigger(CloseId);*/
            StopAllCoroutines();
        }

        private IEnumerator SpawnLooterator()
        {
            yield return _wait;
            SpawnLoot();
        }


        private void SpawnLoot()
        {
            var rb = Instantiate(lootPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, force, 0) + transform.forward*force, ForceMode.Impulse);
            hasBeenLooted = true;
        }

        public void Interact()
        {
            if (Opened)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
}