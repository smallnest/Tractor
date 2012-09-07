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
    class CalculateRegionHelper
    {
        MainForm mainForm;
        internal CalculateRegionHelper(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        //计算是否点击到牌面
        internal bool CalculateClickedRegion(MouseEventArgs e,int clicks)
        {
            //采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count];
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++)
            {
                if ((bool)mainForm.myCardIsReady[i])
                {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 355, 71, 96));
                }
                else
                {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 375, 71, 96));
                }
            }

            //计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个
            int k = mainForm.myCardsLocation.Count;
            int m = 0;
            if (mainForm.myCardsLocation.Count > 5)
            {
                for (int i = 0; i < mainForm.myCardsLocation.Count - 5; i++)
                {
                    regions[i].Exclude(regions[i + 1]);
                    regions[i].Exclude(regions[i + 2]);
                    regions[i].Exclude(regions[i + 3]);
                    regions[i].Exclude(regions[i + 4]);
                    regions[i].Exclude(regions[i + 5]);
                }

                m = mainForm.myCardsLocation.Count - 5;
                k = 5;
            }

            for (int i = 0; i < k - 1; i++)
            {
                for (int j = 1; j < (k - i); j++)
                {
                    regions[i + m].Exclude(regions[m + i + j]);
                }
            }


            //计算鼠标点是否落入区域中
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++)
            {
                if (regions[i].IsVisible(e.X, e.Y))
                {
                    if (clicks == 2)
                    {
                        mainForm.myCardIsReady[i] = true;
                    }
                    else if (clicks == 1)
                    {
                        mainForm.myCardIsReady[i] = !(bool)mainForm.myCardIsReady[i];
                    }
                    return true;
                }
            }

            return false;
        }

        //计算是否点击到牌面
        internal bool CalculateDoubleClickedRegion(MouseEventArgs e)
        {
            //采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count];
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++)
            {
                if ((bool)mainForm.myCardIsReady[i])
                {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 355, 71, 96));
                }
                else
                {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 375, 71, 96));
                }
            }

            //计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个
            int k = mainForm.myCardsLocation.Count;
            int m = 0;
            if (mainForm.myCardsLocation.Count > 5)
            {
                for (int i = 0; i < mainForm.myCardsLocation.Count - 5; i++)
                {
                    regions[i].Exclude(regions[i + 1]);
                    regions[i].Exclude(regions[i + 2]);
                    regions[i].Exclude(regions[i + 3]);
                    regions[i].Exclude(regions[i + 4]);
                    regions[i].Exclude(regions[i + 5]);
                }

                m = mainForm.myCardsLocation.Count - 5;
                k = 5;
            }

            for (int i = 0; i < k - 1; i++)
            {
                for (int j = 1; j < (k - i); j++)
                {
                    regions[i + m].Exclude(regions[m + i + j]);
                }
            }


            //计算鼠标点是否落入区域中
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++)
            {
                if (regions[i].IsVisible(e.X, e.Y))
                {
                    mainForm.myCardIsReady[i] = true;
                    
                    return true;
                }
            }

            return false;
        }


        internal int CalculateRightClickedRegion(MouseEventArgs e)
        {
            //采用区域计算
            Region[] regions = new Region[mainForm.myCardsLocation.Count];
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++)
            {
                if ((bool)mainForm.myCardIsReady[i])
                {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 355, 71, 96));
                }
                else
                {
                    regions[i] = new Region(new Rectangle((int)mainForm.myCardsLocation[i], 375, 71, 96));
                }
            }

            //计算交集,最后一个肯定不会被覆盖,最多第5个会覆盖第0个
            int k = mainForm.myCardsLocation.Count;
            int m = 0;
            if (mainForm.myCardsLocation.Count > 5)
            {
                for (int i = 0; i < mainForm.myCardsLocation.Count - 5; i++)
                {
                    regions[i].Exclude(regions[i + 1]);
                    regions[i].Exclude(regions[i + 2]);
                    regions[i].Exclude(regions[i + 3]);
                    regions[i].Exclude(regions[i + 4]);
                    regions[i].Exclude(regions[i + 5]);
                }

                m = mainForm.myCardsLocation.Count - 5;
                k = 5;
            }

            for (int i = 0; i < k - 1; i++)
            {
                for (int j = 1; j < (k - i); j++)
                {
                    regions[i + m].Exclude(regions[m + i + j]);
                }
            }


            //计算鼠标点是否落入区域中
            for (int i = 0; i < mainForm.myCardsLocation.Count; i++)
            {
                if (regions[i].IsVisible(e.X, e.Y))
                {
                    mainForm.myCardIsReady[i] = !(bool)mainForm.myCardIsReady[i];
                    return i;
                }
            }

            return -1;
        }
    }
}
