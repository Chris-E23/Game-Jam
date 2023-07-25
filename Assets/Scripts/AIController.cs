using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CorruptElementary
{
    public class AIController : MonoBehaviour
    {
        public Transform target;
        public NavMeshAgent agent;
        
        private GameObject _player;
        
        [SerializeField] private Transform touchplayerdirection;
        [SerializeField] private LayerMask playerlayer;
        [SerializeField] public LayerMask bullylayer;
        [SerializeField] private AudioSource bullydying;
        [SerializeField] private GameObject NPCHandler;

        private Camera _cam;

        private void Start()
        {
            agent = this.gameObject.GetComponent<NavMeshAgent>();

            _player = GameController.instance.player;
            _cam = Camera.main;
        }

        private void Update()
        {
            if (GameController.instance.state == GameController.gamestate.recess && gameObject.CompareTag("bully") &&
                !GameController.instance.pause)
            {
                target = _player.transform;
                agent = this.gameObject.GetComponent<NavMeshAgent>();
                this.GetComponent<NavMeshAgent>().enabled = true;
                MoveToTarget();
            }
            else if (GameController.instance.attackgoing)
            {
                gangAttack();
            }
        }

        private void MoveToTarget()
        {
            agent.SetDestination(target.position);
            var ray = new Ray(touchplayerdirection.position, touchplayerdirection.forward);
            
            if (Physics.Raycast(ray, out _, 2f, playerlayer))
            {
                GameController.instance.money = 0;
            }
        }

        public void gangAttack()
        {
            target = GameController.instance.bully.transform;
            agent = this.gameObject.GetComponent<NavMeshAgent>();
            this.GetComponent<NavMeshAgent>().enabled = true;
            this.GetComponent<NavMeshAgent>().speed = 10f;

            agent.SetDestination(target.position);

            Ray ray = new Ray(touchplayerdirection.position, touchplayerdirection.forward);
            GameController.instance.bully.GetComponent<AIController>().enabled = false;
            GameController.instance.bully.GetComponent<NavMeshAgent>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            this.DoAfter(8f, () => GameController.instance.winScreen.SetActive(true));

        }
    }
}
