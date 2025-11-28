using UnityEngine;
using System.Globalization;

using TMPro;

namespace calculator
{
    public class UIPanelController : IEventListener
    {
        private UIPanel uiPanel;
        private UICallback uiCallback;

        private TMP_Text resultText;


        public UIPanelController(UIPanel panel, UICallback callback)
        {
            uiPanel = panel;
            uiCallback = callback;
            Initialize();
        }

        public void AddEventListener()
        {
            UICallback.OnResultValueUpdateRequest += UpdateResult;
        }

        public void RemoveEventListener()
        {
            UICallback.OnResultValueUpdateRequest -= UpdateResult;
        }

        private void Initialize()
        {
            resultText = uiPanel.resultPanel.GetComponent<TMP_Text>();
        }

        private void UpdateResult(string value)
        {
            resultText.text = value;
        }

       
    }

}
