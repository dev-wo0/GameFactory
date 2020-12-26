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
    public partial class mabel2 : Form
    {
        mabel1 dlg;
        public string lname;
        public int i;
        public mabel2(mabel1 a)
        {
            InitializeComponent();
            dlg = a;
            this.Text = "올림픽/세계여행";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "안동")
            {
                lname = "안동";
                i = 1;
            }
            else if (textBox1.Text == "경주")
            {
                lname = "경주";
                i = 2;
            }
            else if (textBox1.Text == "포항")
            {
                lname = "포항";
                i = 3;
            }
            else if (textBox1.Text == "독도")
            {
                lname = "독도";
                i = 4;
            }
            else if (textBox1.Text == "원주")
            {
                lname = "원주";
                i = 5;
            }
            else if (textBox1.Text == "강릉")
            {
                lname = "강릉";
                i = 6;
            }
            else if (textBox1.Text == "무인도")
            {
                lname = "무인도";
                i = 7;
            }
            else if (textBox1.Text == "목포")
            {
                lname = "목포";
                i = 8;
            }
            else if (textBox1.Text == "울릉도")
            {
                lname = "울릉도";
                i = 9;
            }
            else if (textBox1.Text == "여수")
            {
                lname = "여수";
                i = 10;
            }
            else if (textBox1.Text == "용인")
            {
                lname = "용인";
                i = 11;
            }
            else if (textBox1.Text == "성남")
            {
                lname = "성남";
                i = 12;
            }
            else if (textBox1.Text == "수원")
            {
                lname = "수원";
                i = 13;
            }
            else if (textBox1.Text == "올림픽")
            {
                lname = "올림픽";
                i = 14;
            }

            else if (textBox1.Text == "제주")
            {
                lname = "제주";
                i = 15;
            }
            else if (textBox1.Text == "세종")
            {
                lname = "세종";
                i = 16;
            }
            else if (textBox1.Text == "울산")
            {
                lname = "울산";
                i = 17;
            }
            else if (textBox1.Text == "광주")
            {
                lname = "광주";
                i = 18;
            }
            else if (textBox1.Text == "오이도")
            {
                lname = "오이도";
                i = 19;
            }
            else if (textBox1.Text == "대전")
            {
                lname = "대전";
                i = 20;
            }

            else if (textBox1.Text == "대구")
            {
                lname = "대구";
                i = 22;
            }
            else if (textBox1.Text == "우도")
            {
                lname = "우도";
                i = 23;
            }
            else if (textBox1.Text == "인천")
            {
                lname = "인천";
                i = 24;
            }
            else if (textBox1.Text == "부산")
            {
                lname = "부산";
                i = 25;
            }
            else if (textBox1.Text == "월미도")
            {
                lname = "월미도";
                i = 26;
            }
            else if (textBox1.Text == "서울")
            {
                lname = "서울";
                i = 27;
            }
            else if (textBox1.Text == "출발지")
            {
                lname = "출발지";
                i = 0;
            }
            else
            {
                MessageBox.Show("잘못된 땅입니다.");
                return;
            }
            
            if (dlg.select == true && dlg.turn == false) // 1p 올림픽 부분
            {
                Etc temp = new Etc();
                while (true)
                {
                    if (lname == "안동") temp = (Etc)dlg.al[1];
                    if (lname == "경주") temp = (Etc)dlg.al[2];
                    if (lname == "포항") temp = (Etc)dlg.al[3];
                    if (lname == "독도") temp = (Etc)dlg.al[4];
                    if (lname == "원주") temp = (Etc)dlg.al[5];
                    if (lname == "강릉") temp = (Etc)dlg.al[6];

                    if (lname == "목포") temp = (Etc)dlg.al[8];
                    if (lname == "울릉도") temp = (Etc)dlg.al[9];
                    if (lname == "여수") temp = (Etc)dlg.al[10];
                    if (lname == "용인") temp = (Etc)dlg.al[11];
                    if (lname == "성남") temp = (Etc)dlg.al[12];
                    if (lname == "수원") temp = (Etc)dlg.al[13];

                    if (lname == "제주") temp = (Etc)dlg.al[15];
                    if (lname == "세종") temp = (Etc)dlg.al[16];
                    if (lname == "울산") temp = (Etc)dlg.al[17];
                    if (lname == "광주") temp = (Etc)dlg.al[18];
                    if (lname == "오이도") temp = (Etc)dlg.al[19];
                    if (lname == "대전") temp = (Etc)dlg.al[20];

                    if (lname == "대구") temp = (Etc)dlg.al[22];
                    if (lname == "우도") temp = (Etc)dlg.al[23];
                    if (lname == "인천") temp = (Etc)dlg.al[24];
                    if (lname == "부산") temp = (Etc)dlg.al[25];
                    if (lname == "월미도") temp = (Etc)dlg.al[26];
                    if (lname == "서울") temp = (Etc)dlg.al[27];

                    if (temp.owner == 1 && temp.landmark == false)  // 소유주가 1p이고, 랜드마크가 없을때 -> 통행료 2배
                    {
                        temp.toll = temp.toll * 2;
                        dlg.al[i] = temp;
                        MessageBox.Show(lname + "의 통행료가 2배가 되었습니다.");
                        break;
                    }
                    else if (temp.owner == 1 && temp.landmark == true)   // 소유주가 1p이고, 랜드마크가 있을때 -> 랜드마크 통행료 2배
                    {
                        temp.landmark_toll = temp.landmark_toll * 2;
                        dlg.al[i] = temp;
                        MessageBox.Show(lname + "의 통행료가 2배가 되었습니다.");
                        break;
                    }
                    else if (temp.owner == 0 || temp.owner == 2)
                    {
                        MessageBox.Show("소유한 땅이 아닙니다. 다시 적어주세요.");
                        textBox1.Text = "";
                        return;
                    }
                }
                Close();
            }

            if (dlg.select == true && dlg.turn == true) // 2p 올림픽 부분
            {
                Etc temp = new Etc();
                while (true)
                {
                    if (lname == "안동") temp = (Etc)dlg.al[1];
                    if (lname == "경주") temp = (Etc)dlg.al[2];
                    if (lname == "포항") temp = (Etc)dlg.al[3];
                    if (lname == "독도") temp = (Etc)dlg.al[4];
                    if (lname == "원주") temp = (Etc)dlg.al[5];
                    if (lname == "강릉") temp = (Etc)dlg.al[6];

                    if (lname == "목포") temp = (Etc)dlg.al[8];
                    if (lname == "울릉도") temp = (Etc)dlg.al[9];
                    if (lname == "여수") temp = (Etc)dlg.al[10];
                    if (lname == "용인") temp = (Etc)dlg.al[11];
                    if (lname == "성남") temp = (Etc)dlg.al[12];
                    if (lname == "수원") temp = (Etc)dlg.al[13];

                    if (lname == "제주") temp = (Etc)dlg.al[15];
                    if (lname == "세종") temp = (Etc)dlg.al[16];
                    if (lname == "울산") temp = (Etc)dlg.al[17];
                    if (lname == "광주") temp = (Etc)dlg.al[18];
                    if (lname == "오이도") temp = (Etc)dlg.al[19];
                    if (lname == "대전") temp = (Etc)dlg.al[20];

                    if (lname == "대구") temp = (Etc)dlg.al[22];
                    if (lname == "우도") temp = (Etc)dlg.al[23];
                    if (lname == "인천") temp = (Etc)dlg.al[24];
                    if (lname == "부산") temp = (Etc)dlg.al[25];
                    if (lname == "월미도") temp = (Etc)dlg.al[26];
                    if (lname == "서울") temp = (Etc)dlg.al[27];

                    if (temp.owner == 2 && temp.landmark == false)  // 소유주가 2p이고, 랜드마크가 없을때 -> 통행료 2배
                    {
                        temp.toll = temp.toll * 2;
                        dlg.al[i] = temp;
                        MessageBox.Show(lname + "의 통행료가 2배가 되었습니다.");
                        break;
                    }
                    else if (temp.owner == 2 && temp.landmark == true)   // 소유주가 2p이고, 랜드마크가 있을때 -> 랜드마크 통행료 2배
                    {
                        temp.landmark_toll = temp.landmark_toll * 2;
                        dlg.al[i] = temp;
                        MessageBox.Show(lname + "의 통행료가 2배가 되었습니다.");
                        break;
                    }
                    else if (temp.owner == 0 || temp.owner == 1)
                    {
                        MessageBox.Show("소유한 땅이 아닙니다. 다시 적어주세요.");
                        textBox1.Text = "";
                        return;
                    }
                }
                Close();
            }

            if (dlg.select == false && dlg.turn == false) // 1p 세계여행 부분
            {
                Etc temp = new Etc();
                while (true)
                {
                    if (lname == "안동") dlg.user1.index = 1;
                    else if (lname == "경주") dlg.user1.index = 2;
                    else if (lname == "포항") dlg.user1.index = 3;
                    else if (lname == "독도") dlg.user1.index = 4;
                    else if (lname == "원주") dlg.user1.index = 5;
                    else if (lname == "강릉") dlg.user1.index = 6;
                    else if (lname == "무인도") dlg.user1.index = 7;

                    else if (lname == "목포") dlg.user1.index = 8;
                    else if (lname == "울릉도") dlg.user1.index = 9;
                    else if (lname == "여수") dlg.user1.index = 10;
                    else if (lname == "용인") dlg.user1.index = 11;
                    else if (lname == "성남") dlg.user1.index = 12;
                    else if (lname == "수원") dlg.user1.index = 13;
                    else if (lname == "올림픽") dlg.user1.index = 14;

                    else if (lname == "제주") dlg.user1.index = 15;
                    else if (lname == "세종") dlg.user1.index = 16;
                    else if (lname == "울산") dlg.user1.index = 17;
                    else if (lname == "광주") dlg.user1.index = 18;
                    else if (lname == "오이도") dlg.user1.index = 19;
                    else if (lname == "대전") dlg.user1.index = 20;

                    else if (lname == "대구") dlg.user1.index = 22;
                    else if (lname == "우도") dlg.user1.index = 23;
                    else if (lname == "인천") dlg.user1.index = 24;
                    else if (lname == "부산") dlg.user1.index = 25;
                    else if (lname == "월미도") dlg.user1.index = 26;
                    else if (lname == "서울") dlg.user1.index = 27;
                    else if (lname == "출발지") dlg.user1.index = 0;

                    else
                    {
                        MessageBox.Show("잘못된 땅입니다. 다시 입력해주세요.");
                        textBox1.Text = "";
                        return;
                    }
                    dlg.user1_change();
                    dlg.user1_building_system();
                    break;
                }
                Close();
            }

            if (dlg.select == false && dlg.turn == true) // 2p 세계여행 부분
            {
                Etc temp = new Etc();
                while (true)
                {
                    if (lname == "안동") dlg.user2.index = 1;
                    else if (lname == "경주") dlg.user2.index = 2;
                    else if (lname == "포항") dlg.user2.index = 3;
                    else if (lname == "독도") dlg.user2.index = 4;
                    else if (lname == "원주") dlg.user2.index = 5;
                    else if (lname == "강릉") dlg.user2.index = 6;
                    else if (lname == "무인도") dlg.user2.index = 7;

                    else if (lname == "목포") dlg.user2.index = 8;
                    else if (lname == "울릉도") dlg.user2.index = 9;
                    else if (lname == "여수") dlg.user2.index = 10;
                    else if (lname == "용인") dlg.user2.index = 11;
                    else if (lname == "성남") dlg.user2.index = 12;
                    else if (lname == "수원") dlg.user2.index = 13;
                    else if (lname == "올림픽") dlg.user2.index = 14;

                    else if (lname == "제주") dlg.user2.index = 15;
                    else if (lname == "세종") dlg.user2.index = 16;
                    else if (lname == "울산") dlg.user2.index = 17;
                    else if (lname == "광주") dlg.user2.index = 18;
                    else if (lname == "오이도") dlg.user2.index = 19;
                    else if (lname == "대전") dlg.user2.index = 20;

                    else if (lname == "대구") dlg.user2.index = 22;
                    else if (lname == "우도") dlg.user2.index = 23;
                    else if (lname == "인천") dlg.user2.index = 24;
                    else if (lname == "부산") dlg.user2.index = 25;
                    else if (lname == "월미도") dlg.user2.index = 26;
                    else if (lname == "서울") dlg.user2.index = 27;
                    else if (lname == "출발지") dlg.user2.index = 0;

                    else
                    {
                        MessageBox.Show("잘못된 땅입니다. 다시 입력해주세요.");
                        textBox1.Text = "";
                        return;
                    }

                    dlg.user2_change();
                    dlg.user2_building_system();
                    break;
                }
                Close();
            }
        }
    }
}
