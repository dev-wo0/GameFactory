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
    public partial class maze4 : Form
    {
        maze1 form1;
        maze5 form5;
        private Pen pen;
        private Brush brush;
        private Image In, Out, Ver, Hor;
        char[,] position;
        public Graphics g;

        public maze4(maze1 fom1)
        {
            form1 = fom1;
            InitializeComponent();
            maze5 fom5 = new maze5(fom1, this);
            form5 = fom5;
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Purple);
            position = new char[11, 11];
            In = GameFactory.Properties.Resources.입구;
            Out = GameFactory.Properties.Resources.출구;
            Ver = GameFactory.Properties.Resources.세로_벽;
            Hor = GameFactory.Properties.Resources.가로_벽;
            g = this.CreateGraphics();

            fom5.Show();

            for (int a = 0; a < 11; a++)
                for (int b = 0; b < 11; b++)
                {
                    if (form1.mazeB[a, b] == 's')
                    {
                        position[a, b] = '.';
                    }
                }             
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int x = 0; x < 6; x++)
            {
                g.DrawLine(pen, 50, 50 + x * 100, 550, 50 + x * 100);
            }

            for (int x = 0; x < 6; x++)
            {
                g.DrawLine(pen, 50 + x * 100, 50, 50 + x * 100, 550);
            }

            //입구,출구,현재위치,발견한 벽 출력
            for (int a = 0; a < 11; a++)
                for (int b = 0; b < 11; b++)
                {
                    //입구
                    if (form1.mazeB[a, b] == 's')
                    {
                        g.DrawImage(In, ((b + 1) / 2 * 100) - 25, ((a + 1) / 2 * 100) - 25);
                    }

                    //출구
                    if (form1.mazeB[a, b] == 'e')
                    {
                        g.DrawImage(Out, ((b + 1) / 2 * 100) - 25, ((a + 1) / 2 * 100) - 25);
                    }

                    //현재위치
                    if (position[a, b] == '.')
                    {
                        Rectangle rect = new Rectangle(((b + 1) / 2 * 100) - 20, ((a + 1) / 2 * 100) - 20, 40, 40);
                        g.FillEllipse(brush, rect);
                    }

                    //발견한 벽
                    if (position[a, b] == '1')
                    {
                        if (a % 2 == 1)
                        {
                            g.DrawImage(Ver, (b / 2 * 100) + 45, ((a + 1) / 2 * 100) - 50);
                        }

                        if (a % 2 == 0)
                        {
                            g.DrawImage(Hor, ((b + 1) / 2 * 100) - 50, (a / 2 * 100) + 45);
                        }
                    }
                }
        }

        //왼쪽클릭
        private void button1_Click(object sender, EventArgs e)
        {

            if (form1.TurnAB == false)
             {
                  MessageBox.Show("상대턴입니다.\n기다려주세요.");
                  return;
             }

             for (int a = 0; a < 11; a++)
                for (int b = 0; b < 11; b++)               
                    if (position[a, b] == '.')
                    {
                        if(position[a,b-1]=='1')                                                                           //////////////
                        {
                            return;
                        }

                        if (form1.mazeB[a,b-1]=='1')
                        {
                            label1.Text = "벽이 있습니다!\n상대턴으로 넘어갑니다.";
                            position[a, b - 1] = '1';
                            g.DrawImage(Ver, ((b - 1) / 2 * 100) + 45, ((a + 1) / 2 * 100) - 50);
                            form1.TurnAB = false;
                            return;
                        }

                        if (form1.mazeB[a, b - 1] == '2')                                                               /////////////////////
                        {                       
                             return;
                        }

                        if (form1.mazeB[a, b - 2] == 'e')
                        {
                            int score;                          

                            //설치되었던 벽 출력                                                                           ////////////////////////
                            for (int x = 0; x < 11; x++)
                                for (int y = 0; y < 11; y++)
                                {
                                    if (form1.mazeA[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            form5.g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            form5.g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }

                                    if (form1.mazeB[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }
                                }

                            MessageBox.Show("PlayerA win!!! ");
                            score = ++form1.playerA_score;
                            form1.label3.Text = score.ToString();

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeA[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeA[x, y] = '2';

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeB[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeB[x, y] = '2';

                           

                            Close();
                            form5.Close();
                        }

                        else
                        {
                             label1.Text = "벽은 없습니다!\n한번더 움직여주세요.";                            
                             position[a,b]='0';
                             position[a, b - 2] = '.';
                             Invalidate();
                             return;
                        }

                    }         
        }
       
        //오른쪽클릭
        private void button2_Click(object sender, EventArgs e)
        {

            if (form1.TurnAB == false)
            {
                MessageBox.Show("상대턴입니다.\n기다려주세요.");
                return;
            }

            for (int a = 0; a < 11; a++)
                for (int b = 0; b < 11; b++)
                    if (position[a, b] == '.')
                    {
                        if (position[a, b + 1] == '1')
                        {
                            return;
                        }

                        if (form1.mazeB[a, b + 1] == '1')
                        {
                            label1.Text = "벽이 있습니다!\n상대턴으로 넘어갑니다.";
                            position[a, b + 1] = '1';
                            g.DrawImage(Ver, ((b + 1) / 2 * 100) + 45, ((a + 1) / 2 * 100) - 50);
                            form1.TurnAB = false;
                            return;
                        }

                        if (form1.mazeB[a, b + 1] == '2')
                        {
                            return;
                        }

                        if (form1.mazeB[a, b + 2] == 'e')
                        {
                            int score;

                            //설치되었던 벽 출력
                            for (int x = 0; x < 11; x++)
                                for (int y = 0; y < 11; y++)
                                {
                                    if (form1.mazeA[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            form5.g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            form5.g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }

                                    if (form1.mazeB[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }
                                }

                            MessageBox.Show("PlayerA win!!! ");
                            score = ++form1.playerA_score;
                            form1.label3.Text = score.ToString();

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeA[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeA[x, y] = '2';

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeB[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeB[x, y] = '2';

                            Close();
                            form5.Close();
                        }

                        else
                        {
                            label1.Text = "벽은 없습니다!\n한번더 움직여주세요.";
                            position[a, b] = '0';
                            position[a, b + 2] = '.';
                            Invalidate();
                            return;
                        }

                    }

        }
       
        //위쪽클릭
        private void button3_Click(object sender, EventArgs e)
        {

            if (form1.TurnAB == false)
            {
                MessageBox.Show("상대턴입니다.\n기다려주세요.");
                return;
            }

            for (int a = 0; a < 11; a++)
                for (int b = 0; b < 11; b++)
                    if (position[a, b] == '.')
                    {
                        if (position[a - 1, b] == '1')
                        {
                            return;
                        }

                        if (form1.mazeB[a - 1, b] == '1')
                        {
                            label1.Text = "벽이 있습니다!\n상대턴으로 넘어갑니다.";
                            position[a - 1, b] = '1';
                            g.DrawImage(Hor, ((b + 1) / 2 * 100) - 50, ((a - 1) / 2 * 100) + 45);
                            form1.TurnAB = false;
                            return;
                        }

                        if (form1.mazeB[a - 1, b] == '2')
                        {
                            return;
                        }

                        if (form1.mazeB[a - 2, b] == 'e')
                        {
                            int score;

                            //설치되었던 벽 출력
                            for (int x = 0; x < 11; x++)
                                for (int y = 0; y < 11; y++)
                                {
                                    if (form1.mazeA[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            form5.g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            form5.g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }

                                    if (form1.mazeB[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }
                                }

                            MessageBox.Show("PlayerA win!!! ");
                            score = ++form1.playerA_score;
                            form1.label3.Text = score.ToString();

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeA[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeA[x, y] = '2';

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeB[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeB[x, y] = '2';

                            Close();
                            form5.Close();
                        }

                        else
                        {
                            label1.Text = "벽은 없습니다!\n한번더 움직여주세요.";
                            position[a, b] = '0';
                            position[a - 2, b] = '.';
                            Invalidate();
                            return;
                        }

                    }
        }

        //아래쪽클릭
        private void button4_Click(object sender, EventArgs e)
        {

            if (form1.TurnAB == false)
            {
                MessageBox.Show("상대턴입니다.\n기다려주세요.");
                return;
            }

            for (int a = 0; a < 11; a++)
                for (int b = 0; b < 11; b++)
                    if (position[a, b] == '.')
                    {
                        if (position[a + 1, b] == '1')
                        {
                            return;
                        }

                        if (form1.mazeB[a + 1, b] == '1')
                        {
                            label1.Text = "벽이 있습니다!\n상대턴으로 넘어갑니다.";
                            position[a + 1, b] = '1';
                            g.DrawImage(Hor, ((b + 1) / 2 * 100) - 50, ((a + 1) / 2 * 100) + 45);
                            form1.TurnAB = false;
                            return;
                        }

                        if (form1.mazeB[a + 1, b] == '2')
                        {
                            return;
                        }

                        if (form1.mazeB[a + 2, b] == 'e')
                        {
                            int score;

                            //설치되었던 벽 출력
                            for (int x = 0; x < 11; x++)
                                for (int y = 0; y < 11; y++)
                                {
                                    if (form1.mazeA[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            form5.g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            form5.g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }

                                    if (form1.mazeB[x, y] == '1')
                                    {
                                        if (x % 2 == 1)
                                        {
                                            g.DrawImage(Ver, (y / 2 * 100) + 45, ((x + 1) / 2 * 100) - 50);
                                        }

                                        if (x % 2 == 0)
                                        {
                                            g.DrawImage(Hor, ((y + 1) / 2 * 100) - 50, (x / 2 * 100) + 45);
                                        }
                                    }
                                }

                            MessageBox.Show("PlayerA win!!! ");
                            score = ++form1.playerA_score;
                            form1.label3.Text = score.ToString();

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeA[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeA[x, y] = '2';

                            for (int x = 1; x < 10; x++)
                                for (int y = 1; y < 10; y++)
                                    form1.mazeB[x, y] = '0';

                            for (int x = 2; x <= 8; x += 2)
                                for (int y = 2; y <= 8; y += 2)
                                    form1.mazeB[x, y] = '2';

                            Close();
                            form5.Close();
                        }

                        else
                        {
                            label1.Text = "벽은 없습니다!\n한번더 움직여주세요.";
                            position[a, b] = '0';
                            position[a + 2, b] = '.';
                            Invalidate();
                            return;
                        }

                    }
        } 
    }
}