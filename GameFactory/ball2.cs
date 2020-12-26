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
using System.IO;
using System.Media;


namespace GameFactory
{

    public partial class ball2 : Form
    {   
        ball1 ms;
     
        //Game Mouse Image       
        private Bitmap man;
        private Bitmap select_gem;
        private Bitmap parentImage;
        private Bitmap parentImage2;

        //Game gem`s Image
        private Bitmap level1_emerald;
        private Bitmap level2_ruby;
        private Bitmap level3_sapphire;
        private Bitmap level4_diamond;
            
        //level select
        public bool press = false;
        private bool level1_select = false;
        private bool level2_select = false;
        private bool level3_select = false;
        private bool level4_select = false;
      
        //Current Game State
        private int game_set = 0;

        //Mouse locate
        private int man_x = 0;
        private int man_y = 0;
        private int locate_man = 0;
        
        //level select 1~4( gem count 4 to 7 )
        private int level;      
        
  
        //initialization Mouse image properties
        private int parentImage_width = 0;                                
        private int parentImage_height = 0;

        //parentImage slope;
        private int[] j = new int[7];  //slope x
        private int[] k = new int[7];  //slope y

        //each gem`s coordinates
        private int[] parnetImage_x = new int[7]; //x-coordinate
        private int[] parentImage_y = new int[7]; //y-coordinate
      
        //field size
        private int field_size_x = 425, field_size_y = 425; 

        //score variable
        public int total_score;      //use for Score_table  declare public       
        private int score;                 
        private int bonus;               


        public int count = 0;   //use for Score_table  declare public
   
        public ball2(ball1 cc)  //보석이미지
        {
            InitializeComponent();
            ms = cc;
            this.StartButton.Visible = false;           
            this.Text = "gem`s Dodge Game ";
 
            //마우스 이미지

            man = new Bitmap(Properties.Resources.곰캐릭터);

 

            parentImage2 = ms.bmp_avata;
           level1_emerald = new Bitmap(Properties.Resources.레벨1에메랄드);      
           level2_ruby = new Bitmap(Properties.Resources.레벨2루비);       
           level3_sapphire = new Bitmap(Properties.Resources.레벨3사파이어);         
           level4_diamond = new Bitmap(Properties.Resources.레벨4다이아몬드);
           
       
            parentImage = new Bitmap(Properties.Resources.공);
            parentImage_width = parentImage.Width / 15;
            parentImage_height = parentImage.Height / 15;                         
        }
        private void init()  //변경된 값 초기화
        {
  
           //gems initial location
            {
                for (int gems_locate = 0; gems_locate < level; gems_locate++)
                {
                    parnetImage_x[gems_locate] = (gems_locate * 30 + 5);
                    parentImage_y[gems_locate] = 0;
                    k[gems_locate] = -1;
                    j[gems_locate] = -2;
                }
              
                // 점수초기화
                level = 0;
                score = 0;                  
                bonus = 0;
                game_set = 0;
                
                //레벨선택 초기화
                level1_select = false;
                level2_select = false;
                level3_select = false;
                level4_select = false;
            }
            menuStrip1.Visible = true;
        }
        private void Form1_Load(object sender, EventArgs e)   //폼이 로드되면 초기화
        {
            init();
        }

        private void level1ToolStripMenuItem_Click(object sender, EventArgs e)   //레벨1선택 
        {
            init();
            this.StartButton.Visible = true;
            level = 4;
            score = 1;
            bonus = 2;
            level1_select = true;
            level1_emerald.MakeTransparent(Color.White);
        }
        private void level2ToolStripMenuItem_Click(object sender, EventArgs e)   //레벨2선택
        {
            init();
            this.StartButton.Visible = true;
            level = 5;
            score = 2;
            bonus = 3;
            level2_select = true;
        level2_ruby.MakeTransparent(Color.White);
        }
        private void level3ToolStripMenuItem_Click(object sender, EventArgs e)   //레벨3선택
        {
            init();
            this.StartButton.Visible = true;
            level = 6;
            score = 3;
            bonus = 4;
            level3_select = true;
            level3_sapphire.MakeTransparent(Color.White); 
        }  
        private void level4ToolStripMenuItem_Click(object sender, EventArgs e)   //레벨4선택
        {
            init();
            this.StartButton.Visible = true;
            level = 7;
            score = 4;
            bonus = 5;
            level4_select = true;
            level4_diamond.MakeTransparent(Color.White);
        }

        private void StartButton_Click_1(object sender, EventArgs e) //게임시작 이벤트
        {
      
            this.StartButton.Visible = false;
            
          
            
            press = true;
            timer1_Tick(sender, e);
            timer1.Start();


            locate_man = 1;
            Score_label.Text = "0";
            total_score = 0;
            game_set = 1;

            //커서모양 변경완료
            Cursor.Hide();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) //마우스이동 이벤트
        {
            //마우스의 위치좌표를 캐릭터 좌표에 대입
            man_x = e.X;
            man_y = e.Y;
        }

        public void Form1_Paint(object sender, PaintEventArgs e) //인게임
        {
             this.SetStyle(ControlStyles.DoubleBuffer, true);
             this.SetStyle(ControlStyles.UserPaint, true);
             this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
     
             if (game_set == 1)
             {
                  //게임판 그리기
                Graphics p = e.Graphics;
                Pen pen = new Pen(Color.Red);
                p.DrawPolygon(pen, new Point[]{
                new Point(425,25), new Point(425,425) });

                Graphics p2 = e.Graphics;
                Pen pen2 = new Pen(Color.Red);
                p2.DrawPolygon(pen2, new Point[]{
                new Point(425,425), new Point(0,425) });

                Graphics p3 = e.Graphics;
                Pen pen3 = new Pen(Color.Red);
                p3.DrawPolygon(pen3, new Point[]{
                new Point(0,25), new Point(425,25) });

                Graphics p4 = e.Graphics;
                Pen pen4 = new Pen(Color.Red);
                p4.DrawPolygon(pen4, new Point[]{
                new Point(0,0), new Point(0,425) });

                Graphics g = e.Graphics;
                  g.DrawImage(parentImage2, man_x * locate_man, man_y * locate_man, 20, 20);

                  //랜덤배열생성
                  Random Rnd = new Random();
                  int[] rnd_x = new int[7];
                  int[] rnd_y = new int[7];

                  //gem의 속도 조절
                  for (int gem_speed = 0; gem_speed < level; gem_speed++)
                  {
                       rnd_x[gem_speed] = Rnd.Next(3, 8);
                       rnd_y[gem_speed] = Rnd.Next(3, 8);
                  }
                  //gem 그리기
                  for (int draw = 0; draw < level; draw++)
                  {

                      if (level1_select == true)  //레벨1선택
                      {
                          level1_emerald.MakeTransparent(Color.White);
                          select_gem = level1_emerald;
                          select_gem.MakeTransparent(Color.White);
                          g.DrawImage(select_gem, parnetImage_x[draw], parentImage_y[draw], 20, 20);
                      }


                      if (level2_select == true)  //레벨2선택
                      {
                          level2_ruby.MakeTransparent(Color.White);
                          select_gem = level2_ruby;
                          select_gem.MakeTransparent(Color.White);
                          g.DrawImage(select_gem, parnetImage_x[draw], parentImage_y[draw], 20, 20);
                      }


                      if (level3_select == true)  //레벨3선택
                      {
                          level3_sapphire.MakeTransparent(Color.White);
                          select_gem = level3_sapphire;
                          select_gem.MakeTransparent(Color.White);
                          g.DrawImage(select_gem, parnetImage_x[draw], parentImage_y[draw], 20, 20);
                      }


                      if (level4_select == true)  //레벨4선택
                      {
                          level4_diamond.MakeTransparent(Color.White);
                          select_gem = level4_diamond;
                          select_gem.MakeTransparent(Color.White);
                          g.DrawImage(select_gem, parnetImage_x[draw], parentImage_y[draw], 20, 20);
                      }
                  }

                  //마우스커서 펜라인 밖을 넘어가면 게임아웃
                  if (man_x == 0 || man_x > field_size_x - 20)
                  {
                       game_set = 2;
                  
                  }
                  if (man_y == 0 || man_y > field_size_y - 20)
                  {
                       game_set = 2;
                  
                }

                  //벽과공의 출동 처리
                  for (int crash_gem_wall = 0; crash_gem_wall < level; crash_gem_wall++)
                  {

                       if (parnetImage_x[crash_gem_wall] < 0) //왼쪽벽에 부딪혔을 때
                       {
                            k[crash_gem_wall] = rnd_x[crash_gem_wall]; //X축 방향 반대로

                       }
                       if (parnetImage_x[crash_gem_wall] > field_size_x - 22) //오른쪽벽에 부딪혔을 때
                       {
                            k[crash_gem_wall] = -rnd_x[crash_gem_wall]; //X축 방향 반대로

                       }
                       if (parentImage_y[crash_gem_wall] - 24 < 0) //위쪽벽에 부딪혔을 때
                       {
                            j[crash_gem_wall] = rnd_y[crash_gem_wall]; //Y축 방향 반대로

                       }
                       if (parentImage_y[crash_gem_wall] > field_size_y - 22) //아래쪽 벽에 부딪혔을 때
                       {
                            j[crash_gem_wall] = -rnd_y[crash_gem_wall]; //Y축 방향 반대로

                       }

                       //마우스와 보석의 충돌처리
                       if ((parnetImage_x[crash_gem_wall] <= man_x && parnetImage_x[crash_gem_wall] + 20 >= man_x) || (man_x <= parnetImage_x[crash_gem_wall] && man_x + 20 >= parnetImage_x[crash_gem_wall]))
                       {
                           if ((parentImage_y[crash_gem_wall] <= man_y && parentImage_y[crash_gem_wall] + 20 >= man_y) || (man_y <= parentImage_y[crash_gem_wall] &&
                                   man_y + 20 >= parentImage_y[crash_gem_wall]))
                            {
                                 game_set = 2;
                           
                        }
                       }

                       parnetImage_x[crash_gem_wall] = parnetImage_x[crash_gem_wall] + k[crash_gem_wall];
                       parentImage_y[crash_gem_wall] = parentImage_y[crash_gem_wall] + j[crash_gem_wall];
                  }

                  //보석과과 보석의 출동 처리
                  for (int crash_gem_gem = 0; crash_gem_gem < level; crash_gem_gem++) // 보석1
                  {
                      for (int crash_gem_gem2 = crash_gem_gem + 1; crash_gem_gem2 < level; crash_gem_gem2++) //보석2
                       {
                            // 보석2의 오른쪽면과 보석1의 왼쪽면이 충돌한다.

                           if (parnetImage_x[crash_gem_gem] <= parnetImage_x[crash_gem_gem2] && parnetImage_x[crash_gem_gem]+ 20 >= parnetImage_x[crash_gem_gem2])
                            {
                                 //y축좌표도 비교한다. 위쪽이나 아래쪽이다.
                                if (parentImage_y[crash_gem_gem] <= parentImage_y[crash_gem_gem2] && parentImage_y[crash_gem_gem] + 20 >= parentImage_y[crash_gem_gem2] || (parentImage_y[crash_gem_gem2] <= parentImage_y[crash_gem_gem] &&
                                     parentImage_y[crash_gem_gem2] + 20 >= parentImage_y[crash_gem_gem]))
                                 {
                                     k[crash_gem_gem] = -rnd_x[crash_gem_gem];
                                      k[crash_gem_gem2] = rnd_x[crash_gem_gem2];
                                 }
                            }

                            //보석1의 오른쪽과 보석2의 왼쪽면이 충돌한다
                           if (parnetImage_x[crash_gem_gem] >= parnetImage_x[crash_gem_gem2] && parnetImage_x[crash_gem_gem] <= parnetImage_x[crash_gem_gem2] + 20)
                            {
                                 //y축좌표도 비교한다. 위쪽이나 아래쪽이다.
                                if (parentImage_y[crash_gem_gem] <= parentImage_y[crash_gem_gem2] && parentImage_y[crash_gem_gem] + 20 >= parentImage_y[crash_gem_gem2] || parentImage_y[crash_gem_gem2] <= parentImage_y[crash_gem_gem] &&
                                     parentImage_y[crash_gem_gem2] + 20 >= parentImage_y[crash_gem_gem])
                                 {
                                     k[crash_gem_gem] = rnd_x[crash_gem_gem];
                                      k[crash_gem_gem2] = -rnd_x[crash_gem_gem2];
                                 }
                            }

                            //보석2의 아랫면과 보석1의 윗면이 충돌한다
                           if (parentImage_y[crash_gem_gem] <= parentImage_y[crash_gem_gem2] && parentImage_y[crash_gem_gem] + 20 >= parentImage_y[crash_gem_gem2])
                            {
                                 //x축 좌표도 비교한다. 즉 오른쪽 왼쪽
                                if ( parnetImage_x[crash_gem_gem] <= parnetImage_x[crash_gem_gem2] && parnetImage_x[crash_gem_gem] + 20 >= parnetImage_x[crash_gem_gem2] || parnetImage_x[crash_gem_gem2] <= parnetImage_x[crash_gem_gem] &&
                                     parnetImage_x[crash_gem_gem2] + 20 >= parnetImage_x[crash_gem_gem])
                                 {
                                     j[crash_gem_gem] = -rnd_y[crash_gem_gem];
                                      j[crash_gem_gem2] = rnd_y[crash_gem_gem2];
                                 }
                            }

                            //보석1의 아랫면과 보석2의 윗면이 충돌한다.
                           if (parentImage_y[crash_gem_gem] >= parentImage_y[crash_gem_gem2] && parentImage_y[crash_gem_gem] <= parentImage_y[crash_gem_gem2] + 20)
                            {
                                 //x축 좌표도 비교한다. 즉 오른쪽 왼쪽
                                if (parnetImage_x[crash_gem_gem] <= parnetImage_x[crash_gem_gem2] && parnetImage_x[crash_gem_gem] + parentImage_width >= parnetImage_x[crash_gem_gem2] || parnetImage_x[crash_gem_gem2] <= parnetImage_x[crash_gem_gem] &&
                                     parnetImage_x[crash_gem_gem2] + 20 >= parnetImage_x[crash_gem_gem])
                                 {
                                     j[crash_gem_gem] = rnd_y[crash_gem_gem];
                                      j[crash_gem_gem2] = -rnd_y[crash_gem_gem2];
                                 }
                            }

                       }
                  }
             }
             else if (game_set == 2) //게임이 끝나면 
             {

                  init();
                  Cursor.Show();
                  timer1.Stop();
               

                if (DialogResult.OK == MessageBox.Show("게임종료!!"))
                  {
                       ball3 ball3 = new ball3(this);
                       ball3.ShowDialog();
                  }
             }
        }

        public void timer1_Tick(object sender, EventArgs e)   //타이머이벤트
        {
           
            if (press == true)
            {
                Score_label.Text = Convert.ToString(total_score);

                score = score + bonus;

                total_score = score;
             
                Invalidate();
                
            }
            Invalidate();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)  //점수표보기
        {
            ball3  ball3= new ball3(this);
            ball3.ShowDialog();
        }     
    }
}
