using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangulation.Views
{
    public partial class UnionForm : Form
    {
        public UnionForm()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        public int Method { get; private set; }

        public int Percent { get; private set; }

        private void OnOkClick(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show(this, @"Неверно указаны параметры", @"Ошибка");
                return;
            }

            Method = index;

            int percent;
            if (!int.TryParse(textBox1.Text, out percent))
            {
                MessageBox.Show(this, @"Неверно указаны параметры", @"Ошибка");
                return;
            }

            Percent = percent;

            DialogResult = DialogResult.OK;
        }
    }
}
