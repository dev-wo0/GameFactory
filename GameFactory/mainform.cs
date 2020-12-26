using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameFactory
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
        }

        //미로탈출
        private void button1_Click(object sender, EventArgs e)
        {
            maze1 maze1 = new maze1();
            maze1.ShowDialog();
        }

        //부르마블
        private void button2_Click(object sender, EventArgs e)
        {
            mabel1 mabel1 = new mabel1();
            mabel1.ShowDialog();
        }

        //보석 피하기
        private void button3_Click(object sender, EventArgs e)
        {
            ball1 ball1 = new ball1();
            ball1.ShowDialog();
        }
    }
}
