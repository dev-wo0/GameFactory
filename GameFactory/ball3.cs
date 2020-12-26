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

namespace GameFactory
{

    public partial class ball3 : Form
    {

        ball2 frm;
        Array_list al;
        public Ad_Score ad_score;
        private int rank;
      
      
        public ball3(ball2 form1) //폼로드시 어레이리스트에 들어있는 값들을 출력한다.
        {
            InitializeComponent();
            al = new Array_list();

            this.Text = "ScoreTable";
            
            frm = form1;
            String[] arr = new String[3];
          
            for (int a = 0; a < frm.count; a++)
            {
                arr[0] = Convert.ToString(frm.count);
                arr[1] = al[a].name;
                arr[2] = al[a].playerscore;
                ListViewItem lvt = new ListViewItem(arr);
                listView1.Items.Add(lvt);
            }
        }

        private void button1_Click(object sender, EventArgs e) //확인버튼 클릭이벤트
        {
            if ((name.Text == "") || (frm.press == false))
            {
                MessageBox.Show("게임을하거나 이름일 입력해주세요.");
                name.Text = "";
                return;
            }
           
            // 게임도하고 이름도 입력했으면 점수표에 기입

            listView1.Items.Clear();  //리스트뷰 아이템들을 전부 삭제한후

            String sort_name;
            String sort_playerscore;
            String[] arr = new String[3];
            ad_score = new Ad_Score();

            ad_score.name = name.Text;
            ad_score.playerscore = frm.Score_label.Text;
            al[frm.count] = ad_score;

            for (int a = 0; a < frm.count; a++)  //버블정렬한후
                for (int b = 0; b < frm.count -a ; b++)
                {
                    Ad_Score tmp = (Ad_Score)al[b];
                    Ad_Score tmp2 = (Ad_Score)al[b + 1];
                    if ((Int32.Parse(al[b].playerscore)) < (Int32.Parse(al[b+1].playerscore)))
                    {
                        sort_name = al[b].name;
                        al[b].name = al[b + 1].name;
                        al[b + 1].name = sort_name;

                        sort_playerscore = al[b].playerscore;
                        al[b].playerscore = al[b + 1].playerscore;
                        al[b + 1].playerscore = sort_playerscore;
                    }
                }

            //버튼클릭시 정렬된 값들을 리스트뷰에 띄움
            for (int c = 0; c < frm.count + 1; c++)
            {
                rank = c + 1;
                arr[0] = Convert.ToString(rank);
                arr[1] = al[c].name;
                arr[2] = al[c].playerscore;
                ListViewItem lvt = new ListViewItem(arr);
                listView1.Items.Add(lvt);
            }
            

         
            frm.count++;
            name.Text = "";
            frm.Score_label.Text = " ";
            frm.press = false;

        }
    }

}

