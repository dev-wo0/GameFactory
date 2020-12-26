using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameFactory
{
    public class Etc
    {
        public int owner=0;                 // 소유주 0 = 소유X, 1 = 1p, 2 = 2p
        public bool landmark = false;       // 랜드마크 없으면 false, 있으면true
        public int buy_charge;              // 구매비용
        public int buy_lnadmark;            // 래드마크 구매비용
        public int toll;                    // 통행료
        public int landmark_toll;           // 랜드마크 통행료
        public int accep_charge;            // 인수비용
    }

    public class User
    {
        public int order;           // 순서
        public int MONEY = 10000;   // 자산
        public int index = 0;       // 말 위치
        public int get_land = 0;    // 소유한 도시 수
        public int get_tour = 0;    // 관광지 개수
    }
}