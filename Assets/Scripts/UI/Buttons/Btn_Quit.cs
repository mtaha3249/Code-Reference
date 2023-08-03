using UnityEngine;

namespace UI
{
    public class Btn_Quit : Btn_Base
    {
        public override void OnClick()
        {
            Application.Quit();
        }
    }
}