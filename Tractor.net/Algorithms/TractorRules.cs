using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Kuaff.CardResouces;

namespace Kuaff.Tractor
{
    /// <summary>
    /// 提供升级的规则
    /// </summary>
    class TractorRules
    {

        //判断我出的牌是否合法
        internal static bool IsInvalid(MainForm mainForm, ArrayList[] currentSendedCards, int who)
        {
            
            CurrentPoker[] cp = new CurrentPoker[4];
            int suit = mainForm.currentState.Suit;
            int first = mainForm.firstSend;

            int rank = mainForm.currentRank;

            cp[who-1] = new CurrentPoker();
            cp[who - 1].Suit = suit;
            cp[who - 1].Rank = rank;

            ArrayList list = new ArrayList();
            CurrentPoker tmpCP = new CurrentPoker();
            tmpCP.Suit = suit;
            tmpCP.Rank = rank;

            for (int i = 0; i < mainForm.myCardIsReady.Count; i++)
            {
                if ((bool)mainForm.myCardIsReady[i])
                {
                    cp[who - 1].AddCard((int)mainForm.myCardsNumber[i]);
                    list.Add((int)mainForm.myCardsNumber[i]);
                }
                else
                {
                    tmpCP.AddCard((int)mainForm.myCardsNumber[i]);
                }
            }
            int[] users = CommonMethods.OtherUsers(who);

            cp[users[0] - 1] = CommonMethods.parse(mainForm.currentSendCards[users[0] - 1], suit, rank);
            cp[users[1] - 1] = CommonMethods.parse(mainForm.currentSendCards[users[1] - 1], suit, rank);
            cp[users[2] - 1] = CommonMethods.parse(mainForm.currentSendCards[users[2] - 1], suit, rank);
            cp[0].Sort();
            cp[1].Sort();
            cp[2].Sort();
            cp[3].Sort();

            

            //如果我出牌
            if (first == who)
            {
                if (cp[who -1].Count ==0)
                {
                    return false;
                }

                if (cp[who-1].IsMixed())
                {
                    return false;
                }

                return true;
            }
            else
            {
                if (list.Count != currentSendedCards[first - 1].Count)
                {
                    return false;
                }


                //得到第一个家伙出的花色
                int previousSuit = CommonMethods.GetSuit((int)currentSendedCards[first - 1][0], suit, rank);
               
                //0.如果我是混合的，则判断我手中是否还剩出的花色，如果剩,false;如果不剩;true
                if (cp[who-1].IsMixed())
                {
                    if (tmpCP.HasSomeCards(previousSuit))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }


                //如果出的花色不一致
                int mysuit = CommonMethods.GetSuit((int)list[0], suit, rank);


                //如果确实花色不一致
                if (mysuit != previousSuit) 
                {
                    //而且确实没有此花色
                    if (tmpCP.HasSomeCards(previousSuit))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }


                //3.别人如果出对，我也应该出对
                int firstPairs = cp[first - 1].GetPairs().Count;
                int mypairs = cp[who - 1].GetPairs().Count;
                int myCurrentPairs = mainForm.currentPokers[who - 1].GetPairs(previousSuit).Count;


                //2.如果别人出拖拉机，我如果有，也应该出拖拉机
                if (cp[first-1].HasTractors())
                {
                    if ((!cp[who - 1].HasTractors()) && (mainForm.currentPokers[who-1].GetTractor(previousSuit) > -1))
                    {
                        return false;
                    }
                    else if ((mypairs == 1) && (myCurrentPairs> 1)) //出了单个对，但是实际多于1个对
                    {
                        return false;
                    }
                    else if ((mypairs == 0) && (myCurrentPairs > 0)) //没出对，但实际有对
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                

                if (firstPairs > 0)
                {
                    if ((myCurrentPairs >= firstPairs) && (mypairs < firstPairs))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            return true;

        }

        internal static bool IsInvalid(MainForm mainForm, ArrayList[] currentSendedCards,ArrayList currentSendedCard, int who)
        {

            CurrentPoker[] cp = new CurrentPoker[4];
            int suit = mainForm.currentState.Suit;
            int first = mainForm.firstSend;

            int rank = mainForm.currentRank;

            cp[who - 1] = new CurrentPoker();
            cp[who - 1].Suit = suit;
            cp[who - 1].Rank = rank;

            ArrayList list = new ArrayList();
            CurrentPoker tmpCP = new CurrentPoker();
            tmpCP.Suit = suit;
            tmpCP.Rank = rank;

            for (int i = 0; i < currentSendedCard.Count; i++)
            {

                cp[who - 1].AddCard((int)currentSendedCard[i]);
                list.Add((int)currentSendedCard[i]);
               
            }
            int[] users = CommonMethods.OtherUsers(who);

            cp[users[0] - 1] = CommonMethods.parse(mainForm.currentSendCards[users[0] - 1], suit, rank);
            cp[users[1] - 1] = CommonMethods.parse(mainForm.currentSendCards[users[1] - 1], suit, rank);
            cp[users[2] - 1] = CommonMethods.parse(mainForm.currentSendCards[users[2] - 1], suit, rank);
            cp[0].Sort();
            cp[1].Sort();
            cp[2].Sort();
            cp[3].Sort();



            //如果我出牌
            if (first == who)
            {
                if (cp[who-1].Count == 0)
                {
                    return false;
                }

                if (cp[who - 1].IsMixed())
                {
                    return false;
                }

                return true;
            }
            else
            {
                if (list.Count != currentSendedCards[first - 1].Count)
                {
                    return false;
                }


                //得到第一个家伙出的花色
                int previousSuit = CommonMethods.GetSuit((int)currentSendedCards[first - 1][0], suit, rank);

                //0.如果我是混合的，则判断我手中是否还剩出的花色，如果剩,false;如果不剩;true
                if (cp[who - 1].IsMixed())
                {
                    if (tmpCP.HasSomeCards(previousSuit))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }


                //如果出的花色不一致
                int mysuit = CommonMethods.GetSuit((int)list[0], suit, rank);


                //如果确实花色不一致
                if (mysuit != previousSuit)
                {
                    //而且确实没有此花色
                    if (tmpCP.HasSomeCards(previousSuit))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }


                //3.别人如果出对，我也应该出对
                int firstPairs = cp[first - 1].GetPairs().Count;
                int mypairs = cp[who - 1].GetPairs().Count;
                int myCurrentPairs = mainForm.currentPokers[who - 1].GetPairs(previousSuit).Count;


                //2.如果别人出拖拉机，我如果有，也应该出拖拉机
                if (cp[first - 1].HasTractors())
                {
                    if ((!cp[who - 1].HasTractors()) && (mainForm.currentPokers[who - 1].GetTractor(previousSuit) > -1))
                    {
                        return false;
                    }
                    else if ((mypairs == 1) && (myCurrentPairs > 1)) //出了单个对，但是实际多于1个对
                    {
                        return false;
                    }
                    else if ((mypairs == 0) && (myCurrentPairs > 0)) //没出对，但实际有对
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }



                if (firstPairs > 0)
                {
                    if ((myCurrentPairs >= firstPairs) && (mypairs < firstPairs))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            return true;

        }


        //根据得分判断应该跳几级
        internal static void GetNextRank(MainForm mainForm, bool success)
        {
            
            int user = mainForm.currentState.Master; //打本次时的主
            int rank = 0;

            int number = 0;
            if (success)
            {
                if (mainForm.Scores == 0)  //大光
                {
                    number += 3;
                }
                else if ((mainForm.Scores >= 0) && (mainForm.Scores < 40)) //小光
                {
                    number += 2;
                }
                else
                {
                    number++;
                }
            }
            else
            {
                number = (mainForm.Scores - 80) / 40;
            }


            
            string mustRank = "," + mainForm.gameConfig.MustRank + ",";

            if ((user == 1) || (user == 2))
            {
                rank = mainForm.currentState.OurCurrentRank;
                int oldRank = rank;

                if (rank == 53)
                {
                    rank = 13;
                }
                rank += number;

                //判断是否必打
                if (oldRank < 3 && rank > 3)
                {
                    if (mustRank.IndexOf(",3,")>=0)
                    {
                        rank = 3;
                    }
                }
                else if (oldRank < 8 && rank > 8)
                {
                    if (mustRank.IndexOf(",8,") >= 0)
                    {
                        rank = 8;
                    }
                }
                else if (oldRank < 9 && rank > 9)
                {
                    if (mustRank.IndexOf(",9,") >= 0)
                    {
                        rank = 9;
                    }
                }
                else if (oldRank < 10 && rank > 10)
                {
                    if (mustRank.IndexOf(",10,") >= 0)
                    {
                        rank = 10;
                    }
                }
                else if (oldRank < 11 && rank > 11)
                {
                    if (mustRank.IndexOf(",11,") >= 0)
                    {
                        rank = 11;
                    }
                }
                else if (oldRank < 12 && rank > 12)
                {
                    if (mustRank.IndexOf(",12,") >= 0)
                    {
                        rank = 12;
                    }
                }
                else if (oldRank < 13 && rank > 13)
                {
                    if (mustRank.IndexOf(",13,") >= 0)
                    {
                        rank = 13;
                    }
                }


                if (rank > 13)
                {
                    if ((user == 1) || (user == 2))
                    {
                        mainForm.currentState.OurTotalRound++;
                    }
                    else
                    {
                        mainForm.currentState.OpposedTotalRound++;
                    }
                    rank -= 14;
                }
                else if (rank == 13)
                {
                    rank = 53;
                }

              
                mainForm.currentState.OurCurrentRank = rank;
                mainForm.currentRank = rank;
            }
            else if ((user == 3) || (user == 4))
            {
                rank = mainForm.currentState.OpposedCurrentRank;
                int oldRank = rank;

                if (rank == 53)
                {
                    rank = 13;
                }
                rank += number;

                //判断是否必打
                if (oldRank < 3 && rank > 3)
                {
                    if (mustRank.IndexOf(",3,") >= 0)
                    {
                        rank = 3;
                    }
                }
                else if (oldRank < 8 && rank > 8)
                {
                    if (mustRank.IndexOf(",8,") >= 0)
                    {
                        rank = 8;
                    }
                }
                else if (oldRank < 9 && rank > 9)
                {
                    if (mustRank.IndexOf(",9,") >= 0)
                    {
                        rank = 9;
                    }
                }
                else if (oldRank < 10 && rank > 10)
                {
                    if (mustRank.IndexOf(",10,") >= 0)
                    {
                        rank = 10;
                    }
                }
                else if (oldRank < 11 && rank > 11)
                {
                    if (mustRank.IndexOf(",11,") >= 0)
                    {
                        rank = 11;
                    }
                }
                else if (oldRank < 12 && rank > 12)
                {
                    if (mustRank.IndexOf(",12,") >= 0)
                    {
                        rank = 12;
                    }
                }
                else if (oldRank < 13 && rank > 13)
                {
                    if (mustRank.IndexOf(",13,") >= 0)
                    {
                        rank = 13;
                    }
                }

                if (rank > 13)
                {
                    rank -= 13;
                }
                else if (rank == 13)
                {
                    rank = 53;
                }

                
                mainForm.currentState.OpposedCurrentRank = rank;
                mainForm.currentRank = rank;
            }

        }


        //最后一把是否护住了底
        internal static bool IsMasterOK(MainForm mainForm, int who)
        {
            bool success = false;

            if (mainForm.currentState.Master == 1)
            {
                if ((who == 1) || (who == 2))
                {
                    success = true;
                }
            }
            else if (mainForm.currentState.Master == 2)
            {
                if ((who == 1) || (who == 2))
                {
                    success = true;
                    
                }
               
            }
            else if (mainForm.currentState.Master == 3)
            {
                if ((who == 3) || (who == 4))
                {
                    success = true;
                }
                
            }
            else if (mainForm.currentState.Master == 4)
            {
                if ((who == 3) || (who == 4))
                {
                    success = true;
                }
            }

            return success;
        }

        //是否成功
        internal static int CalculateNextMaster(MainForm mainForm,bool success)
        {
            int master = mainForm.currentState.Master;

            if (mainForm.currentState.Master == 1)
            {
                if (success)
                {
                    master = 2;
                }
                else
                {
                    master = 4;
                }
            }
            else if (mainForm.currentState.Master == 2)
            {
                if (success)
                {
                    master = 1;
                }
                else
                {
                    master = 3;
                }
            }
            else if (mainForm.currentState.Master == 3)
            {
                if (success)
                {
                    master = 4;
                }
                else
                {
                    master = 1;
                }
            }
            else if (mainForm.currentState.Master == 4)
            {
                if (success)
                {
                    master = 3;
                }
                else
                {
                    master = 2;
                }
            }

            return master;
        }

       
        internal static void GetNextMasterUser(MainForm mainForm)
        {
            

            //最后一把谁赢得
            int who = GetNextOrder(mainForm);
            //确定是否护住底
            bool lastMasterOk = IsMasterOK(mainForm,who);

            CurrentPoker CP = new CurrentPoker();
            CP.Suit = mainForm.currentState.Suit;
            CP.Rank = mainForm.currentRank;
            CP = CommonMethods.parse(mainForm.currentSendCards[who - 1],CP.Suit,CP.Rank);

           
            if (!lastMasterOk)
            {
                CalculateScore(mainForm);
                int howmany = 2;

                if (CP.HasTractors()) //TODO:可能是长拖拉机
                {
                    howmany = 8;
                }
                else if (CP.GetPairs().Count > 0)
                {
                    howmany = 4;
                }
                else
                {
                    howmany = 2;
                }

                //计算总得分
                Calculate8CardsScore(mainForm, howmany);
            }


            //已经计算本次的总得分

            //是否成功晋级,小于80分,成功晋级
            bool success = mainForm.Scores < 80;
            int oldMaster = mainForm.currentState.Master;

            int master = CalculateNextMaster(mainForm, success);

            mainForm.currentState.Master = master;

            GetNextRank(mainForm, success);

            //J到底,Q到半
            if (mainForm.gameConfig.JToBottom && (CP.Rank == 9) && (!success))
            {
                if (mainForm.currentSendCards[who - 1].Contains(9) || mainForm.currentSendCards[who - 1].Contains(22) || mainForm.currentSendCards[who - 1].Contains(35) || mainForm.currentSendCards[who - 1].Contains(48))
                {
                    if ((oldMaster == 1) || (oldMaster == 2))
                    {
                        mainForm.currentState.OurCurrentRank = 0;
                    }
                    if ((oldMaster == 3) || (oldMaster == 4))
                    {
                        mainForm.currentState.OpposedCurrentRank = 0;
                    }
                }
            }
            if (mainForm.gameConfig.QToHalf && (CP.Rank == 10) && (!success))
            {
                if (mainForm.currentSendCards[who - 1].Contains(10) || mainForm.currentSendCards[who - 1].Contains(23) || mainForm.currentSendCards[who - 1].Contains(36) || mainForm.currentSendCards[who - 1].Contains(49))
                {
                    if ((oldMaster == 1) || (oldMaster == 2))
                    {
                        mainForm.currentState.OurCurrentRank = 4;
                    }
                    if ((oldMaster == 3) || (oldMaster == 4))
                    {
                        mainForm.currentState.OpposedCurrentRank = 4;
                    }
                }
            }

            if (mainForm.gameConfig.AToJ && (CP.Rank == 12) && (!success))
            {
                if (mainForm.currentSendCards[who - 1].Contains(12) || mainForm.currentSendCards[who - 1].Contains(25) || mainForm.currentSendCards[who - 1].Contains(38) || mainForm.currentSendCards[who - 1].Contains(51))
                {
                    if ((oldMaster == 1) || (oldMaster == 2))
                    {
                        mainForm.currentState.OurCurrentRank = 9;
                    }
                    if ((oldMaster == 3) || (oldMaster == 4))
                    {
                        mainForm.currentState.OpposedCurrentRank = 9;
                    }
                }
            }
        }

       
        //确定下一次该谁出牌
        internal static int GetNextOrder(MainForm mainForm)
        {
            CurrentPoker[] cp = new CurrentPoker[4];
            int suit = mainForm.currentState.Suit;
            int rank = mainForm.currentRank;
            cp[0] = CommonMethods.parse(mainForm.currentSendCards[0], suit, rank);
            cp[1] = CommonMethods.parse(mainForm.currentSendCards[1], suit, rank);
            cp[2] = CommonMethods.parse(mainForm.currentSendCards[2], suit, rank);
            cp[3] = CommonMethods.parse(mainForm.currentSendCards[3], suit, rank);
            cp[0].Sort();
            cp[1].Sort();
            cp[2].Sort();
            cp[3].Sort();



            int count = mainForm.currentSendCards[0].Count;
            

            int order = mainForm.firstSend;

            int firstSuit = CommonMethods.GetSuit((int)mainForm.currentSendCards[order-1][0],suit,rank);



            int[] users = CommonMethods.OtherUsers(order);

            //如果是混合牌（甩牌或者多个对）,返回首家order
            if ((cp[order - 1].HasTractors()) && (cp[order - 1].Count > 4)) //有对有单张牌
            {
                int orderMax = cp[order - 1].GetTractor();
                if (cp[users[0] - 1].HasTractors() && (!cp[users[0] - 1].IsMixed()))
                {
                    int tmpMax = cp[users[0] - 1].GetTractor();
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[0];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[1] - 1].HasTractors() && (!cp[users[1] - 1].IsMixed()))
                {
                    int tmpMax = cp[users[1] - 1].GetTractor();
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[1];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[2] - 1].HasTractors() && (!cp[users[2] - 1].IsMixed()))
                {
                    int tmpMax = cp[users[2] - 1].GetTractor();
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[2];
                        orderMax = tmpMax;
                    }
                }
            }
            if ((cp[order -1].GetPairs().Count*2 < count) && (cp[order -1].GetPairs().Count>0)) //有对有单张牌
            {
                //如果有单个对
                int orderMax = (int)cp[order - 1].GetPairs()[0];
                if (cp[users[0] - 1].GetPairs().Count > 0 && (!cp[users[0] - 1].IsMixed()))
                {
                    int tmpMax = (int)cp[users[0] - 1].GetPairs()[0];
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[0];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[1] - 1].GetPairs().Count > 0 && (!cp[users[1] - 1].IsMixed()))
                {
                    int tmpMax = (int)cp[users[1] - 1].GetPairs()[0];
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[1];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[2] - 1].GetPairs().Count > 0 && (!cp[users[2] - 1].IsMixed()))
                {
                    int tmpMax = (int)cp[users[2] - 1].GetPairs()[0];
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[2];
                        orderMax = tmpMax;
                    }
                }

            }
            else if ((count> 1) && (cp[order -1].GetPairs().Count == 0)) //甩多个单张牌
            {
                int orderMax = (int)mainForm.currentSendCards[order - 1][0];
                int tmpMax = (int)mainForm.currentSendCards[users[0] - 1][0]; 
                if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                {
                    order = users[0];
                    orderMax = tmpMax;
                }

                tmpMax = (int)mainForm.currentSendCards[users[1] - 1][0]; 
                if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                {
                    order = users[1];
                    orderMax = tmpMax;
                }
                tmpMax = (int)mainForm.currentSendCards[users[2] - 1][0]; 
                if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                {
                    order = users[2];
                    orderMax = tmpMax;
                }
            }
            
            else if (cp[order - 1].HasTractors())
            {
                //如果有拖拉机
                int orderMax = cp[order - 1].GetTractor();
                if (cp[users[0] - 1].HasTractors())
                {
                    int tmpMax = cp[users[0] - 1].GetTractor();
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[0];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[1] - 1].HasTractors())
                {
                    int tmpMax = cp[users[1] - 1].GetTractor();
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[1];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[2] - 1].HasTractors())
                {
                    int tmpMax = cp[users[2] - 1].GetTractor();
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[2];
                        orderMax = tmpMax;
                    }
                }

                return order;
            }
            else if (cp[order - 1].GetPairs().Count == 1 && (count ==2))
            {
                //如果有单个对
                int orderMax = (int)cp[order - 1].GetPairs()[0];
                if (cp[users[0] - 1].GetPairs().Count>0)
                {
                    int tmpMax = (int)cp[users[0] - 1].GetPairs()[0];
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[0];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[1] - 1].GetPairs().Count>0)
                {
                    int tmpMax = (int)cp[users[1] - 1].GetPairs()[0];
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[1];
                        orderMax = tmpMax;
                    }
                }
                if (cp[users[2] - 1].GetPairs().Count>0)
                {
                    int tmpMax = (int)cp[users[2] - 1].GetPairs()[0];
                    if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                    {
                        order = users[2];
                        orderMax = tmpMax;
                    }
                }

                return order;
            }
            else if (count == 1)
            {
                //如果是单张牌
                int orderMax = (int)mainForm.currentSendCards[order - 1][0];
                int tmpMax = (int)mainForm.currentSendCards[users[0] - 1][0]; 
                if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                {
                    order = users[0];
                    orderMax = tmpMax;
                }

                tmpMax = (int)mainForm.currentSendCards[users[1] - 1][0]; 
                if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                {
                    order = users[1];
                    orderMax = tmpMax;
                }
                tmpMax = (int)mainForm.currentSendCards[users[2] - 1][0]; 
                if (!CommonMethods.CompareTo(orderMax, tmpMax, suit, rank, firstSuit))
                {
                    order = users[2];
                    orderMax = tmpMax;
                }

                return order;
            }

            return order;
        }


        //计算每次的得分
        internal static void CalculateScore(MainForm mainForm)
        {
            int score = 0;

            score += GetScores(mainForm.currentSendCards[0]);
            score += GetScores(mainForm.currentSendCards[1]);
            score += GetScores(mainForm.currentSendCards[2]);
            score += GetScores(mainForm.currentSendCards[3]);

            mainForm.Scores += score;

            //mainForm.Text = mainForm.Scores + "";
        }

        //得到底牌的分数
        internal static void Calculate8CardsScore(MainForm mainForm,int howmany)
        {
            int score = GetScores(mainForm.send8Cards);

            score = score * howmany;
            mainForm.Scores += score;
   
        }

        private static int GetScores(ArrayList list)
        {
            int number = 0;
            int score = 0;

            for (int i = 0; i < list.Count; i++)
            {
                number = (int)list[i] % 13;
                if (number == 3)
                {
                    score += 5;
                }
                else if (number == 8)
                {
                    score += 10;
                }
                else if (number == 11)
                {
                    score += 10;
                }
            }
            return score;
        }

        //玩家甩牌时的检查,如果所有的牌都是最大的，true
        internal static bool CheckSendCards(MainForm mainForm, ArrayList minCards,int who)
        {
            //ArrayList minCards = new ArrayList();
            int[] users = CommonMethods.OtherUsers(who);

            ArrayList list = new ArrayList();
            CurrentPoker cp = new CurrentPoker();
            int suit = mainForm.currentState.Suit;
            int rank = mainForm.currentRank;
            cp.Suit = suit;
            cp.Rank = rank;

            
            for (int i = 0; i < mainForm.myCardIsReady.Count; i++)
            {
                if ((bool)mainForm.myCardIsReady[i])
                {
                    list.Add(mainForm.myCardsNumber[i]);
                }
            }

            int firstSuit = CommonMethods.GetSuit((int)list[0],cp.Suit,cp.Rank);

            cp = CommonMethods.parse(list, cp.Suit, cp.Rank);
            cp.Sort();

           

            if (list.Count == 1) //如果是单张牌
            {
                return true;
            }
            else if (list.Count == 2 && (cp.GetPairs().Count == 1)) //如果是一对
            {
                return true;
            }
            else if (list.Count == 4 && (cp.HasTractors())) //如果是拖拉机
            {
                return true;
            }
            else //我甩混合牌时
            {
                if (cp.HasTractors())
                {
                    int myMax = cp.GetTractor();
                    int[] ttt = cp.GetTractorOtherCards(myMax);
                    cp.RemoveCard(myMax);
                    cp.RemoveCard(myMax);
                    cp.RemoveCard(ttt[1]);
                    cp.RemoveCard(ttt[1]);

                    int[] myMaxs = cp.GetTractorOtherCards(myMax);
                    int max4 = mainForm.currentPokers[users[0]].GetTractor(firstSuit);
                    int max2 = mainForm.currentPokers[users[1]].GetTractor(firstSuit);
                    int max3 = mainForm.currentPokers[users[2]].GetTractor(firstSuit);
                    if (!CommonMethods.CompareTo(myMax, max2, suit, rank, firstSuit))
                    {
                        minCards.Add(myMax);
                        minCards.Add(myMax);
                        minCards.Add(ttt[1]);
                        minCards.Add(ttt[1]);
                        return false;
                    }
                    else if (!CommonMethods.CompareTo(myMax, max3, suit, rank, firstSuit))
                    {
                        minCards.Add(myMax);
                        minCards.Add(myMax);
                        minCards.Add(ttt[1]);
                        minCards.Add(ttt[1]);
                        return false;
                    }
                    else if (!CommonMethods.CompareTo(myMax, max4, suit, rank, firstSuit))
                    {
                        minCards.Add(myMax);
                        minCards.Add(myMax);
                        minCards.Add(ttt[1]);
                        minCards.Add(ttt[1]);
                        return false;
                    }
                }

                if (cp.GetPairs().Count>0)
                {
                    ArrayList list0 = cp.GetPairs();

                    ArrayList list4 = mainForm.currentPokers[users[0]].GetPairs(firstSuit);
                    ArrayList list2 = mainForm.currentPokers[users[1]].GetPairs(firstSuit);
                    ArrayList list3 = mainForm.currentPokers[users[2]].GetPairs(firstSuit);

                    
                    int max4 = -1;
                    int max2 = -1;
                    int max3 = -1;
                    if (list4.Count > 0)
                    {
                        max4 = (int)list4[list4.Count - 1];
                    }
                    if (list3.Count > 0)
                    {
                        max3 = (int)list3[list3.Count - 1];
                    }

                    if (list2.Count > 0)
                    {
                        max2 = (int)list2[list2.Count - 1];
                    }

                    

                    for (int i = 0; i < list0.Count; i++)
                    {
                        int myMax = (int)list0[i];
                        cp.RemoveCard(myMax);
                        cp.RemoveCard(myMax);

                        if (!CommonMethods.CompareTo(myMax, max2, suit, rank, firstSuit) && max2 > -1)
                        {
                            minCards.Add(myMax);
                            minCards.Add(myMax);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(myMax, max3, suit, rank, firstSuit) && max3 > -1)
                        {
                            minCards.Add(myMax);
                            minCards.Add(myMax);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(myMax, max4, suit, rank, firstSuit) && max4 > -1)
                        {
                            minCards.Add(myMax);
                            minCards.Add(myMax);
                            return false;
                        }
                    }

                }

                //依次检查每张牌是否是最大。
                int[] cards = cp.GetCards();
                int mmax4 = mainForm.currentPokers[users[0]].GetMaxCard(firstSuit);
                int mmax2 = mainForm.currentPokers[users[1]].GetMaxCard(firstSuit);
                int mmax3 = mainForm.currentPokers[users[2]].GetMaxCard(firstSuit);
                for (int i = 0; i < 54; i++)
                {
                    if (cards[i] == 1)
                    {
                        if (!CommonMethods.CompareTo(i, mmax2, suit, rank, firstSuit))
                        {
                            minCards.Add(i);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(i, mmax3, suit, rank, firstSuit))
                        {
                            minCards.Add(i);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(i, mmax4, suit, rank, firstSuit))
                        {
                            minCards.Add(i);
                            return false;
                        }
                    }
                }
            }

            return true;
        }


        internal static bool CheckSendCards(MainForm mainForm, ArrayList sendCards,ArrayList minCards, int who)
        {
            //ArrayList minCards = new ArrayList();
            int[] users = CommonMethods.OtherUsers(who);

            ArrayList list = new ArrayList();
            CurrentPoker cp = new CurrentPoker();
            int suit = mainForm.currentState.Suit;
            int rank = mainForm.currentRank;
            cp.Suit = suit;
            cp.Rank = rank;


            for (int i = 0; i < sendCards.Count; i++)
            {
                 list.Add(sendCards[i]);
                
            }

            int firstSuit = CommonMethods.GetSuit((int)list[0], cp.Suit, cp.Rank);

            cp = CommonMethods.parse(list, cp.Suit, cp.Rank);
            cp.Sort();



            if (list.Count == 1) //如果是单张牌
            {
                return true;
            }
            else if (list.Count == 2 && (cp.GetPairs().Count == 1)) //如果是一对
            {
                return true;
            }
            else if (list.Count == 4 && (cp.HasTractors())) //如果是拖拉机
            {
                return true;
            }
            else //我甩混合牌时
            {
                if (cp.HasTractors())
                {
                    int myMax = cp.GetTractor();
                    int[] ttt = cp.GetTractorOtherCards(myMax);
                    cp.RemoveCard(myMax);
                    cp.RemoveCard(myMax);
                    cp.RemoveCard(ttt[1]);
                    cp.RemoveCard(ttt[1]);

                    int[] myMaxs = cp.GetTractorOtherCards(myMax);
                    int max4 = mainForm.currentPokers[users[0]].GetTractor(firstSuit);
                    int max2 = mainForm.currentPokers[users[1]].GetTractor(firstSuit);
                    int max3 = mainForm.currentPokers[users[2]].GetTractor(firstSuit);
                    if (!CommonMethods.CompareTo(myMax, max2, suit, rank, firstSuit))
                    {
                        minCards.Add(myMax);
                        minCards.Add(myMax);
                        minCards.Add(ttt[1]);
                        minCards.Add(ttt[1]);
                        return false;
                    }
                    else if (!CommonMethods.CompareTo(myMax, max3, suit, rank, firstSuit))
                    {
                        minCards.Add(myMax);
                        minCards.Add(myMax);
                        minCards.Add(ttt[1]);
                        minCards.Add(ttt[1]);
                        return false;
                    }
                    else if (!CommonMethods.CompareTo(myMax, max4, suit, rank, firstSuit))
                    {
                        minCards.Add(myMax);
                        minCards.Add(myMax);
                        minCards.Add(ttt[1]);
                        minCards.Add(ttt[1]);
                        return false;
                    }
                }

                if (cp.GetPairs().Count > 0)
                {
                    ArrayList list0 = cp.GetPairs();

                    ArrayList list4 = mainForm.currentPokers[users[0]].GetPairs(firstSuit);
                    ArrayList list2 = mainForm.currentPokers[users[1]].GetPairs(firstSuit);
                    ArrayList list3 = mainForm.currentPokers[users[2]].GetPairs(firstSuit);


                    int max4 = -1;
                    int max2 = -1;
                    int max3 = -1;
                    if (list4.Count > 0)
                    {
                        max4 = (int)list4[list4.Count - 1];
                    }
                    if (list3.Count > 0)
                    {
                        max3 = (int)list3[list3.Count - 1];
                    }

                    if (list2.Count > 0)
                    {
                        max2 = (int)list2[list2.Count - 1];
                    }



                    for (int i = 0; i < list0.Count; i++)
                    {
                        int myMax = (int)list0[i];
                        cp.RemoveCard(myMax);
                        cp.RemoveCard(myMax);

                        if (!CommonMethods.CompareTo(myMax, max2, suit, rank, firstSuit) && max2 > -1)
                        {
                            minCards.Add(myMax);
                            minCards.Add(myMax);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(myMax, max3, suit, rank, firstSuit) && max3 > -1)
                        {
                            minCards.Add(myMax);
                            minCards.Add(myMax);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(myMax, max4, suit, rank, firstSuit) && max4 > -1)
                        {
                            minCards.Add(myMax);
                            minCards.Add(myMax);
                            return false;
                        }
                    }

                }

                //依次检查每张牌是否是最大。
                int[] cards = cp.GetCards();
                int mmax4 = mainForm.currentPokers[users[0]].GetMaxCard(firstSuit);
                int mmax2 = mainForm.currentPokers[users[1]].GetMaxCard(firstSuit);
                int mmax3 = mainForm.currentPokers[users[2]].GetMaxCard(firstSuit);
                for (int i = 0; i < 54; i++)
                {
                    if (cards[i] == 1)
                    {
                        if (!CommonMethods.CompareTo(i, mmax2, suit, rank, firstSuit))
                        {
                            minCards.Add(i);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(i, mmax3, suit, rank, firstSuit))
                        {
                            minCards.Add(i);
                            return false;
                        }
                        else if (!CommonMethods.CompareTo(i, mmax4, suit, rank, firstSuit))
                        {
                            minCards.Add(i);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

    }
}
