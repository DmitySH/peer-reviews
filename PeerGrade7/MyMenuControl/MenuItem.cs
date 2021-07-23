using System;

namespace MenuControl
{
    public class MenuItem : MenuParent
    {
        // Methods to run.
        private readonly Action<MenuParent> methodToCallWithBack;
        private readonly Action methodToCall;


        public MenuItem(string text, Action<MenuParent> methodToCallWitchBack)
        {
            this.methodToCallWithBack = methodToCallWitchBack;
            Text = text;
        }

        public MenuItem(string text, Action methodToCall)
        {
            this.methodToCall = methodToCall;
            Text = text;
        }

        /// <summary>
        /// Runs method.
        /// </summary>
        public override void Create()
        {
            try
            {
                methodToCallWithBack?.Invoke(this);
                methodToCall?.Invoke();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
