using UnityEngine;

namespace CorruptElementary
{
    public class Lunchbox : MonoBehaviour
    {
        public float money = 5;

        public void Update()
        {
            if (GameController.instance.state == GameController.gamestate.newday)
            {
                money = 5;
            }
        }

        public void ResetMoney()
        {
            money = 5;
        }
    }
}
