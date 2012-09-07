using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Kuaff.Tractor
{
    /// <summary>
    /// 通用处理类.
    /// 用来处理程序中常用的方法，比如解析等.
    /// </summary>
    class CommonMethods
    {
        
        /// <summary>
        /// 对一个随机产生的序列进行解析
        /// </summary>
        /// <param name="list">要排序的列表</param>
        /// <param name="suit">当前花色</param>
        /// <param name="rank">当前牌局</param>
        /// <returns>返回CurrentPoker对象</returns>
        internal static CurrentPoker parse(ArrayList list, int suit, int rank)
        {
            //红桃0-12
            //黑桃13-25
            //方块26-38
            //梅花39-51
            //小王52
            //大王53
            CurrentPoker poker = new CurrentPoker();

            poker.Rank = rank;
            poker.Suit = suit;

            //解析用户的牌局
            foreach (int i in list)
            {
                //大王
                if (i == 53)
                {
                    poker.BigJack++;
                    continue;
                }
                else if (i == 52)
                {
                    poker.SmallJack++;
                    continue;
                }
                else if (i < 52)
                {
                    if (i >= 0 && i < 13)
                    {
                        poker.Hearts[i]++;
                        if (i != rank)
                        {
                            poker.HeartsNoRank[i]++;
                            poker.HeartsNoRankTotal++;
                        }
                        else
                        {
                            poker.HeartsRankTotal++;
                        }
                    }
                    else if (i >= 13 && i < 26)
                    {
                        poker.Peachs[i - 13]++;
                        if ((i - 13) != rank)
                        {
                            poker.PeachsNoRank[i - 13]++;
                            poker.PeachsNoRankTotal++;
                        }
                        else
                        {
                            poker.PeachsRankTotal++;
                        }
                    }
                    else if (i >= 26 && i < 39)
                    {
                        poker.Diamonds[i - 26]++;
                        if ((i - 26) != rank)
                        {
                            poker.DiamondsNoRank[i - 26]++;
                            poker.DiamondsNoRankTotal++;
                        }
                        else
                        {
                            poker.DiamondsRankTotal++;
                        }
                    }
                    else if (i >= 39 && i < 52)
                    {
                        poker.Clubs[i - 39]++;
                        if ((i - 39) != rank)
                        {
                            poker.ClubsNoRank[i - 39]++;
                            poker.ClubsNoRankTotal++;
                        }
                        else
                        {
                            poker.ClubsRankTotal++;
                        }
                    }

                    if (suit > 0)
                    {
                        if (i == ((suit - 1) * 13 + rank))
                        {
                            poker.MasterRank++;
                        }
                    }
                }

                

            }
           
            return poker;
        }

        /// <summary>
        /// 判断当前发出的牌中包含的分数
        /// </summary>
        /// <param name="currentSendCards">各家出的牌</param>
        /// <returns>计算出的分数</returns>
        internal static int GetScores(ArrayList[] currentSendCards)
        {
            int scores = 0;
            for (int i = 0; i < currentSendCards.Length; i++)
            {
                for (int j = 0; j < currentSendCards[i].Count; j++)
                {
                    if ((int)currentSendCards[i][j] % 13 == 11)
                    {
                        scores += 10;
                    }
                    if ((int)currentSendCards[i][j] % 13 == 8)
                    {
                        scores += 10;
                    }
                    if ((int)currentSendCards[i][j] % 13 == 3)
                    {
                        scores += 5;
                    }
                }
            }

            return scores;
        }

        /// <summary>
        /// 计算其他各家用户的编号
        /// </summary>
        /// <param name="me">基准用户的编号</param>
        /// <returns></returns>
        internal static int[] OtherUsers(int me)
        {
            int[] users = { 0, 0, 0 };
            if (me == 1)
            {
                users[0] = 4;
                users[1] = 2;
                users[2] = 3;
            }
            else if (me == 2)
            {
                users[0] = 3;
                users[1] = 1;
                users[2] = 4;
            }
            else if (me == 3)
            {
                users[0] = 1;
                users[1] = 4;
                users[2] = 2;
            }
            else if (me == 4)
            {
                users[0] = 2;
                users[1] = 3;
                users[2] = 1;
            }

            return users;
        }

        /// <summary>
        /// 判断一张牌是否是主
        /// </summary>
        /// <param name="number"></param>
        /// <param name="suit"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        internal static bool IsMaster(int number,int suit,int rank)
        {
            if ((number == 53) || (number == 52))
            {
                return true;
            }
            else if ((number%13) == rank)
            {
                return true;
            }
            else if (suit ==1)
            {
                if (number>=0 && number<13)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (suit == 2)
            {
                if (number >= 13 && number < 26)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (suit == 3)
            {
                if (number >= 26 && number < 39)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (suit == 4)
            {
                if (number >= 39 && number < 52)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// 比较是否是同一花色
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="suit">主花色</param>
        /// <param name="rank">主数字</param>
        /// <returns></returns>
        internal static bool IsSameSuit(int a, int b, int suit, int rank)
        {
            bool b1 = IsMaster(a, suit, rank);
            bool b2 = IsMaster(a, suit, rank);

            if ((b1) && (b2)) //都是主，比较主
            {
                return true;
            }
            else if ((!b1) && (!b2)) //都是副
            {
                int suit1 = GetSuit(a);
                int suit2 = GetSuit(b);
                if (suit1 != suit2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 得到一个牌的花色
        /// </summary>
        /// <param name="a">牌值</param>
        /// <returns>花色</returns>
        internal static int GetSuit(int a)
        {
            if (a >= 0 && a < 13)
            {
                return 1;
            }
            else if (a >= 13 && a < 26)
            {
                return 2;
            }
            else if (a >= 26 && a < 39)
            {
                return 3;
            }

            else if (a >= 39 && a < 52)
            {
                return 4;
            }

            else 
            {
                return 5;
            }
        }

        /// <summary>
        /// 得到一张牌的花色，如果是主，则返回主的花色
        /// </summary>
        /// <param name="a">牌值</param>
        /// <param name="suit">主花色</param>
        /// <param name="rank">主Rank</param>
        /// <returns>花色</returns>
        internal static int GetSuit(int a,int suit,int rank)
        {
            int firstSuit = 0;
            
            if (a == 53 || a == 52)
            {
                firstSuit = suit;
            }
            else if ((a % 13) == rank)
            {
                firstSuit = suit;
            }
            else
            {
                firstSuit = GetSuit(a);
            }

            return firstSuit;
        }

        /// <summary>
        /// 从一堆牌中找出最大的牌，考虑主
        /// </summary>
        /// <param name="sendCards">一堆主</param>
        /// <param name="suit">花色</param>
        /// <param name="rank">主</param>
        /// <returns>最大的牌</returns>
        internal static int GetMaxCard(ArrayList sendCards,int suit,int rank)
        {
            CurrentPoker cp = new CurrentPoker();
            cp.Suit = suit;
            cp.Rank = rank;
            cp = parse(sendCards, suit, rank);
            int thisSuit = CommonMethods.GetSuit((int)sendCards[0]);


            if (cp.IsMixed())
            {
                return -1;
            }

            bool hasTractor = cp.HasTractors();
            int pairTotal = cp.GetPairs().Count;
            int count = cp.Count;

            //如果拖拉机
            if (hasTractor)
            {
                return cp.GetTractor();
            }
            else if (count == 1) //单张牌
            {
                return (int)sendCards[0];
            }
            else if (count == pairTotal * 2 && (count > 1)) //都是对
            {
                return (int)cp.GetPairs()[pairTotal - 1];
            }
            else //有对和有单张牌，得到对的最大值
            {
                if (pairTotal>0)
                {
                    return (int)cp.GetPairs()[pairTotal - 1];
                }
                else
                {
                    return cp.GetMaxCard(thisSuit);
                }
                
            }

          
        }

        /// <summary>
        /// 得到某种花色牌的数量
        /// </summary>
        /// <param name="cp">牌</param>
        /// <param name="suit">主花色</param>
        /// <param name="rank">主Rank</param>
        /// <param name="mysuit">查找的花色</param>
        /// <returns>牌的数量</returns>
        internal static int GetSuitCount(CurrentPoker cp,int suit,int rank,int mysuit)
        {
            if (suit == mysuit)
            {
                int count = cp.MasterRank + cp.SubRank + cp.BigJack + cp.SmallJack;
                if (mysuit == 1)
                {
                    count += cp.HeartsNoRankTotal;
                }
                else if (mysuit == 2)
                {
                    count += cp.PeachsNoRankTotal;
                }
                else if (mysuit == 3)
                {
                    count += cp.DiamondsNoRankTotal;
                }
                else if (mysuit == 4)
                {
                    count += cp.ClubsNoRankTotal;
                }

                return count;
            }
            else
            {
                if (mysuit == 1)
                {
                    return cp.HeartsNoRankTotal;
                }

                if (mysuit == 2)
                {
                    return cp.PeachsNoRankTotal;
                    
                }

                if (mysuit == 3)
                {
                    return cp.DiamondsNoRankTotal;

                }

                if (mysuit == 4)
                {
                    return cp.ClubsNoRankTotal;
                   
                }

                if (mysuit == 5)
                {
                    return cp.SmallJack + cp.BigJack;
                }
            }

            return 0;
        }

        /// <summary>
        /// 比较两张牌孰大孰小
        /// </summary>
        /// <param name="a">第一张牌</param>
        /// <param name="b">第二张牌</param>
        /// <param name="suit">主花色</param>
        /// <param name="rank">主Rank</param>
        /// <param name="firstSuit">第一张牌的花色</param>
        /// <returns>如果第一张大于等于第二张牌，返回true,否则返回false</returns>
        internal static bool CompareTo(int a,int b,int suit,int rank, int firstSuit)
        {
            if ((a == -1) && (b == -1))
            {
                return true;
            }
            else if ((a == -1) && (b != -1))
            {
                return false;
            }
            else if ((a != -1) && (b == -1))
            {
                return true;
            }


            int suit1 = GetSuit(a, suit, rank);
            int suit2 = GetSuit(b, suit, rank);

            if ((suit1 == firstSuit) && (suit2 != firstSuit))
            {
                if (suit1 == suit)
                {
                    return true;
                }
                else if (suit2 == suit)
                {
                    return false;
                }
                return true;
            }
            else if ((suit1 != firstSuit) && (suit2 == firstSuit))
            {
                if (suit1 == suit)
                {
                    return true;
                }
                else if (suit2 == suit)
                {
                    return false;
                }

                return false;
            }

            if (a == 53)
            {
                return true;
            }


            if (a == 52)
            {
                if (b == 53)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (b == 52)
            {
                if (a == 53)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            if (a == (suit-1) * 13 +rank) 
            {
                if (b == 53 || b == 52)
                {
                    return false;
                }
                else 
                {
                    return true;
                }
            }
            else if (a% 13 == rank)
            {
                if (b == 53 || b == 52 || (b == (suit - 1) * 13 + rank))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (b == (suit - 1) * 13 + rank)
            {
                if (a == 53 || a == 52)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (b % 13 == rank)
            {
                if (a == 53 || a == 52 || (a == (suit - 1) * 13 + rank))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if ((suit1 == suit) && (suit2 != suit))
                {
                    return true;
                }
                else if ((suit1 != suit) && (suit2 == suit))
                {
                    return false;
                }
                else if (suit1 == suit2)
                {
                    return (a - b >= 0);
                }
                else
                {
                    return true;
                }
                
            }
        }

        /// <summary>
        /// 出牌的通用做法，从CurrentPoker中删除此牌，从pokerList中删除此牌，将此牌放入出牌列表中
        /// </summary>
        /// <param name="sends">出牌数组列表</param>
        /// <param name="cp">CurrentPoker对象</param>
        /// <param name="pokerList">pokerList对象</param>
        /// <param name="number">出牌数字</param>
        internal static void SendCards(ArrayList sends,CurrentPoker cp,ArrayList pokerList,int number)
        {
            sends.Add(number);
            cp.RemoveCard(number);
            pokerList.Remove(number);
        }
    }
}
