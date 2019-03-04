using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class frmCalculator : Form
    {
        private Calculator calc;

        public frmCalculator()
        {
            InitializeComponent();
        }

        private void frmCalculator_Load(object sender, EventArgs e)
        {
            calc = new Calculator();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string input = txtOperation.Text;

            input = input.Replace(" ", String.Empty);

            calc.Calculate(input);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtOperation.Text = String.Empty;
        }
    }
}
