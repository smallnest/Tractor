using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Resources;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;



using Kuaff.CardResouces;
using Kuaff.ModelResources;
using Kuaff.OperaResources;

using Kuaff.TractorFere;


namespace Kuaff.Tractor
{
    internal partial class MainForm : Form
    {
        #region 变量声明
        //缓冲区图像
        internal Bitmap bmp = null;
        //原始背景图片
        internal Bitmap image = null;
      

        //*状态
        //当前的状态
        internal CurrentState currentState ;
        //当前的Rank,代表当前牌局的Rank,0代表实际的牌局2.....11代表K,12代表A,53代表打王
        internal int currentRank = 0;
        //是否是新开始的游戏
        internal bool isNew = true;

        //亮牌的次数
        internal int showSuits = 0;
        //谁亮的牌
        internal int whoShowRank = 0;


        //*发牌序列
        //得到一次发牌的序列,dpoker时发牌的帮助类，pokerList是每个人手中的牌的列表
        internal DistributePokerHelper dpoker = null;
        internal ArrayList[] pokerList = null;

        //每个人手中解析好的牌
        internal CurrentPoker[] currentPokers = { new CurrentPoker(), new CurrentPoker(), new CurrentPoker(), new CurrentPoker() };
        //画图的次数（仅在发牌时使用）
        internal int currentCount = 0;
        //当前一轮各家的出牌情况
        internal ArrayList[] currentSendCards = new ArrayList[4];
        //应该谁出牌
        internal int whoseOrder = 0;//0未定,1我，2对家，3西家,4东家
        //一次出来中谁最先开始出的牌
        internal int firstSend = 0;

        //*辅助变量
        //当前手中牌的坐标
        internal ArrayList myCardsLocation = new ArrayList();
        //当前手中牌的数值
        internal ArrayList myCardsNumber = new ArrayList();
        //当前手中牌的是否被点出
        internal ArrayList myCardIsReady = new ArrayList();
        //当前扣底的牌
        internal ArrayList send8Cards = new ArrayList();

        //*画我的牌的辅助变量
        //画牌顺序
        internal int cardsOrderNumber = 0;

        //确定程序休眠的最长时间
        internal long sleepTime;
        internal long sleepMaxTime = 2000;
        internal CardCommands wakeupCardCommands;

        //*绘画辅助类
        //DrawingForm变量
        internal DrawingFormHelper drawingFormHelper = null;
        internal　CalculateRegionHelper calculateRegionHelper = null;

        //记录本次得分
        internal int Scores = 0;

       
        //游戏设置
        internal GameConfig gameConfig = new GameConfig();

        //出牌时目前牌最大的那一家
        internal int whoIsBigger = 0;


        //音乐文件
        private string musicFile = "";
        //牌面图案
        internal Bitmap[] cardsImages = new Bitmap[54];

        //出牌算法
        internal object[] UserAlgorithms = { null, null, null, null };

        //当前一局已经出的牌
        internal CurrentPoker[] currentAllSendPokers = { new CurrentPoker(), new CurrentPoker(), new CurrentPoker(), new CurrentPoker() };

        #endregion // 变量声明

    
        internal MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.StandardDoubleClick, true);

           
            //读取程序配置
            InitAppSetting();
            
            notifyIcon.Text = Text;
            BackgroundImage = image;
        
            //变量初始化
            bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            
            
            drawingFormHelper = new DrawingFormHelper(this);
            calculateRegionHelper = new CalculateRegionHelper(this);


            for (int i = 0; i < 54; i++)
            {
                cardsImages[i] = null; //初始化
            }
        }

        private void InitAppSetting()
        {
            //没有配置文件，则从config文件中读取
            if (!File.Exists("gameConfig"))
            {
                AppSettingsReader reader = new AppSettingsReader();
                try
                {
                    Text = (String)reader.GetValue("title", typeof(String));
                }
                catch (Exception ex)
                {
                    Text = "拖拉机大战";
                }

                try
                {
                    gameConfig.MustRank = (String)reader.GetValue("mustRank", typeof(String));
                }
                catch (Exception ex)
                {
                    gameConfig.MustRank = ",3,8,11,12,13,";
                }

                try
                {
                    gameConfig.IsDebug = (bool)reader.GetValue("debug", typeof(bool));
                }
                catch (Exception ex)
                {
                    gameConfig.IsDebug = false;
                }

                try
                {
                    gameConfig.BottomAlgorithm = (int)reader.GetValue("bottomAlgorithm", typeof(int));
                }
                catch (Exception ex)
                {
                    gameConfig.BottomAlgorithm = 1;
                }
            }
            else
            {
                //实际从gameConfig文件中读取
                Stream stream = null;
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream("gameConfig", FileMode.Open, FileAccess.Read, FileShare.Read);
                    gameConfig = (GameConfig)formatter.Deserialize(stream);
                    
                }
                catch (Exception ex)
                {
                    
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
            }

            //未序列化的值
            AppSettingsReader myreader = new AppSettingsReader();
            gameConfig.CardsResourceManager = Kuaff_Cards.ResourceManager;
            try
            {
                String bkImage = (String)myreader.GetValue("backImage", typeof(String));
                image = new Bitmap(bkImage);
                KuaffToolStripMenuItem.CheckState = CheckState.Unchecked;

            }
            catch (Exception ex)
            {
                image = global::Kuaff.Tractor.Properties.Resources.Backgroud;
            }

            try
            {
                Text = (String)myreader.GetValue("title", typeof(String));
            }
            catch (Exception ex)
            {

            }

            gameConfig.CardImageName = "";

            if (gameConfig.IsDebug)
            {
                RobotToolStripMenuItem.CheckState = CheckState.Checked;
            }

        }



        #region 窗口事件处理程序

        internal void MenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (menuItem.Text.Equals("退出"))
            {
                this.Close();
            }

            if (menuItem.Text.Equals("开始新游戏"))
            {
                PauseGametoolStripMenuItem.Text = "暂停游戏";


                //新游戏初始状态，我家和敌方都从2开始，令牌为开始发牌
                currentState = new CurrentState(0, 0, 0, 0,0,0,CardCommands.ReadyCards);
                currentRank = 0;

                isNew = true;
                whoIsBigger = 0;

                //初始化
                init();

                //开始定时器，进行发牌
                timer.Start();
            }

        }


        //初始化
        internal void init()
        {
            //每次初始化都重绘背景
            Graphics g = Graphics.FromImage(bmp);
            drawingFormHelper.DrawBackground(g);


            //发一次牌
            dpoker = new DistributePokerHelper();
            pokerList = dpoker.Distribute();

            //每个人手中的牌清空,准备摸牌
            currentPokers[0].Clear();
            currentPokers[1].Clear(); 
            currentPokers[2].Clear();
            currentPokers[3].Clear();

            //清空已发送的牌
            currentAllSendPokers[0].Clear();
            currentAllSendPokers[1].Clear();
            currentAllSendPokers[2].Clear();
            currentAllSendPokers[3].Clear();


            //为每个人的currentPokers设置Rank
            currentPokers[0].Rank = currentRank;
            currentPokers[1].Rank = currentRank;
            currentPokers[2].Rank = currentRank;
            currentPokers[3].Rank = currentRank;
            currentPokers[0].Suit = 0;
            currentPokers[1].Suit = 0;
            currentPokers[2].Suit = 0;
            currentPokers[3].Suit = 0;


            currentSendCards[0] = new ArrayList();
            currentSendCards[1] = new ArrayList();
            currentSendCards[2] = new ArrayList();
            currentSendCards[3] = new ArrayList();

            //
            myCardsLocation= new ArrayList();
            myCardsNumber= new ArrayList();
            myCardIsReady= new ArrayList();
            send8Cards= new ArrayList();


            //设置命令
            currentState.CurrentCardCommands = CardCommands.ReadyCards;
            currentState.Suit = 0;
        

            //设置还未发牌,循环25次将牌发完
            currentCount = 0;

            //目前不可以反牌
            showSuits = 0;
            whoShowRank = 0;

            //得分清零
            Scores = 0;
            

            //绘制Sidebar
            drawingFormHelper.DrawSidebar(g);
            //绘制东南西北
            drawingFormHelper.DrawOtherMaster(g, 0, 0);
            
            if (currentState.Master != 0)
            {
                drawingFormHelper.DrawMaster(g, currentState.Master, 1);
                drawingFormHelper.DrawOtherMaster(g, currentState.Master, 1);
            }

            //绘制Rank
            drawingFormHelper.DrawRank(g,currentState.OurCurrentRank,true,false);
            drawingFormHelper.DrawRank(g, currentState.OpposedCurrentRank, false, false);

            //绘制花色
            drawingFormHelper.DrawSuit(g, 0, true, false);
            drawingFormHelper.DrawSuit(g, 0, false, false);

            send8Cards = new ArrayList();
            //调整花色
            if (currentRank == 53)
            {
                currentState.Suit = 5;
            }

            whoIsBigger = 0;

            //如果设置了游戏截止，则停止游戏
            if (gameConfig.WhenFinished > 0)
            {
                
                bool b = false;

                if ((currentState.OurTotalRound + 1) > gameConfig.WhenFinished) 
                {
                    b = true;
                }
                if ((currentState.OpposedTotalRound + 1) > gameConfig.WhenFinished)
                {
                    b = true;
                }
                if (b)
                {
                    timer.Stop();
                    PauseGametoolStripMenuItem.Text = "继续游戏";
                    PauseGametoolStripMenuItem.Image = Properties.Resources.MenuResume;
                }
            }
        }

       


        //窗口绘画处理,将缓冲区图像画到窗口上
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //将bmp画到窗口上
            g.DrawImage(bmp, 0, 0);
        }


       

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            //this.Text = "X=" + e.X + ",Y=" + e.Y + ";" + e.Clicks;

            //左键
            //只有发牌时和该我出牌时才能相应鼠标事件
            if (((currentState.CurrentCardCommands == CardCommands.WaitingForMySending) || (currentState.CurrentCardCommands == CardCommands.WaitingForSending8Cards)) && (whoseOrder == 1))
            {

                if (e.Button == MouseButtons.Left)
                {
                    if ((e.X >= (int)myCardsLocation[0] && e.X <= ((int)myCardsLocation[myCardsLocation.Count - 1] + 71)) && (e.Y >= 355 && e.Y < 472))
                    {
                        if (calculateRegionHelper.CalculateClickedRegion(e, 1))
                        {
                            drawingFormHelper.DrawMyPlayingCards(currentPokers[0]);
                            Refresh();
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)  //右键
                {
                    int i = calculateRegionHelper.CalculateRightClickedRegion(e);
                    if (i > -1 && i < myCardIsReady.Count)
                    {
                        bool b = (bool)myCardIsReady[i];
                        int x = (int)myCardsLocation[i];
                        for (int j = 1; j <= i; j++)
                        {
                            if ((int)myCardsLocation[i - j] == (x - 13))
                            {
                                myCardIsReady[i - j] = b;
                                x = x - 13;
                            }
                            else
                            {
                                break;
                            }
                        }

                        drawingFormHelper.DrawMyPlayingCards(currentPokers[0]);
                        Refresh();

                    }


                }


                //判断是否点击了小猪*********和以上的点击不同
                Rectangle pigRect = new Rectangle(296, 300, 53, 46);
                Region region = new Region(pigRect);
                if (region.IsVisible(e.X, e.Y))
                {
                    //判断是否处在扣牌阶段
                    if ((currentState.CurrentCardCommands == CardCommands.WaitingForSending8Cards)) //如果等我扣牌
                    {

                        //扣牌,所以擦去小猪
                        Graphics g = Graphics.FromImage(bmp);
                        g.DrawImage(image, pigRect, pigRect, GraphicsUnit.Pixel);
                        g.Dispose();

                        ArrayList readyCards = new ArrayList();
                        for (int i = 0; i < myCardIsReady.Count; i++)
                        {
                            if ((bool)myCardIsReady[i])
                            {
                                readyCards.Add((int)myCardsNumber[i]);
                            }
                        }
                        
                        if (readyCards.Count == 8)
                        {
                            send8Cards = new ArrayList();
                            for (int i = 0; i < 8; i++)
                            {
                                CommonMethods.SendCards(send8Cards, currentPokers[0], pokerList[0], (int)readyCards[i]);
                            }
                            initSendedCards();
                            currentState.CurrentCardCommands = CardCommands.DrawMySortedCards;
                        }

                        

                    }
                    else if (currentState.CurrentCardCommands == CardCommands.WaitingForMySending) //如果等我发牌
                    {
                        //如果我准备出的牌合法
                        if (TractorRules.IsInvalid(this, currentSendCards, 1))
                        {
                            //出牌，所以擦去小猪
                            Graphics g = Graphics.FromImage(bmp);
                            g.DrawImage(image, pigRect, pigRect, GraphicsUnit.Pixel);
                            g.Dispose();

                            //在这里检查甩牌的检查
                            if (firstSend == 1)
                            {
                                whoIsBigger = 1;
                                ArrayList minCards = new ArrayList();
                                if (TractorRules.CheckSendCards(this, minCards,0))
                                {

                                    currentSendCards[0] = new ArrayList();
                                    for (int i = 0; i < myCardIsReady.Count; i++)
                                    {
                                        if ((bool)myCardIsReady[i])
                                        {
                                            CommonMethods.SendCards(currentSendCards[0], currentPokers[0], pokerList[0], (int)myCardsNumber[i]);
                                        }
                                    }


                                }
                                else
                                {
                                    for (int i = 0; i < minCards.Count; i++)
                                    {
                                        CommonMethods.SendCards(currentSendCards[0], currentPokers[0], pokerList[0], (int)minCards[i]);
                                    }
                                }


                            }
                            else
                            {

                                currentSendCards[0] = new ArrayList();
                                for (int i = 0; i < myCardIsReady.Count; i++)
                                {
                                    if ((bool)myCardIsReady[i])
                                    {
                                        CommonMethods.SendCards(currentSendCards[0], currentPokers[0], pokerList[0], (int)myCardsNumber[i]);
                                    }
                                }

                            }

                            drawingFormHelper.DrawMyFinishSendedCards();
                        }
                    }

                }

            }
            else if (currentState.CurrentCardCommands == CardCommands.ReadyCards)
            {
                drawingFormHelper.IsClickedRanked(e);
            }
        }

       
        private void MainForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //    return;

            //如果当前没有牌可出 
            if (currentPokers[0].Count == 0)
            {
                return;
            }

            bool  b = calculateRegionHelper.CalculateDoubleClickedRegion(e);
            if (!b)
            {
                return;
            }

            currentSendCards[0]= new ArrayList();


            //出牌，所以擦去小猪
            Rectangle pigRect = new Rectangle(296, 300, 53, 46);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(image, pigRect, pigRect, GraphicsUnit.Pixel);
           
           

            //扣牌还是出牌
            if ((currentState.CurrentCardCommands == CardCommands.WaitingForSending8Cards) && (whoseOrder == 1)) //如果等我扣牌
            {
                ArrayList readyCards = new ArrayList();
                for (int i = 0; i < myCardIsReady.Count; i++)
                {
                    if ((bool)myCardIsReady[i])
                    {
                        readyCards.Add((int)myCardsNumber[i]);
                    }
                }

                if (readyCards.Count == 8)
                {
                    send8Cards = new ArrayList();
                    for (int i = 0; i < 8; i++)
                    {
                        CommonMethods.SendCards(send8Cards, currentPokers[0], pokerList[0], (int)readyCards[i]);
                    }
                    initSendedCards();
                    currentState.CurrentCardCommands = CardCommands.DrawMySortedCards;
                }


            }
            else if (currentState.CurrentCardCommands == CardCommands.WaitingForMySending) //如果等我发牌
            {
               

                if (TractorRules.IsInvalid(this, currentSendCards, 1))
                {
                    if (firstSend == 1)
                    {
                        whoIsBigger = 1;

                        ArrayList minCards = new ArrayList();
                        if (TractorRules.CheckSendCards(this, minCards,0))
                        {
                            currentSendCards[0] = new ArrayList(); 
                            for (int i = 0; i < myCardIsReady.Count; i++)
                            {
                                if ((bool)myCardIsReady[i])
                                {
                                    CommonMethods.SendCards(currentSendCards[0], currentPokers[0], pokerList[0], (int)myCardsNumber[i]);
                                }
                            }

                        }
                        else
                        {
                            for (int i = 0; i < minCards.Count; i++)
                            {
                                CommonMethods.SendCards(currentSendCards[0], currentPokers[0], pokerList[0], (int)minCards[i]);
                            }
                           
                        }


                    }
                    else
                    {
                        currentSendCards[0] = new ArrayList(); 
                        for (int i = 0; i < myCardIsReady.Count; i++)
                        {
                            if ((bool)myCardIsReady[i])
                            {
                                CommonMethods.SendCards(currentSendCards[0], currentPokers[0], pokerList[0], (int)myCardsNumber[i]);
                            }
                        }
                    }


                    drawingFormHelper.DrawMyFinishSendedCards();
                }
            }


        }

        //初始化每个人出的牌
        internal void initSendedCards()
        {
            //重新解析每个人手中的牌
            currentPokers[0] = CommonMethods.parse(pokerList[0], currentState.Suit, currentRank);
            currentPokers[1] = CommonMethods.parse(pokerList[1], currentState.Suit, currentRank);
            currentPokers[2] = CommonMethods.parse(pokerList[2], currentState.Suit, currentRank);
            currentPokers[3] = CommonMethods.parse(pokerList[3], currentState.Suit, currentRank);
        }


        #endregion // 窗口事件处理程序


        //定时器,用来显示发牌时的动画
        internal void timer_Tick(object sender, EventArgs e)
        {

            if (musicFile.Length > 0 && (!MciSoundPlayer.IsPlaying()) && PlayMusicToolStripMenuItem.Checked)
            {
                MciSoundPlayer.Stop();
                MciSoundPlayer.Close();
                MciSoundPlayer.Play(musicFile,"song");
            }
            else if (musicFile.Length > 0 && (!MciSoundPlayer.IsPlaying()) && RandomPlayToolStripMenuItem.Checked)
            {
                PlayRandomSongs();
            }
            //1.分牌
            if (currentState.CurrentCardCommands == CardCommands.ReadyCards) //分牌
            {
                if (currentCount ==0)
                {
                    //画工具栏
                    if (!gameConfig.IsDebug)
                    {
                        drawingFormHelper.DrawToolbar();
                    }

                }

                if (currentCount < 25)
                {
                    drawingFormHelper.ReadyCards(currentCount);
                    currentCount++;
                    
                }
                else
                {
                    currentState.CurrentCardCommands = CardCommands.DrawCenter8Cards;
                }
            }
            else if (currentState.CurrentCardCommands == CardCommands.WaitingShowBottom) //翻底牌完毕后的清理工作
            {
                drawingFormHelper.DrawCenterImage();
                //画8张牌的背面
                Graphics g = Graphics.FromImage(bmp);

                for (int i = 0; i < 8; i++)
                {
                    g.DrawImage(gameConfig.BackImage, 200 + i * 2, 186, 71, 96);
                }

                SetPauseSet(gameConfig.Get8CardsTime, CardCommands.DrawCenter8Cards);

            }
            else if (currentState.CurrentCardCommands == CardCommands.DrawCenter8Cards) //2.抓底牌
            {

                //如果无人亮主，流局
                if (drawingFormHelper.DoRankNot())
                {
                    if (gameConfig.IsPass) //如果设置为流局
                    {
                        //暂停3秒
                        init();
                        isNew = false;
                        //画图片
                        drawingFormHelper.DrawPassImage();
                        SetPauseSet(gameConfig.NoRankPauseTime, CardCommands.WaitingShowPass);
                        return;
                    }
                    else //如果设置为翻底牌
                    {
                        //将底牌的第三张的花色设置为主
                        ArrayList bottom = new ArrayList();
                        bottom.Add(pokerList[0][0]);
                        bottom.Add(pokerList[0][1]);
                        bottom.Add(pokerList[1][0]);
                        bottom.Add(pokerList[1][1]);
                        bottom.Add(pokerList[2][0]);
                        bottom.Add(pokerList[2][1]);
                        bottom.Add(pokerList[3][0]);
                        bottom.Add(pokerList[3][1]);
                        int suit = CommonMethods.GetSuit((int)bottom[2]);
                        currentState.Suit = suit;

                        Graphics g = Graphics.FromImage(bmp);

                        if (currentState.Master == 1 || currentState.Master == 2)
                        {
                            drawingFormHelper.DrawSuit(g, suit, true, true);
                        }
                        else if (currentState.Master == 3 || currentState.Master == 4)
                        {
                            drawingFormHelper.DrawSuit(g, suit, false, true);
                        }

                        g.Dispose();

                        //在中央画8张底牌,第三张稍微向上
                        drawingFormHelper.DrawCenterImage();
                        drawingFormHelper.DrawBottomCards(bottom);

                        //暂停一段时间,让大家能看到翻的底牌
                        SetPauseSet(gameConfig.NoRankPauseTime, CardCommands.WaitingShowBottom);

                        return;
                    }
                }


                whoseOrder = currentState.Master;//第一次由主家发牌
                firstSend = whoseOrder;

                SetPauseSet(gameConfig.Get8CardsTime, CardCommands.DrawMySortedCards);

                drawingFormHelper.DrawCenter8Cards();

                initSendedCards();
                drawingFormHelper.DrawMySortedCards(currentPokers[0], currentPokers[0].Count);
                currentState.CurrentCardCommands = CardCommands.WaitingForSending8Cards; //摸牌完毕，排序我的牌

                //初始化得分牌
                drawingFormHelper.DrawScoreImage(0);


            }
            else if (currentState.CurrentCardCommands == CardCommands.WaitingShowPass) //显示流局信息
            {
                //将流局图片清理掉
                drawingFormHelper.DrawCenterImage();
                //drawingFormHelper.DrawScoreImage(0);
                Refresh();
                currentState.CurrentCardCommands = CardCommands.ReadyCards;
            }
            else if (currentState.CurrentCardCommands == CardCommands.WaitingForSending8Cards) //3.扣底牌
            {


                //如果需要
                switch (currentState.Master)
                {
                    case 1:
                        if (gameConfig.IsDebug)
                        {
                            Algorithm.Send8Cards(this, 1);
                        }
                        else
                        {
                            drawingFormHelper.DrawMyPlayingCards(currentPokers[0]);
                            Refresh();
                            return;
                        }
                        break;
                    case 2:
                        Algorithm.Send8Cards(this, 2);
                        break;
                    case 3:
                        Algorithm.Send8Cards(this, 3);
                        break;
                    case 4:
                        Algorithm.Send8Cards(this, 4);
                        break;
                }



            }
            else if (currentState.CurrentCardCommands == CardCommands.DrawMySortedCards) //4.画我的牌
            {

                //将最后自己的牌进行排序显示
                SetPauseSet(gameConfig.SortCardsTime, CardCommands.DrawMySortedCards);

                drawingFormHelper.DrawMySortedCards(currentPokers[0], currentPokers[0].Count);
                Refresh();

                currentState.CurrentCardCommands = CardCommands.WaitingForSend;

            }
            else if (currentState.CurrentCardCommands == CardCommands.WaitingForSend) //等待出牌
            {
                //如果是对家
                if (whoseOrder == 2)
                {
                    drawingFormHelper.DrawFrieldUserSendedCards();
                }
                if (whoseOrder == 3)
                {
                    drawingFormHelper.DrawPreviousUserSendedCards();
                }
                if (whoseOrder == 4)
                {
                    drawingFormHelper.DrawNextUserSendedCards();
                }
                if (whoseOrder == 1)
                {
                    if (gameConfig.IsDebug)
                    {
                        if (firstSend == 1)
                        {
                            Algorithm.ShouldSendedCards(this, 1, currentPokers, currentSendCards, currentState.Suit, currentRank);
                        }
                        else
                        {
                            Algorithm.MustSendedCards(this, 1, currentPokers, currentSendCards, currentState.Suit, currentRank, currentSendCards[firstSend - 1].Count);
                        }
                        drawingFormHelper.DrawMyFinishSendedCards();
                        if (currentSendCards[3].Count > 0) //是否完成
                        {
                            currentState.CurrentCardCommands = CardCommands.Pause;
                            SetPauseSet(gameConfig.FinishedOncePauseTime, CardCommands.DrawOnceFinished);
                        }
                        else
                        {
                            whoseOrder = 4;
                            currentState.CurrentCardCommands = CardCommands.WaitingForSend;
                        }
                    }
                    else
                    {
                        currentState.CurrentCardCommands = CardCommands.WaitingForMySending;//等待鼠标事件
                    }
                }
            }
            else if (currentState.CurrentCardCommands == CardCommands.Pause) //如果需要暂停
            {
                //如果是Pause,则只是让程序休息一会()
                long interval = (DateTime.Now.Ticks - sleepTime) / 10000;

                if (interval > sleepMaxTime)
                {
                    currentState.CurrentCardCommands = wakeupCardCommands;
                }


            }
            else if (currentState.CurrentCardCommands == CardCommands.DrawOnceFinished) //如果是大家都出完牌
            {
                drawingFormHelper.DrawFinishedOnceSendedCards(); //完成清理工作
                if (currentPokers[0].Count > 0)
                {
                    currentState.CurrentCardCommands = CardCommands.WaitingForSend;
                }
            }
            else if (currentState.CurrentCardCommands == CardCommands.DrawOnceRank) //如果本轮大家都出完牌
            {
                currentState.CurrentCardCommands = CardCommands.Undefined;
                init();
            }
        }

        //设置暂停的最大时间，以及暂停结束后的执行命令
        internal void SetPauseSet(int max, CardCommands wakeup)
        {
            sleepMaxTime = max;
            sleepTime = DateTime.Now.Ticks;
            wakeupCardCommands = wakeup;
            currentState.CurrentCardCommands = CardCommands.Pause;
        }


        #region 菜单事件处理
        //牌面图案
        private void SelectCardImage_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            if (menuItem.Text.Equals("普通图案"))
            {
                gameConfig.CardsResourceManager = Kuaff_Cards.ResourceManager;
                CommonToolStripMenuItem.CheckState = CheckState.Checked;
                ModelToolStripMenuItem.CheckState = CheckState.Unchecked;
                OperaToolStripMenuItem.CheckState = CheckState.Unchecked;
                CustomCardImageToolStripMenuItem.CheckState = CheckState.Unchecked;

                CustomCardImageToolStripMenuItem.Text = "自定义";
                gameConfig.CardImageName = "";

            }
            else if (menuItem.Text.Equals("香车美女"))
            {
                gameConfig.CardsResourceManager = Kuaff_Model.ResourceManager;
                CommonToolStripMenuItem.CheckState = CheckState.Unchecked;
                ModelToolStripMenuItem.CheckState = CheckState.Checked;
                OperaToolStripMenuItem.CheckState = CheckState.Unchecked;
                CustomCardImageToolStripMenuItem.CheckState = CheckState.Unchecked;

                CustomCardImageToolStripMenuItem.Text = "自定义";
                gameConfig.CardImageName = "";
            }
            else if (menuItem.Text.Equals("京剧脸谱"))
            {
                gameConfig.CardsResourceManager = Kuaff_Opera.ResourceManager;
                CommonToolStripMenuItem.CheckState = CheckState.Unchecked;
                ModelToolStripMenuItem.CheckState = CheckState.Unchecked;
                OperaToolStripMenuItem.CheckState = CheckState.Checked;
                CustomCardImageToolStripMenuItem.CheckState = CheckState.Unchecked;

                CustomCardImageToolStripMenuItem.Text = "自定义";
                gameConfig.CardImageName = "";
            }
            else if (menuItem.Text.StartsWith("自定义"))
            {
                SelectCardsImage sci = new SelectCardsImage(this);
                if (sci.ShowDialog(this) == DialogResult.OK)
                {
                    gameConfig.CardImageName = sci.CardsName;
                    menuItem.Text = "自定义--" + gameConfig.CardImageName;

                    CommonToolStripMenuItem.CheckState = CheckState.Unchecked;
                    ModelToolStripMenuItem.CheckState = CheckState.Unchecked;
                    OperaToolStripMenuItem.CheckState = CheckState.Unchecked;
                    CustomCardImageToolStripMenuItem.CheckState = CheckState.Checked;
                }
            }
        }
        //牌背图片
        private void SelectBackImage_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            if (menuItem.Text.Equals("蔚蓝世界"))
            {
                gameConfig.BackImage = Kuaff_Cards.back;
                BlueWorldToolStripMenuItem.CheckState = CheckState.Checked;
                GreenAgeToolStripMenuItem.CheckState = CheckState.Unchecked;
                AntelopeToolStripMenuItem.CheckState = CheckState.Unchecked;

                CustomBackImageToolStripMenuItem.CheckState = CheckState.Unchecked;
                CustomBackImageToolStripMenuItem.Text = "自定义";
            }
            else if (menuItem.Text.Equals("青涩年华"))
            {
                gameConfig.BackImage = Kuaff_Cards.back2;
                BlueWorldToolStripMenuItem.CheckState = CheckState.Unchecked;
                GreenAgeToolStripMenuItem.CheckState = CheckState.Checked;
                AntelopeToolStripMenuItem.CheckState = CheckState.Unchecked;

                CustomBackImageToolStripMenuItem.CheckState = CheckState.Unchecked;
                CustomBackImageToolStripMenuItem.Text = "自定义";
            }
            else if (menuItem.Text.Equals("草原羚羊"))
            {
                gameConfig.BackImage = Kuaff_Cards.back3;
                BlueWorldToolStripMenuItem.CheckState = CheckState.Unchecked;
                GreenAgeToolStripMenuItem.CheckState = CheckState.Unchecked;
                AntelopeToolStripMenuItem.CheckState = CheckState.Checked;

                CustomBackImageToolStripMenuItem.CheckState = CheckState.Unchecked;
                CustomBackImageToolStripMenuItem.Text = "自定义";
            }
            else if (menuItem.Text.StartsWith("自定义"))
            {
                SelectCardbackImage sci = new SelectCardbackImage(this);
                if (sci.ShowDialog(this) == DialogResult.OK)
                {
                    menuItem.Text = "自定义--" + sci.CardBackImageName;

                    BlueWorldToolStripMenuItem.CheckState = CheckState.Unchecked;
                    GreenAgeToolStripMenuItem.CheckState = CheckState.Unchecked;
                    AntelopeToolStripMenuItem.CheckState = CheckState.Unchecked;
                    CustomBackImageToolStripMenuItem.CheckState = CheckState.Checked;
                }

            }
        }

        //选择背景图片
        private void SelectImage_Click(object sender, EventArgs e)
        {
            PauseGametoolStripMenuItem.Text = "暂停游戏";

            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (menuItem.Text.Equals("夸父科技"))
            {
                KuaffToolStripMenuItem.CheckState = CheckState.Checked;
                image = global::Kuaff.Tractor.Properties.Resources.Backgroud;
                BackgroundImage = image;

                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(image, ClientRectangle, ClientRectangle,GraphicsUnit.Pixel);

                init();
                //绘制东南西北

                drawingFormHelper.DrawOtherMaster(g, 0, 0);
              
                if (isNew && (currentRank == 0))
                {
                }
                else
                {
                    if (currentState.Master != 0)
                    {
                        drawingFormHelper.DrawMaster(g, currentState.Master, 1);
                        drawingFormHelper.DrawOtherMaster(g, currentState.Master, 1);
                    }
                }
                g.Dispose();
                Refresh();
            }
            else if (menuItem.Text.Equals("自定义图片"))
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    KuaffToolStripMenuItem.CheckState = CheckState.Unchecked;
                    image = new Bitmap(openFileDialog.OpenFile());
                    image = new Bitmap(image,new Size(ClientRectangle.Width,ClientRectangle.Height));
                    //BackgroundImage = image;

                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawImage(image, ClientRectangle, ClientRectangle, GraphicsUnit.Pixel);
                   
                    init();
                    //绘制东南西北

                    drawingFormHelper.DrawOtherMaster(g, 0, 0);
                   
                    if (isNew && (currentRank == 0))
                    {
                    }
                    else
                    {
                        if (currentState.Master != 0)
                        {
                            drawingFormHelper.DrawMaster(g, currentState.Master, 1);
                            drawingFormHelper.DrawOtherMaster(g, currentState.Master, 1);
                        }
                    }
                    g.Dispose();
                    Refresh();
                }
                
            }

        }

        //托盘事件处理
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                notifyIcon.Visible = true;
            }
            else
            {
                notifyIcon.Visible = false;
            }
        }

        //设置游戏速度
        private void GameSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSpeedDialog dialog = new SetSpeedDialog(this);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                //调整速度
                gameConfig.FinishedOncePauseTime = (int)(150 * Math.Pow(10, dialog.trackBar1.Value / 25.0));
                gameConfig.NoRankPauseTime = (int)(500 * Math.Pow(10, dialog.trackBar2.Value / 25.0));
                gameConfig.Get8CardsTime = (int)(100 * Math.Pow(10, dialog.trackBar3.Value / 25.0));
                gameConfig.SortCardsTime = (int)(100 * Math.Pow(10, dialog.trackBar4.Value / 25.0));
                gameConfig.FinishedThisTime = (int)(250 * Math.Pow(10, dialog.trackBar5.Value / 25.0));
                gameConfig.TimerDiDa = (int)(10 * Math.Pow(10, dialog.trackBar6.Value / 25.0));
                timer.Interval = gameConfig.TimerDiDa;
            }
        }

        //保存牌局
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream("backup", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, currentState);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        //读取牌局
        private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PauseGametoolStripMenuItem.Text = "暂停游戏";

            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream("backup", FileMode.Open, FileAccess.Read, FileShare.Read);
                CurrentState cs = (CurrentState)formatter.Deserialize(stream);
                
                currentState = cs;

               
                if (currentState.Master == 1 || currentState.Master == 2)
                {
                    currentRank = currentState.OurCurrentRank;
                }
                else if(currentState.Master == 3 || currentState.Master == 4)
                {
                    currentRank = currentState.OpposedCurrentRank;
                }
                else
                {
                    isNew = true;
                    currentRank = 0;
                }

                init();

                timer.Start();
                

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        //显示帮助
        private void GameHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this,"Tractor.CHM");
        }

        //Aboutme
        private void AboutMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show(this);
        }

        private void PauseGametoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (menuItem.Text.Equals("暂停游戏"))
            {
                timer.Stop();
                menuItem.Text = "继续游戏";
                menuItem.Image = Properties.Resources.MenuResume;
            }
            else
            {
                timer.Start();
                menuItem.Text = "暂停游戏";
                menuItem.Image = Properties.Resources.MenuPause;
            }
        }

        private void RobotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            if (menuItem.CheckState == CheckState.Checked)
            {
                gameConfig.IsDebug = true;
            }
            else
            {
                gameConfig.IsDebug = false;
            }
        }

        private void SetRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRules sr = new SetRules(this);
            sr.ShowDialog(this);
        }

        private void NoBackMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            menuItem.CheckState = CheckState.Checked;
            
        }

        private void PlayMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //弹出内置音乐选择对话框
            SelectMusic sem = new SelectMusic();
            if (sem.ShowDialog(this) == DialogResult.OK)
            {
                NoBackMusicToolStripMenuItem.CheckState = CheckState.Unchecked;

                //如果选择了一首曲子，则播放
                try
                {
                    string music = (string)sem.music.SelectedItem;
                    String newMusicFile = Path.Combine(Application.StartupPath, "music\\" + music);
                    if (musicFile != newMusicFile && musicFile.Length > 0)
                    {
                        MciSoundPlayer.Stop();
                        MciSoundPlayer.Close();
                    }
                    musicFile = newMusicFile;
                    MciSoundPlayer.Play(musicFile,"song");

                    NoBackMusicToolStripMenuItem.CheckState = CheckState.Unchecked;
                    PlayMusicToolStripMenuItem.CheckState = CheckState.Checked;
                    RandomPlayToolStripMenuItem.CheckState = CheckState.Unchecked;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                //NoBackMusicToolStripMenuItem.CheckState = CheckState.Checked;

            }
        }

        private void NoBackMusicToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            musicFile = "";
            MciSoundPlayer.Stop();
            MciSoundPlayer.Close();
            NoBackMusicToolStripMenuItem.CheckState = CheckState.Checked;
            RandomPlayToolStripMenuItem.CheckState = CheckState.Unchecked;
            PlayMusicToolStripMenuItem.CheckState = CheckState.Unchecked;
        }

        private void RandomPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PlayRandomSongs();

            NoBackMusicToolStripMenuItem.CheckState = CheckState.Unchecked;
            RandomPlayToolStripMenuItem.CheckState = CheckState.Checked;
            PlayMusicToolStripMenuItem.CheckState = CheckState.Unchecked;
        }

        //随机播放音乐
        private void PlayRandomSongs()
        {
            try
            {
                SelectMusic sem = new SelectMusic();
                int count = sem.music.Items.Count;
                Random random = new Random();
                string music = (string)sem.music.Items[random.Next(count)];
                sem.Dispose();
                String newMusicFile = Path.Combine(Application.StartupPath, "music\\" + music);
                if (musicFile != newMusicFile && musicFile.Length > 0)
                {
                    MciSoundPlayer.Stop();
                    MciSoundPlayer.Close();
                }
                musicFile = newMusicFile;
                MciSoundPlayer.Play(musicFile, "song");

            }
            catch (Exception ex)
            {

            }
        }

        private void FereToolStripMenuItem_Click(object sender, EventArgs e) //拖拉机伴侣
        {
            Fere fere = new Fere();
            fere.Show(this);
        }

        private void SeeTotalScoresToolStripMenuItem_Click(object sender, EventArgs e) //得分统计
        {
            TotalScores ts = new TotalScores(this);
            ts.Show(this);
        }

        private void SelectAlgorithmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectUserAlgorithm sua = new SelectUserAlgorithm(this);
            sua.ShowDialog(this);
        }

        #endregion // 菜单事件处理

        private void SetGameFinishedtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGameFinished sgf = new SetGameFinished(this);
            sgf.ShowDialog(this);
        }

        

               
    }
}