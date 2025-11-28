using UnityEngine.UI;

namespace calculator
{
    public class UISpecialButtonController 
    {
        private UISpecialButton uiButton;
        private UICallback uiCallback;

        private Button allClearBtn;
        private Button singleClearBtn;
        private Button additionBtn;
        private Button subtractBtn;
        private Button multiplyBtn;
        private Button divisionBtn;
        private Button equalBtn;

        public UISpecialButtonController(UISpecialButton uiButton, UICallback uiCallback)
        {
            this.uiButton = uiButton;
            this.uiCallback = uiCallback;
            Initialize(uiButton);
        }


        private void Initialize(UISpecialButton uiButton)
        {
            allClearBtn = uiButton.allClearBtn;
            singleClearBtn = uiButton.singleClearBtn;
            additionBtn = uiButton.additionBtn;
            subtractBtn = uiButton.subtractBtn;
            multiplyBtn = uiButton.multiplyBtn;
            divisionBtn = uiButton.divisionBtn;
            equalBtn = uiButton.equalBtn;

            allClearBtn.onClick.AddListener(uiCallback.OnClickAllClearBtnRequested);
            singleClearBtn.onClick.AddListener(uiCallback.OnClickSingleClearBtnRequested);
            additionBtn.onClick.AddListener(()=> uiCallback.OnClickNumberBtnRequested("+"));
            subtractBtn.onClick.AddListener(() => uiCallback.OnClickNumberBtnRequested("-"));
            multiplyBtn.onClick.AddListener(() => uiCallback.OnClickNumberBtnRequested("x"));
            divisionBtn.onClick.AddListener(() => uiCallback.OnClickNumberBtnRequested("/"));
            equalBtn.onClick.AddListener(uiCallback.OnClickEqualsBtnRequested);
        }
    }
}