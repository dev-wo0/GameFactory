using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;   //bgm
using System.Reflection;
using System.IO;
namespace GameFactory
{
    public partial class ball1 : Form
    {
        public ImageList shirt = null;
        public Bitmap bmp_avata = null;  //출력이미지
        
        private int current_position = 0; //현재 이미지 인덱스


        public ball1()   //마우스선택폼, 마우스이미지리스트
        {
            InitializeComponent();
            bmp_avata = new Bitmap(98, 195);
            this.Text = "비행기를 선택해주세용!";
            
             shirt = new ImageList();
            shirt.ColorDepth = ColorDepth.Depth8Bit;
            shirt.ImageSize = new Size(150, 150);

            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;

            shirt.Images.Add(new Bitmap(Properties.Resources.비행기1));
            shirt.Images.Add(new Bitmap(Properties.Resources.비행기2));
            shirt.Images.Add(new Bitmap(Properties.Resources.비행기3));
            shirt.Images.Add(new Bitmap(Properties.Resources.비행기4));

           
        }
        protected override void OnPaint(PaintEventArgs e)  //배경이미지 그리기
        {
            // Get image compiled as an embedded resource.
            
            Bitmap backgroundImage = new Bitmap(Properties.Resources.폼1이미지);
            Size resize_back = new Size(this.Width, this.Height);
            backgroundImage = new Bitmap(backgroundImage, resize_back);
            pictureBox1.Image = backgroundImage;

           
        }
        private void Mouse_Select_Load(object sender, EventArgs e)  //폼로드시 0번째 리스트에있는 마우스이미지 띄워주기
        {
            pictureBox2.Image = shirt.Images[0];
        }
        public void left_button_Click(object sender, EventArgs e) //왼쪽버튼 클릭이벤트
        {
            current_position--;
            if (current_position < 0)
            {
                MessageBox.Show("처음 캐릭터입니다.");
                current_position = 0;
            }
            else
                pictureBox2.Image = shirt.Images[current_position];                   
        }
        public void right_button_Click(object sender, EventArgs e) //오른쪽버튼 클릭이벤트
        {
            current_position++;
            if (current_position == 1)
                pictureBox2.Image = shirt.Images[1];
            if (current_position == 2)
                pictureBox2.Image = shirt.Images[2];
            if (current_position == 3)
                pictureBox2.Image = shirt.Images[3];
            if (current_position >= 4)
            {
                MessageBox.Show("마지막 캐릭터입니다.");
                current_position = 3;
            }
        }
        private void select_button_Click(object sender, EventArgs e)//확인버튼 클릭이벤트
        {
            bmp_avata = (Bitmap)shirt.Images[current_position];
            ball2 form1 = new ball2(this);
            form1.Show();
            this.Visible = false;
        }
    }
}
