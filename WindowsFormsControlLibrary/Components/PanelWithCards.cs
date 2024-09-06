using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsControlLibrary.Components
{
    public partial class PanelWithCards : UserControl
    {
        public PanelWithCards(Card card1, Card card2, string playerName)
        {
            InitializeComponent();

            pictureBox1.BackgroundImag = card1.images;
        }

        private void PanelWithCards_Load(object sender, EventArgs e)
        {

        }
    }


}
