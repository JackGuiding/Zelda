using System;
using UnityEngine;
using UnityEngine.UI;

namespace Zelda {

    public class Panel_Login : MonoBehaviour {

        [SerializeField] Button btnStart;
        [SerializeField] Button btnSettings;
        [SerializeField] Button btnStaff;
        [SerializeField] Button btnExit;

        public Action OnStartHandle;
        public Action OnSettingsHandle;
        public Action OnStaffHandle;
        public Action OnExitHandle;

        public void Ctor() {

            btnStart.onClick.AddListener(() => {
                OnStartHandle?.Invoke();
            });

            btnSettings.onClick.AddListener(() => {
                OnSettingsHandle?.Invoke();
            });

            btnStaff.onClick.AddListener(() => {
                OnStaffHandle?.Invoke();
            });

            btnExit.onClick.AddListener(() => {
                OnExitHandle?.Invoke();
            });

        }

    }

}