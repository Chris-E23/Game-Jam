using UnityEngine;

namespace CorruptElementary
{
    public class Positioning : MonoBehaviour
    {
        public Transform lunchposition;
        public Transform recesspositon;
        public Transform classposition;
        private GameObject _player;

        void Start()
        {
            _player = GameController.instance.player;
            if (this.gameObject.CompareTag("bully"))
            {
                this.gameObject.GetComponent<AIController>().enabled = true;


            }
        }


        void Update()
        {


        }

        public void changeposition()
        {
            switch (GameController.instance.state)
            {
                case GameController.gamestate.classtime:
                    this.transform.position = classposition.position;
                    this.transform.rotation = classposition.rotation;
                    break;
                case GameController.gamestate.lunchtime:
                    this.transform.position = lunchposition.position;
                    this.transform.rotation = lunchposition.rotation;
                    break;
                case GameController.gamestate.recess:
                    this.transform.position = recesspositon.position;
                    this.transform.rotation = recesspositon.rotation;
                    break;
            }
        }

        public void resetRotation()
        {
            var t = this.transform;
            t.rotation = GameController.instance.state switch
            {
                GameController.gamestate.classtime => classposition.rotation,
                GameController.gamestate.lunchtime => lunchposition.rotation,
                GameController.gamestate.recess => recesspositon.rotation,
                _ => t.rotation
            };
        }
    }

}
