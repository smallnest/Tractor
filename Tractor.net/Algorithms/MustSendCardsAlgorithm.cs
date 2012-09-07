using System;
using System.Collections;
using System.Text;

namespace Kuaff.Tractor
{
    /// <summary>
    /// 如何跟牌的算法
    /// </summary>
    class MustSendCardsAlgorithm
    {
        /// <summary>
        /// 跟牌基本算法
        /// </summary>
        /// <param name="mainForm">主窗体</param>
        /// <param name="currentPokers">当前各家手中的扑克</param>
        /// <param name="whoseOrder">该谁出牌</param>
        /// <param name="sendedCards">whoseOrder应该出的牌</param>
        /// <param name="count">出牌数量</param>
        internal static void MustSendCards(MainForm mainForm, CurrentPoker[] currentPokers, int whoseOrder, ArrayList sendedCards, int count)
        {
            //当前的花色和Rank
            int suit = mainForm.currentState.Suit;
            int rank = mainForm.currentRank;

            //本次花色
            int firstSuit = CommonMethods.GetSuit((int)mainForm.currentSendCards[mainForm.firstSend-1][0],suit,rank);

            int sendTotal = mainForm.currentSendCards[0].Count + mainForm.currentSendCards[1].Count + mainForm.currentSendCards[2].Count + mainForm.currentSendCards[3].Count;

            if (sendTotal == count) //whoseOrder是第二个出牌
            {
                WhoseOrderIs2(mainForm, currentPokers, whoseOrder, sendedCards, count, suit, rank, firstSuit);
                
            }
            else if (sendTotal == count*2) //whoseOrder是第三个出牌
            {
                WhoseOrderIs3(mainForm, currentPokers, whoseOrder, sendedCards, count, suit, rank, firstSuit);
                
            }
            else if (sendTotal == count * 3) //whoseOrder是第四个出牌
            {
                WhoseOrderIs4(mainForm, currentPokers, whoseOrder, sendedCards, count, suit, rank, firstSuit);
               
            }
        }

        //whoseOrder是第二个出牌
        internal static void WhoseOrderIs2(MainForm mainForm, CurrentPoker[] currentPokers, int whoseOrder, ArrayList sendedCards, int count, int suit, int rank, int firstSuit)
        {
            ArrayList firstSendCards = mainForm.currentSendCards[mainForm.firstSend-1]; //首家出牌
            CurrentPoker firstCP = new CurrentPoker();
            firstCP.Suit = suit;
            firstCP.Rank = rank;
            firstCP = CommonMethods.parse(firstSendCards, suit, rank);
            
            int firstMax = CommonMethods.GetMaxCard(firstSendCards, suit, rank); //首家的最大牌
            int pairTotal = firstCP.GetPairs().Count;

            CurrentPoker myCP = currentPokers[whoseOrder - 1];
            ArrayList myPokerList = mainForm.pokerList[whoseOrder - 1];


            //whose的此花色的牌数
            int myTotal = CommonMethods.GetSuitCount(currentPokers[whoseOrder - 1], suit, rank, firstSuit); 
            //此花色的牌
            int[] cards = myCP.GetSuitCards(firstSuit);

            

            ArrayList myList = new ArrayList(cards);
            CurrentPoker mySuitCP = new CurrentPoker(); //我此花色的牌
            mySuitCP.Suit = suit;
            mySuitCP.Rank = rank;
            mySuitCP = CommonMethods.parse(myList,suit,rank);
            mySuitCP.Sort();

            firstCP.Sort();

            myCP.Sort();


           //考虑毕
            if (myTotal == 0)
            {
                if (firstSuit != suit)
                {

                    if (myCP.GetMasterCardsTotal() >= count && count == 1) //单张牌
                    {
                        //如果目前最大的那一家是主 
                        int biggerMax = (int)mainForm.currentSendCards[mainForm.whoIsBigger - 1][0];
                        int maxMaster = myCP.GetMaxMasterCards();
                        //如果我的牌能大过最大的那家的牌
                        if (!CommonMethods.CompareTo(biggerMax, maxMaster, suit, rank, firstSuit))
                        {
                            mainForm.whoIsBigger = whoseOrder;
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, maxMaster);
                            return;
                        }
                    }
                    else if (myCP.GetMasterCardsTotal() >= count && pairTotal == 1 && count == 2) //出一个对时
                    {
                        //如果目前最大的那一家是主 
                        int biggerMax = (int)mainForm.currentSendCards[mainForm.whoIsBigger - 1][0];
                        ArrayList masterPairs  = myCP.GetMasterPairs();
                        //如果我的牌能大过最大的那家的牌
                        if (masterPairs.Count > 0)
                        {
                            mainForm.whoIsBigger = whoseOrder;
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                            return;
                        }
                    }
                    else if (myCP.GetMasterCardsTotal() >= count && pairTotal == 0 && count > 1) //单张甩牌
                    {
                        //如果目前最大的那一家是主 
                        int biggerMax = (int)mainForm.currentSendCards[mainForm.whoIsBigger - 1][0];
                        int maxMaster = myCP.GetMaxMasterCards();
                        //如果我的牌能大过最大的那家的牌
                        if (!CommonMethods.CompareTo(biggerMax, maxMaster, suit, rank, firstSuit))
                        {
                            mainForm.whoIsBigger = whoseOrder;
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, maxMaster);

                            SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, true);
                            SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true);
                            SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, false);
                            SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false);

                            return;
                        }

                    }
                }
            }

            if (myTotal < count) //本花色少牌
            {
               
                for (int i = 0; i < myTotal; i++)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, cards[i]);
                }

                SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList,true);
                SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList,true);
                SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList,true);
                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList,true);
                SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, false);
                SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false);
                SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList, false);
                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false);

               
                return;

            }
          
            //以下确保此花色的牌绝对够用
            else if (firstCP.HasTractors())  //如果首家出了拖拉机
            {
                //如果我有拖拉机，出最大的拖拉机
                if (mySuitCP.HasTractors())
                {
                    int k = mySuitCP.GetTractor();
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, k);
                    int[] ks = mySuitCP.GetTractorOtherCards(k);
                    for (int i = 0; i < 3; i++)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, ks[i]);
                    }

                    if (!CommonMethods.CompareTo(firstCP.GetTractor(), k, suit, rank, firstSuit)) //如果我的拖拉机的牌大
                    {
                        mainForm.whoIsBigger = whoseOrder;
                    }
                }
                else if (mySuitCP.GetPairs().Count > 0) //如果有对，出两个对
                {
                    ArrayList list = mySuitCP.GetPairs();
                    if (list.Count >= 2)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[1]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[1]);
                    }
                    else
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                    }

                }


                //否则出最小的牌
                SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true);
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);

                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                
                return;
            }
            else if (count == 1) //首家出单张牌 
            {
                int myMax = -1;
                if (firstSuit == suit)
                {
                    myMax = mySuitCP.GetMaxMasterCards();
                }
                else
                {
                    myMax = mySuitCP.GetMaxCards(firstSuit);
                }

               

                //如果得到的此花色的最大的牌大于首家的牌
                if (!CommonMethods.CompareTo(firstMax,myMax,suit,rank,firstSuit))
                {
                    if (myMax > -1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myMax);

                        mainForm.whoIsBigger = whoseOrder;
                        
                        return;
                    }
                }

                


                SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true);
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);

                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

               
                return;
            }
            else if ((pairTotal == 1) && (count == 2)) //首家出了一个对
            {
                ArrayList list = mySuitCP.GetPairs();
                if (list.Count >= 1)
                {
                    int myMax = (int)list[list.Count - 1];

                    //如果得到的此花色的最大的牌大于首家的牌
                    if (!CommonMethods.CompareTo(firstMax, myMax, suit, rank, firstSuit))
                    {
                        mainForm.whoIsBigger = whoseOrder;
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                        
                        return;
                    }
                    else
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);

                        
                        return;
                    }
                }
                else
                {
                    //否则出最小的牌
                    SendThisSuitNoScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                    SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);

                    SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                    
                    return;
                }

            }
            else if (count == pairTotal * 2 && (count>0)) //都是对
            {
                ArrayList list = mySuitCP.GetPairs();
                for (int i = 0; i < pairTotal && i < list.Count;i++ )
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                }

                //否则出最小的牌
                SendThisSuitNoScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);

                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                
                return;

            }
            else //有对和有单张牌，是甩牌
            {
                ArrayList list = mySuitCP.GetPairs();
                for (int i = 0; i < pairTotal && i < list.Count; i++)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                }

                //否则出最小的牌
                SendThisSuitNoScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);

                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

               
                return;
            }


        }

        private static void SendOtherSuitNoScores(ArrayList sendedCards, int count, int firstSuit, CurrentPoker myCP, ArrayList myPokerList,bool protectPairs)
        {


            for (int asuit = 1; asuit < 5; asuit++)
            {
                if (asuit == firstSuit)
                {
                    continue;
                }
                if (asuit == myCP.Suit)
                {
                    continue;
                }


                int[] cards = myCP.GetSuitAllCards(asuit);

                for (int m = 0; m < 13; m++)
                {
                    if (m == 3 || m == 8 || m ==11)
                    {
                        continue;
                    }

                    if (cards[m] > 0)
                    {
                        if (sendedCards.Count < count)
                        {
                            if (protectPairs) //保护对
                            {
                                if (cards[m] == 1)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                            else
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                        }
                        if (sendedCards.Count >= count)
                        {
                            return;
                        }
                    }
                }

            }



               
        }

        private static void SendOtherSuitOrScores(ArrayList sendedCards, int count, int firstSuit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {
            for (int asuit = 1; asuit < 5; asuit++)
            {
                if (asuit == firstSuit)
                {
                    continue;
                }
                if (asuit == myCP.Suit)
                {
                    continue;
                }


                int[] cards = myCP.GetSuitAllCards(asuit);

                int n = 8;
                if (cards[n] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cards[n] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                                }
                            }
                        }
                    }
                }
                n = 11;

                if (cards[n] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cards[n] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                                }
                            }
                        }
                    }
                }


                n = 5;

                if (cards[n] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cards[n] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            }
                        }
                    }
                }

                for (int m = 0; m < 13; m++)
                {
                    if (m == 3 || m == 8 || m == 11)
                    {
                        continue;
                    }

                    if (cards[m] > 0)
                    {
                        if (sendedCards.Count < count)
                        {
                            if (protectPairs) //保护对
                            {
                                if (cards[m] == 1)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                            else
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                        }
                        if (sendedCards.Count >= count)
                        {
                            return;
                        }
                    }
                }

            }


        }

        private static void SendOtherSuit(ArrayList sendedCards, int count, int firstSuit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {
            for (int asuit = 1; asuit < 5; asuit++)
            {
                if (asuit == firstSuit)
                {
                    continue;
                }
                if (asuit == myCP.Suit)
                {
                    continue;
                }


                int[] cards = myCP.GetSuitAllCards(asuit);

                for (int m = 0; m < 13; m++)
                {
                    if (cards[m] > 0)
                    {
                        if (sendedCards.Count < count)
                        {
                            if (protectPairs) //保护对
                            {
                                if (cards[m] == 1)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                            else
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                        }
                        if (sendedCards.Count >= count)
                        {
                            return;
                        }
                    }
                }

            }


        }


        private static void SendThisSuitNoScores(ArrayList sendedCards, int count, int suit, int firstSuit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {
            if (suit == firstSuit)
            {
                SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList, protectPairs);
                return;
            }

            int[] cards = myCP.GetSuitAllCards(firstSuit);

            for (int m = 0; m < 13; m++)
            {
                if (m == 3 || m == 8 || m == 11)
                {
                    continue;
                }

                if (cards[m] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cards[m] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            }
                        }

                    }
                    if (sendedCards.Count >= count)
                    {
                        return;
                    }
                }
            }

        }

        private static void SendThisSuitOrScores(ArrayList sendedCards, int count, int suit, int firstSuit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {
            if (suit == firstSuit)
            {
                SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList,protectPairs);
                return;
            }

            int[] cards = myCP.GetSuitAllCards(firstSuit);

            int n = 8;
            if (cards[n] > 0)
            {
                if (sendedCards.Count < count)
                {
                    if (protectPairs) //保护对
                    {
                        if (cards[n] == 1)
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        }
                    }
                    else
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        if (sendedCards.Count < count)
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        }
                    }

                }
                if (sendedCards.Count >= count)
                {
                    return;
                }
            }

            n = 11;
            if (cards[n] > 0)
            {
                if (sendedCards.Count < count)
                {
                    if (protectPairs) //保护对
                    {
                        if (cards[n] == 1)
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        }
                    }
                    else
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        if (sendedCards.Count < count)
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        }
                    }

                }
                if (sendedCards.Count >= count)
                {
                    return;
                }
            }
            n = 3;
            if (cards[n] > 0)
            {
                if (sendedCards.Count < count)
                {
                    if (protectPairs) //保护对
                    {
                        if (cards[n] == 1)
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        }
                    }
                    else
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        if (sendedCards.Count < count)
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (firstSuit - 1) * 13);
                        }
                    }

                }
                if (sendedCards.Count >= count)
                {
                    return;
                }
            }



            for (int m = 0; m < 13; m++)
            {
               
                if (cards[m] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cards[m] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            }
                        }

                    }
                    if (sendedCards.Count >= count)
                    {
                        return;
                    }
                }
            }

        }

        private static void SendThisSuit(ArrayList sendedCards, int count, int suit, int firstSuit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {
            if (suit == firstSuit)
            {
                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList,protectPairs);
                return;
            }

            int[] cards = myCP.GetSuitAllCards(firstSuit);

            for (int m = 0; m < 13; m++)
            {
               
                if (cards[m] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cards[m] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (firstSuit - 1) * 13);
                            }
                        }

                    }
                    if (sendedCards.Count >= count)
                    {
                        return;
                    }
                }
            }

        }


        private static void SendMasterSuitNoScores(ArrayList sendedCards, int count, int suit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {

            if (sendedCards.Count >= count)
            {
                return;
            }

            for (int asuit = 1; asuit < 5; asuit++)
            {
                if (asuit != suit)
                {
                    continue;
                }

                int[] cardsCount = myCP.GetSuitAllCards(asuit);


                for (int m = 0; m < 13; m++)
                {
                    if (m == 3 || m == 8 || m == 11)
                    {
                        continue;
                    }

                    if (cardsCount[m] > 0)
                    {
                        if (sendedCards.Count < count)
                        {
                            if (protectPairs) //保护对
                            {
                                if (cardsCount[m] == 1)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (suit - 1) * 13);
                                }
                            }
                            else
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                        }
                        if (sendedCards.Count >= count)
                        {
                            return;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }



            if (protectPairs) //保护对
            {
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
            }
            else
            {
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
            }

            //
            if (myCP.SmallJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 52);

                }
            }
            if (myCP.SmallJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 52);

                }
            }
            if (myCP.BigJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 53);

                }
            }
            if (myCP.BigJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 53);

                }
            }
        }

        private static void SendMasterSuitOrScores(ArrayList sendedCards, int count, int suit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {
            if (sendedCards.Count >= count)
            {
                return;
            }

            for (int asuit = 1; asuit < 5; asuit++)
            {
                if (asuit != suit)
                {
                    continue;
                }

                int[] cardsCount = myCP.GetSuitAllCards(asuit);

                

                int n = 8;

                if (myCP.Rank == 8)
                {
                    n = 11; //打10时，先出K
                }

                if (cardsCount[n] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cardsCount[n] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (suit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            }
                        }
                    }
                    if (sendedCards.Count >= count)
                    {
                        return;
                    }
                }
                else
                {
                    continue;
                }

                n = 11;

                if (myCP.Rank == 8)
                {
                    n = 8; //打10时，后出10
                }

                if (cardsCount[n] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cardsCount[n] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (suit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            }
                        }
                    }
                    if (sendedCards.Count >= count)
                    {
                        return;
                    }
                }
                else
                {
                    continue;
                }

                n = 3;

                if (cardsCount[n] > 0)
                {
                    if (sendedCards.Count < count)
                    {
                        if (protectPairs) //保护对
                        {
                            if (cardsCount[n] == 1)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (suit - 1) * 13);
                            }
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            if (sendedCards.Count < count)
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, n + (asuit - 1) * 13);
                            }
                        }
                    }
                    if (sendedCards.Count >= count)
                    {
                        return;
                    }
                }
                else
                {
                    continue;
                }
            }
            

            //非分
            for (int asuit = 1; asuit < 5; asuit++)
            {
                if (asuit != suit)
                {
                    continue;
                }

                int[] cardsCount = myCP.GetSuitAllCards(asuit);


                for (int m = 0; m < 13; m++)
                {
                    

                    if (cardsCount[m] > 0)
                    {
                        if (sendedCards.Count < count)
                        {
                            if (protectPairs) //保护对
                            {
                                if (cardsCount[m] == 1)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (suit - 1) * 13);
                                }
                            }
                            else
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                        }
                        if (sendedCards.Count >= count)
                        {
                            return;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            //Rank
            if (protectPairs) //保护对
            {
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
            }
            else
            {
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
            }
           
            //
            if (myCP.SmallJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 52);

                }
            }
            if (myCP.SmallJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 52);

                }
            }
            if (myCP.BigJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 53);

                }
            }
            if (myCP.BigJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 53);

                }
            }
        }

        private static void SendMasterSuit(ArrayList sendedCards, int count, int suit, CurrentPoker myCP, ArrayList myPokerList, bool protectPairs)
        {
            if (sendedCards.Count >= count)
            {
                return;
            }

            for (int asuit = 1; asuit < 5; asuit++)
            {
                if (asuit != suit)
                {
                    continue;
                }

                int[] cardsCount = myCP.GetSuitAllCards(asuit);


                for (int m = 0; m < 13; m++)
                {
                   

                    if (cardsCount[m] > 0)
                    {
                        if (sendedCards.Count < count)
                        {
                            if (protectPairs) //保护对
                            {
                                if (cardsCount[m] == 1)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (suit - 1) * 13);
                                }
                            }
                            else
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                if (sendedCards.Count < count)
                                {
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, m + (asuit - 1) * 13);
                                }
                            }
                        }
                        if (sendedCards.Count >= count)
                        {
                            return;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }



            if (protectPairs) //保护对
            {
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
            }
            else
            {
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.HeartsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.PeachsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 13);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.DiamondsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 26);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
                if (sendedCards.Count < count)
                {
                    if (myCP.ClubsRankTotal > 0)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myCP.Rank + 39);
                    }
                }
            }
            
            //
            if (myCP.SmallJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 52);

                }
            }
            if (myCP.SmallJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 52);

                }
            }
            if (myCP.BigJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 53);

                }
            }
            if (myCP.BigJack > 0)
            {
                if (sendedCards.Count < count)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, 53);

                }
            }
        }


        //whoseOrder是第三个出牌
        internal static void WhoseOrderIs3(MainForm mainForm, CurrentPoker[] currentPokers, int whoseOrder, ArrayList sendedCards, int count, int suit, int rank, int firstSuit)
        {
            ArrayList firstSendCards = mainForm.currentSendCards[mainForm.firstSend - 1]; //首家出的牌
            CurrentPoker firstCP = new CurrentPoker();
            firstCP.Suit = suit;
            firstCP.Rank = rank;
            firstCP = CommonMethods.parse(firstSendCards, suit, rank);

            int firstMax = CommonMethods.GetMaxCard(firstSendCards, suit, rank); //首家出的最大的牌
            int pairTotal = firstCP.GetPairs().Count; //首家出的对的数目

            CurrentPoker myCP = currentPokers[whoseOrder - 1]; //我手中的牌
            ArrayList myPokerList = mainForm.pokerList[whoseOrder - 1];


            //whose的此花色的牌数
            int myTotal = CommonMethods.GetSuitCount(currentPokers[whoseOrder - 1], suit, rank, firstSuit);
            //此花色的牌
            int[] cards = myCP.GetSuitCards(firstSuit);

           
            
            ArrayList myList = new ArrayList(cards);
            CurrentPoker mySuitCP = new CurrentPoker(); //我此花色的牌
            mySuitCP.Suit = suit;
            mySuitCP.Rank = rank;
            mySuitCP = CommonMethods.parse(myList, suit, rank);
            mySuitCP.Sort();

            firstCP.Sort();
            myCP.Sort();

            int[] users = CommonMethods.OtherUsers(mainForm.firstSend);

            CurrentPoker secondCP = new CurrentPoker(); //第二家出的牌
            secondCP.Suit = suit;
            secondCP.Rank = rank;
            secondCP = CommonMethods.parse(mainForm.currentSendCards[users[0]-1],suit,rank);

            //
            
            //考虑是否毕
            //将来考虑首家出的牌的大小，目前是能毕则毕
            if (myTotal == 0) 
            {
                if (firstSuit != suit)
                {
                    //如果目前最大的那一家是主 
                    int biggerMax = (int)mainForm.currentSendCards[mainForm.whoIsBigger - 1][0];
                    int[] tmpUsers = CommonMethods.OtherUsers(whoseOrder);

                    if (myCP.GetMasterCardsTotal() >= count &&  (mainForm.whoIsBigger == tmpUsers[1]) && ((biggerMax % 13) > 8))
                    {
                        //不毕，但是有可能贴的副牌也比大的那一家大
                        SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色非分牌
                        SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色分牌
                        SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色非分牌
                        SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌


                        int sendOtherSuitsTotal = sendedCards.Count; //没有副牌可贴，只能出主

                        if (firstCP.HasTractors() && sendOtherSuitsTotal == 0) //单张牌
                        {
                            int minMaster = myCP.GetMasterTractor();
                            int tmpFirstTractor = firstCP.GetTractor();

                            //如果我的牌能大过最大的那家的牌

                            if ((!CommonMethods.CompareTo(tmpFirstTractor, minMaster, suit, rank, firstSuit)) && (minMaster > -1))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, minMaster);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, minMaster);
                                int[] ttt = myCP.GetTractorOtherCards(minMaster);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, ttt[1]);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, ttt[1]);

                                return;
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count && count == 1 && sendOtherSuitsTotal ==0) //单张牌
                        {
                            int maxMaster = myCP.GetMaxMasterCards();
                            //如果我的牌能大过最大的那家的牌
                            if (!CommonMethods.CompareTo(biggerMax, maxMaster, suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, maxMaster);
                                return;
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 1 && count == 2 && sendOtherSuitsTotal == 0) //出一个对时
                        {

                            ArrayList masterPairs = myCP.GetMasterPairs();
                            //如果我的牌能大过最大的那家的牌
                            if (masterPairs.Count > 0)
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                return;
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 0 && count > 1 && sendOtherSuitsTotal == 0) //单张甩牌
                        {

                            int maxMaster = myCP.GetMaxMasterCards();
                            //如果我的牌能大过最大的那家的牌
                            if (!CommonMethods.CompareTo(biggerMax, maxMaster, suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, maxMaster);

                                SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, true);
                                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true);
                                SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, false);
                                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false);

                                return;
                            }

                        }

                        SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList, true); //主牌非分牌
                        SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true); //主牌分牌
                        SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList, false); //主牌非分牌
                        SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false); //主牌分牌

                    }
                    else
                    {
                        if (firstCP.HasTractors()) //出一个对时
                        {
                            SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色非分牌
                            SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色分牌
                            SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其它花色非分牌
                            SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌
                        }
                        else if (myCP.GetMasterCardsTotal() >= count && count == 1) //单张牌
                        {
                            
                            int maxMaster = myCP.GetMaxMasterCards();
                            //如果我的牌能大过最大的那家的牌
                            if (!CommonMethods.CompareTo(biggerMax, maxMaster, suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, maxMaster);
                                return;
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 1 && count == 2) //出一个对时
                        {
                           
                            ArrayList masterPairs = myCP.GetMasterPairs();
                            //如果我的牌能大过最大的那家的牌
                            if (masterPairs.Count > 0)
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                return;
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 0 && count > 1) //单张甩牌
                        {
                           
                            int maxMaster = myCP.GetMaxMasterCards();
                            //如果我的牌能大过最大的那家的牌
                            if (!CommonMethods.CompareTo(biggerMax, maxMaster, suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, maxMaster);

                                SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, true);
                                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true);
                                SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, false);
                                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false);

                                return;
                            }

                        }
                    }
                }
            }
            if (myTotal < count) //本花色少牌
            {

                for (int i = 0; i < myTotal; i++)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, cards[i]);
                }

                SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList,true); //其他花色非分牌
                SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList,true); //其他花色分牌
                SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList,true); //主牌非分牌
                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList,true); //主牌分牌


                SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色非分牌
                SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌
                SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList, false); //主牌非分牌
                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false); //主牌分牌

              

                return;

            }
            
            else if (firstCP.HasTractors())  //如果首家出了拖拉机
            {
                //如果我有拖拉机，出最大的拖拉机
                if (mySuitCP.HasTractors())
                {
                    int k = mySuitCP.GetTractor();
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, k);
                    int[] ks = mySuitCP.GetTractorOtherCards(k);
                    for (int i = 0; i < 3; i++)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, ks[i]);
                    }


                    CurrentPoker tmpCP = CommonMethods.parse(mainForm.currentSendCards[mainForm.whoIsBigger - 1], suit, rank);
                    int tmp = tmpCP.GetTractor();
                    if (!CommonMethods.CompareTo(tmp, k, suit, rank, firstSuit))
                    {
                        mainForm.whoIsBigger = whoseOrder;
                    }
                    
                }
                else if (mySuitCP.GetPairs().Count > 0) //如果我有对，出两个对
                {
                    ArrayList list = mySuitCP.GetPairs();
                    if (list.Count >= 2) //超过两个对
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[1]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[1]);
                    }
                    else
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                    }


                }

                //否则出最小的牌
                if (mainForm.whoIsBigger == users[1])
                {
                    SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, true);
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, true);

                    SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                }
                else
                {
                    SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, true);
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, true);

                    SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                }

               
                return;

            }
            else if (count == 1) //首家出了单张牌 
            {
                int myMax = -1;  //我的此花色的最大值
                if (firstSuit == suit)
                {
                    myMax = mySuitCP.GetMaxMasterCards();
                }
                else
                {
                    myMax = mySuitCP.GetMaxCards(firstSuit);
                }

                //第二家最大牌
                int max2 = CommonMethods.GetMaxCard(mainForm.currentSendCards[users[0]-1], suit, rank);

               

                //首家大于第二家
                if (CommonMethods.CompareTo(firstMax, max2, suit, rank,firstSuit))
                {
                    //如果第四家有比首家大的牌，我应该管住
                    int[] fourthCards = mainForm.currentPokers[users[2] - 1].GetSuitCards(firstSuit);
                    if (fourthCards.Length>0)
                    {
                        int fourthMax = fourthCards[fourthCards.Length -1];
                        if (!CommonMethods.CompareTo(firstMax, fourthMax, suit, rank, firstSuit))
                        {
                            
                            //第四家最大，我应该出最大的非分牌
                            //如果我有比第四家大的牌
                            if (CommonMethods.CompareTo(myMax, fourthMax, suit, rank, firstSuit))
                            {
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, myMax);
                            }
                            else //我也管不住
                            {
                                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, true);
                                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, true);

                                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                            }

                        }
                        else
                        {
                            SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList,true);
                            SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                        }
                    }
                    else
                    {
                        SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList,true); //出分牌或者最小的牌
                        SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList,false); //出分牌或者最小的牌
                    }

                    if ((!CommonMethods.CompareTo(firstMax, (int)sendedCards[0], suit, rank, firstSuit)) && (!CommonMethods.CompareTo(max2, (int)sendedCards[0], suit, rank, firstSuit)))
                    {
                        mainForm.whoIsBigger = whoseOrder;
                    }

                }
                else if (!CommonMethods.CompareTo(max2, myMax, suit, rank, firstSuit)) //首家最小，我的最大
                {
                    //出大牌
                    if (myMax > -1)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myMax);
                        mainForm.whoIsBigger = whoseOrder;


                        return;
                    }
                }



                SendThisSuitNoScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);

                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                return;

            }
            else if ((pairTotal == 1) && (count == 2)) //首家出了一个对
            {
                ArrayList list = mySuitCP.GetPairs();
                if (list.Count >= 1 && (secondCP.GetPairs().Count < 1)) //我们有对，第二家无对
                {
                    if (!CommonMethods.CompareTo((int)mainForm.currentSendCards[mainForm.firstSend-1][0],(int)list[0],suit,rank,firstSuit))
                    {
                        mainForm.whoIsBigger = whoseOrder;
                    }
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);

                   
                    return;
                    
                }
                else if (list.Count >= 1 && (secondCP.GetPairs().Count >= 1)) //我们都有对
                {
                    int myMax = (int)list[list.Count - 1];
                   
                    int max2 = (int)secondCP.GetPairs()[0];

                    //如果我的的牌大于第二家的牌
                    if (!CommonMethods.CompareTo(max2, myMax, suit, rank,firstSuit))
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                        if (!CommonMethods.CompareTo((int)mainForm.currentSendCards[mainForm.firstSend - 1][0], (int)list[0], suit, rank, firstSuit))
                        {
                            mainForm.whoIsBigger = whoseOrder;
                        }
                        
                        return;
                    }
                    else //否则
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);

                     
                        return;
                    }
                }
                else if (list.Count < 1 && secondCP.GetPairs().Count >= 1) //如果第二家也出了对,我无对
                {
                    int max2 = (int)secondCP.GetPairs()[0];
                    //首家大
                    if (CommonMethods.CompareTo(firstMax, max2, suit, rank, firstSuit))
                    {
                        SendThisSuitOrScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                        SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                        SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                        SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                        
                        return;
                    }
                    else
                    {
                        SendThisSuitNoScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                        SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                        SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                        SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                       
                        return;
                    }
                }
                else if (list.Count < 1 && secondCP.GetPairs().Count < 1)
                {
                    //目前只有对家出了对
                    ArrayList fourthPairs = mainForm.currentPokers[users[2] - 1].GetPairs(firstSuit);
                    if (fourthPairs.Count > 0)
                    {
                        int fourthMax = (int)fourthPairs[fourthPairs.Count-1];
                        if (!CommonMethods.CompareTo(firstMax, fourthMax, suit, rank, firstSuit))
                        {
                            //第四家最大，我应该出最大的非分牌
                            SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList,true);
                            SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                        }
                        else
                        {
                            SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList,true);
                            SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                        }
                    }
                    else
                    {
                        SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList,true); //出分牌或者最小的牌
                        SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                    }


                    SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                   
                    return;
                }

            }
            else if (count == pairTotal * 2 && count > 0) //多个对，肯定首家最大
            {
                ArrayList list = mySuitCP.GetPairs();
                for (int i = 0; i < pairTotal && i < list.Count; i++)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                }
                SendThisSuitOrScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                return;
            }
            else //如果是甩牌
            {
                ArrayList list = mySuitCP.GetPairs();
                for (int i = 0; i < pairTotal && i < list.Count; i++)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                }

                //否则出最小的牌
                SendThisSuitOrScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

              
                return;
            }


           

        }
        //whoseOrder是第四个出牌
        internal static void WhoseOrderIs4(MainForm mainForm, CurrentPoker[] currentPokers, int whoseOrder, ArrayList sendedCards, int count, int suit, int rank, int firstSuit)
        {
            ArrayList firstSendCards = mainForm.currentSendCards[mainForm.firstSend - 1]; //首家出的牌
            CurrentPoker firstCP = new CurrentPoker();
            firstCP.Suit = suit;
            firstCP.Rank = rank;
            firstCP = CommonMethods.parse(firstSendCards, suit, rank); //首家出的牌

            int firstMax = CommonMethods.GetMaxCard(firstSendCards, suit, rank); //得到首家出的最大的牌
            int pairTotal = firstCP.GetPairs().Count;

            CurrentPoker myCP = currentPokers[whoseOrder - 1];         //我的牌
            ArrayList myPokerList = mainForm.pokerList[whoseOrder - 1]; //我的牌


            //whose的此花色的牌数
            int myTotal = CommonMethods.GetSuitCount(currentPokers[whoseOrder - 1], suit, rank, firstSuit); //此花色牌数
            //此花色的牌
            int[] cards = myCP.GetSuitCards(firstSuit); //此花色的牌
           
           

            ArrayList myList = new ArrayList(cards);
            CurrentPoker mySuitCP = new CurrentPoker();  //我的此花色的牌
            mySuitCP.Suit = suit;
            mySuitCP.Rank = rank;
            mySuitCP = CommonMethods.parse(myList, suit, rank);
            mySuitCP.Sort();

            firstCP.Sort();
            myCP.Sort();

            int[] users = CommonMethods.OtherUsers(mainForm.firstSend); //其他三位用户

            CurrentPoker secondCP = new CurrentPoker();
            secondCP.Suit = suit;
            secondCP.Rank = rank;
            secondCP = CommonMethods.parse(mainForm.currentSendCards[users[0] - 1], suit, rank); //首家后下一家用户

            CurrentPoker thirdCP = new CurrentPoker();
            thirdCP.Suit = suit;
            thirdCP.Rank = rank;
            thirdCP = CommonMethods.parse(mainForm.currentSendCards[users[1] - 1], suit, rank); //首家后第二家用户

            int[] tmpUsers = CommonMethods.OtherUsers(whoseOrder);
            
            //考虑毕
            if (myTotal == 0)
            {
                if (firstSuit != suit)
                {
                    //如果目前最大的那一家是主 
                    int biggerMax = (int)mainForm.currentSendCards[mainForm.whoIsBigger - 1][0];
                    

                    if (mainForm.whoIsBigger == tmpUsers[1])
                    {
                        //不毕，但是有可能贴的副牌也比大的那一家大
                        SendOtherSuitOrScores(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色非分牌
                        SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色分牌
                        SendOtherSuitOrScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其它花色非分牌
                        SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌


                        int sendOtherSuitsTotal = sendedCards.Count; //没有副牌可贴，只能出主

                        if (firstCP.HasTractors() && sendOtherSuitsTotal == 0) //单张牌
                        {
                            int minMaster = myCP.GetMasterTractor();
                            int tmpFirstTractor = firstCP.GetTractor();

                            //如果我的牌能大过最大的那家的牌

                            if ((!CommonMethods.CompareTo(tmpFirstTractor, minMaster, suit, rank, firstSuit)) && (minMaster> -1))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, minMaster);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, minMaster);
                                int[] ttt = myCP.GetTractorOtherCards(minMaster);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, ttt[1]);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, ttt[1]);
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count && count == 1 && sendOtherSuitsTotal== 0) //单张牌
                        {
                            int minMaster = myCP.GetMinMasterCards(suit);
                            //如果我的牌能大过最大的那家的牌
                            if (!CommonMethods.CompareTo(biggerMax, minMaster, suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, minMaster);
                                return;
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 1 && count == 2 && sendOtherSuitsTotal == 0) //出一个对时
                        {
                            ArrayList masterPairs = myCP.GetMasterPairs();
                            //如果我的牌能大过最大的那家的牌
                            if (masterPairs.Count > 0)
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                return;
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 0 && count > 1 && sendOtherSuitsTotal == 0) //单张甩牌
                        {
                            int minMaster = myCP.GetMinMasterCards(suit);
                            //如果我的牌能大过最大的那家的牌
                            if (!CommonMethods.CompareTo(biggerMax, minMaster, suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                                CommonMethods.SendCards(sendedCards, myCP, myPokerList, minMaster);

                                SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, true);
                                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true);
                                SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, false);
                                SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false);

                                return;
                            }

                        }

                        SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, true); //主非分牌
                        SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true); //主分牌
                        SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, false); //主非分牌
                        SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false); //主分牌

                       
                    }
                    else
                    {
                        if (firstCP.HasTractors()) //出一个对时
                        {
                            SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色非分牌
                            SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色分牌
                            SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其它花色非分牌
                            SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌
                        }
                        else if (myCP.GetMasterCardsTotal() >= count && count == 1) //单张牌
                        {

                            //int maxMaster = myCP.GetMaxMasterCards();
                            int[] masterCards = myCP.GetSuitCards(suit);
                            for (int i = 0; i < masterCards.Length; i++)
                            {
                                //如果我的牌能大过最大的那家的牌
                                if (!CommonMethods.CompareTo(biggerMax, masterCards[i], suit, rank, firstSuit))
                                {
                                    mainForm.whoIsBigger = whoseOrder;
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, masterCards[i]);
                                    return;
                                }
                            }

                            SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, true); //其它花色非分牌
                            SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其它花色非分牌
                            SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色分牌
                            SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌

                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 1 && count == 2) //出一个对时
                        {
                            ArrayList masterPairs = myCP.GetMasterPairs();
                            //如果我的牌能大过最大的那家的牌
                            

                            if (masterPairs.Count > 0)
                            {
                                for (int i = 0; i < masterPairs.Count; i++)
                                {
                 
                                    if (!CommonMethods.CompareTo(biggerMax, (int)masterPairs[i], suit, rank, firstSuit))
                                    {
                                        mainForm.whoIsBigger = whoseOrder;
                                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)masterPairs[0]);
                                        return;
                                    }
                                   
                                }
                            }
                        }
                        else if (myCP.GetMasterCardsTotal() >= count &&  pairTotal == 0 && count > 1) //单张甩牌
                        {
                            int maxMaster = myCP.GetMaxMasterCards();
                            //如果我的牌能大过最大的那家的牌
                            int[] masterCards = myCP.GetSuitCards(suit);
                            for (int i = 0; i < masterCards.Length; i++)
                            {
                                if (!CommonMethods.CompareTo(biggerMax, masterCards[i], suit, rank, firstSuit))
                                {
                                    mainForm.whoIsBigger = whoseOrder;
                                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, masterCards[i]);

                                    SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, true);
                                    SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true);
                                    SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, false);
                                    SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false);

                                    return;
                                }
                            }

                        }
                    }
                }
            }
            
            if (myTotal < count) //本花色根本就不够
            {

                for (int i = 0; i < myTotal; i++) //先将此花色
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList,cards[i]);
                }

                if (mainForm.whoIsBigger == tmpUsers[1])
                {
                    SendOtherSuitOrScores(sendedCards, count, firstSuit, myCP, myPokerList, true); //其它花色非分牌
                    SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色分牌
                    SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, true); //主非分牌
                    SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true); //主分牌

                    SendOtherSuitOrScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其它花色非分牌
                    SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌
                    SendMasterSuitOrScores(sendedCards, count, suit, myCP, myPokerList, false); //主非分牌
                    SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false); //主分牌
                }
                else
                {
                    SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, true); //其它花色非分牌
                    SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, true); //其他花色分牌
                    SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList, true); //主非分牌
                    SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, true); //主分牌

                    SendOtherSuitNoScores(sendedCards, count, firstSuit, myCP, myPokerList, false); //其它花色非分牌
                    SendOtherSuit(sendedCards, count, firstSuit, myCP, myPokerList, false); //其他花色分牌
                    SendMasterSuitNoScores(sendedCards, count, suit, myCP, myPokerList, false); //主非分牌
                    SendMasterSuit(sendedCards, count, suit, myCP, myPokerList, false); //主分牌
                }

             
                return;
            }
           //以下是此花色的牌比首家出的牌多
            else if (firstCP.HasTractors())  //如果首家出了拖拉机
            {
                //如果我有拖拉机，出最大的拖拉机，剩余的牌在下面出
                if (mySuitCP.HasTractors())
                {
                    int k = mySuitCP.GetTractor();
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, k);
                    int[] ks = mySuitCP.GetTractorOtherCards(k);
                    for (int i = 0; i < 3; i++)
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, ks[i]);
                    }


                    CurrentPoker tmpCP = CommonMethods.parse(mainForm.currentSendCards[mainForm.whoIsBigger - 1], suit, rank);
                    int tmp = tmpCP.GetTractor();
                    if (!CommonMethods.CompareTo(tmp, k, suit, rank, firstSuit))
                    {
                        mainForm.whoIsBigger = whoseOrder;
                    }
                    
                }
                else if (mySuitCP.GetPairs().Count > 0) //如果有对
                {
                    ArrayList list = mySuitCP.GetPairs();
                    if (list.Count >= 2) //如果我有多个对,那至少出两个对
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[1]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[1]);
                       
                    }
                    else //否则只能出一个对,其余出小牌
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                    }


                }


                //既然大不过首家，出最小的牌
                //而且，本花色绝对可以满足出牌
                if (mainForm.whoIsBigger == tmpUsers[1])
                {
                    SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, true); //此花色的非分的牌
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, true); //此花色的分牌

                    SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false); //此花色的非分的牌
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false); //此花色的分牌
                }
                else
                {
                    SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, true); //此花色的非分的牌
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, true); //此花色的分牌

                    SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false); //此花色的非分的牌
                    SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false); //此花色的分牌
                }


                return;
            }
            else if (count == 1) //单张牌,而且其哦确实有此花色的牌
            {
                int myMax = -1; //我的此花色最大牌
                if (firstSuit == suit)
                {
                    myMax = mySuitCP.GetMaxMasterCards();
                }
                else
                {
                    myMax = mySuitCP.GetMaxCards(firstSuit);
                }


                int max2 = CommonMethods.GetMaxCard(mainForm.currentSendCards[users[0] - 1], suit, rank); //第二家
                int max3 = CommonMethods.GetMaxCard(mainForm.currentSendCards[users[1] - 1], suit, rank); //第三家

               
                //对家(第二家)大
                if ((!CommonMethods.CompareTo(firstMax, max2, suit, rank,firstSuit)) && (CommonMethods.CompareTo(max2,max3,suit,rank,firstSuit)))
                {
                    SendThisSuitOrScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                    SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                    if (!CommonMethods.CompareTo(max2,(int)sendedCards[0],suit,rank,firstSuit))
                    {
                        mainForm.whoIsBigger = whoseOrder;
                    }
                   
                    return;
                } //我大
                else if ((!CommonMethods.CompareTo(firstMax, myMax, suit, rank, firstSuit)) && (!CommonMethods.CompareTo(max3, myMax, suit, rank, firstSuit)))
                {
                    if (myMax > -1) //这里应该永远为true
                    {
                        CommonMethods.SendCards(sendedCards, myCP, myPokerList, myMax);
                        mainForm.whoIsBigger = whoseOrder;

                        return;
                    }
                }

                SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true); //我们不大，出小非分牌
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true); //出分牌

                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false); //我们不大，出小非分牌
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false); //出分牌

                if ((!CommonMethods.CompareTo(firstMax, (int)sendedCards[0], suit, rank, firstSuit)) && (!CommonMethods.CompareTo(max2, (int)sendedCards[0], suit, rank, firstSuit)) && (!CommonMethods.CompareTo(max3, myMax, suit, rank, firstSuit)))
                {
                    mainForm.whoIsBigger = whoseOrder;
                }

                return;
            }
            else if ((pairTotal == 1) && (count == 2)) //如果是一个对
            {
                ArrayList list = mySuitCP.GetPairs(); //我的对
                //如果我对家大
                bool b2 = secondCP.GetPairs().Count > 0; //如果对家有对
                bool b3 = thirdCP.GetPairs().Count > 0; //如果第三家也出了对

                int max2 = -1;
                int max3 = -1;

                if (b2)
                {
                    max2 = (int)secondCP.GetPairs()[0];
                }
                if (b3)
                {
                    max3 = (int)thirdCP.GetPairs()[0];
                }

                //如果我有对
                if (list.Count > 0)
                {
                    int myMax = (int)list[list.Count - 1];
                   
                    if (b2 && b3) //2,3都有对
                    {
                        //对家大
                        if ((!CommonMethods.CompareTo(firstMax, max2, suit, rank, firstSuit)) && (CommonMethods.CompareTo(max2, max3, suit, rank, firstSuit)))
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);

                            if (!CommonMethods.CompareTo(max2, (int)list[0], suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                            }

                         
                            return;
                        }//如果我大
                        else if ((!CommonMethods.CompareTo(firstMax, myMax, suit, rank, firstSuit)) && (!CommonMethods.CompareTo(max3, myMax, suit, rank, firstSuit)))
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                            mainForm.whoIsBigger = whoseOrder;
                            
                            return;
                        }
                        else //对方大
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);

                            return;
                        }

                       

                    }
                    else if (b2 && (!b3)) //2有对,3无对
                    {
                        //对家大
                        if ((!CommonMethods.CompareTo(firstMax, max2, suit, rank, firstSuit)))
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            if (!CommonMethods.CompareTo(max2, (int)list[0], suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                            }


                        
                            return;
                        } //我大
                        else if ((!CommonMethods.CompareTo(firstMax, myMax, suit, rank, firstSuit)))
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);

                            mainForm.whoIsBigger = whoseOrder;
                            return;
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);

                            return;
                        }
                    }
                    else if ((!b2) && b3) //2无对，3有对
                    {
                        //如果我大
                        if ((!CommonMethods.CompareTo(firstMax, myMax, suit, rank, firstSuit)) && (!CommonMethods.CompareTo(max3, myMax, suit, rank, firstSuit)))
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                            mainForm.whoIsBigger = whoseOrder;

                            return;
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                           
                            return;
                        }
                    }
                    else if ((!b2) && (!b3)) //2,3皆无对
                    {
                        if ((!CommonMethods.CompareTo(firstMax, myMax, suit, rank, firstSuit)))
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[list.Count - 1]);
                            mainForm.whoIsBigger = whoseOrder;
                            

                            return;
                        }
                        else
                        {
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[0]);
                            if (!CommonMethods.CompareTo(firstMax, (int)list[0], suit, rank, firstSuit))
                            {
                                mainForm.whoIsBigger = whoseOrder;
                            }

                            return;
                        }
                    }
                }
                else //如果我无对
                {
                    if (b2 && b3) //2,3皆有对
                    {
                        //对家大
                        if ((!CommonMethods.CompareTo(firstMax, max2, suit, rank, firstSuit)) && (CommonMethods.CompareTo(max2, max3, suit, rank, firstSuit)))
                        {
                            SendThisSuitOrScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                            SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                            return;
                        }
                        else
                        {
                            SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true);
                            SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                            SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                            SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                            return;
                        }
                        
                    }
                    else if (b2 && (!b3))
                    {
                        //对家大
                        if ((!CommonMethods.CompareTo(firstMax, max2, suit, rank, firstSuit)))
                        {
                            SendThisSuitOrScores(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                            SendThisSuitOrScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                            return;
                        }
                        else
                        {
                            SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true);
                            SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                            SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                            SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                            return;
                        }
                    }
                    else
                    {
                        SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true);
                        SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                        SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                        SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);

                        return;
                    }
                    
                }

            }
            else if (count == pairTotal * 2 && (count > 0)) //都是对,肯定其对是最大的
            {
                ArrayList list = mySuitCP.GetPairs();
                for (int i = 0; i < pairTotal && i < list.Count; i++)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                }

                //否则出最小的牌
                SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true);
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
              
                return;
            }
            else //有对和有单张牌，是甩牌
            {
                ArrayList list = mySuitCP.GetPairs();
                for (int i = 0; i < pairTotal && i < list.Count; i++)
                {
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                    CommonMethods.SendCards(sendedCards, myCP, myPokerList, (int)list[i]);
                }

                //否则出最小的牌，顺序，此花色非分牌，此花色分牌
                SendThisSuitNoScores(sendedCards, count, suit,firstSuit, myCP, myPokerList,true);
                SendThisSuit(sendedCards, count,suit, firstSuit, myCP, myPokerList,true);
                SendThisSuitNoScores(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
                SendThisSuit(sendedCards, count, suit, firstSuit, myCP, myPokerList, false);
               
                return;
            }
           
        }

        
    }
}
