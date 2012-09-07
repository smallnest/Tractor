using System;
using System.Collections;
using System.Text;

namespace Kuaff.Tractor
{
    /// <summary>
    /// 首先出牌的算法
    /// </summary>
    class ShouldSendedCardsAlgorithm
    {
        internal static void ShouldSendCards(MainForm mainForm, CurrentPoker[] currentPokers, int whoseOrder, ArrayList sendedCards)
        {
            currentPokers[0].Sort();
            currentPokers[1].Sort();
            currentPokers[2].Sort();
            currentPokers[3].Sort();

            mainForm.whoIsBigger = whoseOrder;

            //1.挑出最大的副牌出
            CurrentPoker cp = currentPokers[whoseOrder - 1];

           

            #region 如果副牌有拖拉机
            int t = cp.GetNoRankNoSuitTractor(); //副牌拖拉机
            if (t > -1)
            {
                int[] othercards = cp.GetTractorOtherCards(t);

                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], t);
                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], t);
                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], othercards[1]);
                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], othercards[1]);


                return;
            }
            #endregion // 如果副牌有拖拉机

           


            #region 如果有副牌最大的对
            if (cp.GetNoRankNoSuitPairs().Count > 0 )
            {
                

                ArrayList al = cp.GetNoRankNoSuitPairs();
                int[] max = { (int)al[al.Count - 1], (int)al[al.Count - 1] };

                bool b3 = cp.Count < 25;
                bool b1 = cp.Rank != 12 && (((int)al[al.Count - 1] % 13) == 12) ;
                bool b2 = cp.Rank == 12 && (((int)al[al.Count - 1] % 13) == 11) ;

                if (b3 || b1 || b2) //不打A时
                {

                    if (whoseOrder == 2)
                    {
                        if ((!currentPokers[2].CompareTo(max)) && (!currentPokers[3].CompareTo(max)))
                        {
                          
                            CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], max[0]);
                            CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], max[0]);


                            if ((mainForm.currentRank != 12) && (mainForm.currentRank != 11)) //同时甩A
                            {
                                if ((max[0] % 13) == 11)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 12) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 12);
                                    }
                                }
                                else if ((max[0] % 13) == 12)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 11) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 11);
                                    }
                                }
                            }
                            else if ((mainForm.currentRank != 12) && (mainForm.currentRank == 11)) //同时甩A
                            {
                                if ((max[0] % 13) == 10)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 12) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 12);
                                    }
                                }
                                else if ((max[0] % 13) == 12)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 10) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 10);
                                    }
                                }
                            }
                            else if (mainForm.currentRank == 12) //同时甩A
                            {
                                if ((max[0] % 13) == 10)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 11) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 11);
                                    }
                                }
                                else if ((max[0] % 13) == 11)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 10) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 10);
                                    }
                                }
                            }

                           

                            return;
                        }

                    }
                    else if (whoseOrder == 3)
                    {
                        if ((!currentPokers[0].CompareTo(max)) && (!currentPokers[1].CompareTo(max)))
                        {
                           

                            CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], max[0]);
                            CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], max[0]);

                            if ((mainForm.currentRank != 12) && (mainForm.currentRank != 11)) //同时甩A
                            {
                                if ((max[0] % 13) == 11)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 12) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 12);
                                    }
                                }
                                else if ((max[0] % 13) == 12)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 11) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 11);
                                    }
                                }
                            }
                            else if ((mainForm.currentRank != 12) && (mainForm.currentRank == 11)) //同时甩A
                            {
                                if ((max[0] % 13) == 10)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 12) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 12);
                                    }
                                }
                                else if ((max[0] % 13) == 12)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 10) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 10);
                                    }
                                }
                            }
                            else if (mainForm.currentRank == 12) //同时甩A
                            {
                                if ((max[0] % 13) == 10)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 11) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 11);
                                    }
                                }
                                else if ((max[0] % 13) == 11)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 10) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 10);
                                    }
                                }
                            }

                           
                            return;
                        }
                    }
                    else if (whoseOrder == 4)
                    {
                        if ((!currentPokers[0].CompareTo(max)) && (!currentPokers[1].CompareTo(max)))
                        {
                           

                            CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], max[0]);
                            CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], max[0]);

                            if ((mainForm.currentRank != 12) && (mainForm.currentRank != 11)) //同时甩A
                            {
                                if ((max[0] % 13) == 11)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 12) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 12);
                                    }
                                }
                                else if ((max[0] % 13) == 12)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 11) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 11);
                                    }
                                }
                            }
                            else if ((mainForm.currentRank != 12) && (mainForm.currentRank == 11)) //同时甩A
                            {
                                if ((max[0] % 13) == 10)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 12) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 12);
                                    }
                                }
                                else if ((max[0] % 13) == 12)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 10) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 10);
                                    }
                                }
                            }
                            else if (mainForm.currentRank == 12) //同时甩A
                            {
                                if ((max[0] % 13) == 10)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 11) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 11);
                                    }
                                }
                                else if ((max[0] % 13) == 11)
                                {
                                    if (cp.GetCardCount((CommonMethods.GetSuit(max[0]) - 1) * 13 + 10) == 1)
                                    {
                                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (CommonMethods.GetSuit(max[0]) - 1) * 13 + 10);
                                    }
                                }
                            }

                           

                            return;
                        }
                    }
                }
            }
            #endregion // 如果有副牌最大的对

           

            #region 如果有单张最大的牌
            //判断单张牌
            int maxCards = -1;
            for (int i = 1; i < 5; i++)
            {
                if (i == cp.Suit)
                {
                    continue;
                }

                maxCards = cp.GetMaxCards(i);
                if (maxCards == -1)
                {
                    continue;
                }

                if (whoseOrder == 2)
                {
                    if ((!currentPokers[2].CompareTo(maxCards)) && (!currentPokers[3].CompareTo(maxCards)))
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], maxCards);

                       
                        return;
                    }

                }
                else if (whoseOrder == 3)
                {
                    if ((!currentPokers[0].CompareTo(maxCards)) && (!currentPokers[1].CompareTo(maxCards)))
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], maxCards);

                      
                        return;
                    }
                }
                else if (whoseOrder == 4)
                {
                    if ((!currentPokers[0].CompareTo(maxCards)) && (!currentPokers[1].CompareTo(maxCards)))
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], maxCards);

                        return;
                    }
                }
            }
            #endregion // 如果有单张最大的牌

          


            #region 虽然没有最大副牌,检查对家是否有最大的副牌
            for (int i = 1; i < 5; i++)
            {
                if (i == cp.Suit)
                {
                    continue;
                }

                if (whoseOrder == 2)
                {
                    maxCards = currentPokers[0].GetMaxCards(i);
                    if (maxCards == -1)
                    {
                        continue;
                    }

                    if ((!currentPokers[2].CompareTo(maxCards)) && (!currentPokers[3].CompareTo(maxCards)))
                    {
                        int max2 = currentPokers[2].GetMaxCard(i);
                        int max3 = currentPokers[2].GetMaxCard(i);

                        if (CommonMethods.CompareTo(max2, max3, cp.Suit, cp.Rank, i))
                        {
                            int rt = currentPokers[whoseOrder - 1].GetMinCardsOrScores(i);
                            if (rt > -1)
                            {
                                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);
                                return;
                            }
                        }
                    }

                }
                else if (whoseOrder == 3)
                {
                    maxCards = currentPokers[3].GetMaxCards(i);
                    if (maxCards == -1)
                    {
                        continue;
                    }
                    if ((!currentPokers[0].CompareTo(maxCards)) && (!currentPokers[1].CompareTo(maxCards)))
                    {
                        int max2 = currentPokers[2].GetMaxCard(i);
                        int max3 = currentPokers[2].GetMaxCard(i);

                        if (CommonMethods.CompareTo(max2, max3, cp.Suit, cp.Rank, i))
                        {
                            int rt = currentPokers[whoseOrder - 1].GetMinCardsOrScores(i);
                            if (rt > -1)
                            {
                                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);
                                return;
                            }
                        }
                    }
                }
                else if (whoseOrder == 4)
                {
                    maxCards = currentPokers[2].GetMaxCards(i);
                    if (maxCards == -1)
                    {
                        continue;
                    }

                    if ((!currentPokers[0].CompareTo(maxCards)) && (!currentPokers[1].CompareTo(maxCards)))
                    {
                         int max2 = currentPokers[2].GetMaxCard(i);
                        int max3 = currentPokers[2].GetMaxCard(i);

                        if (CommonMethods.CompareTo(max2, max3, cp.Suit, cp.Rank, i))
                        {
                            int rt = currentPokers[whoseOrder - 1].GetMinCardsOrScores(i);
                            if (rt > -1)
                            {
                                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);
                                return;
                            }
                        }
                    }
                }
            }
            #endregion // 虽然没有最大副牌,检查对家是否有最大的副牌

           

            //3.是否能清主

            #region 如果没有副牌可出，调主
            if (currentPokers[whoseOrder - 1].GetMasterCardsTotal() > 0)
            {
                t = currentPokers[whoseOrder - 1].GetMasterTractor();
                if (t > -1 && t != 53)
                {
                    int[] ttt = currentPokers[whoseOrder - 1].GetTractorOtherCards(t);
                    CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], t);
                    CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], t);
                    CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], ttt[1]);
                    CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], ttt[1]);

                  
                    return;
                }

                //对
                ArrayList al = currentPokers[whoseOrder - 1].GetMasterPairs();

                int[] users = CommonMethods.OtherUsers(whoseOrder);

                if (currentPokers[whoseOrder - 1].Count<= 12 && al.Count>0)
                {
                    int t1 = currentPokers[whoseOrder - 1].GetMasterCardsTotal();
                    int t2 = currentPokers[users[0] - 1].GetMasterCardsTotal();
                    int t3 = currentPokers[users[2] - 1].GetMasterCardsTotal();

                    if (t1 > t2 && t1 > t3 )
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (int)al[al.Count - 1]);
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], (int)al[al.Count - 1]);
                        return;
                    }
                   
                    
                }

                int rt = currentPokers[whoseOrder - 1].GetMinCardsNoScores(cp.Suit);
                if (rt > -1 && currentPokers[whoseOrder - 1].GetCardCount(rt) == 1)
                {
                    CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);
                    
                    return;
                }
                //
                if (cp.Suit != 1)
                {
                    if (cp.HeartsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], cp.Rank);
                      
                        return;
                    }
                }
                if (cp.Suit != 2)
                {
                    if (cp.PeachsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], cp.Rank + 13);
                   
                        return;
                    }
                }
                if (cp.Suit != 3)
                {
                    if (cp.DiamondsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], cp.Rank + 26);
                     
                        return;
                    }
                }
                if (cp.Suit != 4)
                {
                    if (cp.ClubsRankTotal == 1)
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], cp.Rank + 39);
                    
                        return;
                    }
                }

            }
            
            //调主的对

            #endregion // 如果没有副牌可出，调主

           
            //5.随便出小的副牌
            #region 不能继续调主的话，随便出一张不是分的副牌
            for (int i = 0; i < 5; i++)
            {
                if (i == cp.Suit)
                {
                    continue;
                }
                int rt = currentPokers[whoseOrder - 1].GetMinCardsNoScores(i);
                if (rt != 3 && rt != 8 && rt != 11)
                {

                    if (rt > -1)
                    {
                        CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);

                        return;
                    }
                }
            }
            #endregion // 不能继续调主的话，随便出一张不是分的副牌


            

            //6.全剩主牌和分副牌，出主
            #region 全剩主牌和分副牌
            ArrayList mPairs = currentPokers[whoseOrder - 1].GetMasterPairs();
            if (mPairs.Count > 0) //先出主对
            {
                int rt = (int)mPairs[mPairs.Count - 1];
                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);
                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);
               
                return;
            }
            int maserMin = currentPokers[whoseOrder - 1].GetMinMasterCards(cp.Suit);
            //if ((maserMin > -1) && (maserMin % 13 = 3) && (maserMin % 13 = 8) && (maserMin % 13 = 11))
            if (maserMin > -1)
            {
                CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], maserMin);
               
                return;
            }

            for (int i = 1; i < 5; i++)
            {
                int rt = currentPokers[whoseOrder - 1].GetMinCards(i);
                if (rt > -1)
                {
                    CommonMethods.SendCards(sendedCards, currentPokers[whoseOrder - 1], mainForm.pokerList[whoseOrder - 1], rt);
                   
                    return;
                }
            }
            #endregion // 全剩主牌和分副牌

           
        }



    }
}
