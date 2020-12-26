using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFactory
{ 

    public class Array_list
    {
          public static ArrayList al = new ArrayList();

        public Ad_Score this[int index]
        {


            get
            {
                if (index > -1 & index < al.Count)
                {
                    return (Ad_Score)al[index];
                }


                else
                {
                    Console.WriteLine("sid[" + index + "] 는 없습니다!");
                    return null;
                }
            }

            set
            {
                if (index > -1 & index < al.Count)
                {
                    al[index] = value;
                }
                else if (index == al.Count)
                {
                    al.Add(value);
                }
                else { Console.WriteLine("sid[" + index + "]: 입력 범위 초과 에러!!"); }
            }
        }

        public void RemoveAt(int index)
        {
            al.RemoveAt(index);
        }
    }
    
}
