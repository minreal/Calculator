using System;

namespace calculator
{
    public class UICallback
    {
        public static event Action OnClickAllClearBtnRequest;
        public static event Action OnClickSingleClearBtnRequest;
        public static event Action OnClickAdditionBtnRequest;
        public static event Action OnClickSubtractBtnRequest;
        public static event Action OnClickMultiplyBtnRequest;
        public static event Action OnClickDivisionBtnRequest;
        public static event Action OnClickEqualsBtnRequest;

        public static event Action<string> OnClickNumberBtnRequest;
        public static event Action<string> OnResultValueUpdateRequest;

        public void OnClickAllClearBtnRequested()
        {
            OnClickAllClearBtnRequest?.Invoke();
        }

        public void OnClickSingleClearBtnRequested()
        {
            OnClickSingleClearBtnRequest?.Invoke();
        }  
        
        public void OnClickAdditionBtnRequested()
        {
            OnClickAdditionBtnRequest?.Invoke();
        }     
        
        public void OnClickSubtractBtnRequested()
        {
            OnClickSubtractBtnRequest?.Invoke();
        }  
        
        public void OnClickMultiplyBtnRequested()
        {
            OnClickMultiplyBtnRequest?.Invoke();
        }  
        
        public void OnClickDivisionBtnRequested()
        {
            OnClickDivisionBtnRequest?.Invoke();
        }

        public void OnClickEqualsBtnRequested()
        {
            OnClickEqualsBtnRequest?.Invoke();
        }

        public void OnClickNumberBtnRequested(string value)
        {
            OnClickNumberBtnRequest?.Invoke(value);
        }

        public void OnResultValueUpdateRequested(string value)
        {
            OnResultValueUpdateRequest?.Invoke(value);
        }
    }
}