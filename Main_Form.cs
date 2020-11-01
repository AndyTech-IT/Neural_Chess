using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neural_Chess
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
            Chess_Core core = new Chess_Core();
            Console.WriteLine(core);
            core.MoveFigure(new BoardPosition(6, 7), new BoardPosition(5, 7));
            Console.WriteLine(core);
        }
    }
}
