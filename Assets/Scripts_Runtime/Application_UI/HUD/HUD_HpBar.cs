using UnityEngine;
using UnityEngine.UI;

namespace Zelda {

    public class HUD_HpBar : MonoBehaviour {

        [SerializeField] Image imgBG;
        [SerializeField] Image imgBar;

        public void Ctor() {

        }

        public void SetHp(float hp, float maxHp) {
            if (maxHp == 0) {
                imgBG.fillAmount = 0;
                imgBar.fillAmount = 0;
                return;
            }
            imgBar.fillAmount = hp / maxHp;
        }

        public void SetPosition(Vector3 position, Vector3 cameraForward) {
            transform.position = position;
            transform.forward = cameraForward;
        }

    }

}