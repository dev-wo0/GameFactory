using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameFactory
{
    public partial class mabel1 : Form
    {
        public mabel2 l_sel;
        public ArrayList al = new ArrayList();
        public Pen pen;
        public Brush whitebrush, blackbrush, redbrush, bluebrush;
        public int dice1 = 0, dice2 = 0;
        public User user1 = new User();
        public User user2 = new User();
        public bool turn;  // true이면 1p, false이면 2p
        public bool select; // true이면 올림픽, false이면 세계여행
        public bool flag = false;   // 처음 시작 bool변수
        public int user1_x = 510;
        public int user1_y = 540;
        public int user2_x = 530;
        public int user2_y = 540;
        public int island1 = 0;
        public int island2 = 0;
        public string landname1;
        public string landname2;

        public string owner_string;
        public string landmark_string;
        public string toll_string;

        Etc t0; Etc t1; Etc t2; Etc t3; Etc t4; Etc t5; Etc t6; Etc t7;
        Etc t8; Etc t9; Etc t10; Etc t11; Etc t12; Etc t13; Etc t14;
        Etc t15; Etc t16; Etc t17; Etc t18; Etc t19; Etc t20; Etc t21;
        Etc t22; Etc t23; Etc t24; Etc t25; Etc t26; Etc t27;

        public void user1_building_system()  //user1 도시 도착 함수
        {
            ///////////////////////////////////////
            if (user1.index == 1) landname1 = "안동";
            if (user1.index == 2) landname1 = "경주";
            if (user1.index == 3) landname1 = "포항";
            if (user1.index == 4) landname1 = "독도";
            if (user1.index == 5) landname1 = "원주";
            if (user1.index == 6) landname1 = "강릉";

            if (user1.index == 8) landname1 = "목포";
            if (user1.index == 9) landname1 = "울릉도";
            if (user1.index == 10) landname1 = "여수";
            if (user1.index == 11) landname1 = "용인";
            if (user1.index == 12) landname1 = "성남";
            if (user1.index == 13) landname1 = "수원";

            if (user1.index == 15) landname1 = "제주";
            if (user1.index == 16) landname1 = "세종";
            if (user1.index == 17) landname1 = "울산";
            if (user1.index == 18) landname1 = "광주";
            if (user1.index == 19) landname1 = "오이도";
            if (user1.index == 20) landname1 = "대전";

            if (user1.index == 22) landname1 = "대구";
            if (user1.index == 23) landname1 = "우도";
            if (user1.index == 24) landname1 = "인천";
            if (user1.index == 25) landname1 = "부산";
            if (user1.index == 26) landname1 = "월미도";
            if (user1.index == 27) landname1 = "서울";
            ///////////////////////////////////////
            Etc build = new Etc();
            build = (Etc)al[user1.index];
            if ((user1.index > 0 && user1.index < 7) || (user1.index > 7 && user1.index < 14) || (user1.index > 14 && user1.index < 21) || (user1.index > 21 && user1.index < 28))
            {
                if (build.owner == 0)   //주인없는 땅
                {
                    if (MessageBox.Show(landname1 + "에 도착했습니다. 건물을 짓겠습니까? (건설비용 = " + build.buy_charge + "원)", "도시 도착", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (user1.MONEY >= build.buy_charge)
                        {
                            if (user1.index == 4 || user1.index == 9 || user1.index == 19 || user1.index == 23 || user1.index == 26) //관광지
                            {
                                user1.get_tour = user1.get_tour + 1;
                                tour_victory();
                            }
                            user1.MONEY = user1.MONEY - build.buy_charge;
                            user1.get_land++;
                            user_stat();
                            build.owner = 1;
                            al[user1.index] = build;
                        }
                        else
                        {
                            MessageBox.Show("건물을 지을 돈이 부족합니다.", "건설비용 부족");
                            return;
                        }
                    }
                    else return;
                }

                else if (build.owner == 1 && build.landmark == false)   //자기의 땅, 건물있음
                {
                    if (MessageBox.Show(landname1 + "은(는) 본인이 소유한 도시입니다. 랜드마크를 건설하시겠습니까? (랜드마크 건설 비용 = " + build.buy_lnadmark + "원)", "랜드마크 건설", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (user1.MONEY >= build.buy_lnadmark)
                        {
                            user1.MONEY = user1.MONEY - build.buy_lnadmark;
                            user_stat();
                            build.landmark = true;
                            al[user1.index] = build;
                        }
                        else
                        {
                            MessageBox.Show("랜드마크 건설 비용이 부족합니다.", "랜드마크 건설 비용 부족");
                            return;
                        }

                    }
                    else
                        return;
                }

                else if (build.owner == 2 && build.landmark == false)   //상대편 땅 도착, 랜마 없음
                {
                    if (MessageBox.Show(landname1 + "은(는) 상대방의 도시이므로 통행료를 지불합니다. (통행료 = " + build.toll + "원)", "상대 도시 도착", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //통행료 지불
                        if (user1.MONEY >= build.toll)
                        {
                            user1.MONEY = user1.MONEY - build.toll;
                            user2.MONEY = user2.MONEY + build.toll;
                            user_stat();
                            if (MessageBox.Show(landname1 + "을(를) 인수하시겠습니까? (인수 비용 = " + build.accep_charge + "원)", "상대 도시 인수", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (user1.MONEY >= build.accep_charge)
                                {
                                    if (user1.index == 4 || user1.index == 9 || user1.index == 19 || user1.index == 23 || user1.index == 26)   //관광지
                                    {
                                        user2.get_tour = user2.get_tour - 1;
                                        user1.get_tour = user1.get_tour + 1;
                                        tour_victory();
                                    }
                                    //인수료 지불
                                    user1.MONEY = user1.MONEY - build.accep_charge;
                                    user2.MONEY = user2.MONEY + build.accep_charge;
                                    user1.get_land++;
                                    user2.get_land--;
                                    user_stat();
                                    build.owner = 1;
                                    al[user1.index] = build;
                                }
                                else
                                {
                                    MessageBox.Show("인수비용이 부족합니다.", "인수비용 부족");
                                    return;
                                }

                            }
                            else return;
                        }
                        //통행료 낼 돈이 없음.
                        else
                        {
                            MessageBox.Show("통행료를 낼 돈이 없습니다. 게임에서 졌습니다, 2P 승리", "2P 승리");
                            Close();
                        }
                    }
                }

                else if (build.owner == 2 && build.landmark == true)    //상대편 땅 도착, 랜마 있음
                {
                    if (MessageBox.Show(landname1 + "은(는) 상대방의 랜드마크이므로 통행료를 지불합니다.(인수 불가능)\n(랜드마크 통행료 = " + build.landmark_toll + "원)", "상대 도시 도착", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //랜마통행료 지불
                        if (user1.MONEY > build.landmark_toll)
                        {
                            user1.MONEY = user1.MONEY - build.landmark_toll;
                            user2.MONEY = user2.MONEY + build.landmark_toll;
                            user_stat();
                        }
                        else
                        {
                            MessageBox.Show("통행료를 낼 돈이 없습니다. 게임에서 졌습니다, 2P 승리", "2P 승리");
                            Close();
                        }
                    }
                }
                else    //자신의 랜드마크에 도착했을때
                    user_stat();
            }

            if (user1.index == 0)
            {
                MessageBox.Show("출발지에 도착했습니다.");
            }

            if (user1.index == 7)
            {
                MessageBox.Show("무인도에 도착했습니다.");
            }

            if (user1.index == 14)
            {
                if (user1.get_land == 0)
                {
                    MessageBox.Show("소유한 땅이 없습니다. 다음을 이용하세요.");
                    return;
                }
                else
                {
                    l_sel = new mabel2(this);
                    MessageBox.Show("올림픽에 도착했습니다. 땅을 적어주세요\n(통행료가 2배가 됩니다.)");
                    select = true;
                    l_sel.Show();
                }
            }

            if (user1.index == 21)
            {
                l_sel = new mabel2(this);
                MessageBox.Show("세계여행에 도착했습니다. 이동하실 땅을 적어주세요\n(순간이동 합니다.)");
                select = false;
                l_sel.Show();
            }
        }

        public void user2_building_system()  //user2 도시 도착 함수
        {

            ///////////////////////////////////////
            if (user2.index == 1) landname2 = "안동";
            if (user2.index == 2) landname2 = "경주";
            if (user2.index == 3) landname2 = "포항";
            if (user2.index == 4) landname2 = "독도";
            if (user2.index == 5) landname2 = "원주";
            if (user2.index == 6) landname2 = "강릉";

            if (user2.index == 8) landname2 = "목포";
            if (user2.index == 9) landname2 = "울릉도";
            if (user2.index == 10) landname2 = "여수";
            if (user2.index == 11) landname2 = "용인";
            if (user2.index == 12) landname2 = "성남";
            if (user2.index == 13) landname2 = "수원";

            if (user2.index == 15) landname2 = "제주";
            if (user2.index == 16) landname2 = "세종";
            if (user2.index == 17) landname2 = "울산";
            if (user2.index == 18) landname2 = "광주";
            if (user2.index == 19) landname2 = "오이도";
            if (user2.index == 20) landname2 = "대전";

            if (user2.index == 22) landname2 = "대구";
            if (user2.index == 23) landname2 = "우도";
            if (user2.index == 24) landname2 = "인천";
            if (user2.index == 25) landname2 = "부산";
            if (user2.index == 26) landname2 = "월미도";
            if (user2.index == 27) landname2 = "서울";
            ///////////////////////////////////////
            Etc build = new Etc();
            build = (Etc)al[user2.index];
            if ((user2.index > 0 && user2.index < 7) || (user2.index > 7 && user2.index < 14) || (user2.index > 14 && user2.index < 21) || (user2.index > 21 && user2.index < 28))
            {
                if (build.owner == 0)   //주인없는 땅
                {
                    if (MessageBox.Show(landname2 + "에 도착했습니다. 건물을 짓겠습니까? (건설비용 = " + build.buy_charge + "원)", "도시 도착", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (user2.MONEY >= build.buy_charge)
                        {
                            if (user2.index == 4 || user2.index == 9 || user2.index == 19 || user2.index == 23 || user2.index == 26) //관광지
                            {
                                user2.get_tour = user2.get_tour + 1;
                                tour_victory();
                            }
                            user2.MONEY = user2.MONEY - build.buy_charge;
                            user2.get_land++;
                            user_stat();
                            build.owner = 2;
                            al[user2.index] = build;
                        }
                        else
                        {
                            MessageBox.Show("건물을 지을 돈이 부족합니다.", "건설비용 부족");
                            return;
                        }
                    }
                    else return;
                }

                else if (build.owner == 2 && build.landmark == false)   //자기의 땅, 건물있음
                {
                    if (MessageBox.Show(landname2 + "은(는) 본인이 소유한 도시입니다. 랜드마크를 건설하시겠습니까? (랜드마크 건설 비용 = " + build.buy_lnadmark + "원)", "랜드마크 건설", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (user2.MONEY >= build.buy_lnadmark)
                        {
                            user2.MONEY = user2.MONEY - build.buy_lnadmark;
                            user_stat();
                            build.landmark = true;
                            al[user2.index] = build;
                        }
                        else
                        {
                            MessageBox.Show("랜드마크 건설 비용이 부족합니다.", "랜드마크 건설 비용 부족");
                            return;
                        }

                    }
                    else
                        return;
                }

                else if (build.owner == 1 && build.landmark == false)   //상대편 땅 도착, 랜마 없음
                {
                    if (MessageBox.Show(landname2 + "은(는) 상대방의 도시이므로 통행료를 지불합니다. (통행료 = " + build.toll + "원)", "상대 도시 도착", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //통행료 지불
                        if (user2.MONEY >= build.toll)
                        {
                            user2.MONEY = user2.MONEY - build.toll;
                            user1.MONEY = user1.MONEY + build.toll;
                            user_stat();
                            if (MessageBox.Show(landname2 + "을(를) 인수하시겠습니까? (인수 비용 = " + build.accep_charge + "원)", "상대 도시 인수", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (user2.MONEY >= build.accep_charge)
                                {
                                    if (user2.index == 4 || user2.index == 9 || user2.index == 19 || user2.index == 23 || user2.index == 26)   //관광지
                                    {
                                        user1.get_tour = user1.get_tour - 1;
                                        user2.get_tour = user2.get_tour + 1;
                                        tour_victory();
                                    }
                                    //인수료 지불
                                    user2.MONEY = user2.MONEY - build.accep_charge;
                                    user1.MONEY = user1.MONEY + build.accep_charge;
                                    user2.get_land++;
                                    user1.get_land--;
                                    user_stat();
                                    build.owner = 2;
                                    al[user2.index] = build;
                                }
                                else
                                {
                                    MessageBox.Show("인수비용이 부족합니다.", "인수비용 부족");
                                    return;
                                }

                            }
                            else return;
                        }
                        //통행료 낼 돈이 없음.
                        else
                        {
                            MessageBox.Show("통행료를 낼 돈이 없습니다. 게임에서 졌습니다, 1P 승리", "1P 승리");
                            Close();
                        }
                    }
                }

                else if (build.owner == 1 && build.landmark == true)    //상대편 땅 도착, 랜마 있음
                {
                    if (MessageBox.Show(landname2 + "은(는) 상대방의 랜드마크이므로 통행료를 지불합니다.(인수 불가능)\n(랜드마크 통행료 = " + build.landmark_toll + "원)", "상대 도시 도착", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //랜마통행료 지불
                        if (user2.MONEY >= build.landmark_toll)
                        {
                            user2.MONEY = user2.MONEY - build.landmark_toll;
                            user1.MONEY = user1.MONEY + build.landmark_toll;
                            user_stat();
                        }
                        else
                        {
                            MessageBox.Show("통행료를 낼 돈이 없습니다. 게임에서 졌습니다, 1P 승리", "1P 승리");
                            Close();
                        }
                    }
                }

                else    //자신의 랜드마크에 도착했을때
                    user_stat();
            }

            if (user2.index == 0)
            {
                MessageBox.Show("출발지에 도착했습니다.");
            }

            if (user2.index == 7)
            {
                MessageBox.Show("무인도에 도착했습니다. 2턴간 못움직입니다.");
            }
            if (user2.index == 14)
            {
                if (user2.get_land == 0)
                {
                    MessageBox.Show("소유한 땅이 없습니다. 다음을 이용하세요.");
                    return;
                }
                else
                {
                    l_sel = new mabel2(this);
                    MessageBox.Show("올림픽에 도착했습니다. 땅을 적어주세요\n(통행료가 2배가 됩니다.)");
                    select = true;
                    l_sel.Show();
                }
            }
            if (user2.index == 21)
            {
                l_sel = new mabel2(this);
                MessageBox.Show("세계여행에 도착했습니다.");
                select = false;
                l_sel.Show();
            }
        }

        public void user1_change()    //user1 좌표변경 함수
        {
            Graphics G = this.CreateGraphics();

            if (user1.index == 0)
            {
                user1_x = 510;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 1)   //1레인
            {
                user1_x = 440;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 2)
            {
                user1_x = 370;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 3)
            {
                user1_x = 300;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 4)
            {
                user1_x = 230;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 5)
            {
                user1_x = 160;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 6)
            {
                user1_x = 90;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 7)
            {
                user1_x = 20;
                user1_y = 540;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 8)   //2레인
            {
                user1_x = 20;
                user1_y = 470;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 9)
            {
                user1_x = 20;
                user1_y = 400;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 10)
            {
                user1_x = 20;
                user1_y = 330;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 11)
            {
                user1_x = 20;
                user1_y = 260;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 12)
            {
                user1_x = 20;
                user1_y = 190;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 13)
            {
                user1_x = 20;
                user1_y = 120;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 14)
            {
                user1_x = 20;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 15)  //3레인
            {
                user1_x = 90;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 16)
            {
                user1_x = 160;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 17)
            {
                user1_x = 230;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 18)
            {
                user1_x = 300;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 19)
            {
                user1_x = 370;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 20)
            {
                user1_x = 440;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 21)
            {
                user1_x = 510;
                user1_y = 50;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 22)  //4레인
            {
                user1_x = 510;
                user1_y = 120;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 23)
            {
                user1_x = 510;
                user1_y = 190;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 24)
            {
                user1_x = 510;
                user1_y = 260;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 25)
            {
                user1_x = 510;
                user1_y = 330;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 26)
            {
                user1_x = 510;
                user1_y = 400;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            if (user1.index == 27)
            {
                user1_x = 510;
                user1_y = 470;
                Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
                G.FillRectangle(redbrush, pone1);
                Refresh();
            }
            Invalidate();
        }

        public void user2_change()    //user2 좌표변경 함수
        {
            Graphics G = this.CreateGraphics();
            if (user2.index == 0)
            {
                user2_x = 530;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 1)   //1레인
            {
                user2_x = 460;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 2)
            {
                user2_x = 390;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 3)
            {
                user2_x = 320;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 4)
            {
                user2_x = 250;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 5)
            {
                user2_x = 180;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 6)
            {
                user2_x = 110;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 7)
            {
                user2_x = 40;
                user2_y = 540;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 8)   //2레인
            {
                user2_x = 40;
                user2_y = 470;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 9)
            {
                user2_x = 40;
                user2_y = 400;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 10)
            {
                user2_x = 40;
                user2_y = 330;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 11)
            {
                user2_x = 40;
                user2_y = 260;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 12)
            {
                user2_x = 40;
                user2_y = 190;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 13)
            {
                user2_x = 40;
                user2_y = 120;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 14)
            {
                user2_x = 40;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 15)  //3레인
            {
                user2_x = 110;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 16)
            {
                user2_x = 180;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 17)
            {
                user2_x = 250;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 18)
            {
                user2_x = 320;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 19)
            {
                user2_x = 390;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 20)
            {
                user2_x = 460;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 21)
            {
                user2_x = 530;
                user2_y = 50;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 22)  //4레인
            {
                user2_x = 530;
                user2_y = 120;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 23)
            {
                user2_x = 530;
                user2_y = 190;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 24)
            {
                user2_x = 530;
                user2_y = 260;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 25)
            {
                user2_x = 530;
                user2_y = 330;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 26)
            {
                user2_x = 530;
                user2_y = 400;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            if (user2.index == 27)
            {
                user2_x = 530;
                user2_y = 470;
                Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
                G.FillRectangle(redbrush, pone2);
                Refresh();
            }
            Invalidate();
        }

        public void user_stat()       //user 현재 상태
        {
            user1_money_lab.Text = user1.MONEY.ToString();
            user1_build_lab.Text = user1.get_land.ToString();
            user2_money_lab.Text = user2.MONEY.ToString();
            user2_build_lab.Text = user2.get_land.ToString();
        }

        public void tour_victory()  //관광지 독점 승리
        {
            if (user1.get_tour == 4)
            {
                MessageBox.Show("1P의 관광지 독점 찬스입니다.", "1P 관광지 독점 찬스");
            }
            if (user2.get_tour == 4)
            {
                MessageBox.Show("2P의 관광지 독점 찬스입니다.", "2P 관광지 독점 찬스");
            }
            if (user1.get_tour == 5)
            {
                MessageBox.Show("1P 가 관광지 독점으로 승리하였습니다.", "1P 관광지 독점 승리");
                Close();
            }

            if (user2.get_tour == 5)
            {
                MessageBox.Show("2P 가 관광지 독점으로 승리하였습니다.", "2P 관광지 독점 승리");
                Close();
            }
        }

        private void 게임시작ToolStripMenuItem_Click(object sender, EventArgs e)  //게임시작 turn생성
        {
            flag = true;
            turn_lab.Visible = true;
            Random rnd = new Random();
            MessageBox.Show("선 정하기", "선 고르기", MessageBoxButtons.OK);
            user1.order = rnd.Next(1, 3);
            if (user1.order == 1)
            {
                MessageBox.Show("user1이 선입니다.", "user1 선!!");
                turn = true;
            }
            else
            {
                MessageBox.Show("user2이 선입니다.", "user2 선!!");
                turn = false;
            }
        }

        private void JWForm_Load(object sender, EventArgs e)    //깜박임 최소화
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            ResizeRedraw = true;
        }

        private void JWForm_MouseDown(object sender, MouseEventArgs e)
        {
        }   //사용X(마우스 클릭)
        
        private void JWForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }   //사용X(마우스 더블클릭)

        ////////////ㅡㅡㅡㅡㅡㅡㅡㅡ땅 정보ㅡㅡㅡㅡㅡㅡㅡㅡㅡ////////////

        private void label6_MouseHover(object sender, EventArgs e)  //안동
        {
            Etc temp = new Etc();
            temp = (Etc)al[1];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("안동 소유주 : " + owner_string + "\n안동 통행료 : " + toll_string + "\n안동 랜드마크 : " + landmark_string, label6);
        }

        private void label7_MouseHover(object sender, EventArgs e)  //경주
        {
            Etc temp = new Etc();
            temp = (Etc)al[2];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("경주 소유주 : " + owner_string + "\n경주 통행료 : " + toll_string + "\n경주 랜드마크 : " + landmark_string, label7);
        }

        private void label8_MouseHover(object sender, EventArgs e)  //포항
        {
            Etc temp = new Etc();
            temp = (Etc)al[3];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("포항 소유주 : " + owner_string + "\n포항 통행료 : " + toll_string + "\n포항 랜드마크 : " + landmark_string, label8);
        }

        private void label13_MouseHover(object sender, EventArgs e) //독도
        {
            Etc temp = new Etc();
            temp = (Etc)al[4];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("독도 소유주 : " + owner_string + "\n독도 통행료 : " + toll_string + "\n독도 랜드마크 : " + landmark_string, label13);
        }

        private void label14_MouseHover(object sender, EventArgs e) //원주
        {
            Etc temp = new Etc();
            temp = (Etc)al[5];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("원주 소유주 : " + owner_string + "\n원주 통행료 : " + toll_string + "\n원주 랜드마크 : " + landmark_string, label14);
        }

        private void label15_MouseHover(object sender, EventArgs e) //강릉
        {
            Etc temp = new Etc();
            temp = (Etc)al[6];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("강릉 소유주 : " + owner_string + "\n강릉 통행료 : " + toll_string + "\n강릉 랜드마크 : " + landmark_string, label15);
        }

        private void label17_MouseHover(object sender, EventArgs e) //목포
        {
            Etc temp = new Etc();
            temp = (Etc)al[8];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("목포 소유주 : " + owner_string + "\n목포 통행료 : " + toll_string + "\n목포 랜드마크 : " + landmark_string, label17);
        }

        private void label18_MouseHover(object sender, EventArgs e) //울릉도
        {
            Etc temp = new Etc();
            temp = (Etc)al[9];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("울릉도 소유주 : " + owner_string + "\n울릉도 통행료 : " + toll_string + "\n울릉도 랜드마크 : " + landmark_string, label18);
        }

        private void label19_MouseHover(object sender, EventArgs e) //여수
        {
            Etc temp = new Etc();
            temp = (Etc)al[10];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("여수 소유주 : " + owner_string + "\n여수 통행료 : " + toll_string + "\n여수 랜드마크 : " + landmark_string, label19);
        }

        private void label20_MouseHover(object sender, EventArgs e) //용인
        {
            Etc temp = new Etc();
            temp = (Etc)al[11];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("용인 소유주 : " + owner_string + "\n용인 통행료 : " + toll_string + "\n용인 랜드마크 : " + landmark_string, label20);
        }

        private void label21_MouseHover(object sender, EventArgs e) //성남
        {
            Etc temp = new Etc();
            temp = (Etc)al[12];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("성남 소유주 : " + owner_string + "\n성남 통행료 : " + toll_string + "\n성남 랜드마크 : " + landmark_string, label21);
        }

        private void label22_MouseHover(object sender, EventArgs e) //수원
        {
            Etc temp = new Etc();
            temp = (Etc)al[13];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("수원 소유주 : " + owner_string + "\n수원 통행료 : " + toll_string + "\n수원 랜드마크 : " + landmark_string, label22);
        }

        private void label24_MouseHover(object sender, EventArgs e) //제주
        {
            Etc temp = new Etc();
            temp = (Etc)al[15];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("제주 소유주 : " + owner_string + "\n제주 통행료 : " + toll_string + "\n제주 랜드마크 : " + landmark_string, label24);
        }

        private void label25_MouseHover(object sender, EventArgs e) //세종
        {
            Etc temp = new Etc();
            temp = (Etc)al[16];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("세종 소유주 : " + owner_string + "\n세종 통행료 : " + toll_string + "\n세종 랜드마크 : " + landmark_string, label25);
        }

        private void label26_MouseHover(object sender, EventArgs e) //울산
        {
            Etc temp = new Etc();
            temp = (Etc)al[17];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("울산 소유주 : " + owner_string + "\n울산 통행료 : " + toll_string + "\n울산 랜드마크 : " + landmark_string, label26);
        }

        private void label27_MouseHover(object sender, EventArgs e) //광주
        {
            Etc temp = new Etc();
            temp = (Etc)al[18];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("광주 소유주 : " + owner_string + "\n광주 통행료 : " + toll_string + "\n광주 랜드마크 : " + landmark_string, label27);
        }

        private void label28_MouseHover(object sender, EventArgs e) //오이도
        {
            Etc temp = new Etc();
            temp = (Etc)al[19];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("오이도 소유주 : " + owner_string + "\n오이도 통행료 : " + toll_string + "\n오이도 랜드마크 : " + landmark_string, label28);
        }

        private void label29_MouseHover(object sender, EventArgs e) //대전
        {
            Etc temp = new Etc();
            temp = (Etc)al[20];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("대전 소유주 : " + owner_string + "\n대전 통행료 : " + toll_string + "\n대전 랜드마크 : " + landmark_string, label29);
        }

        private void label31_MouseHover(object sender, EventArgs e) //대구
        {
            Etc temp = new Etc();
            temp = (Etc)al[22];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("대구 소유주 : " + owner_string + "\n대구 통행료 : " + toll_string + "\n대구 랜드마크 : " + landmark_string, label31);
        }

        private void label32_MouseHover(object sender, EventArgs e) //우도
        {
            Etc temp = new Etc();
            temp = (Etc)al[23];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("우도 소유주 : " + owner_string + "\n우도 통행료 : " + toll_string + "\n우도 랜드마크 : " + landmark_string, label32);
        }

        private void label33_MouseHover(object sender, EventArgs e) //인천
        {
            Etc temp = new Etc();
            temp = (Etc)al[24];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("인천 소유주 : " + owner_string + "\n인천 통행료 : " + toll_string + "\n인천 랜드마크 : " + landmark_string, label33);
        }

        private void label34_MouseHover(object sender, EventArgs e) //부산
        {
            Etc temp = new Etc();
            temp = (Etc)al[25];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("부산 소유주 : " + owner_string + "\n부산 통행료 : " + toll_string + "\n부산 랜드마크 : " + landmark_string, label34);
        }

        private void label35_MouseHover(object sender, EventArgs e) //월미도
        {
            Etc temp = new Etc();
            temp = (Etc)al[26];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("월미도 소유주 : " + owner_string + "\n월미도 통행료 : " + toll_string + "\n월미도 랜드마크 : " + landmark_string, label35);
        }

        private void label36_MouseHover(object sender, EventArgs e) //서울
        {
            Etc temp = new Etc();
            temp = (Etc)al[27];
            if (temp.owner == 0) owner_string = "없음";
            if (temp.owner == 1) owner_string = "1P";
            if (temp.owner == 2) owner_string = "2P";
            if (temp.landmark == true)
            {
                landmark_string = "있음";
                toll_string = temp.landmark_toll.ToString();
            }
            if (temp.landmark == false)
            {
                landmark_string = "없음";
                toll_string = temp.toll.ToString();
            }
            toolTip1.Show("서울 소유주 : " + owner_string + "\n서울 통행료 : " + toll_string + "\n서울 랜드마크 : " + landmark_string, label36);
        }

        ////////////ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ////////////

        public void dice_btn_Click(object sender, EventArgs e)  //1p 주사위
        {
            if(flag == false)
            {
                MessageBox.Show("게임시작 버튼을 눌러주세요.");
                return;
            }
            if (turn == true)
            {
                if (user1.index == 7)
                {
                    if (island1 == 0)
                    {
                        MessageBox.Show("무인도에 도착하여 2턴간 움직이실 수 없습니다", "user1의 무인도");
                        turn = false;
                        island1++;
                        return;
                    }

                    if (island1 == 1)
                    {
                        MessageBox.Show("무인도에 도착하여 1턴간 움직이실 수 없습니다", "user1의 무인도");
                        turn = false;
                        island1++;
                        return;
                    }

                    MessageBox.Show("무인도에서 벗어나셨습니다.", "user1의 무인도 탈출");
                    island1 = 0;
                }
                Random rnd = new Random();
                dice1 = rnd.Next(1, 7);
                dice1_lab.BackColor = Color.White;
                dice1_lab.Text = dice1.ToString();
                for (int i = 0; i < dice1; i++)
                {
                    user1.index++;
                    user1_change();
                    Invalidate();
                    if (user1.index >= 28)
                    {
                        MessageBox.Show("출발지를 지났기때문에 용돈 1500원 넣어드려요.");
                        user1.MONEY = user1.MONEY + 1500;
                        user1.index = user1.index - 28;
                        user1_change();
                        Invalidate();
                    }
                    System.Threading.Thread.Sleep(500); //이동하는부분 0.5초 간격
                }
                user1_building_system();
                turn = false;
            }
            else
            {
                MessageBox.Show("이번 차례는 2p 차례입니다. 다음 턴을 기대하세요~~");
            }
        }

        private void button1_Click(object sender, EventArgs e)  //2p 주사위
        {
            if (flag == false)
            {
                MessageBox.Show("게임시작 버튼을 눌러주세요.");
                return;
            }
            if (turn == false)
            {
                if (user2.index == 7)
                {
                    if (island2 == 0)
                    {
                        MessageBox.Show("무인도에 도착하여 2턴간 움직이실 수 없습니다", "user2의 무인도");
                        turn = true;
                        island2++;
                        return;
                    }

                    if (island2 == 1)
                    {
                        MessageBox.Show("무인도에 도착하여 1턴간 움직이실 수 없습니다", "user2의 무인도");
                        turn = true;
                        island2++;
                        return;
                    }

                    MessageBox.Show("무인도에서 벗어나셨습니다.", "user2의 무인도 탈출");
                    island2 = 0;
                }
                Random rnd = new Random();
                dice2 = rnd.Next(1, 7);
                dice1_lab.BackColor = Color.White;
                dice1_lab.Text = dice2.ToString();
                for (int i = 0; i < dice2; i++)
                {
                    user2.index++;
                    user2_change();
                    Invalidate();
                    if (user2.index >= 28)
                    {
                        MessageBox.Show("출발지를 지났기때문에 용돈 1500원 넣어드려요.");
                        user2.MONEY = user2.MONEY + 1500;
                        user2.index = user2.index - 28;
                        user2_change();
                        Invalidate();
                    }
                    System.Threading.Thread.Sleep(500); //이동하는부분 0.5초 간격
                }
                user2_building_system();
                turn = true;
            }
            else
            {
                MessageBox.Show("이번 차례는 1p 차례입니다. 다음 턴을 기대하세요~~");
            }
        }

        public mabel1() //Form생성자
        {
            InitializeComponent();            
            pen = new Pen(Color.Black);
            whitebrush = new SolidBrush(Color.White);
            blackbrush = new SolidBrush(Color.Black);
            redbrush = new SolidBrush(Color.Red);
            bluebrush = new SolidBrush(Color.Blue);
            turn_lab.Visible = false;
            for (int i = 0; i < 28; i++)
            {
                Etc newland = new Etc();
                al.Add(newland);
            }
            for (int j = 0; j < 28; j++)
            {
                Etc temp = new Etc();
                temp = (Etc)al[j];
                temp.buy_charge = 500;
                temp.toll = 700;
                temp.accep_charge = 1000;
                temp.buy_lnadmark = 250;
                temp.landmark_toll = 1400;
                al[j] = temp;
            }
            t0 = (Etc)al[0];
            t1 = (Etc)al[1];
            t2 = (Etc)al[2];
            t3 = (Etc)al[3];
            t4 = (Etc)al[4];
            t5 = (Etc)al[5];
            t6 = (Etc)al[6];
            t7 = (Etc)al[7];
            t8 = (Etc)al[8];
            t9 = (Etc)al[9];
            t10 = (Etc)al[10];
            t11 = (Etc)al[11];
            t12 = (Etc)al[12];
            t13 = (Etc)al[13];
            t14 = (Etc)al[14];
            t15 = (Etc)al[15];
            t16 = (Etc)al[16];
            t17 = (Etc)al[17];
            t18 = (Etc)al[18];
            t19 = (Etc)al[19];
            t20 = (Etc)al[20];
            t21 = (Etc)al[21];
            t22 = (Etc)al[22];
            t23 = (Etc)al[23];
            t24 = (Etc)al[24];
            t25 = (Etc)al[25];
            t26 = (Etc)al[26];
            t27 = (Etc)al[27];
        }

        protected override void OnPaint(PaintEventArgs e)   //Onpaint
        {
            Rectangle land1 = new Rectangle(430, 525, 70, 10);  //안동
            Rectangle land2 = new Rectangle(360, 525, 70, 10);  //경주
            Rectangle land3 = new Rectangle(290, 525, 70, 10);  //포항
            Rectangle land4 = new Rectangle(220, 525, 70, 10);  //독도
            Rectangle land5 = new Rectangle(150, 525, 70, 10);  //원주
            Rectangle land6 = new Rectangle(80, 525, 70, 10);   //강릉

            Rectangle land8 = new Rectangle(10, 455, 70, 10);    //목포
            Rectangle land9 = new Rectangle(10, 385, 70, 10);   //울릉도
            Rectangle land10 = new Rectangle(10, 315, 70, 10);  //여수
            Rectangle land11 = new Rectangle(10, 245, 70, 10);  //용인
            Rectangle land12 = new Rectangle(10, 175, 70, 10);  //성남
            Rectangle land13 = new Rectangle(10, 105, 70, 10);  //수원

            Rectangle land15 = new Rectangle(80, 35, 70, 10);   //제주
            Rectangle land16 = new Rectangle(150, 35, 70, 10);  //세종
            Rectangle land17 = new Rectangle(220, 35, 70, 10);  //울산
            Rectangle land18 = new Rectangle(290, 35, 70, 10);  //광주
            Rectangle land19 = new Rectangle(360, 35, 70, 10);  //오이도
            Rectangle land20 = new Rectangle(430, 35, 70, 10);  //대전

            Rectangle land22 = new Rectangle(500, 105, 70, 10);   //대구
            Rectangle land23 = new Rectangle(500, 175, 70, 10);  //우도
            Rectangle land24 = new Rectangle(500, 245, 70, 10);  //인천
            Rectangle land25 = new Rectangle(500, 315, 70, 10);  //부산
            Rectangle land26 = new Rectangle(500, 385, 70, 10);  //월미도
            Rectangle land27 = new Rectangle(500, 455, 70, 10);  //서울



            user_stat();
            if (turn == true)
            {
                turn_lab.Text = "user1 차례입니다.";
                turn_lab.BackColor = Color.Red;
            }
            if (turn == false)
            {
                turn_lab.Text = "user2 차례입니다.";
                turn_lab.BackColor = Color.Blue;
            }

            Graphics G = this.CreateGraphics();

            for (int x = 0; x < 9; x++) //x축(가로)
                G.DrawLine(pen, 10, 30 + x * 70, 570, 30 + x * 70);

            for (int y = 0; y < 9; y++) //y축(세로)
                G.DrawLine(pen, 10 + y * 70, 30, 10 + y * 70, 590);
            
            Rectangle rect = new Rectangle(81, 101, 419, 419);  //중앙 가리는 사각형
            G.FillRectangle(whitebrush, rect);

            Rectangle pone1 = new Rectangle(user1_x, user1_y, 20, 20);
            G.FillRectangle(redbrush, pone1);

            Rectangle pone2 = new Rectangle(user2_x, user2_y, 20, 20);
            G.FillRectangle(bluebrush, pone2);

            if (t1.owner == 1) //안동
                G.FillRectangle(redbrush, land1);
            else if (t1.owner == 2)
                G.FillRectangle(bluebrush, land1);

            if (t2.owner == 1) //경주
                G.FillRectangle(redbrush, land2);
            else if (t2.owner == 2)
                G.FillRectangle(bluebrush, land2);

            if (t3.owner == 1) //포항
                G.FillRectangle(redbrush, land3);
            else if (t3.owner == 2)
                G.FillRectangle(bluebrush, land3);

            if (t4.owner == 1) //독도
                G.FillRectangle(redbrush, land4);
            else if (t4.owner == 2)
                G.FillRectangle(bluebrush, land4);

            if (t5.owner == 1) //원주
                G.FillRectangle(redbrush, land5);
            else if (t5.owner == 2)
                G.FillRectangle(bluebrush, land5);

            if (t6.owner == 1) //강릉
                G.FillRectangle(redbrush, land6);
            else if (t6.owner == 2)
                G.FillRectangle(bluebrush, land6);

            if (t8.owner == 1) //목포
                G.FillRectangle(redbrush, land8);
            else if (t8.owner == 2)
                G.FillRectangle(bluebrush, land8);

            if (t9.owner == 1) //울릉도
                G.FillRectangle(redbrush, land9);
            else if (t9.owner == 2)
                G.FillRectangle(bluebrush, land9);

            if (t10.owner == 1) //여수
                G.FillRectangle(redbrush, land10);
            else if (t10.owner == 2)
                G.FillRectangle(bluebrush, land10);

            if (t11.owner == 1) //용인            
                G.FillRectangle(redbrush, land11);
            else if (t11.owner == 2)
                G.FillRectangle(bluebrush, land11);

            if (t12.owner == 1) //성남
                G.FillRectangle(redbrush, land12);
            else if (t12.owner == 2)
                G.FillRectangle(bluebrush, land12);

            if (t13.owner == 1) //수원
                G.FillRectangle(redbrush, land13);
            else if (t13.owner == 2)
                G.FillRectangle(bluebrush, land13);

            if (t15.owner == 1) //제주
                G.FillRectangle(redbrush, land15);
            else if (t15.owner == 2)
                G.FillRectangle(bluebrush, land15);

            if (t16.owner == 1) //세종
                G.FillRectangle(redbrush, land16);
            else if (t16.owner == 2)
                G.FillRectangle(bluebrush, land16);

            if (t17.owner == 1) //울산
                G.FillRectangle(redbrush, land17);
            else if (t17.owner == 2)
                G.FillRectangle(bluebrush, land17);

            if (t18.owner == 1) //광주
                G.FillRectangle(redbrush, land18);
            else if (t18.owner == 2)
                G.FillRectangle(bluebrush, land18);

            if (t19.owner == 1) //오이도
                G.FillRectangle(redbrush, land19);
            else if (t19.owner == 2)
                G.FillRectangle(bluebrush, land19);

            if (t20.owner == 1) //대전
                G.FillRectangle(redbrush, land20);
            else if (t20.owner == 2)
                G.FillRectangle(bluebrush, land20);

            if (t22.owner == 1) //대구
                G.FillRectangle(redbrush, land22);
            else if (t22.owner == 2)
                G.FillRectangle(bluebrush, land22);

            if (t23.owner == 1) //우도
                G.FillRectangle(redbrush, land23);
            else if (t23.owner == 2)
                G.FillRectangle(bluebrush, land23);

            if (t24.owner == 1) //인천
                G.FillRectangle(redbrush, land24);
            else if (t24.owner == 2)
                G.FillRectangle(bluebrush, land24);

            if (t25.owner == 1) //부산
                G.FillRectangle(redbrush, land25);
            else if (t25.owner == 2)
                G.FillRectangle(bluebrush, land25);

            if (t26.owner == 1) //월미도
                G.FillRectangle(redbrush, land26);
            else if (t26.owner == 2)
                G.FillRectangle(bluebrush, land26);

            if (t27.owner == 1) //서울
                G.FillRectangle(redbrush, land27);
            else if (t27.owner == 2)
                G.FillRectangle(bluebrush, land27);
        }
    }
}