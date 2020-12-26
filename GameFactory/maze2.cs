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
    public partial class maze2 : Form
    {    
        maze1 form1;
        private Pen pen;
        private Image In, Out, Ver, Hor;
        int count = 0;
        int wall = 0;
        int ax, ay, a2x, a2y, bx, by, cx, cy;
        bool direction=true;
        bool Truefalse = false;
        char[,] find = new char[11, 11];

        //미로탐색 프로그램
        void findmaze(int i, int j)
        {
            
            if (form1.mazeA[i, j] == 'e')
            {
                Truefalse = true;
            }

            //지나온 길
            find[i, j] = '.';         

            if (j + 1 < 11 && form1.mazeA[i, j + 1] != '1' && form1.mazeA[i, j + 1] != '2' && find[i, j + 1] != '.')
            {
                findmaze(i, j + 1);
            }
            if (j - 1 >= 0 && form1.mazeA[i, j - 1] != '1' && form1.mazeA[i, j - 1] != '2' && find[i, j - 1] != '.')
            {
                findmaze(i, j - 1);
            }
            if (i + 1 < 11 && form1.mazeA[i + 1, j] != '1' && form1.mazeA[i + 1, j] != '2' && find[i + 1, j] != '.')
            {
                findmaze(i + 1, j);
            }
            if (i - 1 >= 0 && form1.mazeA[i - 1, j] != '1' && form1.mazeA[i - 1, j] != '2' && find[i - 1, j] != '.')
            {
                findmaze(i - 1, j);
            }

        }

    public maze2(maze1 fom1)
        {
            form1 = fom1;

            InitializeComponent();
            pen = new Pen(Color.Black);
            In = GameFactory.Properties.Resources.입구;
            Out = GameFactory.Properties.Resources.출구;
            Ver = GameFactory.Properties.Resources.세로_벽;
            Hor = GameFactory.Properties.Resources.가로_벽;

        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();                                       //500X500크기 미로
                                                                                      //가로간격 50부터 100만큼 550까지
            for (int x = 0; x < 6; x++)                                               //세로간격 50부터 100만큼 550까지
            {
                g.DrawLine(pen, 50, 50 + x * 100, 550, 50 + x * 100);
            }
            
            for (int x = 0; x < 6; x++)
            {
                g.DrawLine(pen, 50 + x * 100, 50, 50 + x * 100, 550);
            }

            //플레이어A의 입구,출구,벽 출력
            for (int a = 0; a < 11; a++)
                for (int b = 0; b < 11; b++)
                {
                    //입구
                    if (form1.mazeA[a, b] == 's')
                    {
                        g.DrawImage(In, ((b + 1) / 2 * 100) - 25, ((a + 1) / 2 * 100) - 25);
                    }

                    //출구
                    if (form1.mazeA[a, b] == 'e')
                    {
                        g.DrawImage(Out, ((b + 1) / 2 * 100) - 25, ((a + 1) / 2 * 100) - 25);
                    }

                    //가로, 세로벽
                    if (form1.mazeA[a, b] == '1')
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

        //저장
        private void button1_Click(object sender, EventArgs e)      
        {   
            if (count != 17)
            {
                MessageBox.Show("입구와 출구, 벽이 아직 생성되지 않았습니다!!");
                return;
            }

            //입구지점 탐색 후 미로탐색
            for (int a=0;a<11 ;a++)                              
                for(int b=0; b<11;b++)             
                    if(form1.mazeA[a,b]=='s')
                    {
                        findmaze(a, b);

                        if (Truefalse==false)
                        {
                            MessageBox.Show("만드신 미로는 입구와 출구가 연결되어있지 않습니다.\n다시 미로를 만들어주세요.");
                            return;
                        }                        
                    }

            maze3 form3 = new maze3(form1);
            form3.Show();
            Close();
        }

        //다시만들기
        private void button2_Click(object sender, EventArgs e)   
        {
            for (int x = 1; x < 10; x++)                                                          //배열 초기화
                for (int y = 1; y < 10; y++)
                    form1.mazeA[x, y] = '0';

            for (int x = 2; x <= 8; x += 2)
                for (int y = 2; y <= 8; y += 2)
                    form1.mazeA[x, y] = '2';

            MessageBox.Show("초기화되었습니다.");
            Invalidate();                                                                        //이미지 초기화
            count = 0;
            wall = 0;
            label2.Text = (15 - wall).ToString();
        }

        //가로벽 만들기
        private void button4_Click(object sender, EventArgs e)      
        {
            direction = true;
            MessageBox.Show("가로벽을 만들어 주세요.");
        }

        //세로벽 만들기
        private void button3_Click(object sender, EventArgs e)     
        {
            direction = false;
            MessageBox.Show("세로벽을 만들어 주세요.");
        }

        //왼쪽클릭 이벤트
        private void Form2_MouseDown(object sender, MouseEventArgs e)       
        {
            Graphics g = this.CreateGraphics();          

            if (e.Button != MouseButtons.Left) return;

            ax = ((e.X - 50) / 100 + 1) * 100;                  //50~150 = 100
            ay = ((e.Y - 50) / 100 + 1) * 100;                  //150~250= 200
                                                                //250~350= 300          입구,출구 좌표

            a2x = 2 * (ax / 100) - 1;                         //입구,출구 배열 좌표 설정
            a2y = 2 * (ay / 100) - 1;

            bx = (e.X / 100) * 100;                             //100~200 = 100
            by = ((e.Y - 50) / 100 + 1) * 100;                  //200~300 = 200         세로벽 좌표


            cx = ((e.X - 50) / 100 + 1) * 100;                  //가로벽 좌표
            cy = (e.Y / 100) * 100;

            //입구 생성
            if (count == 0)                                                                
            {
                 form1.mazeA[a2y, a2x] = 's';
                 g.DrawImage(In, ax - 25, ay - 25);
                 count++;
            }

            //출구 생성
            else if (count == 1)                                                              
            {
                 if (form1.mazeA[a2y, a2x] == 's')
                 {
                      MessageBox.Show("입구와 같은 위치에는 출구를 생성할 수 없습니다. \n다른곳에 출구를 생성해주세요.");
                      return;
                 }

                 form1.mazeA[a2y, a2x] = 'e';
                 g.DrawImage(Out, ax - 25, ay - 25);
                 count++;
                 MessageBox.Show("가로벽을 만들어 주세요.");
            }

            //가로, 세로벽 생성
            else if (count > 1 && count < 17)
            {

                //가로벽일때
                if (direction == true)           
                 {
                      if (cx <= 0 || cx >= 600) return;
                      if (cy <= 0 || cy >= 500) return;                 

                    int c2x, c2y;
                      c2x = 2 * (cx / 100) - 1;
                      c2y = 2 * (cy / 100);

                      if (form1.mazeA[c2y, c2x] == '1')        //벽이 생성된 곳을 maze배열에 1을 대입
                      {
                           MessageBox.Show("이미 생성된 곳입니다!");
                           return;
                      }

                    wall++;                                                                                           /////////////////////////////////////////
                    label2.Text = (15 - wall).ToString();

                    form1.mazeA[c2y, c2x] = '1';
                      g.DrawImage(Hor, cx - 50, cy + 45);
                      count++;
                 }

                //세로벽일때
                else if (direction == false)                
                 {
                      if (bx <= 0 || bx >= 500) return;
                      if (by <= 0 || by >= 600) return;

                    int b2x, b2y;
                      b2x = 2 * (bx / 100);              //벽이 생성된 곳을 maze배열에 1을 대입
                      b2y = 2 * (by / 100) - 1;


                      if (form1.mazeA[b2y, b2x] == '1')
                      {
                           MessageBox.Show("이미 생성된 곳입니다!");
                           return;
                      }


                    wall++;
                    label2.Text = (15 - wall).ToString();

                    form1.mazeA[b2y, b2x] = '1';
                      g.DrawImage(Ver, bx + 45, by - 50);
                      count++;
                 }

                if (count == 17)
                {
                    MessageBox.Show("모든 벽을 만들었습니다.");
                }
            }

        }
    }
}

