using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace calculator 
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UISpecialButton uiSpecialButton;
        [SerializeField] private UINumberButton uiNumberButton;
        [SerializeField] private UIPanel uiPanel;

        private UICallback uiCallback;
        private CalculatorCore calculator;
        private UIPanelController uiPanelController;

        void Awake()
        {
            GameInitialize();
        }

        private void OnEnable()
        {
            calculator.AddEventListener();
            uiPanelController.AddEventListener();
        }

        private void OnDisable()
        {
            calculator.RemoveEventListener();
            uiPanelController.RemoveEventListener();
        }

        private void GameInitialize()
        {
            uiCallback = new UICallback();
            calculator = new CalculatorCore(uiCallback);
            uiPanelController = new UIPanelController(uiPanel, uiCallback);

            new UISpecialButtonController(uiSpecialButton, uiCallback);
            new UINumberButtonController(uiNumberButton, uiCallback);
        }
    }

    [System.Serializable]
    public class UISpecialButton
    {
        public Button allClearBtn;
        public Button singleClearBtn;
        public Button additionBtn;
        public Button subtractBtn;
        public Button multiplyBtn;
        public Button divisionBtn;
        public Button equalBtn;
    }

    [System.Serializable]
    public class UINumberButton
    {
        public List<Button> numberButtonList;
        public Button dotButton;
    }

    [System.Serializable]
    public class UIPanel
    {
        public RectTransform resultPanel;
        public RectTransform buttonPanel;
    }
}
