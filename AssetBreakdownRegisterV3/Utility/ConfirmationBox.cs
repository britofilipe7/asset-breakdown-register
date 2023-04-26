using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV2.Utility
{
    public class ConfirmationBox
    {
        private string message;

        public ConfirmationBox(string message) {
            this.message = message;        
        }

        public bool Ask() {
            var result = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) return true;
            else return false;
        }
    }
}
