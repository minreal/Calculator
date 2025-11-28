using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace calculator
{
    public class UINumberButtonController
    {
        private List<Button> numberButtons;
        private Button dotButton;
        private UICallback callback;

        public UINumberButtonController(UINumberButton uiNumberButton, UICallback callback)
        {
            numberButtons = uiNumberButton.numberButtonList;
            dotButton = uiNumberButton.dotButton;
            this.callback = callback;

            Initialize();
        }

        private void Initialize()
        {
            foreach (var btn in numberButtons)
            {
                string value = btn.GetComponentInChildren<TMP_Text>().text;
                btn.onClick.AddListener(() => 
                { 
                    callback.OnClickNumberBtnRequested(value);
                });
            }

            dotButton.onClick.AddListener(() =>
            {
                callback.OnClickNumberBtnRequested(".");
            });
        }
    }
}
