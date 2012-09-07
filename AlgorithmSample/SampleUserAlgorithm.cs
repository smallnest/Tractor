using System;
using System.Collections;
using System.Text;

using Kuaff.Tractor.Plugins;

namespace Kuaff.Tractor.AlgorithmSample
{
    public class SampleUserAlgorithm : IUserAlgorithm
    {
        /// <summary>
        /// 算法作者
        /// </summary>
        public string Author
        {
            get {return "smallnest";}
        }
        /// <summary>
        /// 算法作者的email地址
        /// </summary>
        public string Email
        {
            get {return "smallnest@gmail.com";}
        }
        /// <summary>
        /// 算法名称
        /// </summary>
        public string Name
        {
            get {return "简单演示算法";}
        }
        /// <summary>
        /// 算法介绍
        /// </summary>
        public string Description
        {
            get { return "这是一个算法接口的简单实现，用来演示如何实现算法接口。"; }
        }

        /// <summary>
        /// 首先出牌的算法。
        /// </summary>
        /// <param name="who">当前用户是谁，1为南家，2为北家，3为西家，4为东家</param>
        /// <param name="suit">当前主牌的花色，1为红心，2为黑桃，3为方片，4为梅花,5为王（无主）</param>
        /// <param name="rank">当前打几,0为打2，1为打3,2为打4........11为打K，12为打A,53为打王</param>
        /// <param name="master">当前谁为庄家,1为南家，2为北家，3为西家，4为东家</param>
        /// <param name="sendCards">当前一局各家已经出掉的牌，sendCards[0]为南家，sendCards[1]为北家，sendCards[2]为西家，sendCards[3]为东家</param>
        /// <param name="myCards">此用户手中的牌</param>
        /// <returns></returns>
        public ArrayList ShouldSendCards(int who, int suit, int rank, int master, string[] sendCards, string myCards)
        {
            ArrayList result = new ArrayList();
            string[] cards = myCards.Split(new char[] {','});
            if (cards.Length > 0)
            {
                result.Add(int.Parse(cards[0]));
            }
           
            return result;
        }

        /// <summary>
        /// 改自己出的牌时的算法（自己不是首家）
        /// </summary>
        /// <param name="who">当前用户是谁，1为南家，2为北家，3为西家，4为东家</param>
        /// <param name="suit">当前主牌的花色，1为红心，2为黑桃，3为方片，4为梅花,5为王（无主）</param>
        /// <param name="rank">当前打几,0为打2，1为打3,2为打4........11为打K，12为打A,53为打王</param>
        /// <param name="master">当前谁为庄家,1为南家，2为北家，3为西家，4为东家</param>
        /// <param name="whoIsFirst">谁首先出的牌，1为南家，2为北家，3为西家，4为东家</param>
        /// <param name="sendCards">当前一局各家已经出掉的牌，sendCards[0]为南家，sendCards[1]为北家，sendCards[2]为西家，sendCards[3]为东家</param>
        /// <param name="currentSendCards">首家以及自己的上家出的牌</param>
        /// <param name="myCards">此用户手中的牌</param>
        /// <returns></returns>
        public ArrayList MustSendCards(int who, int suit, int rank, int master, int whoIsFirst, string[] sendCards, ArrayList[] currentSendCards, string myCards)
        {
            ArrayList result = new ArrayList();
            string[] cards = myCards.Split(new char[] { ',' });
            //得到首家出的花色，是某种副牌还是在调主
            
            if (currentSendCards[whoIsFirst-1].Count > 0)
            {
                //得到首家出的一张牌
                int oneCard = (int)currentSendCards[whoIsFirst - 1][0];
                bool isSuit = false;
                if (oneCard == 52 || oneCard == 53) //如果是小王或者大王，毫无疑问应该是主
                {
                    isSuit = true;
                }
                else if ((oneCard % 13) == rank) //如果是主,比如打8，如果出的牌是8，则为调主
                {
                    isSuit = true;
                }
                else
                {
                    if ((oneCard >= 0 && oneCard < 13) && (suit==1)) //如果出的是红心而且主花色也是红心
                    {
                        isSuit = true;
                    }
                    else if ((oneCard >= 13 && oneCard < 26) && (suit == 2)) //如果出的是黑桃而且主花色也是黑桃
                    {
                        isSuit = true;
                    }
                    else if ((oneCard >= 26 && oneCard < 39) && (suit == 3)) //如果出的是方片而且主花色也是方片
                    {
                        isSuit = true;
                    }
                    else if ((oneCard >= 39 && oneCard < 52) && (suit == 4)) //如果出的是梅花而且主花色也是梅花
                    {
                        isSuit = true;
                    }
                }
                int count = currentSendCards[whoIsFirst - 1].Count;

                if (isSuit) //如果是调主，按调主的方法出牌
                {
                    //TODO:判断首家是否是甩牌

                    //TODO:否则判断首家是否有拖拉机

                    //TODO:否则判断首家是否出对

                    //TODO:否则首家应该出单张的牌

                    //TODO:将来删除，下面是最笨的办法,假定首家只出了一张牌,根本未考虑上面的条件
                    for (int i = 0; i < cards.Length; i++)
                    {
                        int number = int.Parse(cards[i]);
                        if ((number / 13) == (suit-1) && number < 52) //有此花色的主
                        {
                            result.Add(number);
                            break;
                        }
                        else if ((number == 52) || (number == 53)) //有大小王
                        {
                            result.Add(number);
                            break;
                        }
                        else if ((number % 13) == rank) //有主，比如打10,有10
                        {
                            result.Add(number);
                            break;
                        }
                    }

                    if (result.Count ==0) //无主，随便出张副牌
                    {
                        result.Add(int.Parse(cards[0]));
                    }
                }
                else //否则出相应的副牌
                {
                    //TODO:判断首家是否是甩牌

                    //TODO:否则判断首家是否有拖拉机

                    //TODO:否则判断首家是否出对

                    //TODO:否则首家应该出单张的牌

                    //TODO:将来删除，下面是最笨的办法,假定首家只出了一张牌,根本未考虑上面的条件
                    int firstSuit = (oneCard / 13) + 1;

                    for (int i = 0; i < cards.Length; i++)
                    {
                        if (cards[i].Length == 0)
                        {
                            continue;
                        }
                        int number = int.Parse(cards[i]);
                        if ((number / 13) == (firstSuit - 1) && number < 52 && ((number % 13) != rank)) //有此花色
                        {
                            result.Add(number);
                            break;
                        }
                       
                    }
                    if (result.Count == 0) //如果没有此花色的牌，随便出一张
                    {
                        result.Add(int.Parse(cards[0]));
                    }
                }
                }

               

            return result;
        }
    }
}
